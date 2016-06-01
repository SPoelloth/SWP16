using System.Windows.Forms;

namespace NSA.View.Controls.Toolbar
{
    public partial class ToolbarControl : UserControl
    {
        public ToolbarControl()
        {
            InitializeComponent();
        }

        public void AddButton(Button b)
        {
            flpContents.Controls.Add(b);
        }
    }
}
