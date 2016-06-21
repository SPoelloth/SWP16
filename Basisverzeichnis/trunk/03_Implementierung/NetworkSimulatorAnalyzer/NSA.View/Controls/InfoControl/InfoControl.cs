using System.Windows.Forms;
using NSA.View.Controls.InfoControl.ControlContents;

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

        }

        /// <summary>
        /// Gets the tab control.
        /// </summary>
        /// <returns></returns>
        public TabControl GetTabControl()
        {
            return tcPages;
        }
    }
}
