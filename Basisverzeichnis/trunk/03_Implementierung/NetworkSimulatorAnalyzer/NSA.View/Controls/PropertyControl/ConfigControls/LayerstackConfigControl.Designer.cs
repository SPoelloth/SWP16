namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    partial class LayerstackConfigControl
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
            this.flpLayers = new System.Windows.Forms.FlowLayoutPanel();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Size = new System.Drawing.Size(98, 24);
            this.labelName.Text = "Layerstack";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(191, 4);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Visible = false;
            // 
            // flpLayers
            // 
            this.flpLayers.AutoScroll = true;
            this.flpLayers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLayers.Location = new System.Drawing.Point(0, 40);
            this.flpLayers.Name = "flpLayers";
            this.flpLayers.Padding = new System.Windows.Forms.Padding(1);
            this.flpLayers.Size = new System.Drawing.Size(186, 158);
            this.flpLayers.TabIndex = 3;
            this.flpLayers.WrapContents = false;
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(191, 40);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(19, 23);
            this.btUp.TabIndex = 0;
            this.btUp.Text = "↑";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(191, 69);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(19, 23);
            this.btDown.TabIndex = 1;
            this.btDown.Text = "↓";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(191, 146);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(19, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "+";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDel
            // 
            this.btDel.Location = new System.Drawing.Point(191, 175);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(19, 23);
            this.btDel.TabIndex = 3;
            this.btDel.Text = "-";
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // LayerstackConfigControl
            // 
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.flpLayers);
            this.Name = "LayerstackConfigControl";
            this.Size = new System.Drawing.Size(214, 201);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.flpLayers, 0);
            this.Controls.SetChildIndex(this.btUp, 0);
            this.Controls.SetChildIndex(this.btDown, 0);
            this.Controls.SetChildIndex(this.btAdd, 0);
            this.Controls.SetChildIndex(this.btDel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpLayers;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDel;
    }
}
