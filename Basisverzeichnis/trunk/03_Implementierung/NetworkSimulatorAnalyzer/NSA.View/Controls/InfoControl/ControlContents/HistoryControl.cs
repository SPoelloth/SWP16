using System;
using System.Data;
using System.Windows.Forms;

namespace NSA.View.Controls.InfoControl.ControlContents
{
    public partial class HistoryControl : UserControl
    {
        private DataTable data = new DataTable();
        /// <summary>
        /// Occurs when [history rerun button is clicked].
        /// </summary>
        public event EventHandler HistoryRerunButtonClicked;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryControl"/> class.
        /// </summary>
        public HistoryControl()
        {
            InitializeComponent();
            dgvHistory.AutoGenerateColumns = false;
            AddColumns();
        }

        /// <summary>
        /// Adds the columns to the datatable and datagridview.
        /// </summary>
        private void AddColumns()
        {
            data.Columns.Add("Simulationen", typeof(string));
            data.Columns.Add("Ergebnis", typeof(string));
            data.Columns.Add("Start", typeof(string));
            data.Columns.Add("Ziel", typeof(string));

            var dataCol1 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Simulation",
                DataPropertyName = "Simulationen",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 1
            };

            var dataCol2 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ergebnis",
                DataPropertyName = "Ergebnis",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 2
            };

            var dataCol3 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Start",
                DataPropertyName = "Start",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 3
            };

            var dataCol4 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ziel",
                DataPropertyName = "Ziel",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 4
            };

            var bnCol = new DataGridViewButtonColumn
            {
                HeaderText = "Aktion",
                Text = "Erneut Ausführen",
                UseColumnTextForButtonValue = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 5
            };

            dgvHistory.DataSource = data;
            dgvHistory.Columns.AddRange(dataCol1, dataCol2, dataCol3, dataCol4, bnCol);
        }


        /// <summary>
        /// Adds the history data.
        /// </summary>
        /// <param name="SimName">Name of the Simulation.</param>
        /// <param name="Result">The result.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Destination">The destination.</param>
        public void AddHistoryData(string SimName, string Result, string Source, string Destination)
        {
            var row = data.NewRow();
            row.ItemArray = new object[] {SimName, Result, Source, Destination};

            data.Rows.InsertAt(row, 0);
        }

        #region Eventhandling
        /// <summary>
        /// Handles the CellContentClick event of the dgvHistory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                HistoryRerunButtonClicked?.Invoke(data.Rows[e.RowIndex], new EventArgs());
            }
        }
        #endregion
    }
}
