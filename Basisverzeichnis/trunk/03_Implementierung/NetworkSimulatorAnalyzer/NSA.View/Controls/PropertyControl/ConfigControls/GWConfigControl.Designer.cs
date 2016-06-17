namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    partial class GwConfigControl
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
            this.checkBoxInternetAccess = new System.Windows.Forms.CheckBox();
            this.comboBoxInterfaces = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Size = new System.Drawing.Size(162, 24);
            this.labelName.Text = "Standard Gateway";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(191, 3);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Visible = false;
            // 
            // labelInterface
            // 
            this.labelInterface.AutoSize = true;
            this.labelInterface.Location = new System.Drawing.Point(14, 63);
            this.labelInterface.Name = "labelInterface";
            this.labelInterface.Size = new System.Drawing.Size(64, 13);
            this.labelInterface.TabIndex = 2;
            this.labelInterface.Text = "Schnittstelle";
            this.labelInterface.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(95, 35);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(116, 20);
            this.textBoxIpAddress.TabIndex = 0;
            this.textBoxIpAddress.TextChanged += new System.EventHandler(this.textBoxIpAddress_TextChanged);
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(14, 38);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(64, 13);
            this.labelIpAddress.TabIndex = 0;
            this.labelIpAddress.Text = "IP-Addresse";
            this.labelIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxInternetAccess
            // 
            this.checkBoxInternetAccess.AutoSize = true;
            this.checkBoxInternetAccess.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxInternetAccess.Enabled = false;
            this.checkBoxInternetAccess.Location = new System.Drawing.Point(8, 87);
            this.checkBoxInternetAccess.Name = "checkBoxInternetAccess";
            this.checkBoxInternetAccess.Size = new System.Drawing.Size(102, 17);
            this.checkBoxInternetAccess.TabIndex = 2;
            this.checkBoxInternetAccess.Text = "Internet Zugang";
            this.checkBoxInternetAccess.UseVisualStyleBackColor = true;
            this.checkBoxInternetAccess.CheckedChanged += new System.EventHandler(this.checkBoxInternetAccess_CheckedChanged);
            // 
            // comboBoxInterfaces
            // 
            this.comboBoxInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterfaces.FormattingEnabled = true;
            this.comboBoxInterfaces.Location = new System.Drawing.Point(95, 60);
            this.comboBoxInterfaces.Name = "comboBoxInterfaces";
            this.comboBoxInterfaces.Size = new System.Drawing.Size(116, 21);
            this.comboBoxInterfaces.TabIndex = 2;
            this.comboBoxInterfaces.SelectedIndexChanged += new System.EventHandler(this.comboBoxInterfaces_SelectedIndexChanged);
            // 
            // GwConfigControl
            // 
            this.Controls.Add(this.comboBoxInterfaces);
            this.Controls.Add(this.checkBoxInternetAccess);
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.labelInterface);
            this.Controls.Add(this.textBoxIpAddress);
            this.Name = "GwConfigControl";
            this.Size = new System.Drawing.Size(214, 107);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.textBoxIpAddress, 0);
            this.Controls.SetChildIndex(this.labelInterface, 0);
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.Controls.SetChildIndex(this.checkBoxInternetAccess, 0);
            this.Controls.SetChildIndex(this.comboBoxInterfaces, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelInterface;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label labelIpAddress;
        private System.Windows.Forms.CheckBox checkBoxInternetAccess;
        private System.Windows.Forms.ComboBox comboBoxInterfaces;
    }
}
