using System;
using System.Collections.Generic;
using System.Text;
using OSGeo.MapServer;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Wrapper class to store mapscript objects along with their parent objects.
    /// </summary>
    public class MapObjectHolder
    {
        private object target;
        private MapObjectHolder parent;

        /// <summary>
        /// Signs that a property have been changed.
        /// </summary>
        public event EventHandler PropertyChanged;
        /// <summary>
        /// Signs that a property is about to change.
        /// </summary>
        public event EventHandler PropertyChanging;
        /// <summary>
        /// Signs that the feature selection have been changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Event handler to sign that the zoom has been changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="zoom">The current zoom setting.</param>
        /// <param name="scale">The current scale setting.</param>
        public delegate void ZoomChangedEventHandler(object sender, double zoom, double scale);

        /// <summary>
        /// The ZoomChanged event object.
        /// </summary>
        public event ZoomChangedEventHandler ZoomChanged;

        /// <summary>
        /// Triggers a ZoomChanged event along with the hierarchy.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        /// <param name="zoom">Current zoom.</param>
        /// <param name="scale">Current scale.</param>
        internal void RaiseZoomChanged(object sender, double zoom, double scale)
        {
            if (ZoomChanged != null)
                ZoomChanged(sender, zoom, scale);
        }

        /// <summary>
        /// Event handler to sign that the position (map center) has been changed.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        /// <param name="x">Current x position in map coordinates</param>
        /// <param name="y">Current y position in map coordinates</param>
        public delegate void PositionChangedEventHandler(object sender, double x, double y);

        /// <summary>
        /// The PositionChanged event object.
        /// </summary>
        public event PositionChangedEventHandler PositionChanged;

        /// <summary>
        /// Triggers a PositionChanged event along with the hierarchy.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        /// <param name="x">Current x position in map coordinates</param>
        /// <param name="y">Current y position in map coordinates</param>
        public void RaisePositionChanged(object sender, double x, double y)
        {
            if (PositionChanged != null)
                PositionChanged(sender, x, y);
        }

        /// <summary>
        /// Triggers a PropertyChanged event along with the hierarchy.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        public void RaisePropertyChanged(object sender)
        {
            if (PropertyChanged != null)
                PropertyChanged(sender, null);
            // propagate the event to the parent objects
            if (parent != null)
                parent.RaisePropertyChanged(sender);
        }

        /// <summary>
        /// Triggers a PropertyChanging event along with the hierarchy.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        public void RaisePropertyChanging(object sender)
        {
            if (PropertyChanging != null)
                PropertyChanging(sender, null);
            // propagate the event to the parent objects
            if (parent != null)
                parent.RaisePropertyChanging(sender);
        }

        /// <summary>
        /// Triggers a SelectionChanged event along with the hierarchy.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        public void RaiseSelectionChanged(object sender)
        {
            if (SelectionChanged != null)
                SelectionChanged(sender, null);
            // propagate the event to the parent objects
            if (parent != null)
                parent.RaiseSelectionChanged(sender);
        }

        /// <summary>
        /// Constructs a new MapObjectHolder object
        /// </summary>
        /// <param name="target">The MapScript object to be stored.</param>
        /// <param name="parent">The wrapper of the parent object.</param>
        public MapObjectHolder(object target, MapObjectHolder parent)
        {
            this.target = target;
            this.parent = parent;
        }

        /// <summary>
        /// Gets or sets the encapsulated object
        /// </summary>
        public object NativeObjectRef
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
                RaisePropertyChanged(this);
            }
        }

        /// <summary>
        /// Allow implicit cast to the mapObj target type.
        /// </summary>
        /// <param name="v">The source object.</param>
        /// <returns>The target type.</returns>
        public static implicit operator mapObj(MapObjectHolder v)
        {
            if (v == null)
                return null;
            return (mapObj)v.target;
        }

        /// <summary>
        /// Allow implicit cast to the layerObj target type.
        /// </summary>
        /// <param name="v">The source object.</param>
        /// <returns>The target type.</returns>
        public static implicit operator layerObj(MapObjectHolder v)
        {
            if (v == null)
                return null;
            return (layerObj)v.target;
        }

        /// <summary>
        /// Allow implicit cast to the classObj target type.
        /// </summary>
        /// <param name="v">The source object.</param>
        /// <returns>The target type.</returns>
        public static implicit operator classObj(MapObjectHolder v)
        {
            if (v == null)
                return null;
            return (classObj)v.target;
        }

        /// <summary>
        /// Allow implicit cast to the styleObj target type.
        /// </summary>
        /// <param name="v">The source object.</param>
        /// <returns>The target type.</returns>
        public static implicit operator styleObj(MapObjectHolder v)
        {
            if (v == null)
                return null;
            return (styleObj)v.target;
        }

        /// <summary>
        /// Allow implicit cast to the labelObj target type.
        /// </summary>
        /// <param name="v">The source object.</param>
        /// <returns>The target type.</returns>
        public static implicit operator labelObj(MapObjectHolder v)
        {
            if (v == null)
                return null;
            return (labelObj)v.target;
        }

        /// <summary>
        /// Allow implicit cast to the scalebarObj target type.
        /// </summary>
        /// <param name="v">The source object.</param>
        /// <returns>The target type.</returns>
        public static implicit operator scalebarObj(MapObjectHolder v)
        {
            if (v == null)
                return null;
            return (scalebarObj)v.target;
        }

        /// <summary>
        /// Allow implicit cast to the queryMapObj target type.
        /// </summary>
        /// <param name="v">The source object.</param>
        /// <returns>The target type.</returns>
        public static implicit operator queryMapObj(MapObjectHolder v)
        {
            if (v == null)
                return null;
            return (queryMapObj)v.target;
        }

        /// <summary>
        /// Retrieve the parent object.
        /// </summary>
        /// <returns>The wrapper of the parent object</returns>
        public MapObjectHolder GetParent()
        {
            return parent;
        }

        /// <summary>
        /// Override the default GetType method by returning the underlying type.
        /// </summary>
        /// <returns>The MapScript based type of the containing object.</returns>
        public new Type GetType()
        {
            return target.GetType();
        }

        /// <summary>
        /// Override the equality method 
        /// </summary>
        /// <param name="obj">The object to test the equality against</param>
        /// <returns>true if the 2 objects hold the same object, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(MapObjectHolder))
                return obj.Equals(target);
            if (this.target.GetType() == obj.GetType())
                return this.target.Equals(obj);
            return false;
        }

        /// <summary>
        /// Returns the hashcode of the inner object
        /// </summary>
        /// <returns>The returned hashcode</returns>
        public override int GetHashCode()
        {
            return target.GetHashCode();
        }
    }
}
