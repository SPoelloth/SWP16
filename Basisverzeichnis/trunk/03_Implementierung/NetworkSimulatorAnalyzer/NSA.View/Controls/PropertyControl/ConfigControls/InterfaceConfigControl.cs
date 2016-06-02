using System;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class InterfaceConfigControl : ConfigControlBase
    {
        public event Action<string, IPAddress, IPAddress> InterfaceChanged;

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
