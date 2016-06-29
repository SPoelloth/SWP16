using System;
using System.Windows.Forms;

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
            tcPages.SelectedTab = tabPageResults;
        }
    }
}
