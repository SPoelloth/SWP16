using System.Windows.Forms;

namespace NSA.View.Controls.Toolbar
{
    /// <summary>
    /// Das Werkzeugleistencontrol.
    /// </summary>
    public partial class ToolbarControl : UserControl
    {
        /// <summary>
        /// Der Konstruktor.
        /// </summary>
        public ToolbarControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fügt der Werkzeugleiste einen Knopf hinzu.
        /// </summary>
        /// <param name="b">Der Knopf</param>
        public void AddButton(Button b)
        {
            flpContents.Controls.Add(b);
        }
    }
}
