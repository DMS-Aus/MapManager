using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.Design;
using System.Reflection;
using System.Globalization;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a base class of a Windows Picker control that allows you to edit a value of any type. 
    /// </summary>
    public abstract class PickerBase : ContainerControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The Type of object that can be edited by this control.</param>
        protected PickerBase(Type type)
        {
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.FixedHeight, true);
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            base.ResizeRedraw = true;

            paintValueOnly = false;
            paintValueFrame = true;
            paintValueWidth = 20;
            textEditable = true;
            autoCompletion = true;

            dropDownCommit = false;
            editingText = false;

            editedType = type;
            typeEditor = null;
            typeConverter = null;
            typeContext = null;
            parseMethod = null;
            editedValue = null;

            base.SuspendLayout();
            base.CausesValidation = true;

            pickerButton = new PickerButton(this);
            pickerValueBox = new PickerValueBox(this);
            pickerTextBox = new TextBox();
            pickerListBox = new PickerListBox(this);
            pickerService = new PickerService(this);

            // pickerButton
            pickerButton.Visible = true;
            pickerButton.Cursor = Cursors.Default;
            pickerButton.MouseDown += new MouseEventHandler(pickerButton_MouseDown);
            pickerButton.MouseUp += new MouseEventHandler(pickerButton_MouseUp);

            // pickerValueBox
            pickerValueBox.Visible = false;
            pickerValueBox.Cursor = Cursors.Default;
            pickerValueBox.MouseDown += new MouseEventHandler(pickerValueBox_MouseDown);

            // pickerTextBox
            pickerTextBox.AcceptsReturn = false;
            pickerTextBox.AcceptsTab = false;
            pickerTextBox.CausesValidation = false;
            pickerTextBox.BorderStyle = BorderStyle.None;
            pickerTextBox.KeyDown += new KeyEventHandler(pickerTextBox_KeyDown);
            pickerTextBox.KeyPress += new KeyPressEventHandler(pickerTextBox_KeyPress);
            pickerTextBox.KeyUp += new KeyEventHandler(pickerTextBox_KeyUp);
            pickerTextBox.TextChanged += new EventHandler(pickerTextBox_TextChanged);
            pickerTextBox.GotFocus += new EventHandler(pickerTextBox_GotFocus);
            pickerTextBox.LostFocus += new EventHandler(pickerTextBox_LostFocus);
            pickerTextBox.Validating += new CancelEventHandler(pickerTextBox_Validating);
            pickerTextBox.Validated += new EventHandler(pickerTextBox_Validated);

            // pickerListBox
            pickerListBox.Visible = true;
            pickerListBox.MouseUp += new MouseEventHandler(pickerListBox_MouseUp);
            pickerListBox.SelectedIndexChanged += new EventHandler(pickerListBox_SelectedIndexChanged);

            // pickerBase
            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;

            base.Controls.Add(pickerButton);
            base.Controls.Add(pickerValueBox);
            base.Controls.Add(pickerTextBox);
            base.ResumeLayout();
        }

        private Type editedType;
        /// <summary>
        /// Gets or sets the Type this control can edit.
        /// </summary>
        protected Type EditedType
        {
            get
            {
                return editedType;
            }
            set
            {
                if (editedType != value)
                {
                    editedType = value;
                    typeEditor = null;
                    typeConverter = null;
                    parseMethod = null;
                    LayoutControls();
                    base.Invalidate(true);
                }
            }
        }

        private UITypeEditor typeEditor;
        /// <summary>
        /// Gets or sets the type editor for this control.
        /// </summary>
        protected UITypeEditor Editor
        {
            get
            {
                if (typeEditor == null)
                {
                    typeEditor = DefaultEditor;
                }
                return typeEditor;
            }
            set
            {
                if (typeEditor != value)
                {
                    typeEditor = value;
                }
            }
        }

        /// <summary>
        /// Gets the default type editor for this control.
        /// </summary>
        protected UITypeEditor DefaultEditor
        {
            get
            {
                return (UITypeEditor)TypeDescriptor.GetEditor(editedType, typeof(UITypeEditor));
            }
        }

        private TypeConverter typeConverter;
        /// <summary>
        /// Gets or sets the type editor for this control.
        /// </summary>
        protected TypeConverter Converter
        {
            get
            {
                if (typeConverter == null)
                {
                    typeConverter = DefaultConverter;
                }
                return typeConverter;
            }
            set
            {
                if (typeConverter != value)
                {
                    typeConverter = value;
                }
            }
        }

        /// <summary>
        /// Gets the default type editor for this control.
        /// </summary>
        protected TypeConverter DefaultConverter
        {
            get
            {
                return TypeDescriptor.GetConverter(editedType);
            }
        }

        private MethodInfo parseMethod;
        /// <summary>
        /// Gets or sets the type parse method for this control.
        /// </summary>
        protected MethodInfo ParseMethod
        {
            get
            {
                if (parseMethod == null)
                {
                    Type[] array = new Type[] { typeof(string), typeof(IFormatProvider) };
                    parseMethod = EditedType.GetMethod("Parse", array);
                }
                return parseMethod;
            }
            set
            {
                if (parseMethod != value)
                {
                    parseMethod = value;
                }
            }
        }

        private ITypeDescriptorContext typeContext;
        /// <summary>
        /// Gets or sets the context that will be used to convert the edited value using type converters.
        /// </summary>
        public ITypeDescriptorContext Context
        {
            get
            {
                return typeContext;
            }
            set
            {
                if ((typeContext == null) || !typeContext.Equals(value))
                {
                    typeContext = value;
                }
            }
        }

        private bool autoCompletion;
        /// <summary>
        /// Gets or sets a value indicating whether to auto completion.
        /// </summary>
        protected bool AutoCompletion
        {
            get
            {
                return autoCompletion;
            }
            set
            {
                if (autoCompletion != value)
                {
                    autoCompletion = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to support paint value.
        /// </summary>
        protected bool PaintValueSupported
        {
            get
            {
                if (Editor != null)
                {
                    return Editor.GetPaintValueSupported();
                }

                return false;
            }
        }

        private bool textEditable;
        /// <summary>
        /// Gets or sets a value indicating whether can edit text.
        /// </summary>
        protected bool TextEditable
        {
            get
            {
                return textEditable;
            }
            set
            {
                if (textEditable != value)
                {
                    textEditable = value;
                }
            }
        }

        private bool paintValueOnly;
        /// <summary>
        /// Gets or sets a value indicating whether to show only the rectangle that displays a representation of the edited value. 
        /// </summary>
        protected bool PaintValueOnly
        {
            get
            {
                return paintValueOnly;
            }
            set
            {
                if (paintValueOnly != value)
                {
                    paintValueOnly = value;
                    LayoutControls();
                    base.Invalidate(true);
                }
            }
        }

        private bool paintValueFrame;
        /// <summary>
        /// Gets or sets a value indicating whether a frame around the area that previews the edited value is displayed or not.
        /// </summary>
        protected bool PaintValueFrame
        {
            get
            {
                return paintValueFrame;
            }
            set
            {
                if (paintValueFrame != value)
                {
                    paintValueFrame = value;
                }
            }
        }

        private int paintValueWidth;
        /// <summary>
        /// Gets or sets the width of the value painter.
        /// </summary>
        protected int PaintValueWidth
        {
            get
            {
                return paintValueWidth;
            }
            set
            {
                if (paintValueWidth != value)
                {
                    paintValueWidth = value;
                }
            }
        }

        private object editedValue;
        /// <summary>
        /// Gets or sets the value edited by the control. 
        /// </summary>
        public object Value
        {
            get
            {
                return editedValue;
            }
            set
            {
                SetValue(value);
            }
        }

        /// <summary>
        /// Gets or sets the prederred height of the control. 
        /// </summary>
        public int PreferredHeight
        {
            get
            {
                return base.FontHeight + SystemInformation.BorderSize.Height * 4 + 3;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether text in the text box is read-only.
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return pickerTextBox.ReadOnly;
            }
            set
            {
                if (ReadOnly != value)
                {
                    pickerTextBox.ReadOnly = value;
                    pickerValueBox.Enabled = !value;
                    pickerButton.Enabled = !value;
                    base.Invalidate(true);
                    OnReadOnlyChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        public override string Text
        {
            get
            {
                return pickerTextBox.Text;
            }
            set
            {
                if (Text != value)
                {
                    CommitText(value);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the control has input focus.
        /// </summary>
        public override bool Focused
        {
            get
            {
                return base.ContainsFocus;
            }
        }

        /// <summary>
        /// Occurs when the ReadOnly property is changed on the control.
        /// </summary>
        public event EventHandler ReadOnlyChanged;
        /// <summary>
        /// Occurs when the Value property is changed on the control. 
        /// </summary>
        public event EventHandler ValueChanged;

        private PickerButton pickerButton;
        private PickerValueBox pickerValueBox;
        private TextBox pickerTextBox;
        private PickerListBox pickerListBox;
        private PickerService pickerService;

        private bool dropDownCommit;
        private bool editingText;

        private void LayoutControls()
        {
            // Get Inner Client Rectangle
            Rectangle inner = base.ClientRectangle;
            inner.Inflate(-2, -2);

            int buttonwidth = SystemInformation.VerticalScrollBarWidth; //17
            int split = 4;

            pickerButton.SetBounds(inner.Right - buttonwidth, inner.Y, buttonwidth, inner.Height);
            pickerButton.DropDown = IsDropDown();

            Rectangle editor = inner;
            editor.Inflate(-1, -1);

            if (PaintValueSupported)
            {
                if (PaintValueOnly)
                {
                    pickerValueBox.SetBounds(editor.X, editor.Y, editor.Width, editor.Height);

                    pickerValueBox.Visible = true;
                    pickerTextBox.Visible = false;
                }
                else
                {
                    pickerValueBox.SetBounds(editor.X, editor.Y, PaintValueWidth, editor.Height);
                    pickerTextBox.SetBounds(editor.X + PaintValueWidth + split, editor.Y, editor.Width - PaintValueWidth - split, editor.Height);

                    pickerValueBox.Visible = true;
                    pickerTextBox.Visible = true;
                }
            }
            else
            {
                pickerTextBox.SetBounds(editor.X, editor.Y, editor.Width, editor.Height);

                pickerValueBox.Visible = false;
                pickerTextBox.Visible = true;
            }
        }
        private void AdjustHeight()
        {
            base.Height = PreferredHeight;
        }
        private bool CommitText(string text)
        {
            object value = null;
            try
            {
                if (Converter != null && Converter.CanConvertFrom(typeContext, typeof(string)))
                {
                    value = Converter.ConvertFromString(typeContext, CultureInfo.CurrentUICulture, text);
                }

                MethodInfo parse = ParseMethod;
                if ((value == null) && (parse != null))
                {
                    value = parse.Invoke(null, new object[] { text });
                }
            }
            catch
            {
            }

            if (value == null)
            {
                return false;
            }
            return CommitValue(value);
        }
        private bool CommitValue(object value)
        {
            try
            {
                pickerService.CloseDropDown();
                SetValue(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void CommitList()
        {
            object item = pickerListBox.SelectedItem;
            dropDownCommit = false;
            if (item != null)
            {
                CommitValue(item);
            }
        }
        private bool CanTextEditable()
        {
            if (TextEditable && (!PaintValueOnly || !PaintValueSupported))
            {
                if (Converter != null)
                {
                    return Converter.CanConvertFrom(typeContext, typeof(string));
                }
            }
            return false;
        }
        private bool IsEnumerable()
        {
            if (Converter != null && Converter.GetStandardValuesSupported(typeContext) && (Converter.GetStandardValues(typeContext).Count != 0))
            {
                return true;
            }
            return false;
        }
        private bool IsDropDown()
        {
            if (Editor != null)
            {
                return Editor.GetEditStyle() != UITypeEditorEditStyle.Modal;
            }
            return true;
        }
        private object[] GetValueList()
        {
            object[] values = null;
            if (Converter.GetStandardValuesSupported(typeContext))
            {
                ICollection collection = Converter.GetStandardValues(typeContext);
                values = new object[collection.Count];
                collection.CopyTo(values, 0);
            }
            return values;
        }
        private string GetValueAsText(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            if (value is string)
            {
                return value.ToString();
            }
            try
            {
                if (Converter != null && Converter.CanConvertTo(typeContext, typeof(string)))
                {
                    return Converter.ConvertToString(typeContext, CultureInfo.CurrentUICulture, value);
                }
            }
            catch
            {
            }

            return value.ToString();
        }
        private bool UpdateTextWithValue()
        {
            string text = GetValueAsText(Value);
            if (text == null)
            {
                pickerTextBox.Text = string.Empty;
                return false;
            }
            if (pickerTextBox.Text != text)
            {
                pickerTextBox.Text = text;
                return true;
            }
            return false;
        }
        private void SetValue(object value)
        {
            if (value != null)
            {
                if (Convert.IsDBNull(value))
                {
                    value = null;
                }
                if (value != null && !EditedType.IsAssignableFrom(value.GetType()))
                {
                    throw new InvalidCastException("PickerBase.Value : Bad value type."
                        + EditedType.ToString() + " - " + value.GetType().ToString() );
                }
            }
            if (editedValue == value || (editedValue != null && value != null && editedValue.Equals(value)))
            {
                UpdateTextWithValue();
            }
            else
            {
                editedValue = value;
                if (UpdateTextWithValue())
                {
                    pickerTextBox.SelectionStart = 0;
                    pickerTextBox.SelectionLength = 0;
                }

                if (PaintValueSupported)
                {
                    base.Invalidate(true);
                }
                OnValueChanged(new EventArgs());
            }
        }
        public virtual void DoDropDown()
        {
            if ((Editor == null) || (Editor.GetEditStyle() == UITypeEditorEditStyle.None))
            {
                if (!IsEnumerable())
                {
                    return;
                }

                object[] array = GetValueList();
                pickerListBox.Items.Clear();
                using (Graphics graphics = base.CreateGraphics())
                {
                    int width = 0;
                    Font font = pickerListBox.Font;
                    foreach (object item in array)
                    {
                        if (!pickerListBox.Items.Contains(item))
                        {
                            string text = GetValueAsText(item);
                            if (!PaintValueOnly)
                            {
                                SizeF size = graphics.MeasureString(text, font);
                                width = (int)Math.Max((float)width, size.Width);
                            }
                            pickerListBox.Items.Add(item);
                        }
                    }
                    if (this.PaintValueSupported)
                    {
                        width += 24;
                    }

                    Rectangle bound = base.Bounds;
                    pickerListBox.SelectedItem = Value;
                    pickerListBox.Height = (int)Math.Max(font.GetHeight() + 2f, (float)Math.Min(200, pickerListBox.PreferredHeight));
                    pickerListBox.Width = Math.Max(width, bound.Width - 2);
                    dropDownCommit = false;
                    pickerService.DropDownControl(pickerListBox);
                    return;
                }
            }

            try
            {
                object value = Editor.EditValue(pickerService, Value);
                CommitValue(value);
            }
            catch
            {
            }
        }
        private bool ProcessEditorKey(Keys key)
        {
            if (!ReadOnly)
            {
                if (key == Keys.Delete)
                {
                    return !CanTextEditable();
                }

                bool alt = (key & Keys.Alt) != Keys.None;
                Keys data = key & Keys.KeyCode;
                if (key == Keys.F4 || (alt && data == Keys.Down))
                {
                    DoDropDown();
                    return true;
                }

                if (!alt && (data == Keys.Down || data == Keys.Up))
                {
                    if (IsEnumerable())
                    {
                        SelectEnumerableValue(data != Keys.Down);
                        return true;
                    }
                }
            }
            return false;
        }
        private void SelectEnumerableValue(bool next)
        {
            if (!IsEnumerable())
            {
                return;
            }

            int index;
            object[] array = GetValueList();
            index = next ? (array.Length - 1) : 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(Value))
                {
                    if (next)
                    {
                        if (i == 0)
                        {
                            return;
                        }
                        index = i - 1;
                        break;
                    }
                    if (i == (array.Length - 1))
                    {
                        return;
                    }
                    index = i + 1;
                    break;
                }
            }
            CommitValue(array[index]);
            pickerTextBox.SelectAll();
        }
        private void PaintBorder(Graphics g)
        {
            if (VisualStyleRenderer.IsSupported)
            {
                ComboBoxRenderer.DrawTextBox(g, base.ClientRectangle, ComboBoxState.Normal);
                Rectangle bound = base.ClientRectangle;
                bound.Inflate(-1, -1);
                using (Brush brush = new SolidBrush(BackColor))
                {
                    g.FillRectangle(brush, bound);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(BackColor))
                {
                    g.FillRectangle(brush, base.ClientRectangle);
                }
                ControlPaint.DrawBorder3D(g, base.ClientRectangle, Border3DStyle.Sunken);
            }
        }

        private void pickerButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && pickerButton.DropDown)
            {
                base.Focus();
                DoDropDown();
            }
        }
        private void pickerButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !pickerButton.DropDown)
            {
                base.Focus();
                DoDropDown();
            }
        }

        private void pickerValueBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                base.Focus();
                if (!CanTextEditable())
                {
                    DoDropDown();
                }
            }
        }

        private void pickerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }
        private void pickerTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
            editingText = false;
        }
        private void pickerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            editingText = (e.KeyChar != '\x001b') && (e.KeyChar != '\b');
            OnKeyPress(e);
        }
        private void pickerTextBox_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
            if (AutoCompletion && editingText && CanTextEditable())
            {
                editingText = false;
                if (Converter != null && Converter.GetStandardValuesSupported(typeContext))
                {
                    string text = Text.ToUpper();
                    if (text.Length != 0)
                    {
                        ICollection collection = Converter.GetStandardValues(typeContext);
                        foreach (object item in collection)
                        {
                            string itemtext = Converter.ConvertToString(typeContext, CultureInfo.CurrentUICulture, item);
                            if (itemtext.ToUpper().StartsWith(text))
                            {
                                pickerTextBox.Text = itemtext;
                                pickerTextBox.Select(text.Length, itemtext.Length - text.Length);
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void pickerTextBox_GotFocus(object sender, EventArgs e)
        {
            base.Invalidate(true);
        }
        private void pickerTextBox_LostFocus(object sender, EventArgs e)
        {
            base.Invalidate(true);
        }
        private void pickerTextBox_Validated(object sender, EventArgs e)
        {
        }
        private void pickerTextBox_Validating(object sender, CancelEventArgs e)
        {
        }

        private void pickerListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CommitList();
            }
        }
        private void pickerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropDownCommit = true;
        }

        /// <summary>
        /// Determines whether the specified key is a regular input key or a special key that requires preprocessing.
        /// </summary>
        /// <param name="key">One of the Keys values.</param>
        /// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
        protected override bool IsInputKey(Keys key)
        {
            if (key == Keys.Delete)
            {
                return true;
            }
            return base.IsInputKey(key);
        }
        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="key">One of the Keys values that represents the key to process.</param>
        /// <returns>true if the key was processed by the control; otherwise, false.</returns>
        protected override bool ProcessDialogKey(Keys key)
        {
            if (!ProcessEditorKey(key))
            {
                return base.ProcessDialogKey(key);
            }
            return true;
        }
        /// <summary>
        /// Performs the work of scaling the entire control and any child controls.
        /// </summary>
        /// <param name="dx">The ratio by which to scale the control horizontally.</param>
        /// <param name="dy">The ratio by which to scale the control vertically.</param>
        protected override void ScaleCore(float dx, float dy)
        {
            base.ScaleCore(dx, dy);
            LayoutControls();
        }
        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new Left property value of the control.</param>
        /// <param name="y">The new Right property value of the control.</param>
        /// <param name="width">The new Width property value of the control.</param>
        /// <param name="height">The new Height property value of the control.</param>
        /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (height != base.Height)
            {
                height = PreferredHeight;
            }
            base.SetBoundsCore(x, y, width, height, specified);
            LayoutControls();
        }
        /// <summary>
        /// Raises the BackColorChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            pickerTextBox.BackColor = BackColor;
            base.OnBackColorChanged(e);
        }
        /// <summary>
        /// Raises the CursorChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnCursorChanged(EventArgs e)
        {
            base.OnCursorChanged(e);
            pickerTextBox.Cursor = Cursor;
        }
        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            pickerTextBox.Enabled = base.Enabled;
            pickerButton.Enabled = base.Enabled;
        }
        /// <summary>
        /// Raises the Enter event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            base.Invalidate(true);
        }
        /// <summary>
        /// Raises the FontChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            pickerTextBox.Font = Font;
            base.OnFontChanged(e);
            AdjustHeight();
        }
        /// <summary>
        /// Raises the ForeColorChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            pickerTextBox.ForeColor = ForeColor;
            base.OnForeColorChanged(e);
        }
        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (pickerTextBox.Visible)
            {
                pickerTextBox.Focus();
            }
        }
        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AdjustHeight();
            LayoutControls();
        }
        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (ProcessEditorKey(e.KeyData))
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!CanTextEditable())
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }
        /// <summary>
        /// Raises the Leave event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnLeave(EventArgs e)
        {
            pickerService.HideHolder();
            base.OnLeave(e);
            base.Invalidate(true);
        }
        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        /// <summary>
        /// Raises the MouseWheel event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.None)
            {
                if (Focused && !ReadOnly)
                {
                    if (IsEnumerable())
                    {
                        SelectEnumerableValue(e.Delta > 0);
                    }
                }
                base.OnMouseWheel(e);
            }
        }
        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintBorder(e.Graphics);
        }
        /// <summary>
        /// Raises the SystemColorsChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            pickerService.SystemColorsChanged();
        }
        /// <summary>
        /// Raises the Validating event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            pickerService.HideHolder();
            if (!CommitText(pickerTextBox.Text))
            {
                e.Cancel = true;
            }
            base.OnValidating(e);
        }

        /// <summary>
        /// Raises the ReadOnlyChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            if (ReadOnlyChanged != null)
            {
                ReadOnlyChanged(this, e);
            }
        }
        /// <summary>
        /// Raises the OnValueChanged event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        #region PickerButton
        private class PickerButton : Button
        {
            public PickerButton(PickerBase parent)
            {
                parentControl = parent;
                isDropDown = false;
                buttonHot = false;
                buttonPressed = false;

                BackColor = SystemColors.Control;
                ForeColor = SystemColors.ControlText;

                base.TabStop = false;
                base.IsDefault = false;

                base.SetStyle(ControlStyles.Selectable, false);
            }

            private PickerBase parentControl;
            private bool buttonPressed;
            private bool buttonHot;
            private bool isDropDown;

            public bool DropDown
            {
                get
                {
                    return isDropDown;
                }
                set
                {
                    isDropDown = value;
                    base.Invalidate();
                }
            }

            protected override void OnGotFocus(EventArgs e)
            {
                base.OnGotFocus(e);
                parentControl.Invalidate(true);
            }
            protected override void OnLostFocus(EventArgs e)
            {
                base.OnLostFocus(e);
                parentControl.Invalidate(true);
            }
            protected override void OnMouseDown(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    buttonPressed = true;
                    base.Invalidate();
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                if (e.Button == MouseButtons.Left)
                {
                    buttonPressed = false;
                    base.Invalidate();
                }
            }
            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                buttonHot = true;
                base.Invalidate();
            }
            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                buttonHot = false;
                base.Invalidate();
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Rectangle bounds = base.ClientRectangle;

                if (isDropDown)
                {
                    if (VisualStyleRenderer.IsSupported)
                    {
                        ComboBoxState state = base.Enabled ? (buttonPressed ? ComboBoxState.Pressed : (buttonHot ? ComboBoxState.Hot : ComboBoxState.Normal)) : ComboBoxState.Disabled;
                        ComboBoxRenderer.DrawDropDownButton(g, bounds, state);
                    }
                    else
                    {
                        ButtonState state = base.Enabled ? (buttonPressed ? ButtonState.Pushed : ButtonState.Normal) : ButtonState.Inactive;
                        ControlPaint.DrawComboButton(g, bounds, state);
                    }
                }
                else
                {
                    if (VisualStyleRenderer.IsSupported)
                    {
                        ButtonRenderer.DrawParentBackground(g, bounds, this);
                        PushButtonState state = base.Enabled ? (buttonPressed ? PushButtonState.Pressed : (buttonHot ? PushButtonState.Hot : PushButtonState.Normal)) : PushButtonState.Disabled;
                        ButtonRenderer.DrawButton(g, bounds, state);
                    }
                    else
                    {
                        base.OnPaint(e);
                    }

                    int x = bounds.Left + bounds.Width / 2 - 4;
                    int y = bounds.Bottom - 5;
                    using (Brush brush = new SolidBrush(base.Enabled ? SystemColors.ControlText : SystemColors.GrayText))
                    {
                        g.FillRectangle(brush, x, y, 1, 2);
                        g.FillRectangle(brush, x + 4, y, 1, 2);
                        g.FillRectangle(brush, x + 8, y, 1, 2);
                    }
                }
            }
            protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
        }
        #endregion

        #region PickerValueBox
        private class PickerValueBox : Control
        {
            public PickerValueBox(PickerBase parent)
            {
                parentControl = parent;

                base.SetStyle(ControlStyles.Selectable, false);
            }

            private PickerBase parentControl;

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Rectangle bounds = base.ClientRectangle;

                using (Brush brush = new SolidBrush(parentControl.BackColor))
                {
                    g.FillRectangle(brush, bounds);
                }

                parentControl.Editor.PaintValue(parentControl.Value, g, bounds);

                if (parentControl.PaintValueFrame)
                {
                    using (Pen pen = new Pen(parentControl.ForeColor))
                    {
                        g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
                    }
                }

            }
        }
        #endregion

        #region PickerService
        private class PickerService : IServiceProvider, IWindowsFormsEditorService
        {
            public PickerService(PickerBase parent)
            {
                parentControl = parent;
            }

            private PickerBase parentControl;
            private bool closingDropDown;
            private DropDownHolder dropDownHolder;

            public PickerBase Picker
            {
                get
                {
                    return parentControl;
                }
            }
            public void CancelEditing()
            {
                HideHolder();
            }
            public virtual void CloseDropDown()
            {
                if (!closingDropDown)
                {
                    try
                    {
                        closingDropDown = true;
                        if ((dropDownHolder == null) || !dropDownHolder.Visible)
                        {
                            return;
                        }

                        if ((dropDownHolder.Component == parentControl.pickerListBox) && parentControl.dropDownCommit)
                        {
                            parentControl.CommitList();
                        }

                        dropDownHolder.SetComponent(null);
                        dropDownHolder.Visible = false;
                        if (parentControl.pickerTextBox.Visible)
                        {
                            parentControl.pickerTextBox.Focus();
                        }
                    }
                    finally
                    {
                        closingDropDown = false;
                    }
                }

            }
            public virtual void DropDownControl(Control control)
            {
                if (dropDownHolder == null)
                {
                    dropDownHolder = new DropDownHolder(this);
                }
                control.RightToLeft = parentControl.RightToLeft;
                dropDownHolder.Visible = false;
                dropDownHolder.SetComponent(control);

                control.BackColor = parentControl.BackColor;
                control.ForeColor = parentControl.ForeColor;
                control.Font = parentControl.Font;

                Rectangle rectparent = parentControl.Bounds;
                Size size = dropDownHolder.Size;
                Point point = parentControl.Parent.PointToScreen(new Point(0, 0));
                Rectangle rectworking = Screen.GetWorkingArea(Control.MousePosition);

                if (parentControl.RightToLeft == RightToLeft.No)
                {
                    point.X = Math.Min((rectworking.X + rectworking.Width) - size.Width, Math.Max(rectworking.X, ((point.X + rectparent.X) + rectparent.Width) - size.Width));
                }
                else
                {
                    point.X = Math.Min((rectworking.X + rectworking.Width) - size.Width, Math.Max(rectworking.X, point.X + rectparent.X));
                }
                point.Y += rectparent.Y;
                if ((rectworking.Y + rectworking.Height) < (size.Height + point.Y + parentControl.pickerTextBox.Height))
                {
                    point.Y -= size.Height;
                }
                else
                {
                    point.Y = point.Y + rectparent.Height + 1;
                }

                dropDownHolder.SetBounds(point.X, point.Y, size.Width, size.Height);
                dropDownHolder.Visible = true;
                dropDownHolder.FocusComponent();
                parentControl.pickerTextBox.SelectAll();
                dropDownHolder.DoModalLoop();
            }
            public object GetService(Type type)
            {
                if (type == typeof(IWindowsFormsEditorService))
                {
                    return this;
                }
                return null;
            }
            public void HideHolder()
            {
                if ((dropDownHolder != null) && dropDownHolder.Visible)
                {
                    dropDownHolder.Visible = false;
                }
            }
            public virtual DialogResult ShowDialog(Form dialog)
            {
                return dialog.ShowDialog(parentControl);
            }
            public void SystemColorsChanged()
            {
                if (dropDownHolder != null)
                {
                    dropDownHolder.SystemColorChanged();
                }
            }
            public void ValidateEditing()
            {
                parentControl.dropDownCommit = true;
                CloseDropDown();
            }
        }
        #endregion

        #region DropDownHoster
        private class DropDownHolder : Form
        {
            public DropDownHolder(PickerService service)
            {
                currentControl = null;
                parentService = service;
                Text = "";

                base.StartPosition = FormStartPosition.Manual;
                base.ShowInTaskbar = false;
                base.ControlBox = false;
                base.MinimizeBox = false;
                base.MaximizeBox = false;
                base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                base.Visible = false;
            }

            private Control currentControl;
            private PickerService parentService;

            public virtual Control Component
            {
                get
                {
                    return currentControl;
                }
            }
            public void DoModalLoop()
            {
                while (base.Visible)
                {
                    Application.DoEvents();
                    //UnsafeNativeMethods.MsgWaitForMultipleObjects(1, 0, true, 250, 0xff);
                }
            }
            public virtual void FocusComponent()
            {
                if ((currentControl != null) && base.Visible)
                {
                    currentControl.Focus();
                }
            }
            public virtual void SetComponent(Control control)
            {
                if (currentControl != null)
                {
                    base.Controls.Remove(this.currentControl);
                    currentControl = null;
                }
                if (control != null)
                {
                    base.Controls.Add(control);
                    base.Size = new Size(2 + control.Width, 2 + control.Height);
                    control.Location = new Point(0, 0);
                    control.Visible = true;
                    currentControl = control;
                    currentControl.Resize += new EventHandler(OnCurrentControlResize);
                }
                base.Enabled = currentControl != null;
            }
            public void SystemColorChanged()
            {
                OnSystemColorsChanged(EventArgs.Empty);
            }

            private void OnCurrentControlResize(object o, EventArgs e)
            {
                if (currentControl != null)
                {
                    int width = base.Width;
                    base.Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
                    if (currentControl.RightToLeft == RightToLeft.No)
                    {
                        base.Left -= base.Width - width;
                    }
                }
            }

            protected override void OnClosed(EventArgs e)
            {
                if (base.Visible)
                {
                    parentService.CancelEditing();
                }
                base.OnClosed(e);
            }
            protected override void OnDeactivate(EventArgs e)
            {
                if (base.Visible)
                {
                    parentService.CancelEditing();
                }
                base.OnDeactivate(e);
            }
            protected override void OnMouseDown(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    parentService.ValidateEditing();
                }
                base.OnMouseDown(e);
            }
            protected override bool ProcessDialogKey(Keys key)
            {
                if ((key & (Keys.Alt | Keys.Control | Keys.Shift)) == Keys.None)
                {
                    Keys keys = key & Keys.KeyCode;
                    if (keys == Keys.Return)
                    {
                        parentService.ValidateEditing();
                        return true;
                    }
                    if (keys == Keys.Escape)
                    {
                        parentService.CancelEditing();
                        return true;
                    }
                }
                return base.ProcessDialogKey(key);
            }
            protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
            {
                if (currentControl != null)
                {
                    currentControl.SetBounds(0, 0, width - 2, height - 2);
                    width = currentControl.Width;
                    height = currentControl.Height;
                    if ((height == 0) && (currentControl is ListBox))
                    {
                        height = ((ListBox)currentControl).ItemHeight;
                        currentControl.Height = height;
                    }
                    width += 2;
                    height += 2;
                }
                base.SetBoundsCore(x, y, width, height, specified);
            }
        }
        #endregion

        #region PickerListBox
        public class PickerListBox : ListBox
        {
            public PickerListBox(PickerBase parent)
            {
                base.IntegralHeight = false;
                DrawMode = DrawMode.OwnerDrawVariable;
                parentControl = parent;
            }

            private PickerBase parentControl;

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams param = base.CreateParams;
                    param.Style &= -8388609; // 0xFF7FFFFF: No border
                    param.ExStyle &= -513; // 0xFFFFFDFF: No client edge
                    return param;
                }
            }
            protected override void OnMeasureItem(MeasureItemEventArgs e)
            {
                e.ItemHeight++;
            }
            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                e.DrawBackground();

                if ((e.Index >= 0) && (e.Index < base.Items.Count))
                {
                    object data = base.Items[e.Index];
                    Rectangle textrect = e.Bounds;
                    Rectangle valuerect = e.Bounds;

                    if (parentControl.PaintValueSupported)
                    {
                        valuerect.Height--;
                        if (parentControl.PaintValueOnly)
                        {
                            valuerect.X += 2;
                            valuerect.Width -= 5;
                        }
                        else
                        {
                            valuerect.Width = parentControl.PaintValueWidth;
                            valuerect.X += 2;
                            textrect.X += parentControl.PaintValueWidth + 6;
                            textrect.Width -= parentControl.PaintValueWidth - 6;
                        }

                        parentControl.Editor.PaintValue(data, e.Graphics, valuerect);
                        Pen pen = new Pen(ForeColor);
                        try
                        {
                            if (parentControl.PaintValueFrame)
                            {
                                e.Graphics.DrawRectangle(pen, valuerect);
                            }
                        }
                        finally
                        {
                            pen.Dispose();
                        }
                    }

                    if (!parentControl.PaintValueOnly || !parentControl.PaintValueSupported)
                    {
                        Brush brush = new SolidBrush(e.ForeColor);
                        try
                        {
                            e.Graphics.DrawString(parentControl.GetValueAsText(data), Font, brush, (RectangleF)textrect);
                        }
                        finally
                        {
                            brush.Dispose();
                        }
                    }
                }

            }
        }
        #endregion
    }
}
