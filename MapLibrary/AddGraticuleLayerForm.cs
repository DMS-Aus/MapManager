using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using OSGeo.MapServer;

namespace DMS.MapLibrary
{
    public partial class AddGraticuleLayerForm : Form
    {
        mapObj map;
        ProjectionBrowserDialog projDialog;

        string layerdef = @"LAYER
      NAME ""%name%""
      TYPE LINE
      STATUS ON
      CLASS
        NAME ""Graticule""
        STYLE
          COLOR %colorR% %colorG% %colorB%
          WIDTH %linewidth%
        END
        LABEL
          COLOR  %labelcolorR% %labelcolorG% %labelcolorB%
          FONT ""arial""
          TYPE truetype
          SIZE %labelsize%
          POSITION AUTO
          PARTIALS FALSE
          BUFFER 2
        END
      END
      PROJECTION
        ""%projection%""
      END
      GRID
        LABELFORMAT ""%labelformat%""
        %minarcs%
        %maxarcs%
        %mininterval%
        %maxinterval%
        %minsubdivide%
        %maxsubdivide%
      END
    END";

        /// <summary>
        /// Constructs the AddGraticuleLayerForm.
        /// </summary>
        public AddGraticuleLayerForm(mapObj map)
        {
            InitializeComponent();
            this.map = map;
        }

        /// <summary>
        /// Common function to validate the double values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateDouble(object sender, CancelEventArgs e)
        {
            double result;
            if (!double.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Common function to validate the double values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateDouble2(object sender, CancelEventArgs e)
        {
            double result;
            if (((TextBoxBase)sender).Text != "" && !double.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Click event handler of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // construct layer definition
            StringBuilder s = new StringBuilder(layerdef);

            s.Replace("%name%", MapUtils.GetUniqueLayerName(map, "grid", 0));
            
            s.Replace("%labelformat%", textBoxLabelFormat.Text);
            s.Replace("%labelcolorR%", colorPickerLabelColor.Value.R.ToString());
            s.Replace("%labelcolorG%", colorPickerLabelColor.Value.G.ToString());
            s.Replace("%labelcolorB%", colorPickerLabelColor.Value.B.ToString());
            s.Replace("%colorR%", colorPickerLineColor.Value.R.ToString());
            s.Replace("%colorG%", colorPickerLineColor.Value.G.ToString());
            s.Replace("%colorB%", colorPickerLineColor.Value.B.ToString());

            if (textBoxLabelSize.Text.Trim() != "")
                s.Replace("%labelsize%", textBoxLabelSize.Text.ToString());
            else
                s.Replace("%labelsize%", "8");

            if (textBoxLineWidth.Text.Trim() != "")
                s.Replace("%linewidth%", textBoxLineWidth.Text.ToString());
            else
                s.Replace("%linewidth%", "2");

            // optional parameters
            if (textBoxMinArcs.Text.Trim() != "")
                s.Replace("%minarcs%", "MINARCS " + textBoxMinArcs.Text.ToString());
            else
                s.Replace("%minarcs%", "");

            if (textBoxMaxArcs.Text.Trim() != "")
                s.Replace("%maxarcs%", "MAXARCS " + textBoxMaxArcs.Text.ToString());
            else
                s.Replace("%maxarcs%", "");

            if (textBoxMinInterval.Text.Trim() != "")
                s.Replace("%mininterval%", "MININTERVAL " + textBoxMinInterval.Text.ToString());
            else
                s.Replace("%mininterval%", "");

            if (textBoxMaxInterval.Text.Trim() != "")
                s.Replace("%maxinterval%", "MAXINTERVAL " + textBoxMaxInterval.Text.ToString());
            else
                s.Replace("%maxinterval%", "");

            if (textBoxMinSubdivide.Text.Trim() != "")
                s.Replace("%minsubdivide%", "MINSUBDIVIDE " + textBoxMinSubdivide.Text.ToString());
            else
                s.Replace("%minsubdivide%", "");

            if (textBoxMaxSubdivide.Text.Trim() != "")
                s.Replace("%maxsubdivide%", "MAXSUBDIVIDE " + textBoxMaxSubdivide.Text.ToString());
            else
                s.Replace("%maxsubdivide%", "");

            if (textBoxProjection.Tag != null)
                s.Replace("%projection%", textBoxProjection.Tag.ToString());
            else
                s.Replace("%projection%", "init=epsg:4326");

            // create layer
            layerObj layer = new layerObj(map);
            try
            {
                layer.updateFromString(s.ToString());

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                map.removeLayer(layer.index);
            }
        }

        /// <summary>
        /// Click event handler of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Click event handler of the buttonProjection control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonProjection_Click(object sender, EventArgs e)
        {
            if (projDialog == null)
                projDialog = new ProjectionBrowserDialog();

            projDialog.Projection = textBoxProjection.Text;
            if (projDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxProjection.Text = projDialog.Projection;
                textBoxProjection.Tag = projDialog.ProjectionNative;
            }
        }
    }
}
