using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OSGeo.MapServer;
using System.Diagnostics;
using MapLibrary.Properties;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Event handler to sign that the cursor is moving.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="x">The x position in map coordinates.</param>
    /// <param name="y">The y position in map coordinates.</param>
    public delegate void CursorMoveEventHandler(object sender, double x, double y);

    /// <summary>
    /// User Control to render maps.
    /// </summary>
    public partial class MapControl : Control, IMapControl
    {
        // properties
        private MapObjectHolder target;
        public mapObj map;
        private Image mapImage = null;
        private InputModes inputMode;
        private Rectangle dragRect;
        private bool dragging;
        private rectObj initial_extent;

        private SolidBrush selectionBrush;
        private Pen selectionPen;

        private double a11, a13, a21, a23;

        private int unitPrecision;
        private string unitName;
        private MS_UNITS mapunits;

        private int gap;
        private bool border;
        private Pen borderPen;

        private string drawMessage;

        private bool forcePan = false;
        private float magnify = 1;
        private int mouseX, mouseY;

        private GraphicsPath trackPoints;

        private bool queryMode = false;
        private bool drawQuery = false;

        private bool enableRendering = true;

        // public events
        /// <summary>
        /// The CursorMove event object.
        /// </summary>
        public event CursorMoveEventHandler CursorMove;
        /// <summary>
        /// The BeforeRefresh event object.
        /// </summary>
        public event EventHandler BeforeRefresh;
        /// <summary>
        /// The AfterRefresh event object.
        /// </summary>
        public event EventHandler AfterRefresh;
        
        /// <summary>
        /// Constructs a new MapControl object.
        /// </summary>
        public MapControl()
        {
            InitializeComponent();
            InputMode = InputModes.Pan;
            dragRect = new Rectangle(0,0,0,0);
            dragging = false;
            selectionBrush = new SolidBrush(Color.FromArgb(75, Color.Gray));
            selectionPen = new Pen(Color.Black,1);
            borderPen = new Pen(Color.Black, 1);
            border = false;

            drawMessage = "The map hasn't been initialised yet.";

            gap = 10;

            unitPrecision = 4;
            unitName = "";
            mapunits = MS_UNITS.MS_METERS;

            a11 = a13 = a21 = a23 = 0;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// Enum for The user editing modes (panning or zooming).
        /// </summary>
        public enum InputModes
        {
            /// <summary>
            /// Panning
            /// </summary>
            Pan,
            /// <summary>
            /// Zooming in
            /// </summary>
            ZoomIn,
            /// <summary>
            /// Zooming out
            /// </summary>
            ZoomOut,
            /// <summary>
            /// Rectangle tracking
            /// </summary>
            TrackRectangle,
            /// <summary>
            /// Polygon tracking
            /// </summary>
            TrackPolygon,
            /// <summary>
            /// Line tracking
            /// </summary>
            TrackLine,
            /// <summary>
            /// Select Item
            /// </summary>
            Select
        }

        public bool EnableRendering
        {
            get
            {
                return enableRendering;
            }
            set
            {
                enableRendering = value;
                RefreshView();
            }
        }

        /// <summary>
        /// The current user editing mode (panning or zooming).
        /// </summary>
        public InputModes InputMode
        {
            get { return inputMode; }
            set 
            { 
                inputMode = value;
                trackPoints = null;
                if (!DesignMode)
                {
                    switch (inputMode)
                    {
                        case InputModes.Pan:
                            this.Cursor = new Cursor(new MemoryStream(Resources.GrabberTool));
                            break;
                        case InputModes.ZoomIn:
                            this.Cursor = new Cursor(new MemoryStream(Resources.ZoomInTool));
                            break;
                        case InputModes.ZoomOut:
                            this.Cursor = new Cursor(new MemoryStream(Resources.ZoomOutTool));
                            break;
                        case InputModes.Select:
                            this.Cursor = new Cursor(new MemoryStream(Resources.SelectTool));
                            break;
                        case InputModes.TrackRectangle:
                            this.Cursor = new Cursor(new MemoryStream(Resources.SelectRectTool));
                            break;
                        case InputModes.TrackPolygon:
                            this.Cursor = new Cursor(new MemoryStream(Resources.SelectPolygonTool));
                            break;
                        default:
                            this.Cursor = Cursors.Default;
                            break;
                    }
                }
            } 
        }

        /// <summary>
        /// The sample feature border gap in pixels.
        /// </summary>
        public int Gap
        {
            get { return gap; }
            set
            {
                gap = value;
            }
        }

        /// <summary>
        /// Gets the current map image object.
        /// </summary>
        public Image MapImage
        {
            get { return mapImage; }
        }

        /// <summary>
        /// Flag to enable the drawing of a single line border.
        /// </summary>
        public bool Border
        {
            get { return border; }
            set
            {
                border = value;
                this.RefreshView();
            }
        }

        /// <summary>
        /// Update the pixel-map transformation coefficients.
        /// </summary>
        private void UpdateTansformations()
        {
            // update the map to pixel transformation
            if (this.Width > 2)
                a11 = (map.extent.maxx - map.extent.minx) / this.Width;
            else a11 = 0;
            a13 = map.extent.minx;
            if (this.Height > 2)
                a21 = -a11;
            else a21 = 0;
            a23 = map.extent.maxy;
            
            // update the pixel to map transformation  
        }

        /// <summary>
        /// Converts the X coordinate from pixel space to map coordinate space.
        /// </summary>
        /// <param name="x">Pixel coordinate.</param>
        /// <returns>Map coordinate.</returns>
        private double Pixel2MapX(double x)
        {
            return a11 * x + a13;
        }

        /// <summary>
        /// Converts the Y coordinate from pixel space to map coordinate space.
        /// </summary>
        /// <param name="y">Pixel coordinate.</param>
        /// <returns>Map coordinate.</returns>
        private double Pixel2MapY(double y)
        {
            return a21 * y + a23;
        }

        /// <summary>
        /// Converts the MapScript shape object to the GDI+ GraphicsPath.
        /// </summary>
        /// <param name="feature">The shapeObj to conver.</param>
        /// <returns>The converted GraphicsPath object</returns>
        public static GraphicsPath ShapeToGraphicsPath(shapeObj feature)
        {
            GraphicsPath path = new GraphicsPath();
            path.Reset();
            if (feature != null)
            {
                lineObj line;
                pointObj point;
                for (int i = 0; i < feature.numlines; i++)
                {
                    path.StartFigure();
                    line = feature.get(i);
                    for (int j = 0; j < line.numpoints; j++)
                    {
                        point = line.get(j);
                        PointF[] pts =  { new PointF((float)point.x, (float)point.y) };
                        path.AddLines(pts);
                    }
                    if (feature.type == (int)MS_SHAPE_TYPE.MS_SHAPE_POLYGON) path.CloseFigure();
                }
            }
            return path;
        }

        /// <summary>
        /// Converts the GDI+ GraphicsPath to the MapScript shape object.
        /// </summary>
        /// <param name="path">The GraphicsPath object to convert.</param>
        /// <returns>The converted shapeObj</returns>
        public shapeObj GraphicsPathToShape(GraphicsPath path)
        {
            shapeObj feature = null;

            if (path != null)
            {
                using (GraphicsPathIterator myPathIterator = new GraphicsPathIterator(path))
                {
                    int myStartIndex;
                    int myEndIndex;
                    bool myIsClosed;
                    // get the number of Subpaths.
                    int numSubpaths = myPathIterator.SubpathCount;
                    while (myPathIterator.NextSubpath(out myStartIndex, out myEndIndex, out myIsClosed) > 0)
                    {
                        lineObj line = new lineObj();
                        for (int i = myStartIndex; i <= myEndIndex; i++)
                        {
                            if (i == myStartIndex || 
                                (path.PathPoints[i].X != path.PathPoints[i - 1].X && 
                                 path.PathPoints[i].Y != path.PathPoints[i - 1].Y))
                                line.add(new pointObj(Pixel2MapX(path.PathPoints[i].X), Pixel2MapY(path.PathPoints[i].Y), 0, 0));
                        }
                        if (feature == null)
                        {
                            if (myIsClosed && line.numpoints > 2)
                                feature = new shapeObj((int)MS_SHAPE_TYPE.MS_SHAPE_POLYGON);
                            else
                                feature = new shapeObj((int)MS_SHAPE_TYPE.MS_SHAPE_LINE);
                        }
                        if (line.numpoints >= 2)
                            feature.add(line);
                    }
                }
            }
            return feature;
        }

        /// <summary>
        /// Zooming the map by using the specified zoom factor
        /// </summary>
        /// <param name="zoomfactor">Zoom factor.</param>
        public void ZoomOut(double zoomfactor)
        {
            if (map != null)
            {
                using (pointObj center = new pointObj(map.width / 2, map.height / 2, 0, 0))
                {
                    map.zoomScale(zoomfactor * map.scaledenom, center, map.width, map.height, map.extent, null);
                    RaiseZoomChanged();
                }
                this.RefreshView();
            }
        }

        /// <summary>
        /// Zooming the map to the specified extent.
        /// </summary>
        /// <param name="minx">The minx of the extent.</param>
        /// <param name="miny">The miny of the extent.</param>
        /// <param name="maxx">The maxx of the extent.</param>
        /// <param name="maxy">The maxy of the extent.</param>
        public void ZoomRectangle(double minx, double miny, double maxx, double maxy)
        {
            if (map != null && (maxx - minx) > 2 && (maxy-miny) > 2)
            {
                using (rectObj imgrect = new rectObj(minx, miny, maxx, maxy, 0))
                {
                    // mapscript requires this hack
                    imgrect.miny = maxy;
                    imgrect.maxy = miny;

                    map.zoomRectangle(imgrect, map.width, map.height, map.extent, null);
                    RaiseZoomChanged();
                    this.RefreshView();
                    return;
                }
            }
        }

        /// <summary>
        /// Recenter the map to the specified pixel coordinate.
        /// </summary>
        /// <param name="imgX">X coordinate in the pixel space.</param>
        /// <param name="imgY">Y coordinate in the pixel space.</param>
        public void PanTo(int imgX, int imgY)
        {
            if (map != null)
            {
                using (pointObj imgpoint = new pointObj(imgX, imgY, 0, 0))
                {
                    map.zoomPoint(1, imgpoint, map.width, map.height, map.extent, null);
                    RaisePositionChanged();
                }
                this.RefreshView();
            }
        }

        /// <summary>
        /// Restore map the extent to the initial values.
        /// </summary>
        public void SetInitialExtent()
        {
            if (map != null)
            {
                map.setExtent(initial_extent.minx, initial_extent.miny,
                    initial_extent.maxx, initial_extent.maxy);
                RaiseZoomChanged();
                this.RefreshView();
            }
        }

        /// <summary>
        /// Get inital extent values.
        /// </summary>
        public rectObj GetInitialExtent()
        {
            return initial_extent;
        }

        /// <summary>
        /// Retrieve the actual zoom width in map coordinates.
        /// </summary>
        /// <returns>The actual zoom width in map coordinates</returns>
        public double GetZoom()
        {
            if (map != null)
                return map.extent.maxx - map.extent.minx;
            return 0;
        }

        /// <summary>
        /// Retrieve the current scale.
        /// </summary>
        /// <returns>The current scale.</returns>
        public double GetScale()
        {
            if (map != null)
                return map.scaledenom;
            return 0;
        }

        /// <summary>
        /// Get the current unit name.
        /// </summary>
        /// <returns>The current unit name.</returns>
        public string GetUnitName()
        {
            return unitName;
        }

        /// <summary>
        /// Get the current unit display unit.
        /// </summary>
        /// <returns>The current unit.</returns>
        public MS_UNITS GetMapUnits()
        {
            return mapunits;
        }

        /// <summary>
        /// Update the unit values according to the mapObj settings.
        /// </summary>
        public void UpdateUnitValues()
        {
            mapunits = map.units;

            unitPrecision = MapUtils.GetUnitPrecision(map.units);

            string newUnit = MapUtils.GetUnitName(mapunits);
            if (unitName != newUnit)
            {
                unitName = newUnit;
                RaiseZoomChanged();
            }
        }

        /// <summary>
        /// Fires a zoomchanged event
        /// </summary>
        private void RaiseZoomChanged()
        {
            double zoom = (map.extent.maxx - map.extent.minx);
            if (mapunits != map.units)
                zoom = zoom * MapUtils.InchesPerUnit(map.units) / MapUtils.InchesPerUnit(mapunits);
            target.RaiseZoomChanged(this, Math.Round(zoom, MapUtils.GetUnitPrecision(mapunits)), map.scaledenom);
        }

        /// <summary>
        /// Fires a positionchanged event
        /// </summary>
        private void RaisePositionChanged()
        {
            target.RaisePositionChanged(this, Math.Round((map.extent.maxx + map.extent.minx) / 2, MapUtils.GetUnitPrecision(mapunits)), Math.Round((map.extent.maxy + map.extent.miny) / 2, MapUtils.GetUnitPrecision(mapunits)));
        }

        /// <summary>
        /// Setting up the selection the drawing modes
        /// </summary>
        /// <param name="bQuery">The desired mode</param>
        private void SetSelectionMode(bool bQuery)
        {
            if (bQuery)
            {
                drawQuery = true;
                queryMode = false;
                for (int i = 0; i < map.numlayers; i++)
                {
                    layerObj layer = map.getLayer(i);
                    if (layer.status != mapscript.MS_OFF)
                    {
                        using (resultCacheObj results = layer.getResults())
                        {
                            if (results != null && results.numresults > 0)
                            {
                                queryMode = true;
                                // suppress the query drawing for the unsupported types
                                if (layer.type != MS_LAYER_TYPE.MS_LAYER_POINT &&
                                    layer.type != MS_LAYER_TYPE.MS_LAYER_POLYGON &&
                                    layer.type != MS_LAYER_TYPE.MS_LAYER_LINE &&
                                    layer.type != MS_LAYER_TYPE.MS_LAYER_ANNOTATION &&
                                    layer.type != MS_LAYER_TYPE.MS_LAYER_RASTER)
                                    drawQuery = false;
                            }
                        }
                    }
                }
                if (!queryMode)
                    drawQuery = false;
            }
            else
            {
                if (queryMode || drawQuery)
                {
                    queryMode = false;
                    drawQuery = false;
                    ClearResults();
                    this.target.RaiseSelectionChanged(this);
                    this.RefreshView();
                }
            }
        }

        /// <summary>
        /// Draw the map.
        /// </summary>
        /// <param name="pe">The argument containing the drawing context to use.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (mapImage != null)
            {
                if (dragging)
                {
                    if (inputMode == InputModes.Pan || forcePan)
                    {
                        float offsetX = (magnify - 1) * mouseX - magnify * dragRect.Width;
                        float offsetY = (magnify - 1) * mouseY - magnify * dragRect.Height;

                        pe.Graphics.DrawImage(mapImage, -offsetX, -offsetY, magnify * mapImage.Width, magnify * mapImage.Height);
                    }
                    else if (inputMode == InputModes.ZoomIn || inputMode == InputModes.ZoomOut || inputMode == InputModes.TrackRectangle)
                    {
                        // drawing the map image
                        pe.Graphics.DrawImage(mapImage, 0, 0);
                        // drawing the rectangle
                        Rectangle normalizedRectangle =
                                new Rectangle(
                                Math.Min(dragRect.X, dragRect.X + dragRect.Width),
                                Math.Min(dragRect.Y, dragRect.Y + dragRect.Height),
                                Math.Abs(dragRect.Width),
                                Math.Abs(dragRect.Height));

                        pe.Graphics.FillRectangle(selectionBrush, normalizedRectangle);
                        pe.Graphics.DrawRectangle(selectionPen, normalizedRectangle);
                    }
                }
                else
                {
                    float offsetX = (magnify - 1) * mouseX;
                    float offsetY = (magnify - 1) * mouseY;
                    // drawing the map image
                    pe.Graphics.DrawImage(mapImage, -offsetX, -offsetY, magnify * mapImage.Width, magnify * mapImage.Height);

                    if (inputMode == InputModes.TrackPolygon || inputMode == InputModes.TrackLine)
                    {
                        if (trackPoints != null && trackPoints.PointCount >= 2)
                        {
                            if (inputMode == InputModes.TrackPolygon)
                                pe.Graphics.FillPath(selectionBrush, trackPoints);
                            pe.Graphics.DrawPath(selectionPen, trackPoints);
                        }

                    }
                }
            }

            if (drawMessage != null)
            {
                using (Font font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point))
                {
                    RectangleF rectF1 = new RectangleF(0, 0, this.Width, this.Height);
                    pe.Graphics.DrawString(drawMessage, font1, Brushes.Blue, rectF1);
                }
            }

            if (border)
            {
                // implement additional drawing if needed
                pe.Graphics.DrawRectangle(borderPen, new Rectangle(0,0, this.Width-1, this.Height-1));
            }
        }

        /// <summary>
        /// Adding a preview feature to the layer according to the layer type. This method is used to initialize the preview controls.
        /// </summary>
        /// <param name="srcLayer">The source layer the felature should be created in.</param>
        /// <param name="numvalues">The number of the feature attributes to be created.</param>
        private void AddSampleFeature(layerObj srcLayer, int numvalues)
        {  
            if (srcLayer.type == MS_LAYER_TYPE.MS_LAYER_LINE)
            {
                using (shapeObj shape = new shapeObj((int)MS_SHAPE_TYPE.MS_SHAPE_LINE))
                {
                    // adding a horizontal line sample
                    using (lineObj line = new lineObj())
                    {
                        using (pointObj point = new pointObj(this.gap, this.Height / 2, 0, 0))
                        {
                            line.add(point);
                            point.setXY(this.Width - this.gap, this.Height / 2, 0);
                            line.add(point);
                        }
                        shape.add(line);
                    }
                    if (numvalues > 0)
                        shape.initValues(numvalues);
                    srcLayer.addFeature(shape);
                }
            }
            else if (srcLayer.type == MS_LAYER_TYPE.MS_LAYER_POLYGON)
            {
                using (shapeObj shape = new shapeObj((int)MS_SHAPE_TYPE.MS_SHAPE_POLYGON))
                {
                    // adding a rectangle sample
                    using (lineObj line = new lineObj())
                    {
                        using (pointObj point = new pointObj(this.gap, this.gap, 0, 0))
                        {
                            line.add(point);
                            point.setXY(this.Width - this.gap, this.gap, 0);
                            line.add(point);
                            point.setXY(this.Width - this.gap, this.Height - this.gap, 0);
                            line.add(point);
                            point.setXY(this.gap, this.Height - this.gap, 0);
                            line.add(point);
                            point.setXY(this.gap, this.gap, 0);
                            line.add(point);
                        }
                        shape.add(line);
                    }
                    if (numvalues > 0)
                        shape.initValues(numvalues);
                    srcLayer.addFeature(shape);
                }
            }
            else
            {
                using (shapeObj shape = new shapeObj((int)MS_SHAPE_TYPE.MS_SHAPE_POINT))
                {
                    // adding a point sample
                    using (lineObj line = new lineObj())
                    {
                        using (pointObj point = new pointObj(this.Width / 2, this.Height / 2, 0, 0))
                        {
                            line.add(point);
                        }
                        shape.add(line);
                    }
                    if (numvalues > 0)
                        shape.initValues(numvalues);
                    srcLayer.addFeature(shape);
                }
            }
        }

        /// <summary>
        /// Create a default layer for creating a preview to another layer
        /// </summary>
        /// <param name="originalMap">The original map.</param>
        /// <param name="originalLayer">The original layer.</param>
        /// <returns>The created layer object.</returns>
        private layerObj InitializeDefaultLayer(mapObj originalMap, layerObj originalLayer)
        {
            // create a new map object
            map = new mapObj(null);
            map.units = MS_UNITS.MS_PIXELS;
            map.setExtent(0, 0, this.Width, this.Height);
            map.width = this.Width;
            map.height = this.Height;
            outputFormatObj format = originalMap.outputformat;
            if (map.getOutputFormatByName(format.name) == null)
                map.appendOutputFormat(format);
            map.selectOutputFormat(originalMap.imagetype);
            // copy symbolset
            for (int i = 1; i < originalMap.symbolset.numsymbols; i++)
            {
                symbolObj origsym = originalMap.symbolset.getSymbol(i);
                map.symbolset.appendSymbol(MapUtils.CloneSymbol(origsym));
            }
            // copy the fontset
            string key = null;
            while ((key = originalMap.fontset.fonts.nextKey(key)) != null)
                map.fontset.fonts.set(key, originalMap.fontset.fonts.get(key, ""));
            // setting a default font
            //map.fontset.fonts.set("", 
            //    originalMap.fontset.fonts.get(originalMap.fontset.fonts.nextKey(null),""));
            // insert a new layer
            layerObj layer = new layerObj(map);
            if (originalLayer != null)
            {
                // the chart type doesn't support having as single class in it
                if (originalLayer.type == MS_LAYER_TYPE.MS_LAYER_CHART)
                    layer.type = MS_LAYER_TYPE.MS_LAYER_POLYGON;
                else    
                    layer.type = originalLayer.type;
                originalLayer.open();
                // add the sample feature to the layer
                AddSampleFeature(layer, originalLayer.numitems);
                if (originalLayer.getResults() == null)
                    originalLayer.close(); // close only is no query results
            }
            else
            {
                layer.type = MS_LAYER_TYPE.MS_LAYER_ANNOTATION;
                // add the sample feature to the layer
                AddSampleFeature(layer, 0);
            }
            layer.status = mapscript.MS_ON;
            
            return layer;
        }

        /// <summary>
        /// Creating a sample (preview) based on a classObj, styleObj or labelObj.
        /// </summary>
        /// <param name="original">The wrapper holding the original object.</param>
        private void CreateSampleMap(MapObjectHolder original)
        {
            MapObjectHolder originalMap = null;
            MapObjectHolder originalLayer = null;
            MapObjectHolder originalClass = null;
            // create a sample map to render a preview of the given object
            if (original.GetType() == typeof(classObj))
            {
                // tracking down the whole object tree
                originalLayer = original.GetParent();
                if (originalLayer != null)
                    originalMap = originalLayer.GetParent();
                // creating a new compatible map object
                if (originalMap != null)
                {
                    layerObj layer = InitializeDefaultLayer(originalMap, originalLayer);
                    layer.insertClass(((classObj)original).clone(), -1);
                    // bindings are not supported with sample maps
                    classObj classobj = layer.getClass(0);
                    for (int i = 0; i < classobj.numstyles; i++)
                        StyleBindingController.RemoveAllBindings(classobj.getStyle(i));
                    for (int i = 0; i < classobj.numlabels; i++)
                        LabelBindingController.RemoveAllBindings(classobj.getLabel(i));
                    classobj.setText("Sample text");
                    classobj.setExpression(""); // remove expression to have the class shown

                    this.target = new MapObjectHolder(classobj, original.GetParent());
                }
            }
            else if (original.GetType() == typeof(styleObj))
            {
                // tracking down the whole object tree
                if (original.GetParent().GetType() == typeof(labelObj))
                    originalClass = original.GetParent().GetParent();
                else
                    originalClass = original.GetParent();
                if (originalClass != null)
                    originalLayer = originalClass.GetParent();
                if (originalLayer != null)
                    originalMap = originalLayer.GetParent();
                // creating a new compatible map object
                if (originalMap != null)
                {
                    layerObj layer = InitializeDefaultLayer(originalMap, originalLayer);
                    classObj classobj = new classObj(layer);
                    classobj.name = MapUtils.GetClassName(layer);
                    styleObj style;
                    if (original.GetParent().GetType() == typeof(labelObj))
                    {
                        
                        classobj.addLabel(new labelObj());
                        labelObj label = classobj.getLabel(classobj.numlabels - 1);
                        MapUtils.SetDefaultLabel(label, layer.map);
                        label.insertStyle(((styleObj)original).clone(), -1);
                        style = label.getStyle(0);
                    }
                    else
                    {
                        classobj.insertStyle(((styleObj)original).clone(), -1);
                        style = classobj.getStyle(0);
                    }
                    
                    // bindings are not supported with sample maps
                    StyleBindingController.RemoveAllBindings(style);
                    classobj.setText("Sample text");
                    this.target = new MapObjectHolder(style, original.GetParent());
                }
            }
            else if (original.GetType() == typeof(labelObj))
            {
                // tracking down the whole object tree
                originalClass = original.GetParent();
                if (originalClass != null)
                {
                    if (originalClass.GetType() == typeof(classObj))
                    {
                        originalLayer = originalClass.GetParent();
                        if (originalLayer != null)
                            originalMap = originalLayer.GetParent();
                    }
                    else if (originalClass.GetType() == typeof(scalebarObj))
                    {
                        originalMap = originalClass.GetParent();
                    }
                }
                
                // creating a new compatible map object
                if (originalMap != null)
                {
                    layerObj layer = InitializeDefaultLayer(originalMap, originalLayer);
                    classObj classobj = new classObj(layer);
                    classobj.name = MapUtils.GetClassName(layer);
                    labelObj label = new labelObj();
                        
                    if (originalClass.GetType() == typeof(classObj))
                    {
                        // copy settings
                        label.updateFromString(((labelObj)original).convertToString());
                    }
                    classobj.addLabel(label);
                    
                    this.target = new MapObjectHolder(layer.getClass(0).getLabel(0), original.GetParent());
                }
            }
            else
                throw new Exception("Invalid target type: " + original.GetType());
        }

        #region IMapControl Members

        /// <summary>
        /// Refresh the controls according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            timerRefresh.Enabled = false;

            if (!enableRendering)
            {
                drawMessage = "Rendering is disabled.";
                this.Refresh();
                return;
            }

            if (BeforeRefresh != null)
                BeforeRefresh(this, null);
            
            // setting up the size of the map image
            mapImage = null;
            if (map != null)
            {
                if (this.Height > 2 && this.Width > 2 &&
                    map.extent.maxx > map.extent.minx && map.extent.maxy > map.extent.miny)
                {
                    if (map.width != this.Width || map.height != this.Height)
                    {
                        map.height = this.Height;
                        map.width = this.Width;
                        map.setExtent(map.extent.minx, map.extent.miny, map.extent.maxx, map.extent.maxy);
                        RaiseZoomChanged();
                    }

                    UpdateTansformations();

                    using (outputFormatObj format = map.outputformat)
                    {
                        string imageType = null;
                        if ((format.renderer != mapscript.MS_RENDER_WITH_GD && 
                             format.renderer != mapscript.MS_RENDER_WITH_AGG &&
                             format.renderer != mapscript.MS_RENDER_WITH_CAIRO_RASTER)
                            || string.Compare(format.mimetype.Trim(), "image/vnd.wap.wbmp", true) == 0
                            || string.Compare(format.mimetype.Trim(), "image/tiff", true) == 0)
                        {
                            // falling back to the png type in case of the esoteric types
                            imageType = map.imagetype;
                            map.selectOutputFormat("png");
                            drawMessage = "MapManager cannot display maps in the output format " + imageType + ", rendering with png instead.";
                        }
                        else
                        {
                            drawMessage = null;
                        }

                        Cursor cur = this.Cursor;
                        try
                        {
                            this.Cursor = Cursors.WaitCursor;
                            if (drawQuery)
                            {
                                using (imageObj image = map.drawQuery())
                                {
                                    byte[] img = image.getBytes();
                                    using (MemoryStream ms = new MemoryStream(img))
                                    {
                                        mapImage = Image.FromStream(ms);
                                    }
                                }
                            }
                            else
                            {
                                using (imageObj image = map.draw())
                                {
                                    byte[] img = image.getBytes();
                                    using (MemoryStream ms = new MemoryStream(img))
                                    {
                                        mapImage = Image.FromStream(ms);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //drawMessage = "Unable to render image. " + ex.Message;
                            EventProvider.RaiseEventMessage(this, ex.Message, EventProvider.EventTypes.Error);
                        }
                        finally
                        {
                            // switch back to the original type
                            if (imageType != null)
                                map.selectOutputFormat(imageType);
                            this.Cursor = cur;
                        } 
                    }
                }
                UpdateUnitValues();
            }

            magnify = 1;
            this.Refresh();

            if (AfterRefresh != null)
                AfterRefresh(this, null);
        }

        /// <summary>
        /// Save the current image to a file.
        /// </summary>
        /// <param name="fileName">The name of the file to be created.</param>
        public void SaveImage(string fileName)
        {
            if (map != null)
            {
                if (this.Height > 2 && this.Width > 2)
                {
                    map.height = this.Height;
                    map.width = this.Width;
                    using (imageObj image = map.draw())
                    {
                        image.save(fileName, map);
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the target object of the editor.
        /// </summary>
        public MapObjectHolder Target
        {
            get
            {
                return target;
            }
            set
            {
                if (value != null)
                {
                    if (value.GetType() == typeof(mapObj))
                    {
                        map = value;
                        target = value;
                    }
                    else
                        CreateSampleMap(value);
                }
                else
                {
                    map = null;
                    target = null;
                }
                this.RefreshView();

                if (map != null)
                {
                    StoreInitialExtent();
                    RaiseZoomChanged();
                }
            }
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties;

        #endregion

        /// <summary>
        /// Stores the current exten as the initial extent
        /// </summary>
        public void StoreInitialExtent()
        {
            if (map != null)
            {
                if (initial_extent == null)
                    initial_extent = new rectObj(0, 0, 0, 0, 0);

                initial_extent.maxx = map.extent.maxx;
                initial_extent.maxy = map.extent.maxy;
                initial_extent.minx = map.extent.minx;
                initial_extent.miny = map.extent.miny;
            }
        }

        /// <summary>
        /// SizeChanged event handler of the Map control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MapControl_SizeChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.RefreshView();
            }
        }

        /// <summary>
        /// MouseDown event handler of the Map control. Occurs when a mouse button is pressed.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Focused)
                this.Focus();   // Allowing to receive the MouseWheel event

            if (!this.Focused)
                return;         // there have been a problem when validating a control

            if (!enableRendering)
                return;
            
            if (inputMode == InputModes.TrackPolygon || inputMode == InputModes.TrackLine)
            {
                if (trackPoints == null)
                {
                    trackPoints = new GraphicsPath();
                    trackPoints.StartFigure();
                }
                trackPoints.AddLine(e.X, e.Y, e.X, e.Y);
            }
            else if (inputMode == InputModes.Select)
            {

            }
            else
            {
                dragging = true;
                forcePan = (e.Button == MouseButtons.Middle);
            }

            dragRect.X = e.X;
            dragRect.Y = e.Y;
            dragRect.Width = 0;
            dragRect.Height = 0;

            this.Refresh();
        }

        /// <summary>
        /// MouseMove event handler of the Map control. Occurs when a mouse is moving over the control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            if (dragging)
            {
                dragRect.Width = e.X - dragRect.X;
                dragRect.Height = e.Y - dragRect.Y;
                this.Refresh();
            }

            if (inputMode == InputModes.TrackPolygon || inputMode == InputModes.TrackLine)
            {
                if (trackPoints != null)
                {
                    PointF[] points = trackPoints.PathPoints;
                    points[points.Length - 1].X = e.X;
                    points[points.Length - 1].Y = e.Y;
                    trackPoints = new GraphicsPath();
                    if (inputMode == InputModes.TrackPolygon && points.Length > 2)
                        trackPoints.AddPolygon(points);
                    else
                        trackPoints.AddLines(points);
                    this.Refresh();
                }
            }

            if (CursorMove != null)
                CursorMove(this, Math.Round(Pixel2MapX(e.X), unitPrecision),
                                Math.Round(Pixel2MapY(e.Y), unitPrecision));
                //CursorMove(this, e.X, e.Y);
        }

        private bool TestResult(pointObj pt)
        {
            layerObj layer = map.getLayer(0);
            layer.queryByPoint(map, pt, mapscript.MS_SINGLE, 4);
            resultCacheObj results = layer.getResults();
            layer.Dispose();
            return (results != null && results.numresults > 0);
        }

        /// <summary>
        /// MouseUp event handler of the Map control. Occurs when a mouse button have been released.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MapControl_MouseUp(object sender, MouseEventArgs e)
        {
            forcePan = false;

            if (inputMode == InputModes.Select)
            {
                //pointObj pt = new pointObj(Pixel2MapX(e.X), Pixel2MapY(e.Y), 0, 0);
                //TestResult(pt);
                //return;
                
                
                using (pointObj imgpoint = new pointObj(Pixel2MapX(e.X), Pixel2MapY(e.Y), 0, 0))
                {
                    ClearResults(); // clear the previous results
                    map.queryByPoint(imgpoint, mapscript.MS_MULTIPLE, 4);
                    SetSelectionMode(true);
                    this.target.RaiseSelectionChanged(this);
                    this.RefreshView();
                }
                return;
            }

            if (magnify != 1)
            {
                double offsetX = ((double)1 - (double)1 / magnify) * mouseX;
                double offsetY = ((double)1 - (double)1 / magnify) * mouseY;

                if (dragging)
                {
                    dragging = false;
                    dragRect.Width = e.X - dragRect.X;
                    dragRect.Height = e.Y - dragRect.Y;
                    offsetX -= dragRect.Width;
                    offsetY -= dragRect.Height;
                }

                this.ZoomRectangle((double)offsetX, (double)offsetY,
                    (double)offsetX + (double)mapImage.Width / magnify,
                    (double)offsetY + (double)mapImage.Height / magnify);
                
                return;
            }

            if (dragging)
            {
                dragging = false;
                dragRect.Width = e.X - dragRect.X;
                dragRect.Height = e.Y - dragRect.Y;

                if (inputMode == InputModes.Pan || e.Button == MouseButtons.Middle)
                {
                    if (map != null)
                        PanTo(map.width / 2 - dragRect.Width, map.height / 2 - dragRect.Height);
                    return;
                }

                if (inputMode == InputModes.ZoomIn)
                {
                    ZoomRectangle(Math.Min(dragRect.X, dragRect.X + dragRect.Width),
                        Math.Min(dragRect.Y, dragRect.Y + dragRect.Height),
                        Math.Max(dragRect.X, dragRect.X + dragRect.Width),
                        Math.Max(dragRect.Y, dragRect.Y + dragRect.Height));
                    return;
                }

                if (inputMode == InputModes.ZoomOut)
                {
                    if (map != null)
                    {
                        //Stephane: change behaviour if small drag of point click to zoom out by a factor of 2, like IntraMaps
                        //if (dragRect.Width <= 2) dragRect.Width = 2;
                        //if (dragRect.Height <= 2) dragRect.Height = 2;
                        if (dragRect.Width <= 2 || dragRect.Height <= 2)
                        {
                            ZoomOut(2);
                        }
                        else
                        {
                            using (pointObj center = new pointObj(dragRect.X + dragRect.Width / 2, dragRect.Y + dragRect.Height / 2, 0, 0))
                            {
                                double zoomfactor = Math.Min((double)map.width / dragRect.Width, (double)map.height / dragRect.Height);
                                map.zoomScale(zoomfactor * map.scaledenom, center, map.width, map.height, map.extent, null);
                                RaiseZoomChanged();
                                this.RefreshView();
                            }
                        }
                    }
                    return;
                }

                if (inputMode == InputModes.TrackRectangle)
                {
                    double x = Pixel2MapX(dragRect.X);
                    double y = Pixel2MapY(dragRect.Y);
                    double x2 = Pixel2MapX(dragRect.X + dragRect.Width);
                    double y2 = Pixel2MapY(dragRect.Y + dragRect.Height);

                    using (rectObj rect = new rectObj(Math.Min(x, x2), Math.Min(y, y2), Math.Max(x, x2), Math.Max(y, y2), 0))
                    {
                        map.queryByRect(rect);
                        SetSelectionMode(true);
                        this.target.RaiseSelectionChanged(this);
                        this.RefreshView();
                        return;
                    }
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// DoubleClick event handler of the Map control. Occurs when a mouse button have been doubleclicked.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MapControl_DoubleClick(object sender, EventArgs e)
        {
            if (inputMode == InputModes.Pan)
            {
                PanTo(dragRect.X, dragRect.Y);
            }
            else if (inputMode == InputModes.TrackPolygon || inputMode == InputModes.TrackLine)
            {
                try
                {
                    using (shapeObj shape = GraphicsPathToShape(this.trackPoints))
                    {
                        map.queryByShape(shape);
                        SetSelectionMode(true);
                        this.trackPoints = null;
                        this.target.RaiseSelectionChanged(this);
                        this.RefreshView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in query, " + ex.Message,
                        "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.trackPoints = null;
                }           
            }
        }

        /// <summary>
        /// Override of the OnMouseWheel function of the base class. Occurs when the mouse wheel have been rotated.
        /// </summary>
        /// <param name="e">The event parameters.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            timerRefresh.Enabled = false;
            mouseX = e.X;
            mouseY = e.Y;
            magnify *= (float)e.Delta / 480 + 1;
            this.Refresh();
            base.OnMouseWheel(e);

            if (!dragging)
                timerRefresh.Enabled = true;
        }

        /// <summary>
        /// Tick event handler of the Timer control. Occurs when the map is magnified by the wheel and no dragging happens.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (magnify != 1)
            {
                double offsetX = ((double)1 - (double)1 / magnify) * mouseX;
                double offsetY = ((double)1 - (double)1 / magnify) * mouseY;
                this.ZoomRectangle((double)offsetX, (double)offsetY,
                    (double)offsetX + (double)mapImage.Width / magnify,
                    (double)offsetY + (double)mapImage.Height / magnify);
            }
        }

        /// <summary>
        /// The selection mode of the map control
        /// </summary>
        public bool QueryMode
        {
            get
            {
                return queryMode;
            }
        }

        /// <summary>
        /// Clear the result cache of the layers in the current map
        /// </summary>
        private void ClearResults()
        {
            if (map != null)
            {
                map.freeQuery(-1);
                // close all layers to free expression tokens
                for (int i = 0; i < map.numlayers; i++)
                    map.getLayer(i).close();
            }
        }

        /// <summary>
        /// Removing the selection an draw the map in normal mode
        /// </summary>
        public void ClearSelection()
        {
            SetSelectionMode(false);
        }
    }
}
