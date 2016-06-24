using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    /// <summary>
    /// Control for configuring a network interface
    /// </summary>
    public partial class InterfaceConfigControl : ConfigControlBase
    {
        private bool ipValidInput = false, subnetMaskValidInput = false;
        /// <summary>
        /// Is fired when interface data has changed and is valid
        /// </summary>
        public event Action<string, IPAddress, IPAddress> InterfaceChanged;
        /// <summary>
        /// Name of the interface
        /// </summary>
        public string InterfaceName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Ip">The IP of the interface</param>
        /// <param name="Subnetmask">The subnetmask of the interface</param>
        /// <param name="name">The name of the interface (ethX)</param>
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
            if (IsValidIP(textBoxSubnetMask.Text) && IsValidSubnetMask(IPAddress.Parse(textBoxSubnetMask.Text)))
            {
                textBoxSubnetMask.BackColor = SystemColors.Window;
                subnetMaskValidInput = true;
                OnDataChanged();
            } else {
                textBoxSubnetMask.BackColor = Color.Red;
                subnetMaskValidInput = false;
            }
        }

        private void ipInput_TextboxKeyPress(object sender, KeyPressEventArgs E) {
            if (!char.IsControl(E.KeyChar) && !char.IsDigit(E.KeyChar) &&
                (E.KeyChar != '.')) {
                E.Handled = true;
            }
        }
    }
}
