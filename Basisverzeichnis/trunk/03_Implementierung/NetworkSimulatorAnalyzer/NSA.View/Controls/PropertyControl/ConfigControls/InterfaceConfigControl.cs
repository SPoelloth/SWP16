using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ComponentRepresentation
{
    public partial class InterfaceConfigControl : ConfigControlBase
    {
        public InterfaceConfigControl(IPAddress ip, IPAddress subnetmask, string name)
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
