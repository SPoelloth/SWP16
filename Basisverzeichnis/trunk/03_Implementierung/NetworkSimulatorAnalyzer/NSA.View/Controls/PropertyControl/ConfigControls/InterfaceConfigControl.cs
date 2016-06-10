using System;
using System.Net;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class InterfaceConfigControl : ConfigControlBase
    {
        public event Action<string, IPAddress, IPAddress> InterfaceChanged;

        public InterfaceConfigControl(IPAddress ip, IPAddress subnetmask, string name)
        {
            InitializeComponent();
        }
    }
}
