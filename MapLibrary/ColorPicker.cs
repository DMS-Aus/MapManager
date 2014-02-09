using System;
using System.Drawing;
using System.Text;
using OSGeo.MapServer;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel;
using System.Globalization;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a Windows picker box that displays Color values.
    /// </summary>
    public class ColorPicker : PickerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ColorPicker()
            : base(typeof(Color))
        {
            Value = Color.White;
            // setting up the modal dialog editor (#4430)
            Editor = new ColorDialogEditor();
        }

        /// <summary>
        /// Value
        /// </summary>
        public new Color Value
        {
            get
            {
                return (Color)base.Value;   
            }
            set
            {
                base.Value = value;
            }
        }

        /// <summary>
        /// Applies the selected color to the given object
        /// </summary>
        /// <param name="color">The object should be updated.</param>
        public void ApplyTo(colorObj color)
        {
            DrawingColor2MapColor(color, Value);
        }

        /// <summary>
        /// Sets the value of the control according to the given object
        /// </summary>
        /// <param name="color">The object representing the color values to be set.</param>
        public void SetColor(colorObj color)
        {
            Value = MapColor2DrawingColor(color);
        }

        /// <summary>
        /// Converts from MapScript color to System.Drawing.Color
        /// </summary>
        /// <param name="color">The object representing the color values to be converted.</param>
        /// <returns>The converted value</returns>
        public static Color MapColor2DrawingColor(colorObj color)
        {
            if (color.red >= 0 && color.green >= 0 && color.blue >= 0)
            {
                return Color.FromArgb(color.alpha, color.red, color.green, color.blue);
            }
            else return Color.Empty;
        }

        /// <summary>
        /// Converts from System.Drawing.Color to MapScript color.
        /// </summary>
        /// <param name="color">The object representing the destination object.</param>
        /// <param name="source">The value to be converted from</param>
        public static void DrawingColor2MapColor(colorObj color, Color source)
        {
            if (source == Color.Empty)
            {
                color.red = -1;
                color.green = -1;
                color.blue = -1;
            }
            else
            {
                color.red = source.R;
                color.green = source.G;
                color.blue = source.B;
                color.alpha = source.A;
            }
        }
    }

    /// <summary>
    /// The UITypeEditor of Color items (for showing the modal dialog box)
    /// </summary>
    internal class ColorDialogEditor : UITypeEditor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ColorDialogEditor()
        {
        }

        /// <summary>
        /// Overloaded. Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>A UITypeEditorEditStyle value that indicates the style of editor used by the EditValue method.</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Indicates whether the specified context supports painting a representation of an object's value within the specified context.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>true if PaintValue is implemented; otherwise, false.</returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Paints a representation of the value of an object using the specified PaintValueEventArgs.
        /// </summary>
        /// <param name="e">A PaintValueEventArgs that indicates what to paint and where to paint it.</param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value != null)
            {
                using (Brush brush = new SolidBrush((Color)e.Value))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
        }

        /// <summary>
        /// Showing the modal color dialog box
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = (Color)value;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.Color;
            }
            return value;
        }
    }
}
