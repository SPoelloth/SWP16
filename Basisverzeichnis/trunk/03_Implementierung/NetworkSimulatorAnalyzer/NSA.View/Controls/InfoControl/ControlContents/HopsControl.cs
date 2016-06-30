using System;
using System.Data;
using System.Windows.Forms;

namespace NSA.View.Controls.InfoControl.ControlContents
{

    /// <summary>
    /// Class for the HopsControl of the InfoControl. 
    /// It displays a list of all packets of the last simulation and shows the corresponding hops.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class HopsControl : UserControl
    {
        /// <summary>
        /// EventHandler delegate for the PacketSelected event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event argument e. It contains the name of the selected packet.</param>
        public delegate void PacketSelectedEventHandler(object sender, string e);
        /// <summary>
        /// Occurs when [packet selected].
        /// </summary>
        public event PacketSelectedEventHandler PacketSelected;
        /// <summary>
        /// Gets the selected packet.
        /// </summary>
        /// <value>
        /// The selected packet.
        /// </value>
        public string SelectedPacket => cbPackets.SelectedItem as string;
        /// <summary>
        /// Is fired when a hop is selected
        /// </summary>
        public event Action<int> HopSelected;

        private readonly DataTable gridData = new DataTable();

        /// <summary>
        /// Initializes a new instance of the <see cref="HopsControl"/> class.
        /// </summary>
        public HopsControl()
        {
            InitializeComponent();
            dgvHops.AutoGenerateColumns = false;
            AddColumns();
            dgvHops.SelectionChanged += DgvHops_SelectionChanged;
            dgvHops.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void DgvHops_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHops.SelectedRows.Count > 0)
            {
                var selection = dgvHops.SelectedRows[0];
                if (selection != null)
                {
                    HopSelected?.Invoke(selection.Index);
                }
            }
        }


        /// <summary>
        /// Adds the columns to the datatable and datagridview.
        /// </summary>
        private void AddColumns()
        {
            gridData.Columns.Add("Start", typeof(string));
            gridData.Columns.Add("ErgebnisStart", typeof(string));
            gridData.Columns.Add("Ziel", typeof(string));
            gridData.Columns.Add("ErgebnisZiel", typeof(string));

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

            dgvHops.DataSource = gridData;
            dgvHops.Columns.AddRange(dataCol1, dataCol2, dataCol3, dataCol4);
        }

        #region Eventhandling

        /// <summary>
        /// Handles the SelectedValueChanged event of the cbPackets control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbPackets_SelectedValueChanged(object sender, EventArgs e)
        {
            PacketSelected?.Invoke(this, cbPackets.SelectedItem as string);
        }

        #endregion


        /// <summary>
        /// Adds the hop.
        /// </summary>
        /// <param name="StartNode">The start node.</param>
        /// <param name="StartResult">The start result.</param>
        /// <param name="EndNode">The end node.</param>
        /// <param name="EndResult">The end result.</param>
        public void AddHop(string StartNode, string StartResult, string EndNode, string EndResult)
        {
            var row = gridData.NewRow();
            row.ItemArray = new object[] { StartNode, StartResult, EndNode, EndResult };

            gridData.Rows.Add(row);
        }

        /// <summary>
        /// Adds the given packet to the packet drop down list.
        /// </summary>
        /// <param name="PacketName">Name of the packet.</param>
        public void AddPacket(string PacketName)
        {
            cbPackets.Items.Add(PacketName);
            cbPackets.SelectedItem = PacketName;
        }
        
        /// <summary>
        /// Clears this tab.
        /// </summary>
        public void Clear()
        {
            cbPackets.Items.Clear();
            gridData.Rows.Clear();
        }

        /// <summary>
        /// Clears only the DataGridView containing the hops of a packet.
        /// </summary>
        public void ClearHopsOnly()
        {
            gridData.Rows.Clear();
        }
    }
}
