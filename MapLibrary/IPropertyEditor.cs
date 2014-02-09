using System;
using System.Collections.Generic;
using System.Text;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Common interface for the Controls to edit the properties of a MapScript target object.
    /// </summary>
    public interface IPropertyEditor : IMapControl
    {
        /// <summary>
        /// Update the values of the target object according to the Editor controls state.
        /// </summary>
        void UpdateValues();

        /// <summary>
        /// Cancel the pending changes in the underlying object.
        /// </summary>
        void CancelEditing();

        /// <summary>
        /// Returns the modified state of the Editor.
        /// </summary>
        /// <returns>The current modified state.</returns>
        bool IsDirty();
    }
}
