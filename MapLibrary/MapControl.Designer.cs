namespace DMS.MapLibrary
{
    partial class MapControl
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
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 1000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // MapControl
            // 
            this.DoubleClick += new System.EventHandler(this.MapControl_DoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseUp);
            this.SizeChanged += new System.EventHandler(this.MapControl_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerRefresh;
    }
}
