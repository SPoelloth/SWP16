using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NSA.View.Forms
{
    public partial class BroadcastSimulationForm : Form
    {
        public string SourceName = "";
        public string TargetSubnet = "";
        public bool ExpectedResult = true;
        private bool subnetValidInput = false;

        public BroadcastSimulationForm()
        {
            InitializeComponent();
            resultCombo.SelectedIndex = 0;
            CanExecute_Start();
        }

        public void SetWorkstations(List<string> AvailableWorkstations)
        {
            sourceComboBox.DataSource = AvailableWorkstations;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.OK;
        }

        private void sourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sourceComboBox.BackColor = sourceComboBox.SelectedIndex < 0 ? Color.Red : SystemColors.Window;
            SourceName = sourceComboBox.SelectedItem.ToString();
            CanExecute_Start();
        }

        private void resultCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExpectedResult = resultCombo.SelectedIndex == 0;
            CanExecute_Start();
        }

        private void CanExecute_Start()
        {
            startButton.Enabled = !string.IsNullOrWhiteSpace(SourceName) && subnetValidInput;
        }

        protected void IntOnlyInput_TextboxKeyPress(object sender, KeyPressEventArgs E)
        {
            if (!char.IsControl(E.KeyChar) && !char.IsDigit(E.KeyChar))
            {
                E.Handled = true;
            }
        }

        private void textBoxSubnet_TextChanged(object sender, EventArgs e)
        {
            if (IsValidSubnetMask(IPAddress.Parse(textBoxSubnet.Text)))
            {
                textBoxSubnet.BackColor = SystemColors.Window;
                subnetValidInput = true;
                TargetSubnet = textBoxSubnet.Text;
                CanExecute_Start();
            }
            else
            {
                textBoxSubnet.BackColor = Color.Red;
                subnetValidInput = false;
                CanExecute_Start();
            }
        }

        private void textBoxSubnet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public static bool IsValidSubnetMask(IPAddress Subnetmask) {
            var address = BitConverter.ToUInt32(Subnetmask.GetAddressBytes().Reverse().ToArray(), 0);
            bool end = false;
            for (int i = 31; i >= 0; i--) {
                if (!end) {
                    end = (address & (1 << i)) == 0;
                }
                if (end && (address & (1 << i)) > 0)
                    return false;
            }
            return true;
        }
    }
}
