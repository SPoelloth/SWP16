namespace NSA.View.Controls.InfoControl.ControlContents
{
    partial class HopVisualizationControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.layerStackVisualizationControl2 = new NSA.View.Controls.InfoControl.ControlContents.LayerStackVisualizationControl();
            this.layerStackVisualizationControl1 = new NSA.View.Controls.InfoControl.ControlContents.LayerStackVisualizationControl();
            this.SuspendLayout();
            // 
            // layerStackVisualizationControl2
            // 
            this.layerStackVisualizationControl2.AutoSize = true;
            this.layerStackVisualizationControl2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.layerStackVisualizationControl2.Location = new System.Drawing.Point(469, 3);
            this.layerStackVisualizationControl2.Name = "layerStackVisualizationControl2";
            this.layerStackVisualizationControl2.Size = new System.Drawing.Size(168, 154);
            this.layerStackVisualizationControl2.TabIndex = 1;
            // 
            // layerStackVisualizationControl1
            // 
            this.layerStackVisualizationControl1.AutoSize = true;
            this.layerStackVisualizationControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.layerStackVisualizationControl1.Location = new System.Drawing.Point(99, 3);
            this.layerStackVisualizationControl1.Name = "layerStackVisualizationControl1";
            this.layerStackVisualizationControl1.Size = new System.Drawing.Size(168, 154);
            this.layerStackVisualizationControl1.TabIndex = 0;
            // 
            // HopVisualizationControl
            // 
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.layerStackVisualizationControl2);
            this.Controls.Add(this.layerStackVisualizationControl1);
            this.Name = "HopVisualizationControl";
            this.Size = new System.Drawing.Size(738, 165);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LayerStackVisualizationControl layerStackVisualizationControl1;
        private LayerStackVisualizationControl layerStackVisualizationControl2;
    }
}
