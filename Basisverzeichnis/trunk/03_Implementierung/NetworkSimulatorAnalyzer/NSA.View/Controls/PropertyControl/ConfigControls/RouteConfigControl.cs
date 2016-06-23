using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class RouteConfigControl : ConfigControlBase
    {
        private static List<string> interfaceList;
        private bool netMaskValidInput = false, destinationValidInput = false, routeValidInput = false;
        public readonly string RouteName;
        public event Action<string, IPAddress, IPAddress, IPAddress, string> RouteChanged;

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

        public static void SetInterfaces(List<string> Interfaces)
        {
            interfaceList = Interfaces;
        }

        private void OnDataChanged() {
            if (initialized && destinationValidInput && netMaskValidInput && routeValidInput && !String.IsNullOrEmpty(comboBoxInterfaces.SelectedText)) {
                RouteChanged?.Invoke(RouteName, IPAddress.Parse(textBoxDestination.Text), IPAddress.Parse(textBoxRoute.Text), IPAddress.Parse(textBoxSubnetMask.Text), comboBoxInterfaces.SelectedText);
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
            if (IsValidSubnetMask(IPAddress.Parse(textBoxSubnetMask.Text)))
            {
                textBoxSubnetMask.BackColor = SystemColors.Window;
                netMaskValidInput = true;
                OnDataChanged();
            } else {
                textBoxSubnetMask.BackColor = Color.Red;
                netMaskValidInput = false;
            }
        }

        protected void ipInput_TextboxKeyPress(object Sender, KeyPressEventArgs E)
        {
            if (!char.IsControl(E.KeyChar) && !char.IsDigit(E.KeyChar) &&
                (E.KeyChar != '.'))
            {
                E.Handled = true;
            }
        }
    }
}
