using System.Data;
using System.Windows.Forms;

namespace NSA.View.Controls.InfoControl.ControlContents
{
    // todo: finish hop control
    public partial class HopsControl : UserControl
    {
        private readonly DataTable data = new DataTable();

        /// <summary>
        /// Initializes a new instance of the <see cref="HopsControl"/> class.
        /// </summary>
        public HopsControl()
        {
            InitializeComponent();
            dgvHops.AutoGenerateColumns = false;
            AddColumns();
        }


        /// <summary>
        /// Adds the columns to the datatable and datagridview.
        /// </summary>
        private void AddColumns()
        {
            data.Columns.Add("Start", typeof(string));
            data.Columns.Add("ErgebnisStart", typeof(string));
            data.Columns.Add("Ziel", typeof(string));
            data.Columns.Add("ErgebnisZiel", typeof(string));

            var dataCol1 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Startknoten",
                DataPropertyName = "Start",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 1
            };

            var dataCol2 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ergebnis am Startknoten",
                DataPropertyName = "ErgebnisStart",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 2
            };

            var dataCol3 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Zielknoten",
                DataPropertyName = "Ziel",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 3
            };

            var dataCol4 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ergebnis am Zielknoten",
                DataPropertyName = "ErgebnisZiel",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayIndex = 4
            };

            dgvHops.DataSource = data;
            dgvHops.Columns.AddRange(dataCol1, dataCol2, dataCol3, dataCol4);
        }


        /// <summary>
        /// Adds the hop.
        /// </summary>
        /// <param name="StartNode">The start node.</param>
        /// <param name="StartResult">The start result.</param>
        /// <param name="EndNode">The end node.</param>
        /// <param name="EndResult">The end result.</param>
        public void AddHop(string StartNode, string StartResult, string EndNode, string EndResult)
        {
            var row = data.NewRow();
            row.ItemArray = new object[] { StartNode, StartResult, EndNode, EndResult };

            data.Rows.Add(row);
        }
        
        /// <summary>
        /// Clears this tab.
        /// </summary>
        public void Clear()
        {
            data.Rows.Clear();
        }
    }
}
