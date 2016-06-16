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
            this.textBoxInterface = new System.Windows.Forms.TextBox();
            this.labelSubnetMask = new System.Windows.Forms.Label();
            this.textBoxSubnetMask = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.TabIndex = 4;
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Location = new System.Drawing.Point(14, 38);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(60, 13);
            this.labelDestination.TabIndex = 12;
            this.labelDestination.Text = "Destination";
            this.labelDestination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDestination
            // 
            this.textBoxDestination.Location = new System.Drawing.Point(78, 35);
            this.textBoxDestination.Name = "textBoxDestination";
            this.textBoxDestination.Size = new System.Drawing.Size(95, 20);
            this.textBoxDestination.TabIndex = 0;
            this.textBoxDestination.TextChanged += new System.EventHandler(this.textBoxDestination_TextChanged);
            this.textBoxDestination.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // textBoxRoute
            // 
            this.textBoxRoute.Location = new System.Drawing.Point(78, 61);
            this.textBoxRoute.Name = "textBoxRoute";
            this.textBoxRoute.Size = new System.Drawing.Size(95, 20);
            this.textBoxRoute.TabIndex = 1;
            this.textBoxRoute.TextChanged += new System.EventHandler(this.textBoxRoute_TextChanged);
            this.textBoxRoute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // labelInterface
            // 
            this.labelInterface.AutoSize = true;
            this.labelInterface.Location = new System.Drawing.Point(23, 116);
            this.labelInterface.Name = "labelInterface";
            this.labelInterface.Size = new System.Drawing.Size(49, 13);
            this.labelInterface.TabIndex = 16;
            this.labelInterface.Text = "Interface";
            this.labelInterface.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRoute
            // 
            this.labelRoute.AutoSize = true;
            this.labelRoute.Location = new System.Drawing.Point(36, 64);
            this.labelRoute.Name = "labelRoute";
            this.labelRoute.Size = new System.Drawing.Size(36, 13);
            this.labelRoute.TabIndex = 15;
            this.labelRoute.Text = "Route";
            this.labelRoute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxInterface
            // 
            this.textBoxInterface.Location = new System.Drawing.Point(78, 113);
            this.textBoxInterface.Name = "textBoxInterface";
            this.textBoxInterface.Size = new System.Drawing.Size(95, 20);
            this.textBoxInterface.TabIndex = 3;
            this.textBoxInterface.TextChanged += new System.EventHandler(this.textBoxInterface_TextChanged);
            // 
            // labelSubnetMask
            // 
            this.labelSubnetMask.AutoSize = true;
            this.labelSubnetMask.Location = new System.Drawing.Point(5, 90);
            this.labelSubnetMask.Name = "labelSubnetMask";
            this.labelSubnetMask.Size = new System.Drawing.Size(70, 13);
            this.labelSubnetMask.TabIndex = 18;
            this.labelSubnetMask.Text = "Subnet Mask";
            this.labelSubnetMask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSubnetMask
            // 
            this.textBoxSubnetMask.Location = new System.Drawing.Point(78, 87);
            this.textBoxSubnetMask.Name = "textBoxSubnetMask";
            this.textBoxSubnetMask.Size = new System.Drawing.Size(95, 20);
            this.textBoxSubnetMask.TabIndex = 2;
            this.textBoxSubnetMask.TextChanged += new System.EventHandler(this.textBoxSubnetMask_TextChanged);
            this.textBoxSubnetMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipInput_TextboxKeyPress);
            // 
            // RouteConfigControl
            // 
            this.Controls.Add(this.labelSubnetMask);
            this.Controls.Add(this.textBoxSubnetMask);
            this.Controls.Add(this.textBoxRoute);
            this.Controls.Add(this.labelInterface);
            this.Controls.Add(this.labelRoute);
            this.Controls.Add(this.textBoxInterface);
            this.Controls.Add(this.labelDestination);
            this.Controls.Add(this.textBoxDestination);
            this.Name = "RouteConfigControl";
            this.Size = new System.Drawing.Size(185, 141);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.textBoxDestination, 0);
            this.Controls.SetChildIndex(this.labelDestination, 0);
            this.Controls.SetChildIndex(this.textBoxInterface, 0);
            this.Controls.SetChildIndex(this.labelRoute, 0);
            this.Controls.SetChildIndex(this.labelInterface, 0);
            this.Controls.SetChildIndex(this.textBoxRoute, 0);
            this.Controls.SetChildIndex(this.textBoxSubnetMask, 0);
            this.Controls.SetChildIndex(this.labelSubnetMask, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.TextBox textBoxDestination;
        private System.Windows.Forms.TextBox textBoxRoute;
        private System.Windows.Forms.Label labelInterface;
        private System.Windows.Forms.Label labelRoute;
        private System.Windows.Forms.TextBox textBoxInterface;
        private System.Windows.Forms.Label labelSubnetMask;
        private System.Windows.Forms.TextBox textBoxSubnetMask;
    }
}
