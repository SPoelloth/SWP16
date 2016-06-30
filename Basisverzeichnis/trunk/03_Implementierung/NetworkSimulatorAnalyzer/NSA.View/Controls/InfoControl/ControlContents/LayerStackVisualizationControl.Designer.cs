namespace NSA.View.Controls.InfoControl.ControlContents
{
    partial class LayerStackVisualizationControl
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
            this.flpLayers = new System.Windows.Forms.FlowLayoutPanel();
            this.labelHardwarenode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flpLayers
            // 
            this.flpLayers.AutoSize = true;
            this.flpLayers.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flpLayers.Location = new System.Drawing.Point(3, 23);
            this.flpLayers.Name = "flpLayers";
            this.flpLayers.Padding = new System.Windows.Forms.Padding(1);
            this.flpLayers.Size = new System.Drawing.Size(162, 125);
            this.flpLayers.TabIndex = 5;
            this.flpLayers.WrapContents = false;
            // 
            // labelHardwarenode
            // 
            this.labelHardwarenode.AutoSize = true;
            this.labelHardwarenode.Location = new System.Drawing.Point(43, 7);
            this.labelHardwarenode.Name = "labelHardwarenode";
            this.labelHardwarenode.Size = new System.Drawing.Size(77, 13);
            this.labelHardwarenode.TabIndex = 6;
            this.labelHardwarenode.Text = "Hardwarenode";
            this.labelHardwarenode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LayerStackVisualizationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.labelHardwarenode);
            this.Controls.Add(this.flpLayers);
            this.Name = "LayerStackVisualizationControl";
            this.Size = new System.Drawing.Size(168, 153);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpLayers;
        private System.Windows.Forms.Label labelHardwarenode;
    }
}
