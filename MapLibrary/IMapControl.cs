using System;
using System.Collections.Generic;
using System.Text;

namespace DMS.MapLibrary
{
    /// <summary>
    /// The signature of the EditProperties event handler. Raised when a MapScript object should be edited by the corresponding editor UserControl.
    /// </summary>
    public delegate void EditPropertiesEventHandler(object sender, MapObjectHolder target);

    /// <summary>
    /// Common interface for the Controls to control the parameters of a MapScript target object.
    /// </summary>
    public interface IMapControl
    {
        /// <summary>
        /// Refresh the View of the Control according to the state of the target object.
        /// </summary>
        void RefreshView();

        /// <summary>
        /// Gets and sets the target object to be controlled
        /// </summary>
        MapObjectHolder Target
        {
            get;
            set;
        }

        /// <summary>
        /// The EditProperties event handler. Raised when a MapScript object should be edited by the corresponding editor UserControl.
        /// </summary>
        event EditPropertiesEventHandler EditProperties;
    }
}
