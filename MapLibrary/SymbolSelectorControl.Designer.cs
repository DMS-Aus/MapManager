namespace DMS.MapLibrary
{
    partial class SymbolSelectorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.listViewPreview = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(205, 175);
            this.listView.TabIndex = 0;
            this.listView.TileSize = new System.Drawing.Size(36, 36);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listViewPreview
            // 
            this.listViewPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listViewPreview.GridLines = true;
            this.listViewPreview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewPreview.Location = new System.Drawing.Point(210, 0);
            this.listViewPreview.MultiSelect = false;
            this.listViewPreview.Name = "listViewPreview";
            this.listViewPreview.Scrollable = false;
            this.listViewPreview.ShowGroups = false;
            this.listViewPreview.ShowItemToolTips = true;
            this.listViewPreview.Size = new System.Drawing.Size(60, 60);
            this.listViewPreview.TabIndex = 1;
            this.listViewPreview.TabStop = false;
            this.listViewPreview.TileSize = new System.Drawing.Size(36, 36);
            this.listViewPreview.UseCompatibleStateImageBehavior = false;
            // 
            // SymbolSelectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewPreview);
            this.Controls.Add(this.listView);
            this.Name = "SymbolSelectorControl";
            this.Size = new System.Drawing.Size(274, 175);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ListView listViewPreview;
    }
}
