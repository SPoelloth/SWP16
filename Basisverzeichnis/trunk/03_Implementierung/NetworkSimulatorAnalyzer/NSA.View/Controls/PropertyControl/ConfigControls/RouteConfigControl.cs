using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class RouteConfigControl : ConfigControlBase
    {
        private bool netMaskValidInput = false, destinationValidInput = false, routeValidInput = false, interfaceValidInput = false;

        public event Action<IPAddress, IPAddress, IPAddress, string> RouteChanged;

        public RouteConfigControl(IPAddress destination, IPAddress route, IPAddress subnetMask, string interfaceName)
        {
            InitializeComponent();
            textBoxDestination.Text = destination.ToString();
            textBoxRoute.Text = route.ToString();
            textBoxSubnetMask.Text = subnetMask.ToString();
            textBoxInterface.Text = interfaceName;
            initialized = true;
        }

        private void OnDataChanged()
        {
            if (initialized && destinationValidInput && netMaskValidInput && routeValidInput && interfaceValidInput)
            {
                RouteChanged?.Invoke(IPAddress.Parse(textBoxDestination.Text), IPAddress.Parse(textBoxRoute.Text), IPAddress.Parse(textBoxSubnetMask.Text), textBoxInterface.Text);
            }
        }

        private void textBoxDestination_TextChanged(object sender, EventArgs e)
        {
            if (IsValidIP(textBoxDestination.Text))
            {
                textBoxDestination.BackColor = SystemColors.Window;
                destinationValidInput = true;
                OnDataChanged();
            } else {
                textBoxDestination.BackColor = Color.Red;
                destinationValidInput = false;
            }
        }

        private void textBoxRoute_TextChanged(object sender, EventArgs e)
        {
            if (IsValidIP(textBoxRoute.Text))
            {
                textBoxRoute.BackColor = SystemColors.Window;
                routeValidInput = true;
                OnDataChanged();
            } else {
                textBoxRoute.BackColor = Color.Red;
                routeValidInput = false;
            }
        }

        private void textBoxSubnetMask_TextChanged(object sender, EventArgs e)
        {
            if (IsValidIP(textBoxSubnetMask.Text))
            {
                textBoxSubnetMask.BackColor = SystemColors.Window;
                netMaskValidInput = true;
                OnDataChanged();
            } else {
                textBoxSubnetMask.BackColor = Color.Red;
                netMaskValidInput = false;
            }
        }

        private void textBoxInterface_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxInterface.Text))
            {
                textBoxInterface.BackColor = SystemColors.Window;
                interfaceValidInput = true;
                OnDataChanged();
            } else {
                textBoxInterface.BackColor = Color.Red;
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
