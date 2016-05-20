using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.PropertyControl
{
    public partial class PropertyControl : UserControl
    {
        public PropertyControl()
        {
            InitializeComponent();
        }

        public void AddControl(UserControl control)
        {
            this.flpContents.Controls.Add(control);
        }
    }
}
