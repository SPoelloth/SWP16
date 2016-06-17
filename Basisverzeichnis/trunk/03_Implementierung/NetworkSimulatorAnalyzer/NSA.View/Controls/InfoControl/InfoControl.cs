using System.Windows.Forms;
using NSA.View.Controls.InfoControl.TabPages;

namespace NSA.View.Controls.InfoControl
{
    public partial class InfoControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InfoControl"/> class.
        /// </summary>
        public InfoControl()
        {
            InitializeComponent();
            AddPages();
        }

        /// <summary>
        /// Gets the tab control.
        /// </summary>
        /// <returns></returns>
        public TabControl GetTabControl()
        {
            return tcPages;
        }

        /// <summary>
        /// Adds the pages.
        /// </summary>
        private void AddPages()
        {
            tcPages.TabPages.Add(new HistoryTabPage());
            tcPages.TabPages.Add(new ResultTabPage());
            tcPages.TabPages.Add(new HopsTabPage());
            tcPages.TabPages.Add(new ScenarioTabPage());
        }

        /// <summary>
        /// Adds the new simulation to history.
        /// </summary>
        /// <param name="SimName">Name of the sim.</param>
        /// <param name="Result">The result.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Destination">The destination.</param>
        public void AddNewSimulationToHistory(string SimName, string Result, string Source, string Destination)
        {
            HistoryTabPage tp = tcPages.TabPages["HistoryTabPage"] as HistoryTabPage;
            if (tp != null) tp.AddHistoryData(SimName, Result, Source, Destination);
        }
    }
}
