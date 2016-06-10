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

        public InterfaceConfigControl(IPAddress ip, IPAddress subnetmask, string name)
        {
            InitializeComponent();
            labelName.Text = name;
            textBoxIpAddress.Text = ip.ToString();
            textBoxSubnetMask.Text = subnetmask.ToString();
            initialized = true;
        }

        private void OnDataChanged()
        {
            if (initialized && ipValidInput && subnetMaskValidInput)
            {
                InterfaceChanged?.Invoke(labelName.Text, IPAddress.Parse(textBoxIpAddress.Text), IPAddress.Parse(textBoxSubnetMask.Text));
            }
        }

        private void textBoxIpAddress_TextChanged(object sender, EventArgs e) {
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

        private void textBoxSubnetMask_TextChanged(object sender, EventArgs e)
        {
            if (IsValidIP(textBoxSubnetMask.Text))
            {
                textBoxSubnetMask.BackColor = SystemColors.Window;
                ipValidInput = true;
                OnDataChanged();
            } else {
                textBoxSubnetMask.BackColor = Color.Red;
                ipValidInput = false;
            }
        }

        protected void ipInput_TextboxKeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.')) {
                e.Handled = true;
            }
        }
    }
}
