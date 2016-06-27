using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    /// <summary>
    /// Control for configuring a route
    /// </summary>
    public partial class RouteConfigControl : ConfigControlBase
    {
        private static List<string> interfaceList;
        private bool netMaskValidInput = false, destinationValidInput = false, routeValidInput = false;
        /// <summary>
        /// Name of the route
        /// </summary>
        public readonly string RouteName;
        /// <summary>
        /// Is fired when the Route has changed and its inputs are valid
        /// </summary>
        public event Action<string, IPAddress, IPAddress, IPAddress, string> RouteChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="RouteName">Name of the route</param>
        /// <param name="Destination">Target of the Route</param>
        /// <param name="Route">IP of the gateway for this route</param>
        /// <param name="SubnetMask">Subnetmask of the route</param>
        /// <param name="InterfaceName">Name of the interface to be used</param>
        public RouteConfigControl(string RouteName, IPAddress Destination, IPAddress Route, IPAddress SubnetMask, string InterfaceName)
        {
            this.RouteName = RouteName;
            InitializeComponent();
            if (interfaceList == null)
            {
                throw new InvalidDataException();
            }
            else
            {
                this.comboBoxInterfaces.Items.AddRange(interfaceList.ToArray());

            }
            textBoxDestination.Text = Destination.ToString();
            textBoxRoute.Text = Route.ToString();
            textBoxSubnetMask.Text = SubnetMask.ToString();
            comboBoxInterfaces.SelectedIndex = comboBoxInterfaces.FindStringExact(InterfaceName);
            initialized = true;
        }

        /// <summary>
        /// Sets the list of interfaces of the current hardwarenode.
        /// </summary>
        /// <param name="Interfaces">List of interfaces of the current hardwarenode</param>
        public static void SetInterfaces(List<string> Interfaces)
        {
            interfaceList = Interfaces;
        }

        private void OnDataChanged() {
            if (initialized && destinationValidInput && netMaskValidInput && routeValidInput && comboBoxInterfaces.SelectedIndex > -1) {
                RouteChanged?.Invoke(RouteName, IPAddress.Parse(textBoxDestination.Text), IPAddress.Parse(textBoxRoute.Text), IPAddress.Parse(textBoxSubnetMask.Text), comboBoxInterfaces.SelectedItem.ToString());
            }
        }

        private void comboBoxInterfaces_SelectedIndexChanged(object Sender, EventArgs E) {
            OnDataChanged();
        }

        private void textBoxDestination_TextChanged(object Sender, EventArgs E)
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

        private void textBoxRoute_TextChanged(object Sender, EventArgs E)
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

        private void textBoxSubnetMask_TextChanged(object Sender, EventArgs E)
        {
            if (IsValidIP(textBoxSubnetMask.Text) && IsValidSubnetMask(IPAddress.Parse(textBoxSubnetMask.Text)))
            {
                textBoxSubnetMask.BackColor = SystemColors.Window;
                netMaskValidInput = true;
                OnDataChanged();
            } else {
                textBoxSubnetMask.BackColor = Color.Red;
                netMaskValidInput = false;
            }
        }

        private void ipInput_TextboxKeyPress(object Sender, KeyPressEventArgs E)
        {
            if (!char.IsControl(E.KeyChar) && !char.IsDigit(E.KeyChar) &&
                (E.KeyChar != '.'))
            {
                E.Handled = true;
            }
        }
    }
}
