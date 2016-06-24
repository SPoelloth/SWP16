using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace NSA.View.Controls.InfoControl.ControlContents
{
    /// <summary>
    /// Class for the ScenariosControl of the InfoControl. 
    /// It displays a list of all loaded testscenarios.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class ScenariosControl : UserControl
    {
        private readonly DataTable data = new DataTable();
        /// <summary>
        /// Occurs when the start scenario button is clicked.
        /// </summary>
        public event EventHandler StartScenarioButtonClicked;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenariosControl"/> class.
        /// </summary>
        public ScenariosControl()
        {
            InitializeComponent();
            dgvScenario.AutoGenerateColumns = false;
            AddColumns();
        }

        /// <summary>
        /// Adds the columns to the datatable and datagridview.
        /// </summary>
        private void AddColumns()
        {
            data.Columns.Add("Testszenario", typeof(string));

            var dataCol1 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Testszenario",
                DataPropertyName = "Testszenario",
                DisplayIndex = 1
            };

            var bnCol = new DataGridViewButtonColumn
            {
                HeaderText = "Aktion",
                Text = "Ausführen",
                UseColumnTextForButtonValue = true,
                DisplayIndex = 2
            };

            dgvScenario.DataSource = data;
            dgvScenario.Columns.AddRange(dataCol1, bnCol);
        }

        /// <summary>
        /// Adds the test scenario.
        /// </summary>
        /// <param name="ScenarioName">Name of the scenario.</param>
        public void AddTestScenario(string ScenarioName)
        {
            var row = data.NewRow();
            row.ItemArray = new object[] { ScenarioName };

            data.Rows.InsertAt(row, 0);
        }

        #region Eventhandling

        /// <summary>
        /// Handles the CellContentClick event of the dgvScenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void dgvScenario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                StartScenarioButtonClicked?.Invoke(data.Rows[e.RowIndex], new EventArgs());
            }
        }
        #endregion


        /// <summary>
        /// Clears this tab.
        /// </summary>
        public void Clear()
        {
            data.Rows.Clear();
        }
    }
}
