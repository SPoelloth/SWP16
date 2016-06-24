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
