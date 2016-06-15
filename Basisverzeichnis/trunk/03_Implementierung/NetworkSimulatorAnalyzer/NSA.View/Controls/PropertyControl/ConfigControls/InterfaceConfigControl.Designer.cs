namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    partial class InterfaceConfigControl
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
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.textBoxSubnetMask = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.labelSubnetMask = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(153, 3);
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(78, 35);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(95, 20);
            this.textBoxIpAddress.TabIndex = 0;
            this.textBoxIpAddress.TextChanged += new System.EventHandler(this.textBoxIpAddress_TextChanged);
            this.textBoxIpAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // textBoxSubnetMask
            // 
            this.textBoxSubnetMask.Location = new System.Drawing.Point(78, 61);
            this.textBoxSubnetMask.Name = "textBoxSubnetMask";
            this.textBoxSubnetMask.Size = new System.Drawing.Size(95, 20);
            this.textBoxSubnetMask.TabIndex = 1;
            this.textBoxSubnetMask.TextChanged += new System.EventHandler(this.textBoxSubnetMask_TextChanged);
            this.textBoxSubnetMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(14, 38);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(58, 13);
            this.labelIpAddress.TabIndex = 4;
            this.labelIpAddress.Text = "IP-Address";
            this.labelIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSubnetMask
            // 
            this.labelSubnetMask.AutoSize = true;
            this.labelSubnetMask.Location = new System.Drawing.Point(5, 64);
            this.labelSubnetMask.Name = "labelSubnetMask";
            this.labelSubnetMask.Size = new System.Drawing.Size(70, 13);
            this.labelSubnetMask.TabIndex = 8;
            this.labelSubnetMask.Text = "Subnet Mask";
            this.labelSubnetMask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InterfaceConfigControl
            // 
            this.Controls.Add(this.textBoxIpAddress);
            this.Controls.Add(this.labelSubnetMask);
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.textBoxSubnetMask);
            this.Name = "InterfaceConfigControl";
            this.Size = new System.Drawing.Size(176, 90);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.textBoxSubnetMask, 0);
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.Controls.SetChildIndex(this.labelSubnetMask, 0);
            this.Controls.SetChildIndex(this.textBoxIpAddress, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxSubnetMask;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label labelIpAddress;
        private System.Windows.Forms.Label labelSubnetMask;
    }
}
