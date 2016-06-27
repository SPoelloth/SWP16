using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    /// <summary>
    /// Control for configuring a default gateway
    /// </summary>
    public partial class GwConfigControl : ConfigControlBase
    {
        private static List<string> interfaceList;
        private bool ipValidInput;
        /// <summary>
        /// Is fired when the gateway data has changed and is valid.
        /// </summary>
        public event Action<IPAddress, string, bool> GatewayChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Ip">The IP address</param>
        /// <param name="InterfaceName">The name of the interface to be used</param>
        /// <param name="IsRouter">Flag indicating whether the selected hardwarenode is a router</param>
        /// <param name="HasInternetAccess">Flag indicating whether the selected hardwarenode has internet access</param>
        public GwConfigControl(IPAddress Ip, string InterfaceName, bool IsRouter, bool HasInternetAccess = false)
        {
            InitializeComponent();
            if (interfaceList == null) {
                throw new InvalidDataException();
            } else {
                this.comboBoxInterfaces.Items.AddRange(interfaceList.ToArray());
            }
            if (IsRouter)
            {
                checkBoxInternetAccess.Enabled = true;
                checkBoxInternetAccess.Checked = HasInternetAccess;
            }
            textBoxIpAddress.Text = Ip.ToString();
            if (String.IsNullOrEmpty(InterfaceName))
            {
                comboBoxInterfaces.SelectedIndex = -1;
            }
            else
            {
                comboBoxInterfaces.SelectedIndex = comboBoxInterfaces.FindStringExact(InterfaceName);
            }
            initialized = true;
        }

        /// <summary>
        /// Sets the list of interfaces for this hardwarenode
        /// </summary>
        /// <param name="Interfaces"></param>
        public static void SetInterfaces(List<string> Interfaces) {
            interfaceList = Interfaces;
        }

        private void OnDataChanged()
        {
            if (initialized && ipValidInput && comboBoxInterfaces.SelectedIndex > -1)
            {
                GatewayChanged?.Invoke(IPAddress.Parse(textBoxIpAddress.Text), comboBoxInterfaces.SelectedItem.ToString(), checkBoxInternetAccess.Checked);
            }
        }

        private void textBoxIpAddress_TextChanged(object Sender, EventArgs E)
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

        private void checkBoxInternetAccess_CheckedChanged(object Sender, EventArgs E) {
            if (initialized)
            {
                OnDataChanged();
            }
        }

        private void comboBoxInterfaces_SelectedIndexChanged(object Sender, EventArgs E) {
            if (initialized)
            {
                OnDataChanged();
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