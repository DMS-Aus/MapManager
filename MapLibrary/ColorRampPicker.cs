using System;
using System.Drawing;
using System.Text;
using OSGeo.MapServer;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;   

namespace DMS.MapLibrary
{
    public enum ColorRampStyle { Gradient, Discrete}
    
    [Serializable]
    public class ColorRampValueList : SortedList<double, Color>, IXmlSerializable
    {
        private string text;
        private ColorRampStyle style;

        public ColorRampValueList()
        {
            style = ColorRampStyle.Gradient;
        }

        public static ColorRampValueList Empty
        {
            get
            {
                ColorRampValueList valueList = new ColorRampValueList();
                valueList.Text = "Empty";
                return valueList; 
            }
        }

        public static ColorRampValueList Random
        {
            get
            {
                ColorRampValueList valueList = new ColorRampValueList();
                valueList.Add(0, Color.Red);
                valueList.Add(20, Color.Orange);
                valueList.Add(40, Color.Brown);
                valueList.Add(60, Color.Cyan);
                valueList.Add(80, Color.Magenta);
                valueList.Add(100, Color.Empty);
                valueList.Text = "Random values";
                valueList.Style = ColorRampStyle.Discrete;
                return valueList;
            }
        }
        
        public override string ToString()
        {
            return text;
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public ColorRampStyle Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
            }
        }

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            reader.ReadStartElement("Style");
            this.Style = (ColorRampStyle)Enum.Parse(typeof(ColorRampStyle), reader.ReadString());
            reader.ReadEndElement();

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("Item");

                reader.ReadStartElement("Offset");
                double key = Convert.ToDouble(reader.ReadString());
                reader.ReadEndElement();

                reader.ReadStartElement("Color");
                Color value = Color.FromArgb(Convert.ToInt32(reader.ReadString()));
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("Style");
            writer.WriteString(Style.ToString());
            writer.WriteEndElement();
            
            foreach (double key in this.Keys)
            {
                writer.WriteStartElement("Item");
                writer.WriteStartElement("Offset");
                writer.WriteValue(key);
                writer.WriteEndElement();

                writer.WriteStartElement("Color");
                Color value = this[key];
                writer.WriteString(value.ToArgb().ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
        #endregion
    }

    /// <summary>
    /// Represents a Windows picker box that displays a color ramp.
    /// </summary>
    public class ColorRampPicker : PickerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ColorRampPicker()
            : base(typeof(ColorRampValueList))
        {
            TextEditable = false;
            PaintValueFrame = false;
            PaintValueWidth = 40;
            Editor = new ColorRampEditor();
            Converter = new ColorRampConverter();
            Value = (ColorRampValueList)Converter.ConvertFrom("Random values");
        }

        /// <summary>
        /// Current Color Ramp Value
        /// </summary>
        public new ColorRampValueList Value
        {
            get
            {
                return (ColorRampValueList)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public void NewValue()
        {
            ColorRampForm dialog = new ColorRampForm((ColorRampEditor)this.Editor, null);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ColorRampConverter converter = (ColorRampConverter)this.Converter;
                converter.AddValue(dialog.Value);
                Value = dialog.Value;
            }
        }

        public void EditValue(string key)
        {
            ColorRampForm dialog = new ColorRampForm((ColorRampEditor)this.Editor, key);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Value = dialog.Value;
            }
        }

        public void DeleteValue(string key)
        {
            ColorRampConverter converter = (ColorRampConverter)this.Converter;
            converter.DeleteValue(key);
        }

        /// <summary>
        /// Get a listbox for displaying the colour ramps
        /// </summary>
        public ListBox GetListBox()
        {
            return new PickerListBox(this);
        }

        /// <summary>
        /// Update the listbox contents
        /// </summary>
        /// <param name="listBox">listbox object</param>
        public void UpdateListBox(ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (ColorRampValueList val in ColorRampConverter.ColorRampList.Values)
            {
                if (val.Text != "New color ramp ..." && val.Text != "Empty" && val.Text != "Random values")
                    listBox.Items.Add(val);
            }

            listBox.SelectedItem = Value;
        }

        /// <summary>
        /// Raises the OnValueChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnValueChanged(EventArgs e)
        {

            if (Value != null && Value.Text == "New color ramp ...")
            {
                NewValue();
            }
            base.OnValueChanged(e);
        }

        public Color GetInterpolatedColor(Color color1, Color color2, double percent)
        {
            int R = (int)(percent * (color2.R - color1.R) + color1.R);
            int G = (int)(percent * (color2.G - color1.G) + color1.G);
            int B = (int)(percent * (color2.B - color1.B) + color1.B);

            if (R < 0) R = 0;
            if (G < 0) G = 0;
            if (B < 0) B = 0;

            if (R > 255) R = 255;
            if (G > 255) G = 255;
            if (B > 255) B = 255;
            
            return Color.FromArgb(R, G, B);
        }

        public Color GetColorAtValue(double percent)
        {
            if (Value.Text == "Random values")
                return Color.FromArgb(MapUtils.GetRandomValue(256), 
                                      MapUtils.GetRandomValue(256), 
                                      MapUtils.GetRandomValue(256));

            // searching for the corresponding value
            int i;
            for (i = 0; i < Value.Count; i++)
            {
                if (Value.Keys[i] > percent)
                {
                    ++i;
                    break;
                }
            }

            // calculate color
            if (i > 0)
            {
                if (i < 2 || Value.Style == ColorRampStyle.Discrete)
                    return Value.Values[i - 1];
                else
                    return GetInterpolatedColor(Value.Values[i - 2], Value.Values[i - 1], percent / 100);
            }
            else
                return Color.Empty;
        }

        public colorObj GetMapColorAtValue(double percent)
        {
            Color source = GetColorAtValue(percent);
            if (source == Color.Empty)
            {
                return new colorObj(-1, -1, -1, 255);
            }
            else
            {
                return new colorObj(source.R, source.G, source.B, 255);
            }
        }
    }

    [Serializable]
    public class ColorRampList : Dictionary<string, ColorRampValueList> , IXmlSerializable
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(ColorRampValueList));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("ColorRamp");

                reader.ReadStartElement("Name");
                string key = reader.ReadString();
                reader.ReadEndElement();

                ColorRampValueList value = (ColorRampValueList)valueSerializer.Deserialize(reader);
                value.Text = key;
                
                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();

            // add extended elements
            ColorRampValueList valueList;
            if (!this.ContainsKey("Random values"))
            {
                this.Add("Random values", ColorRampValueList.Random);
            }
            if (!this.ContainsKey("Empty"))
            {
                this.Add("Empty", ColorRampValueList.Empty);
            }
            if (!this.ContainsKey("New color ramp ..."))
            {
                valueList = new ColorRampValueList();
                valueList.Text = "New color ramp ...";
                this.Add(valueList.Text, valueList);
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(ColorRampValueList));

            foreach (string key in this.Keys)
            {
                if (key != "New color ramp ..." && key != "Empty" && key != "Random values")
                {
                    writer.WriteStartElement("ColorRamp");

                    writer.WriteStartElement("Name");
                    writer.WriteString(key);
                    writer.WriteEndElement();

                    ColorRampValueList value = this[key];
                    valueSerializer.Serialize(writer, value);

                    writer.WriteEndElement();
                }
            }
        }
        #endregion
    }

    public class ColorRampConverter : TypeConverter
    {
        private static ColorRampList colorRampList;

        public void AddValue(ColorRampValueList  value)
        {
            if (!colorRampList.ContainsKey(value.Text))
                colorRampList.Add(value.Text, value);
        }

        public void DeleteValue(string key)
        {
            if (colorRampList.ContainsKey(key))
                colorRampList.Remove(key);
        }

        public static ColorRampList ColorRampList
        {
            get
            {
                return colorRampList;
            }

            set
            {
                colorRampList = value;
            }
        }

        static ColorRampConverter()
        {
            // populate
            colorRampList = new ColorRampList();
            ColorRampValueList valueList = new ColorRampValueList();
            valueList.Add(0, Color.Red);
            valueList.Add(50, Color.Blue);
            valueList.Add(100, Color.Green);
            valueList.Text = "RedBlueGreen";
            colorRampList.Add(valueList.Text, valueList);
            valueList = new ColorRampValueList();
            valueList.Add(0, Color.Red);
            valueList.Add(100, Color.Orange);
            valueList.Text = "RedOrange";
            colorRampList.Add(valueList.Text, valueList);

            // add extended elements
            valueList = new ColorRampValueList();
            valueList.Add(0, Color.Red);
            valueList.Add(20, Color.Orange);
            valueList.Add(40, Color.Brown);
            valueList.Add(60, Color.Cyan);
            valueList.Add(80, Color.Magenta);
            valueList.Add(100, Color.Empty);
            valueList.Text = "Random values";
            valueList.Style = ColorRampStyle.Discrete;
            colorRampList.Add(valueList.Text, valueList);
            valueList = new ColorRampValueList();
            valueList.Text = "Empty";
            colorRampList.Add(valueList.Text, valueList);
            valueList = new ColorRampValueList();
            valueList.Text = "New color ramp ...";
            colorRampList.Add(valueList.Text, valueList);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(colorRampList.Values);
        }

        // Overrides the CanConvertFrom method of TypeConverter.
        // The ITypeDescriptorContext interface provides the context for the
        // conversion. Typically, this interface is used at design time to 
        // provide information about the design-time container.
        public override bool CanConvertFrom(ITypeDescriptorContext context,
           Type sourceType)
        {

            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        // Overrides the ConvertFrom method of TypeConverter.
        public override object ConvertFrom(ITypeDescriptorContext context,
           CultureInfo culture, object value)
        {
            if (value is string)
            {
                if (colorRampList.ContainsKey((string)value))
                    return colorRampList[(string)value];
            }
            return null;
        }
        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context,
           CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((ColorRampValueList)value).Text;
            }
            return null;
        }
    }

    /// <summary>
    /// The UITypeEditor of Color items
    /// </summary>
    internal class ColorRampEditor : UITypeEditor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ColorRampEditor()
        {
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
                ColorRampValueList valueList = (ColorRampValueList)e.Value;

                if (valueList.Count > 0)
                {
                    for (int i = 1; i < valueList.Count; i++)
                    {
                        Rectangle rect = new Rectangle(
                            e.Bounds.X + (int)(valueList.Keys[i - 1] * e.Bounds.Width / 100), e.Bounds.Y,
                            (int)((valueList.Keys[i] - valueList.Keys[i - 1]) * e.Bounds.Width / 100), 
                                     e.Bounds.Height);
                        if (valueList.Style == ColorRampStyle.Discrete)
                        {
                            using (Brush brush = new SolidBrush(valueList.Values[i - 1]))
                            {
                                e.Graphics.FillRectangle(brush, rect);
                            }
                        }
                        else
                        {
                            using (Brush brush =
                                     new LinearGradientBrush(rect, valueList.Values[i - 1], valueList.Values[i],
                                         0.0))
                            {
                                e.Graphics.FillRectangle(brush, rect);
                            }
                        }
                    }
                    
                }
            }
        }
    }
}
