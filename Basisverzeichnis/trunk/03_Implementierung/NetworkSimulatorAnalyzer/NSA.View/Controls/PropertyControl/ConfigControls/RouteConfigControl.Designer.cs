namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    partial class RouteConfigControl
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
            this.labelDestination = new System.Windows.Forms.Label();
            this.textBoxDestination = new System.Windows.Forms.TextBox();
            this.textBoxRoute = new System.Windows.Forms.TextBox();
            this.labelInterface = new System.Windows.Forms.Label();
            this.labelRoute = new System.Windows.Forms.Label();
            this.labelSubnetMask = new System.Windows.Forms.Label();
            this.textBoxSubnetMask = new System.Windows.Forms.TextBox();
            this.comboBoxInterfaces = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Size = new System.Drawing.Size(60, 24);
            this.labelName.Text = "Route";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(191, 4);
            this.buttonClose.TabIndex = 4;
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Location = new System.Drawing.Point(47, 38);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(37, 13);
            this.labelDestination.TabIndex = 12;
            this.labelDestination.Text = "Ziel IP";
            this.labelDestination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDestination
            // 
            this.textBoxDestination.Location = new System.Drawing.Point(95, 35);
            this.textBoxDestination.Name = "textBoxDestination";
            this.textBoxDestination.Size = new System.Drawing.Size(116, 20);
            this.textBoxDestination.TabIndex = 0;
            this.textBoxDestination.TextChanged += new System.EventHandler(this.textBoxDestination_TextChanged);
            this.textBoxDestination.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // textBoxRoute
            // 
            this.textBoxRoute.Location = new System.Drawing.Point(95, 61);
            this.textBoxRoute.Name = "textBoxRoute";
            this.textBoxRoute.Size = new System.Drawing.Size(116, 20);
            this.textBoxRoute.TabIndex = 1;
            this.textBoxRoute.TextChanged += new System.EventHandler(this.textBoxRoute_TextChanged);
            this.textBoxRoute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // labelInterface
            // 
            this.labelInterface.AutoSize = true;
            this.labelInterface.Location = new System.Drawing.Point(20, 116);
            this.labelInterface.Name = "labelInterface";
            this.labelInterface.Size = new System.Drawing.Size(64, 13);
            this.labelInterface.TabIndex = 16;
            this.labelInterface.Text = "Schnittstelle";
            this.labelInterface.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRoute
            // 
            this.labelRoute.AutoSize = true;
            this.labelRoute.Location = new System.Drawing.Point(22, 64);
            this.labelRoute.Name = "labelRoute";
            this.labelRoute.Size = new System.Drawing.Size(62, 13);
            this.labelRoute.TabIndex = 15;
            this.labelRoute.Text = "Gateway IP";
            this.labelRoute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSubnetMask
            // 
            this.labelSubnetMask.AutoSize = true;
            this.labelSubnetMask.Location = new System.Drawing.Point(3, 90);
            this.labelSubnetMask.Name = "labelSubnetMask";
            this.labelSubnetMask.Size = new System.Drawing.Size(81, 13);
            this.labelSubnetMask.TabIndex = 18;
            this.labelSubnetMask.Text = "Subnetz Maske";
            this.labelSubnetMask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSubnetMask
            // 
            this.textBoxSubnetMask.Location = new System.Drawing.Point(95, 87);
            this.textBoxSubnetMask.Name = "textBoxSubnetMask";
            this.textBoxSubnetMask.Size = new System.Drawing.Size(116, 20);
            this.textBoxSubnetMask.TabIndex = 2;
            this.textBoxSubnetMask.TextChanged += new System.EventHandler(this.textBoxSubnetMask_TextChanged);
            this.textBoxSubnetMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // comboBoxInterfaces
            // 
            this.comboBoxInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterfaces.FormattingEnabled = true;
            this.comboBoxInterfaces.Location = new System.Drawing.Point(95, 113);
            this.comboBoxInterfaces.Name = "comboBoxInterfaces";
            this.comboBoxInterfaces.Size = new System.Drawing.Size(116, 21);
            this.comboBoxInterfaces.TabIndex = 3;
            this.comboBoxInterfaces.SelectedIndexChanged += new System.EventHandler(this.comboBoxInterfaces_SelectedIndexChanged);
            // 
            // RouteConfigControl
            // 
            this.Controls.Add(this.comboBoxInterfaces);
            this.Controls.Add(this.labelSubnetMask);
            this.Controls.Add(this.textBoxSubnetMask);
            this.Controls.Add(this.textBoxRoute);
            this.Controls.Add(this.labelInterface);
            this.Controls.Add(this.labelRoute);
            this.Controls.Add(this.labelDestination);
            this.Controls.Add(this.textBoxDestination);
            this.Name = "RouteConfigControl";
            this.Size = new System.Drawing.Size(214, 141);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.textBoxDestination, 0);
            this.Controls.SetChildIndex(this.labelDestination, 0);
            this.Controls.SetChildIndex(this.labelRoute, 0);
            this.Controls.SetChildIndex(this.labelInterface, 0);
            this.Controls.SetChildIndex(this.textBoxRoute, 0);
            this.Controls.SetChildIndex(this.textBoxSubnetMask, 0);
            this.Controls.SetChildIndex(this.labelSubnetMask, 0);
            this.Controls.SetChildIndex(this.comboBoxInterfaces, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.TextBox textBoxDestination;
        private System.Windows.Forms.TextBox textBoxRoute;
        private System.Windows.Forms.Label labelInterface;
        private System.Windows.Forms.Label labelRoute;
        private System.Windows.Forms.Label labelSubnetMask;
        private System.Windows.Forms.TextBox textBoxSubnetMask;
        private System.Windows.Forms.ComboBox comboBoxInterfaces;
    }
}
