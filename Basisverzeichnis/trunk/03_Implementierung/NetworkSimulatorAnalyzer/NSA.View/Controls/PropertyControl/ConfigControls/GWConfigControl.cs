using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class GwConfigControl : ConfigControlBase
    {
        private static List<string> interfaceList;
        private bool ipValidInput;
        public event Action<IPAddress, string, bool> GatewayChanged;

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
            comboBoxInterfaces.SelectedIndex = comboBoxInterfaces.FindStringExact(InterfaceName);
            initialized = true;
        }

        public static void SetInterfaces(List<string> Interfaces) {
            interfaceList = Interfaces;
        }

        private void OnDataChanged()
        {
            if (initialized && ipValidInput)
            {
                GatewayChanged?.Invoke(IPAddress.Parse(textBoxIpAddress.Text), comboBoxInterfaces.SelectedText, checkBoxInternetAccess.Checked);
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
                GatewayChanged?.Invoke(IPAddress.Parse(textBoxIpAddress.Text), comboBoxInterfaces.SelectedText, checkBoxInternetAccess.Checked);
            }
        }

        private void comboBoxInterfaces_SelectedIndexChanged(object Sender, EventArgs E) {
            if (initialized) {
                GatewayChanged?.Invoke(IPAddress.Parse(textBoxIpAddress.Text), comboBoxInterfaces.SelectedText, checkBoxInternetAccess.Checked);
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