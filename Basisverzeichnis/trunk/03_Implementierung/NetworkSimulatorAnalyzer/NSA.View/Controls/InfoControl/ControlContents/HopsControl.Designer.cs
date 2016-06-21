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
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.dgvHops = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHops)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHistory
            // 
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.Size = new System.Drawing.Size(738, 150);
            this.dgvHistory.TabIndex = 0;
            // 
            // dgvHops
            // 
            this.dgvHops.AllowUserToAddRows = false;
            this.dgvHops.AllowUserToDeleteRows = false;
            this.dgvHops.AllowUserToResizeRows = false;
            this.dgvHops.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHops.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHops.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvHops.Location = new System.Drawing.Point(0, 0);
            this.dgvHops.MultiSelect = false;
            this.dgvHops.Name = "dgvHops";
            this.dgvHops.RowHeadersVisible = false;
            this.dgvHops.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHops.Size = new System.Drawing.Size(738, 150);
            this.dgvHops.TabIndex = 1;
            // 
            // HopsControl
            // 
            this.Controls.Add(this.dgvHops);
            this.Controls.Add(this.dgvHistory);
            this.Name = "HopsControl";
            this.Size = new System.Drawing.Size(738, 150);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHops)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridView dgvHops;
    }
}
