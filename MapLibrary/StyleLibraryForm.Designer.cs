namespace DMS.MapLibrary
{
    partial class StyleLibraryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.trackBarMagnify = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPercent = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.layerControlStyles = new DMS.MapLibrary.LayerControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.scintillaControl = new ScintillaNet.Scintilla();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.scintillaControlSymbolset = new ScintillaNet.Scintilla();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listViewFonts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxSample = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.panelRefresh = new System.Windows.Forms.Panel();
            this.labelRefresh = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.buttonRemoveFont = new System.Windows.Forms.Button();
            this.buttonAddFont = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMagnify)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scintillaControl)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scintillaControlSymbolset)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).BeginInit();
            this.panelRefresh.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(451, 385);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(451, 352);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // trackBarMagnify
            // 
            this.trackBarMagnify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarMagnify.Location = new System.Drawing.Point(414, 153);
            this.trackBarMagnify.Maximum = 20;
            this.trackBarMagnify.Minimum = 4;
            this.trackBarMagnify.Name = "trackBarMagnify";
            this.trackBarMagnify.Size = new System.Drawing.Size(112, 45);
            this.trackBarMagnify.TabIndex = 14;
            this.trackBarMagnify.Value = 8;
            this.trackBarMagnify.Scroll += new System.EventHandler(this.trackBarMagnify_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Magnification";
            // 
            // labelPercent
            // 
            this.labelPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPercent.AutoSize = true;
            this.labelPercent.Location = new System.Drawing.Point(528, 156);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(33, 13);
            this.labelPercent.TabIndex = 16;
            this.labelPercent.Text = "200%";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Location = new System.Drawing.Point(3, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(405, 410);
            this.tabControl.TabIndex = 17;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.layerControlStyles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(397, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Style List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // layerControlStyles
            // 
            this.layerControlStyles.BackColor = System.Drawing.Color.White;
            this.layerControlStyles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerControlStyles.IsStyleLibraryControl = true;
            this.layerControlStyles.LegendIconPadding = new System.Windows.Forms.Padding(5);
            this.layerControlStyles.LegendIconSize = new System.Drawing.Size(45, 30);
            this.layerControlStyles.Location = new System.Drawing.Point(3, 3);
            this.layerControlStyles.Name = "layerControlStyles";
            this.layerControlStyles.ShowCheckBoxes = false;
            this.layerControlStyles.ShowClasses = true;
            this.layerControlStyles.ShowLabels = false;
            this.layerControlStyles.ShowRootObject = true;
            this.layerControlStyles.ShowStyles = false;
            this.layerControlStyles.ShowToolbar = false;
            this.layerControlStyles.Size = new System.Drawing.Size(391, 378);
            this.layerControlStyles.TabIndex = 1;
            this.layerControlStyles.Target = null;
            this.layerControlStyles.ItemSelect += new DMS.MapLibrary.LayerControl.ItemSelectEventHandler(this.layerControlStyles_ItemSelect);
            this.layerControlStyles.GoToLayerText += new DMS.MapLibrary.LayerControl.GoToLayerTextEventHandler(this.layerControlStyles_GoToLayerText);
            this.layerControlStyles.BeforeRefresh += new System.EventHandler(this.layerControlStyles_BeforeRefresh);
            this.layerControlStyles.AfterRefresh += new System.EventHandler(this.layerControlStyles_AfterRefresh);
            this.layerControlStyles.EditProperties += new DMS.MapLibrary.EditPropertiesEventHandler(this.layerControlStyles_EditProperties);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.scintillaControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(397, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Style List (Text)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // scintillaControl
            // 
            this.scintillaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaControl.Lexing.Lexer = ScintillaNet.Lexer.Null;
            this.scintillaControl.Lexing.LexerName = "automatic";
            this.scintillaControl.Lexing.LineCommentPrefix = "";
            this.scintillaControl.Lexing.StreamCommentPrefix = "";
            this.scintillaControl.Lexing.StreamCommentSufix = "";
            this.scintillaControl.Location = new System.Drawing.Point(3, 3);
            this.scintillaControl.Name = "scintillaControl";
            this.scintillaControl.Size = new System.Drawing.Size(391, 378);
            this.scintillaControl.Styles.BraceBad.FontName = "Verdana";
            this.scintillaControl.Styles.BraceLight.FontName = "Verdana";
            this.scintillaControl.Styles.ControlChar.FontName = "Verdana";
            this.scintillaControl.Styles.Default.FontName = "Verdana";
            this.scintillaControl.Styles.IndentGuide.FontName = "Verdana";
            this.scintillaControl.Styles.LastPredefined.FontName = "Verdana";
            this.scintillaControl.Styles.LineNumber.FontName = "Verdana";
            this.scintillaControl.Styles.Max.FontName = "Verdana";
            this.scintillaControl.TabIndex = 1;
            this.scintillaControl.TextInserted += new System.EventHandler<ScintillaNet.TextModifiedEventArgs>(this.scintillaControl_TextInserted);
            this.scintillaControl.TextDeleted += new System.EventHandler<ScintillaNet.TextModifiedEventArgs>(this.scintillaControl_TextDeleted);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.scintillaControlSymbolset);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(397, 384);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Symbolset";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // scintillaControlSymbolset
            // 
            this.scintillaControlSymbolset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaControlSymbolset.Lexing.Lexer = ScintillaNet.Lexer.Null;
            this.scintillaControlSymbolset.Lexing.LexerName = "automatic";
            this.scintillaControlSymbolset.Lexing.LineCommentPrefix = "";
            this.scintillaControlSymbolset.Lexing.StreamCommentPrefix = "";
            this.scintillaControlSymbolset.Lexing.StreamCommentSufix = "";
            this.scintillaControlSymbolset.Location = new System.Drawing.Point(0, 0);
            this.scintillaControlSymbolset.Name = "scintillaControlSymbolset";
            this.scintillaControlSymbolset.Size = new System.Drawing.Size(397, 384);
            this.scintillaControlSymbolset.Styles.BraceBad.FontName = "Verdana";
            this.scintillaControlSymbolset.Styles.BraceLight.FontName = "Verdana";
            this.scintillaControlSymbolset.Styles.ControlChar.FontName = "Verdana";
            this.scintillaControlSymbolset.Styles.Default.FontName = "Verdana";
            this.scintillaControlSymbolset.Styles.IndentGuide.FontName = "Verdana";
            this.scintillaControlSymbolset.Styles.LastPredefined.FontName = "Verdana";
            this.scintillaControlSymbolset.Styles.LineNumber.FontName = "Verdana";
            this.scintillaControlSymbolset.Styles.Max.FontName = "Verdana";
            this.scintillaControlSymbolset.TabIndex = 2;
            this.scintillaControlSymbolset.TextInserted += new System.EventHandler<ScintillaNet.TextModifiedEventArgs>(this.scintillaControlSymbolset_TextInserted);
            this.scintillaControlSymbolset.TextDeleted += new System.EventHandler<ScintillaNet.TextModifiedEventArgs>(this.scintillaControlSymbolset_TextDeleted);
            this.scintillaControlSymbolset.Validating += new System.ComponentModel.CancelEventHandler(this.scintillaControlSymbolset_Validating);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listViewFonts);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(397, 384);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Fontset";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listViewFonts
            // 
            this.listViewFonts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewFonts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFonts.FullRowSelect = true;
            this.listViewFonts.HideSelection = false;
            this.listViewFonts.Location = new System.Drawing.Point(3, 3);
            this.listViewFonts.MultiSelect = false;
            this.listViewFonts.Name = "listViewFonts";
            this.listViewFonts.Size = new System.Drawing.Size(391, 378);
            this.listViewFonts.TabIndex = 0;
            this.listViewFonts.UseCompatibleStateImageBehavior = false;
            this.listViewFonts.View = System.Windows.Forms.View.Details;
            this.listViewFonts.SelectedIndexChanged += new System.EventHandler(this.listViewFonts_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Font Name";
            this.columnHeader1.Width = 144;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Path";
            this.columnHeader2.Width = 241;
            // 
            // pictureBoxSample
            // 
            this.pictureBoxSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSample.BackColor = System.Drawing.Color.White;
            this.pictureBoxSample.Location = new System.Drawing.Point(414, 23);
            this.pictureBoxSample.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxSample.Name = "pictureBoxSample";
            this.pictureBoxSample.Size = new System.Drawing.Size(147, 112);
            this.pictureBoxSample.TabIndex = 18;
            this.pictureBoxSample.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Sample";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 500;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // panelRefresh
            // 
            this.panelRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelRefresh.BackColor = System.Drawing.Color.White;
            this.panelRefresh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRefresh.Controls.Add(this.labelRefresh);
            this.panelRefresh.Location = new System.Drawing.Point(73, 152);
            this.panelRefresh.Name = "panelRefresh";
            this.panelRefresh.Size = new System.Drawing.Size(272, 64);
            this.panelRefresh.TabIndex = 21;
            // 
            // labelRefresh
            // 
            this.labelRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelRefresh.AutoSize = true;
            this.labelRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRefresh.Location = new System.Drawing.Point(14, 24);
            this.labelRefresh.Name = "labelRefresh";
            this.labelRefresh.Size = new System.Drawing.Size(243, 16);
            this.labelRefresh.TabIndex = 3;
            this.labelRefresh.Text = "Refreshing style list, please wait...";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInfo.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxInfo.ForeColor = System.Drawing.Color.Red;
            this.textBoxInfo.Location = new System.Drawing.Point(415, 192);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(146, 83);
            this.textBoxInfo.TabIndex = 22;
            // 
            // buttonRemoveFont
            // 
            this.buttonRemoveFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveFont.Location = new System.Drawing.Point(451, 318);
            this.buttonRemoveFont.Name = "buttonRemoveFont";
            this.buttonRemoveFont.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveFont.TabIndex = 23;
            this.buttonRemoveFont.Text = "Delete Font";
            this.buttonRemoveFont.UseVisualStyleBackColor = true;
            this.buttonRemoveFont.Visible = false;
            this.buttonRemoveFont.Click += new System.EventHandler(this.buttonRemoveFont_Click);
            // 
            // buttonAddFont
            // 
            this.buttonAddFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddFont.Location = new System.Drawing.Point(451, 284);
            this.buttonAddFont.Name = "buttonAddFont";
            this.buttonAddFont.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFont.TabIndex = 24;
            this.buttonAddFont.Text = "Add Font";
            this.buttonAddFont.UseVisualStyleBackColor = true;
            this.buttonAddFont.Visible = false;
            this.buttonAddFont.Click += new System.EventHandler(this.buttonAddFont_Click);
            // 
            // StyleLibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 415);
            this.Controls.Add(this.buttonAddFont);
            this.Controls.Add(this.buttonRemoveFont);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.panelRefresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxSample);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.labelPercent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarMagnify);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonOK);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StyleLibraryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Style Library";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StyleLibraryForm_FormClosing);
            this.Load += new System.EventHandler(this.StyleLibraryForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StyleLibraryForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMagnify)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scintillaControl)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scintillaControlSymbolset)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).EndInit();
            this.panelRefresh.ResumeLayout(false);
            this.panelRefresh.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TrackBar trackBarMagnify;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private LayerControl layerControlStyles;
        private System.Windows.Forms.TabPage tabPage2;
        private ScintillaNet.Scintilla scintillaControl;
        private System.Windows.Forms.PictureBox pictureBoxSample;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.TabPage tabPage3;
        private ScintillaNet.Scintilla scintillaControlSymbolset;
        private System.Windows.Forms.Panel panelRefresh;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listViewFonts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonRemoveFont;
        private System.Windows.Forms.Button buttonAddFont;
    }
}