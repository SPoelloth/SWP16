namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    partial class GWConfigControl
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
            this.labelInterface = new System.Windows.Forms.Label();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.textBoxInterface = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Size = new System.Drawing.Size(144, 24);
            this.labelName.Text = "Default Gateway";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(153, 4);
            this.buttonClose.Visible = false;
            // 
            // labelInterface
            // 
            this.labelInterface.AutoSize = true;
            this.labelInterface.Location = new System.Drawing.Point(23, 64);
            this.labelInterface.Name = "labelInterface";
            this.labelInterface.Size = new System.Drawing.Size(49, 13);
            this.labelInterface.TabIndex = 2;
            this.labelInterface.Text = "Interface";
            this.labelInterface.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(78, 35);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(95, 20);
            this.textBoxIpAddress.TabIndex = 0;
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(14, 38);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(58, 13);
            this.labelIpAddress.TabIndex = 0;
            this.labelIpAddress.Text = "IP-Address";
            this.labelIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxInterface
            // 
            this.textBoxInterface.Location = new System.Drawing.Point(78, 61);
            this.textBoxInterface.Name = "textBoxInterface";
            this.textBoxInterface.Size = new System.Drawing.Size(95, 20);
            this.textBoxInterface.TabIndex = 1;
            this.textBoxInterface.TextChanged += new System.EventHandler(this.textBoxInterface_TextChanged);
            // 
            // GWConfigControl
            // 
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.labelInterface);
            this.Controls.Add(this.textBoxInterface);
            this.Controls.Add(this.textBoxIpAddress);
            this.Name = "GWConfigControl";
            this.Size = new System.Drawing.Size(176, 90);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.textBoxIpAddress, 0);
            this.Controls.SetChildIndex(this.textBoxInterface, 0);
            this.Controls.SetChildIndex(this.labelInterface, 0);
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelInterface;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label labelIpAddress;
        private System.Windows.Forms.TextBox textBoxInterface;
    }
}
