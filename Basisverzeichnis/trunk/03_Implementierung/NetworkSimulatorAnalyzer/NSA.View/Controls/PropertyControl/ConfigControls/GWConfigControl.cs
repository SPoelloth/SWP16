using System;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class GWConfigControl : ConfigControlBase
    {
        public event Action<IPAddress> GatewayChanged;

        public GWConfigControl(IPAddress ip)
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
