using System;
using System.Net;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class GWConfigControl : ConfigControlBase
    {
        public event Action<IPAddress> GatewayChanged;

        public GWConfigControl(IPAddress ip)
        {
            InitializeComponent();
        }
    }
}
