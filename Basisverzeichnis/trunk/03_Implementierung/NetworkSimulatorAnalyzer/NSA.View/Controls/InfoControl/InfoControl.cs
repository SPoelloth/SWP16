using System;
using System.Windows.Forms;
using NSA.View.Controls.InfoControl.ControlContents;

namespace NSA.View.Controls.InfoControl
{
    /// <summary>
    /// Class for the InfoControl of the main form.
    /// The InfoControl displays information about executed simulations, hops and testscenarios.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class InfoControl : UserControl
    {
        /// <summary>
        /// Occurs when hopsTabPage is selected.
        /// </summary>
        public event EventHandler HopsTabPage_Selected;
        /// <summary>
        /// Occurs when hopsTabPage is deselected.
        /// </summary>
        public event EventHandler HopsTabPage_Deselected;

        /// <summary>
        /// Property for the tab control
        /// </summary>
        public TabControl TcPages { get; set; }
        /// <summary>
        /// Property for the History control
        /// </summary>
        public HistoryControl HistoryControl => historyControl;

        /// <summary>
        /// Property for the HopsControl control
        /// </summary>
        public HopsControl HopsControl => hopsControl;

        /// <summary>
        /// Property for the Scenarios control
        /// </summary>
        public ScenariosControl ScenariosControl => scenariosControl;

        /// <summary>
        /// Property for the Results control
        /// </summary>
        public ResultsControl ResultsControl => resultsControl;

        /// <summary>
        /// Property for the HopVisualization control
        /// </summary>
        public HopVisualizationControl HopVisualizationControl => hopVisualizationControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoControl"/> class.
        /// </summary>
        public InfoControl()
        {
            InitializeComponent();

        }

        #region Eventhandling
        /// <summary>
        /// Handles the Selected event of the tcPages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TabControlEventArgs"/> instance containing the event data.</param>
        private void tcPages_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPageHops) HopsTabPage_Selected?.Invoke(this, EventArgs.Empty);
            Refresh();
            TcPages.PerformLayout();
        }

        /// <summary>
        /// Handles the Deselected event of the tcPages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TabControlEventArgs"/> instance containing the event data.</param>
        private void tcPages_Deselected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPageHops) HopsTabPage_Deselected?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        /// <summary>
        /// Changes over to the results tab page.
        /// </summary>
        public void ChangeToResultsTab()
        {
            TcPages.SelectedTab = tabPageResults;
        }
    }
}
