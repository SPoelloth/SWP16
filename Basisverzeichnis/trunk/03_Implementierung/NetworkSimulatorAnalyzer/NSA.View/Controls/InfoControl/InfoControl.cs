using System.Windows.Forms;
using NSA.View.Controls.InfoControl.TabPages;

namespace NSA.View.Controls.InfoControl
{
    public partial class InfoControl : UserControl
    {
        public InfoControl()
        {
            InitializeComponent();
            AddPages();
        }

        private void AddPages()
        {
            tcPages.TabPages.Add(new HistoryTabPage());
            tcPages.TabPages.Add(new ResultTabPage());
            tcPages.TabPages.Add(new HopsTabPage());
            tcPages.TabPages.Add(new ScenarioTabPage());
        }
    }
}
