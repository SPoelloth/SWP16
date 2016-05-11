using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ComponentRepresentation
{
    public partial class GWConfigControl : ConfigControlBase
    {
        public GWConfigControl(IPAddress ip, string name)
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
