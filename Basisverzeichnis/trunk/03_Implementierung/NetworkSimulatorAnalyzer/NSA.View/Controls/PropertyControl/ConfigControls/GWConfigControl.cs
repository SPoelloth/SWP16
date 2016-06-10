using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class GWConfigControl : ConfigControlBase
    {
        private bool ipValidInput, interfaceValidInput;
        public event Action<IPAddress, string> GatewayChanged;

        public GWConfigControl(IPAddress ip, string interfaceName)
        {
            InitializeComponent();
            textBoxIpAddress.Text = ip.ToString();
            textBoxInterface.Text = interfaceName;
            initialized = true;
        }

        private void OnDataChanged()
        {
            if (initialized && ipValidInput && interfaceValidInput)
            {
                GatewayChanged?.Invoke(IPAddress.Parse(textBoxIpAddress.Text), textBoxInterface.Text);
            }
        }

        private void textBoxIpAddress_TextChanged(object sender, EventArgs e)
        {
            if (IsValidIP(textBoxIpAddress.Text))
            {
                textBoxIpAddress.BackColor = SystemColors.Window;
                ipValidInput = true;
                OnDataChanged();
            }
            else
            {
                textBoxIpAddress.BackColor = Color.Red;
                ipValidInput = false;
            }
        }

        private void textBoxInterface_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxInterface.Text))
            {
                textBoxInterface.BackColor = SystemColors.Window;
                interfaceValidInput = true;
                OnDataChanged();
            }
            else
            {
                textBoxIpAddress.BackColor = Color.Red;
                interfaceValidInput = false;
            }
        }

        protected void ipInput_TextboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}