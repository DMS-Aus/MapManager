using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Common form for hosting the various Wizard controls.
    /// </summary>
    public partial class ThemeWizardForm : Form
    {
        int pageIndex;
        IWizard wizard;
        MapObjectHolder target;

        /// <summary>
        /// Constructs a new ThemeWizardForm object.
        /// </summary>
        /// <param name="target">The object that this Wizard operates on.</param>
        public ThemeWizardForm(MapObjectHolder target)
        {
            InitializeComponent();

            this.target = target;

            ListViewItem item = new ListViewItem();
            item.Text = "Individual Values";
            IndividualValuesTheme theme = new IndividualValuesTheme();
            theme.Visible = false;
            item.Tag = theme;
            listViewThemes.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Value Ranges";
            RangeTheme theme2 = new RangeTheme();
            theme2.Visible = false;
            item.Tag = theme2;
            listViewThemes.Items.Add(item);

            buttonBack.Enabled = false;
            buttonNext.Enabled = false;
            pageIndex = 1;
        }

        /// <summary>
        /// Retrieve the IWizard interface of the current Wizard control.
        /// </summary>
        /// <returns>The IWizard interface of the current Wizard control.</returns>
        public IWizard GetWizard()
        {
            return this.wizard;
        }

        /// <summary>
        /// Initialize the Wizard by setting the default parameters.
        /// </summary>
        /// <param name="wizard">The Wizard control to be initialized.</param>
        private void InitializeWizard(Control wizard)
        {
            if (wizard == null)
            {
                this.Controls.RemoveAt(this.Controls.Count -1);
                panelDefault.Visible = true;
                this.wizard = null;
                this.Text = "Create Theme";

                buttonBack.Top = panelDefault.Bottom + 8;
                buttonNext.Top = panelDefault.Bottom + 8;
                buttonCancel.Top = panelDefault.Bottom + 8;
            }
            else
            {
                this.Controls.Add(wizard);

                wizard.Visible = true;

                wizard.Location = new System.Drawing.Point(3, 4);
                wizard.TabIndex = 0;

                buttonBack.Top = wizard.Bottom + 8;
                buttonNext.Top = wizard.Bottom + 8;
                buttonCancel.Top = wizard.Bottom + 8;

                this.wizard = (IWizard)wizard;
                ((IPropertyEditor)wizard).Target = target;

                this.Text = "Create Theme: Step " + pageIndex + " of " + Convert.ToString(this.wizard.GetPageCount() + 1);

                panelDefault.Visible = false;
            }

            this.Refresh();
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
        /// Click event handler for the buttonCancel object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click event handler for the buttonNext object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            ++pageIndex;
            if (wizard == null)
            {
                InitializeWizard((Control)listViewThemes.SelectedItems[0].Tag);
            }
            else
            {

                if (pageIndex == wizard.GetPageCount() + 2)
                {
                    wizard.Finish();
                    this.Close();
                }
                else
                {
                    if (pageIndex == wizard.GetPageCount() + 1)
                        buttonNext.Text = "Finish";
                    wizard.SelectPage(pageIndex - 1);
                }
            }

            this.Text = "Create Theme: Step " + pageIndex + " of " + Convert.ToString(this.wizard.GetPageCount() + 1);

            buttonBack.Enabled = true;
        }

        /// <summary>
        /// Click event handler for the buttonBack object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            --pageIndex;
            if (pageIndex == 1)
            {
                buttonBack.Enabled = false;

                InitializeWizard(null);
            }
            else
            {
                wizard.SelectPage(pageIndex - 1);
                this.Text = "Create Theme: Step " + pageIndex + " of " + Convert.ToString(this.wizard.GetPageCount() + 1);
            }
     
            buttonNext.Text = "Next >>";
        }

        /// <summary>
        /// SelectedIndexChanged event handler for the listViewThemes object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void listViewThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonNext.Enabled = true;
        }

        /// <summary>
        /// KeyDown event handler for the ThemeWizardForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ThemeWizardForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        
    }
}