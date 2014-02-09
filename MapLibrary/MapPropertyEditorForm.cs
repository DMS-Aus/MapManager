using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DMS.MapLibrary;
using OSGeo.MapServer;
using System.Globalization;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Common form for hosting the various Editor controls.
    /// </summary>
    public partial class MapPropertyEditorForm : Form
    {
        private DMS.MapLibrary.IPropertyEditor editor;

        public new event HelpEventHandler HelpRequested;

        /// <summary>
        /// Constructs a new MapPropertyEditorForm object.
        /// </summary>
        /// <param name="target">The target object to be edited.</param>
        public MapPropertyEditorForm(MapObjectHolder target) : this(target, null)
        {    
        }

        /// <summary>
        /// Constructs a new MapPropertyEditorForm object.
        /// </summary>
        /// <param name="target">The target object to be edited.</param>
        /// <param name="editor">The editor to be used.</param>
        public MapPropertyEditorForm(MapObjectHolder target, IPropertyEditor editor)
        {
            InitializeComponent();
            this.SuspendLayout();
            if (target.GetType() == typeof(mapObj))
            {
                if (editor == null)
                {
                    editor = new MapPropertyEditor();
                    ((MapPropertyEditor)editor).HelpRequested += 
                        new HelpEventHandler(mapPropertyEditor_HelpRequested);
                }

                this.Text = "Map Properties";
                mapObj map = (mapObj)target;
                if (map.name != "")
                this.Text += " (" + map.name + ")";
                this.editor = editor;
            }
            else if (target.GetType() == typeof(layerObj))
            {
                if (editor == null)
                {
                    editor = new LayerPropertyEditor();
                    ((LayerPropertyEditor)editor).HelpRequested += 
                        new HelpEventHandler(mapPropertyEditor_HelpRequested);
                }
                
                this.Text = "Layer Properties";
                layerObj layer = (layerObj)target;
                if (layer.name != "")
                    this.Text += " (" + layer.name + ")";
                this.editor = editor;
            }
            else if (target.GetType() == typeof(classObj))
            {
                if (editor == null)
                {
                    editor = new ClassPropertyEditor();
                    ((ClassPropertyEditor)editor).HelpRequested += 
                        new HelpEventHandler(mapPropertyEditor_HelpRequested);
                }

                this.Text = "Class Properties";
                classObj classObject = (classObj)target;

                StringBuilder scaledomain = new StringBuilder("");
                if (classObject.minscaledenom >= 0)
                {
                    if (classObject.maxscaledenom >= 0)
                        scaledomain.Append(" 1:" + classObject.minscaledenom.ToString("#,#", CultureInfo.InvariantCulture));
                    else
                        scaledomain.Append(" from 1:" + classObject.minscaledenom.ToString("#,#", CultureInfo.InvariantCulture));
                }
                if (classObject.maxscaledenom >= 0)
                {
                    scaledomain.Append(" to 1:" + classObject.maxscaledenom.ToString("#,#", CultureInfo.InvariantCulture));
                }

                if (classObject.name != "")
                    this.Text += " (" + classObject.name + scaledomain + ")";
                this.editor = editor;
            }
            else if (target.GetType() == typeof(styleObj))
            {
                if (editor == null)
                {
                    editor = new StylePropertyEditor();
                    ((StylePropertyEditor)editor).HelpRequested += 
                        new HelpEventHandler(mapPropertyEditor_HelpRequested);
                }

                this.Text = "Style Properties";
                this.editor = editor;
            }

            else if (target.GetType() == typeof(labelObj))
            {
                if (editor == null)
                {
                    editor = new LabelPropertyEditor();
                    ((LabelPropertyEditor)editor).HelpRequested += 
                        new HelpEventHandler(mapPropertyEditor_HelpRequested);
                }

                this.Text = "Label Properties";
                this.editor = editor;
            }
            else if (target.GetType() == typeof(scalebarObj))
            {
                if (editor == null)
                {
                    editor = new ScalebarPropertyEditor();
                    ((ScalebarPropertyEditor)editor).HelpRequested += 
                        new HelpEventHandler(mapPropertyEditor_HelpRequested);
                }

                this.Text = "Scalebar Properties";
                this.editor = editor;
            }
            else if (target.GetType() == typeof(queryMapObj))
            {
                if (editor == null)
                {
                    editor = new QueryMapPropertyEditor();
                    ((QueryMapPropertyEditor)editor).HelpRequested += 
                        new HelpEventHandler(mapPropertyEditor_HelpRequested);
                }

                this.Text = "Query Map Properties";
                this.editor = editor;
            }
            else
                throw new Exception("No editor have been implemented for this item");

            if (this.editor != null)
            {
                Control c = (Control)this.editor;
                c.Location = new System.Drawing.Point(3, 4);
                c.TabIndex = 0;

                editor.Target = target;
                this.Controls.Add(c);
                target.PropertyChanging += new EventHandler(target_PropertyChanging);
                editor.EditProperties += new EditPropertiesEventHandler(editor_EditProperties);
                               
                buttonOK.Top = c.Bottom + 8;
                buttonCancel.Top = c.Bottom + 8;
                buttonApply.Top = c.Bottom + 8;
            }

            UpdateButtonState();

            this.ResumeLayout(false);
        }

        private void SetupEditor()
        {

        }

        /// <summary>
        /// Gets current property editor.
        /// </summary>
        public IPropertyEditor Editor
        {
            get { return this.editor; }
        }

        /// <summary>
        /// Event handled to edit a sub-object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="target">The object to be edited.</param>
        void editor_EditProperties(object sender, MapObjectHolder target)
        {
            try
            {
                MapPropertyEditorForm mapPropertyEditor = new MapPropertyEditorForm(target);
                mapPropertyEditor.HelpRequested += new HelpEventHandler(mapPropertyEditor_HelpRequested);
                mapPropertyEditor.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// HelpRequested event handler of the mapPropertyEditor control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="hlpevent">The parameters of the help event.</param>
        void mapPropertyEditor_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (this.HelpRequested != null)
                this.HelpRequested(sender, hlpevent);
        }

        /// <summary>
        /// Update the Apply button state
        /// </summary>
        private void UpdateButtonState()
        {           
            if (editor != null)
                buttonApply.Enabled = editor.IsDirty();
        }

        /// <summary>
        /// Event handler for the PropertyChanging event of the target object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void target_PropertyChanging(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
        }

        /// <summary>
        /// Click event handler for the buttonOK object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            editor.UpdateValues();
            this.Close();
        }

        /// <summary>
        /// Click event handler for the buttonCancel object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            editor.CancelEditing();
            this.Close();
        }

        /// <summary>
        /// Click event handler for the buttonApply object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonApply_Click(object sender, EventArgs e)
        {
            editor.UpdateValues();
            UpdateButtonState();
        }

        Color _darkColor = SystemColors.ControlDark;
        Color _lightColor = SystemColors.ControlLightLight;

        /// <summary>
        /// Draw horizontal line in the control.
        /// </summary>
        /// <param name="e">The argument containing the drawing context to use.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Brush lightBrush = new SolidBrush(_lightColor);
            Brush darkBrush = new SolidBrush(_darkColor);
            Pen lightPen = new Pen(lightBrush, 1);
            Pen darkPen = new Pen(darkBrush, 1);

            e.Graphics.DrawLine(darkPen, 0, buttonCancel.Top - 4, this.Width, buttonCancel.Top - 4);
            e.Graphics.DrawLine(lightPen, 0, buttonCancel.Top - 3, this.Width, buttonCancel.Top - 3);
        }

        /// <summary>
        /// The resize event handler of the control.
        /// </summary>
        /// <param name="e">The event parameters.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Refresh();
        }

        /// <summary>
        /// KeyDown event handler for the MapPropertyEditorForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MapPropertyEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                editor.CancelEditing();
                this.Close();
            }
        }
    }
}