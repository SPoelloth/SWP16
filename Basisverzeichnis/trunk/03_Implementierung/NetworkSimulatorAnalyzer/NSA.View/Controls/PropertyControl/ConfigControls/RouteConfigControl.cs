using System;
using System.Net;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class RouteConfigControl : ConfigControlBase
    {
        public event Action<IPAddress, IPAddress, IPAddress, string> RouteChanged;
        public RouteConfigControl(IPAddress source, IPAddress destination, IPAddress route, string parameters)
        {
            InitializeComponent();
        }
    }
}
