using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class InterfaceConfigControl : ConfigControlBase
    {
        private bool ipValidInput = false, subnetMaskValidInput = false;
        public event Action<string, IPAddress, IPAddress> InterfaceChanged;
        public string InterfaceName;

        public InterfaceConfigControl(IPAddress Ip, IPAddress Subnetmask, string name)
        {
            InitializeComponent();
            labelName.Text = InterfaceName = name;
            textBoxIpAddress.Text = Ip.ToString();
            textBoxSubnetMask.Text = Subnetmask.ToString();
            initialized = true;
        }

        private void OnDataChanged()
        {
            if (initialized && ipValidInput && subnetMaskValidInput)
            {
                InterfaceChanged?.Invoke(labelName.Text, IPAddress.Parse(textBoxIpAddress.Text), IPAddress.Parse(textBoxSubnetMask.Text));
            }
        }

        private void textBoxIpAddress_TextChanged(object sender, EventArgs E) {
            if (IsValidIP(textBoxIpAddress.Text)) {
                textBoxIpAddress.BackColor = SystemColors.Window;
                ipValidInput = true;
                OnDataChanged();
            }
            else {
                textBoxIpAddress.BackColor = Color.Red;
                ipValidInput = false;
            }
        }

        private void textBoxSubnetMask_TextChanged(object sender, EventArgs E)
        {
            if (IsValidIP(textBoxSubnetMask.Text))
            {
                textBoxSubnetMask.BackColor = SystemColors.Window;
                subnetMaskValidInput = true;
                OnDataChanged();
            } else {
                textBoxSubnetMask.BackColor = Color.Red;
                ipValidInput = false;
            }
        }

        protected void ipInput_TextboxKeyPress(object sender, KeyPressEventArgs E) {
            if (!char.IsControl(E.KeyChar) && !char.IsDigit(E.KeyChar) &&
                (E.KeyChar != '.')) {
                E.Handled = true;
            }
        }
    }
}
