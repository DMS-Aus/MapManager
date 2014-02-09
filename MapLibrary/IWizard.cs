using System;
using System.Collections.Generic;
using System.Text;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Common interface for the Wizard Controls.
    /// </summary>
    public interface IWizard
    {
        /// <summary>
        /// Selecting a new page of the Wizard.
        /// </summary>
        /// <param name="page">The page number to be selected.</param>
        void SelectPage(int page);

        /// <summary>
        /// Invoke the finish action of the Wizard.
        /// </summary>
        void Finish();

        /// <summary>
        /// Returns the page count of the Wizard.
        /// </summary>
        /// <returns>The total number of the pages.</returns>
        int GetPageCount();
    }
}
