using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class RouteConfigControl : ConfigControlBase
    {
        public RouteConfigControl(IPAddress source, IPAddress destination, IPAddress route, string parameters)
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
