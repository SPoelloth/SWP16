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
            this.layerStackVisualizationControlDest = new NSA.View.Controls.InfoControl.ControlContents.LayerStackVisualizationControl();
            this.layerStackVisualizationControlSrc = new NSA.View.Controls.InfoControl.ControlContents.LayerStackVisualizationControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // layerStackVisualizationControlDest
            // 
            this.layerStackVisualizationControlDest.AutoSize = true;
            this.layerStackVisualizationControlDest.BackColor = System.Drawing.SystemColors.ControlDark;
            this.layerStackVisualizationControlDest.Location = new System.Drawing.Point(469, 3);
            this.layerStackVisualizationControlDest.Name = "layerStackVisualizationControlDest";
            this.layerStackVisualizationControlDest.Size = new System.Drawing.Size(168, 154);
            this.layerStackVisualizationControlDest.TabIndex = 1;
            // 
            // layerStackVisualizationControlSrc
            // 
            this.layerStackVisualizationControlSrc.AutoSize = true;
            this.layerStackVisualizationControlSrc.BackColor = System.Drawing.SystemColors.ControlDark;
            this.layerStackVisualizationControlSrc.Location = new System.Drawing.Point(99, 3);
            this.layerStackVisualizationControlSrc.Name = "layerStackVisualizationControlSrc";
            this.layerStackVisualizationControlSrc.Size = new System.Drawing.Size(168, 154);
            this.layerStackVisualizationControlSrc.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quelle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(402, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ziel:";
            // 
            // HopVisualizationControl
            // 
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.layerStackVisualizationControlDest);
            this.Controls.Add(this.layerStackVisualizationControlSrc);
            this.Name = "HopVisualizationControl";
            this.Size = new System.Drawing.Size(738, 165);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LayerStackVisualizationControl layerStackVisualizationControlSrc;
        private LayerStackVisualizationControl layerStackVisualizationControlDest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
