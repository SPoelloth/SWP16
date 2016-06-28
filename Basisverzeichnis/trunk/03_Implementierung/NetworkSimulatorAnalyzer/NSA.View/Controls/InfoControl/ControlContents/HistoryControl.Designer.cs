namespace NSA.View.Controls.InfoControl.ControlContents
{
    partial class HistoryControl
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
            this.bnClearHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToResizeRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvHistory.Location = new System.Drawing.Point(0, 29);
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(738, 121);
            this.dgvHistory.TabIndex = 0;
            this.dgvHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistory_CellContentClick);
            this.dgvHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvHistory_KeyDown);
            // 
            // bnClearHistory
            // 
            this.bnClearHistory.Location = new System.Drawing.Point(3, 0);
            this.bnClearHistory.Name = "bnClearHistory";
            this.bnClearHistory.Size = new System.Drawing.Size(101, 23);
            this.bnClearHistory.TabIndex = 1;
            this.bnClearHistory.Text = "History leeren";
            this.bnClearHistory.UseVisualStyleBackColor = true;
            this.bnClearHistory.Click += new System.EventHandler(this.bnClearHistory_Click);
            // 
            // HistoryControl
            // 
            this.Controls.Add(this.bnClearHistory);
            this.Controls.Add(this.dgvHistory);
            this.Name = "HistoryControl";
            this.Size = new System.Drawing.Size(738, 150);
            this.Tag = "HistoryTabPage";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button bnClearHistory;
    }
}
