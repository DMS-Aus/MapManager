using System;
using System.Drawing;
using System.Text;
using OSGeo.MapServer;
using System.Drawing.Design;
using System.ComponentModel;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a Windows picker box that displays Font values.
    /// </summary>
    public class FontsetPicker : PickerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FontsetPicker()
            : base(typeof(string))
        {
            Value = "";
            TextEditable = false;
            PaintValueFrame = false;
            PaintValueWidth = 40;
        }

        /// <summary>
        /// Set the fontset based on a MapScript fontSetObj object.
        /// </summary>
        /// <param name="fontset">The MapScript fontSetObj object.</param>
        public void SetFontset(fontSetObj fontset)
        {
            Editor = new FontsetEditor(fontset);
        }

        /// <summary>
        /// Value
        /// </summary>
        public new string Value
        {
            get
            {
                return (string)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }
    }

    /// <summary>
    /// The UITypeEditor of Fontset items
    /// </summary>
    internal class FontsetEditor : UITypeEditor
    {
        fontSetObj fontset;
        /// <summary>
        /// Constructor
        /// </summary>
        public FontsetEditor(fontSetObj fontset)
        {
            this.fontset = fontset;
        }

        /// <summary>
        /// Overloaded. Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>A UITypeEditorEditStyle value that indicates the style of editor used by the EditValue method.</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
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
                //DashStyle style = (DashStyle)e.Value;
                //Rectangle bounds = e.Bounds;

                //int y = bounds.Y + bounds.Height / 2;
                //Point start = new Point(bounds.Left, y);
                //Point end = new Point(bounds.Right, y);

                //Pen pen = new Pen(SystemColors.WindowText);
                //pen.DashStyle = style;
                //pen.Width = 2;
                //Brush brush = new SolidBrush(SystemColors.Window);
                //try
                //{
                //    GraphicsState state = e.Graphics.Save();
                //    e.Graphics.FillRectangle(brush, bounds);
                //    e.Graphics.DrawLine(pen, start, end);
                //    e.Graphics.Restore(state);
                //}
                //finally
                //{
                //    pen.Dispose();
                //    brush.Dispose();
                //}
            }
        }
    }
}
