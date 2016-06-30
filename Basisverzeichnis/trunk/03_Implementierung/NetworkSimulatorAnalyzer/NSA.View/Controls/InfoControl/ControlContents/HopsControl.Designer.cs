namespace NSA.View.Controls.InfoControl.ControlContents
{
    partial class HopsControl
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
            this.dgvHops = new System.Windows.Forms.DataGridView();
            this.cbPackets = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHops)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHops
            // 
            this.dgvHops.AllowUserToAddRows = false;
            this.dgvHops.AllowUserToDeleteRows = false;
            this.dgvHops.AllowUserToResizeRows = false;
            this.dgvHops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHops.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHops.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHops.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHops.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvHops.Location = new System.Drawing.Point(0, 30);
            this.dgvHops.MultiSelect = false;
            this.dgvHops.Name = "dgvHops";
            this.dgvHops.ReadOnly = true;
            this.dgvHops.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvHops.RowHeadersVisible = false;
            this.dgvHops.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHops.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHops.Size = new System.Drawing.Size(738, 120);
            this.dgvHops.TabIndex = 2;
            // 
            // cbPackets
            // 
            this.cbPackets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPackets.FormattingEnabled = true;
            this.cbPackets.Location = new System.Drawing.Point(3, 3);
            this.cbPackets.Name = "cbPackets";
            this.cbPackets.Size = new System.Drawing.Size(121, 21);
            this.cbPackets.TabIndex = 4;
            this.cbPackets.SelectedValueChanged += new System.EventHandler(this.cbPackets_SelectedValueChanged);
            // 
            // HopsControl
            // 
            this.Controls.Add(this.cbPackets);
            this.Controls.Add(this.dgvHops);
            this.Name = "HopsControl";
            this.Size = new System.Drawing.Size(738, 150);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHops)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvHops;
        private System.Windows.Forms.ComboBox cbPackets;
    }
}
