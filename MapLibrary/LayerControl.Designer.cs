namespace DMS.MapLibrary
{
    partial class LayerControl
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
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonNew = new System.Windows.Forms.ToolStripSplitButton();
            this.addVectorLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRasterLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWMSLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMSSQLSpatialLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTileIndexLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMapFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDown = new System.Windows.Forms.ToolStripButton();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVectorLayerFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRasterLayerFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWMSLayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addMSSQLSpatialLayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addTileIndexLayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMapFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveItemUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveItemDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToLayerExtentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToLayerTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToClassTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSplitItems = new System.Windows.Forms.ToolStripSeparator();
            this.autoStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSplitProp = new System.Windows.Forms.ToolStripSeparator();
            this.addThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSplitTheme = new System.Windows.Forms.ToolStripSeparator();
            this.refreshListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.addGraticuleLayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addGraticuleLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonNew,
            this.toolStripButtonDelete,
            this.toolStripButtonUp,
            this.toolStripButtonDown});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(204, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripSplitButtonNew
            // 
            this.toolStripSplitButtonNew.AccessibleName = " ";
            this.toolStripSplitButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVectorLayerToolStripMenuItem,
            this.addRasterLayerToolStripMenuItem,
            this.addWMSLayerToolStripMenuItem,
            this.addMSSQLSpatialLayerToolStripMenuItem,
            this.addTileIndexLayerToolStripMenuItem,
            this.addGraticuleLayerToolStripMenuItem,
            this.addNewLayerToolStripMenuItem,
            this.addMapFileToolStripMenuItem,
            this.addNewClassToolStripMenuItem,
            this.addNewStyleToolStripMenuItem,
            this.addNewLabelToolStripMenuItem});
            this.toolStripSplitButtonNew.Image = global::MapLibrary.Properties.Resources.add;
            this.toolStripSplitButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonNew.Name = "toolStripSplitButtonNew";
            this.toolStripSplitButtonNew.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButtonNew.Text = "toolStripSplitButton1";
            this.toolStripSplitButtonNew.ToolTipText = "Add New Item";
            this.toolStripSplitButtonNew.ButtonClick += new System.EventHandler(this.toolStripSplitButtonNew_ButtonClick);
            // 
            // addVectorLayerToolStripMenuItem
            // 
            this.addVectorLayerToolStripMenuItem.Name = "addVectorLayerToolStripMenuItem";
            this.addVectorLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addVectorLayerToolStripMenuItem.Text = "Add Vector Layer From File...";
            this.addVectorLayerToolStripMenuItem.Click += new System.EventHandler(this.addVectorLayerToolStripMenuItem_Click);
            // 
            // addRasterLayerToolStripMenuItem
            // 
            this.addRasterLayerToolStripMenuItem.Name = "addRasterLayerToolStripMenuItem";
            this.addRasterLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addRasterLayerToolStripMenuItem.Text = "Add Raster Layer From File...";
            this.addRasterLayerToolStripMenuItem.Click += new System.EventHandler(this.addRasterLayerToolStripMenuItem_Click);
            // 
            // addWMSLayerToolStripMenuItem
            // 
            this.addWMSLayerToolStripMenuItem.Name = "addWMSLayerToolStripMenuItem";
            this.addWMSLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addWMSLayerToolStripMenuItem.Text = "Add WMS Layer...";
            this.addWMSLayerToolStripMenuItem.Click += new System.EventHandler(this.addWMSLayerToolStripMenuItem_Click);
            // 
            // addMSSQLSpatialLayerToolStripMenuItem
            // 
            this.addMSSQLSpatialLayerToolStripMenuItem.Name = "addMSSQLSpatialLayerToolStripMenuItem";
            this.addMSSQLSpatialLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addMSSQLSpatialLayerToolStripMenuItem.Text = "Add MS SQL Spatial Layer...";
            this.addMSSQLSpatialLayerToolStripMenuItem.Click += new System.EventHandler(this.addMSSQLSpatialLayerToolStripMenuItem_Click);
            // 
            // addTileIndexLayerToolStripMenuItem
            // 
            this.addTileIndexLayerToolStripMenuItem.Name = "addTileIndexLayerToolStripMenuItem";
            this.addTileIndexLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addTileIndexLayerToolStripMenuItem.Text = "Add Tile Index Layer...";
            this.addTileIndexLayerToolStripMenuItem.Click += new System.EventHandler(this.addTileIndexLayerToolStripMenuItem_Click);
            // 
            // addNewLayerToolStripMenuItem
            // 
            this.addNewLayerToolStripMenuItem.Name = "addNewLayerToolStripMenuItem";
            this.addNewLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addNewLayerToolStripMenuItem.Text = "Add New Layer";
            this.addNewLayerToolStripMenuItem.Click += new System.EventHandler(this.addLayerToolStripMenuItem_Click);
            // 
            // addMapFileToolStripMenuItem
            // 
            this.addMapFileToolStripMenuItem.Name = "addMapFileToolStripMenuItem";
            this.addMapFileToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addMapFileToolStripMenuItem.Text = "Add Layers from Map File...";
            this.addMapFileToolStripMenuItem.Click += new System.EventHandler(this.addMapFileToolStripMenuItem_Click);
            // 
            // addNewClassToolStripMenuItem
            // 
            this.addNewClassToolStripMenuItem.Name = "addNewClassToolStripMenuItem";
            this.addNewClassToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addNewClassToolStripMenuItem.Text = "Add New Class";
            this.addNewClassToolStripMenuItem.Click += new System.EventHandler(this.addClassToolStripMenuItem_Click);
            // 
            // addNewStyleToolStripMenuItem
            // 
            this.addNewStyleToolStripMenuItem.Name = "addNewStyleToolStripMenuItem";
            this.addNewStyleToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addNewStyleToolStripMenuItem.Text = "Add New Style";
            this.addNewStyleToolStripMenuItem.Click += new System.EventHandler(this.addStyleToolStripMenuItem_Click);
            // 
            // addNewLabelToolStripMenuItem
            // 
            this.addNewLabelToolStripMenuItem.Name = "addNewLabelToolStripMenuItem";
            this.addNewLabelToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addNewLabelToolStripMenuItem.Text = "Add New Label";
            this.addNewLabelToolStripMenuItem.Click += new System.EventHandler(this.addLabelToolStripMenuItem_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::MapLibrary.Properties.Resources.remove;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "toolStripButton1";
            this.toolStripButtonDelete.ToolTipText = "Delete The Selected Item";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonUp
            // 
            this.toolStripButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUp.Image = global::MapLibrary.Properties.Resources.up;
            this.toolStripButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUp.Name = "toolStripButtonUp";
            this.toolStripButtonUp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUp.Text = "toolStripButton2";
            this.toolStripButtonUp.ToolTipText = "Move The Selected Item Up";
            this.toolStripButtonUp.Click += new System.EventHandler(this.toolStripButtonUp_Click);
            // 
            // toolStripButtonDown
            // 
            this.toolStripButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDown.Image = global::MapLibrary.Properties.Resources.down;
            this.toolStripButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDown.Name = "toolStripButtonDown";
            this.toolStripButtonDown.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDown.Text = "toolStripButton3";
            this.toolStripButtonDown.ToolTipText = "Move The Selected Item Down";
            this.toolStripButtonDown.Click += new System.EventHandler(this.toolStripButtonDown_Click);
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.BackColor = System.Drawing.Color.Gainsboro;
            this.treeView.CheckBoxes = true;
            this.treeView.ContextMenuStrip = this.contextMenuStrip;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.Name = "treeView";
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(204, 320);
            this.treeView.TabIndex = 2;
            this.treeView.Visible = false;
            this.treeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVectorLayerFromFileToolStripMenuItem,
            this.addRasterLayerFromFileToolStripMenuItem,
            this.addWMSLayerToolStripMenuItem1,
            this.addMSSQLSpatialLayerToolStripMenuItem1,
            this.addTileIndexLayerToolStripMenuItem1,
            this.addGraticuleLayerToolStripMenuItem1,
            this.addLayerToolStripMenuItem,
            this.addMapFileToolStripMenuItem1,
            this.addClassToolStripMenuItem,
            this.addStyleToolStripMenuItem,
            this.addLabelToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteItemToolStripMenuItem,
            this.toolStripMenuItem1,
            this.moveItemUpToolStripMenuItem,
            this.moveItemDownToolStripMenuItem,
            this.zoomToLayerExtentToolStripMenuItem,
            this.goToLayerTextToolStripMenuItem,
            this.goToClassTextToolStripMenuItem,
            this.toolStripMenuItemSplitItems,
            this.autoStyleToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.toolStripMenuItemSplitProp,
            this.addThemeToolStripMenuItem,
            this.toolStripMenuItemSplitTheme,
            this.refreshListToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(225, 512);
            // 
            // addVectorLayerFromFileToolStripMenuItem
            // 
            this.addVectorLayerFromFileToolStripMenuItem.Image = global::MapLibrary.Properties.Resources.add;
            this.addVectorLayerFromFileToolStripMenuItem.Name = "addVectorLayerFromFileToolStripMenuItem";
            this.addVectorLayerFromFileToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addVectorLayerFromFileToolStripMenuItem.Text = "Add Vector Layer From File...";
            this.addVectorLayerFromFileToolStripMenuItem.Click += new System.EventHandler(this.addVectorLayerFromFileToolStripMenuItem_Click);
            // 
            // addRasterLayerFromFileToolStripMenuItem
            // 
            this.addRasterLayerFromFileToolStripMenuItem.Name = "addRasterLayerFromFileToolStripMenuItem";
            this.addRasterLayerFromFileToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addRasterLayerFromFileToolStripMenuItem.Text = "Add Raster Layer From File...";
            this.addRasterLayerFromFileToolStripMenuItem.Click += new System.EventHandler(this.addRasterLayerFromFileToolStripMenuItem_Click);
            // 
            // addWMSLayerToolStripMenuItem1
            // 
            this.addWMSLayerToolStripMenuItem1.Name = "addWMSLayerToolStripMenuItem1";
            this.addWMSLayerToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.addWMSLayerToolStripMenuItem1.Text = "Add WMS Layer...";
            this.addWMSLayerToolStripMenuItem1.Click += new System.EventHandler(this.addWMSLayerToolStripMenuItem_Click);
            // 
            // addMSSQLSpatialLayerToolStripMenuItem1
            // 
            this.addMSSQLSpatialLayerToolStripMenuItem1.Name = "addMSSQLSpatialLayerToolStripMenuItem1";
            this.addMSSQLSpatialLayerToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.addMSSQLSpatialLayerToolStripMenuItem1.Text = "Add MS SQL Spatial Layer...";
            this.addMSSQLSpatialLayerToolStripMenuItem1.Click += new System.EventHandler(this.addMSSQLSpatialLayerToolStripMenuItem_Click);
            // 
            // addTileIndexLayerToolStripMenuItem1
            // 
            this.addTileIndexLayerToolStripMenuItem1.Name = "addTileIndexLayerToolStripMenuItem1";
            this.addTileIndexLayerToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.addTileIndexLayerToolStripMenuItem1.Text = "Add Tile Index Layer...";
            this.addTileIndexLayerToolStripMenuItem1.Click += new System.EventHandler(this.addTileIndexLayerToolStripMenuItem_Click);
            // 
            // addLayerToolStripMenuItem
            // 
            this.addLayerToolStripMenuItem.Name = "addLayerToolStripMenuItem";
            this.addLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addLayerToolStripMenuItem.Text = "Add New Layer";
            this.addLayerToolStripMenuItem.Click += new System.EventHandler(this.addLayerToolStripMenuItem_Click);
            // 
            // addMapFileToolStripMenuItem1
            // 
            this.addMapFileToolStripMenuItem1.Name = "addMapFileToolStripMenuItem1";
            this.addMapFileToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.addMapFileToolStripMenuItem1.Text = "Add Layers from Map File...";
            this.addMapFileToolStripMenuItem1.Click += new System.EventHandler(this.addMapFileToolStripMenuItem_Click);
            // 
            // addClassToolStripMenuItem
            // 
            this.addClassToolStripMenuItem.Name = "addClassToolStripMenuItem";
            this.addClassToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addClassToolStripMenuItem.Text = "Add New Class";
            this.addClassToolStripMenuItem.Click += new System.EventHandler(this.addClassToolStripMenuItem_Click);
            // 
            // addStyleToolStripMenuItem
            // 
            this.addStyleToolStripMenuItem.Name = "addStyleToolStripMenuItem";
            this.addStyleToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addStyleToolStripMenuItem.Text = "Add New Style";
            this.addStyleToolStripMenuItem.Click += new System.EventHandler(this.addStyleToolStripMenuItem_Click);
            // 
            // addLabelToolStripMenuItem
            // 
            this.addLabelToolStripMenuItem.Name = "addLabelToolStripMenuItem";
            this.addLabelToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addLabelToolStripMenuItem.Text = "Add New Label";
            this.addLabelToolStripMenuItem.Click += new System.EventHandler(this.addLabelToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Image = global::MapLibrary.Properties.Resources.remove;
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.deleteItemToolStripMenuItem.Text = "Delete Layer";
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.deleteLayerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(221, 6);
            // 
            // moveItemUpToolStripMenuItem
            // 
            this.moveItemUpToolStripMenuItem.Image = global::MapLibrary.Properties.Resources.up;
            this.moveItemUpToolStripMenuItem.Name = "moveItemUpToolStripMenuItem";
            this.moveItemUpToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.moveItemUpToolStripMenuItem.Text = "Move Layer Up";
            this.moveItemUpToolStripMenuItem.Click += new System.EventHandler(this.moveLayerUpToolStripMenuItem_Click);
            // 
            // moveItemDownToolStripMenuItem
            // 
            this.moveItemDownToolStripMenuItem.Image = global::MapLibrary.Properties.Resources.down;
            this.moveItemDownToolStripMenuItem.Name = "moveItemDownToolStripMenuItem";
            this.moveItemDownToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.moveItemDownToolStripMenuItem.Text = "Move Layer Down";
            this.moveItemDownToolStripMenuItem.Click += new System.EventHandler(this.moveLayerDownToolStripMenuItem_Click);
            // 
            // zoomToLayerExtentToolStripMenuItem
            // 
            this.zoomToLayerExtentToolStripMenuItem.Name = "zoomToLayerExtentToolStripMenuItem";
            this.zoomToLayerExtentToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.zoomToLayerExtentToolStripMenuItem.Text = "Zoom To Layer Extent";
            this.zoomToLayerExtentToolStripMenuItem.Click += new System.EventHandler(this.zoomToLayerExtentToolStripMenuItem_Click);
            // 
            // goToLayerTextToolStripMenuItem
            // 
            this.goToLayerTextToolStripMenuItem.Name = "goToLayerTextToolStripMenuItem";
            this.goToLayerTextToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.goToLayerTextToolStripMenuItem.Text = "Go To Layer (Text)";
            this.goToLayerTextToolStripMenuItem.Click += new System.EventHandler(this.goToLayerTextToolStripMenuItem_Click);
            // 
            // goToClassTextToolStripMenuItem
            // 
            this.goToClassTextToolStripMenuItem.Name = "goToClassTextToolStripMenuItem";
            this.goToClassTextToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.goToClassTextToolStripMenuItem.Text = "Go To Class (Text)";
            this.goToClassTextToolStripMenuItem.Click += new System.EventHandler(this.goToClassTextToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSplitItems
            // 
            this.toolStripMenuItemSplitItems.Name = "toolStripMenuItemSplitItems";
            this.toolStripMenuItemSplitItems.Size = new System.Drawing.Size(221, 6);
            // 
            // autoStyleToolStripMenuItem
            // 
            this.autoStyleToolStripMenuItem.Name = "autoStyleToolStripMenuItem";
            this.autoStyleToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.autoStyleToolStripMenuItem.Text = "Auto Style";
            this.autoStyleToolStripMenuItem.Click += new System.EventHandler(this.autoStyleToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSplitProp
            // 
            this.toolStripMenuItemSplitProp.Name = "toolStripMenuItemSplitProp";
            this.toolStripMenuItemSplitProp.Size = new System.Drawing.Size(221, 6);
            // 
            // addThemeToolStripMenuItem
            // 
            this.addThemeToolStripMenuItem.Name = "addThemeToolStripMenuItem";
            this.addThemeToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addThemeToolStripMenuItem.Text = "Add Theme...";
            this.addThemeToolStripMenuItem.Click += new System.EventHandler(this.addThemeToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSplitTheme
            // 
            this.toolStripMenuItemSplitTheme.Name = "toolStripMenuItemSplitTheme";
            this.toolStripMenuItemSplitTheme.Size = new System.Drawing.Size(221, 6);
            // 
            // refreshListToolStripMenuItem
            // 
            this.refreshListToolStripMenuItem.Image = global::MapLibrary.Properties.Resources.Refresh16;
            this.refreshListToolStripMenuItem.Name = "refreshListToolStripMenuItem";
            this.refreshListToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.refreshListToolStripMenuItem.Text = "Refresh List";
            this.refreshListToolStripMenuItem.Click += new System.EventHandler(this.refreshListToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // treeView2
            // 
            this.treeView2.AllowDrop = true;
            this.treeView2.BackColor = System.Drawing.Color.Gainsboro;
            this.treeView2.CheckBoxes = true;
            this.treeView2.ContextMenuStrip = this.contextMenuStrip;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.HideSelection = false;
            this.treeView2.LabelEdit = true;
            this.treeView2.Location = new System.Drawing.Point(0, 25);
            this.treeView2.Name = "treeView2";
            this.treeView2.ShowRootLines = false;
            this.treeView2.Size = new System.Drawing.Size(204, 320);
            this.treeView2.TabIndex = 1;
            this.treeView2.Visible = false;
            this.treeView2.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
            this.treeView2.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView2.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            this.treeView2.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView2.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView2.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView2.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView2.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.treeView2.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.treeView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // addGraticuleLayerToolStripMenuItem1
            // 
            this.addGraticuleLayerToolStripMenuItem1.Name = "addGraticuleLayerToolStripMenuItem1";
            this.addGraticuleLayerToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.addGraticuleLayerToolStripMenuItem1.Text = "Add Graticule Layer";
            this.addGraticuleLayerToolStripMenuItem1.Click += new System.EventHandler(this.addGraticuleLayerToolStripMenuItem_Click);
            // 
            // addGraticuleLayerToolStripMenuItem
            // 
            this.addGraticuleLayerToolStripMenuItem.Name = "addGraticuleLayerToolStripMenuItem";
            this.addGraticuleLayerToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addGraticuleLayerToolStripMenuItem.Text = "Add Graticule Layer";
            this.addGraticuleLayerToolStripMenuItem.Click += new System.EventHandler(this.addGraticuleLayerToolStripMenuItem_Click);
            // 
            // LayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.toolStrip);
            this.Name = "LayerControl";
            this.Size = new System.Drawing.Size(204, 345);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonUp;
        private System.Windows.Forms.ToolStripButton toolStripButtonDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveItemUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveItemDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSplitItems;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSplitProp;
        private System.Windows.Forms.ToolStripMenuItem refreshListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addVectorLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRasterLayerToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem addThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSplitTheme;
        private System.Windows.Forms.ToolStripMenuItem addClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToLayerExtentToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripMenuItem addVectorLayerFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRasterLayerFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWMSLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToLayerTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMSSQLSpatialLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem autoStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWMSLayerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addMSSQLSpatialLayerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToClassTextToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem addTileIndexLayerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addTileIndexLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMapFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMapFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addGraticuleLayerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addGraticuleLayerToolStripMenuItem;
    }
}
