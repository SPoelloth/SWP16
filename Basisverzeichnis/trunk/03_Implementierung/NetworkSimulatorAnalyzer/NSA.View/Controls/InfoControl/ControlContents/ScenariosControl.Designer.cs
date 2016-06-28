namespace NSA.View.Controls.InfoControl.ControlContents
{
    partial class ScenariosControl
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
            this.dgvScenario = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScenario)).BeginInit();
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
            // dgvScenario
            // 
            this.dgvScenario.AllowUserToAddRows = false;
            this.dgvScenario.AllowUserToDeleteRows = false;
            this.dgvScenario.AllowUserToResizeRows = false;
            this.dgvScenario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScenario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvScenario.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvScenario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScenario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScenario.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvScenario.Location = new System.Drawing.Point(0, 0);
            this.dgvScenario.MultiSelect = false;
            this.dgvScenario.Name = "dgvScenario";
            this.dgvScenario.RowHeadersVisible = false;
            this.dgvScenario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvScenario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScenario.Size = new System.Drawing.Size(738, 150);
            this.dgvScenario.TabIndex = 1;
            this.dgvScenario.Tag = "";
            this.dgvScenario.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvScenario_CellContentClick);
            // 
            // ScenariosControl
            // 
            this.Controls.Add(this.dgvScenario);
            this.Controls.Add(this.dgvHistory);
            this.Name = "ScenariosControl";
            this.Size = new System.Drawing.Size(738, 150);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScenario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridView dgvScenario;
    }
}
