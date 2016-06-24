using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Forms
{
    /// <summary>
    /// Dialog for starting a simulation with provided parameters.
    /// </summary>
    public partial class AdvancedSimulationForm : Form
    {
        /// <summary>
        /// TTL of the simulation packet.
        /// </summary>
        public int MaxHopCount = 255;
        /// <summary>
        /// Name of the source.
        /// </summary>
        public string SourceName = "";
        /// <summary>
        /// Name of the target.
        /// </summary>
        public string TargetName = "";
        /// <summary>
        /// Expected simulation result.
        /// </summary>
        public bool ExpectedResult = true;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AdvancedSimulationForm()
        {
            InitializeComponent();
            resultCombo.SelectedIndex = 0;
            CanExecute_Start();
        }

        /// <summary>
        /// Sets the list of available workstations.
        /// </summary>
        /// <param name="AvailableWorkstations">List of available workstations.</param>
        public void SetWorkstations(List<string> AvailableWorkstations)
        {
            sourceComboBox.DataSource = AvailableWorkstations;
            // Workaround for having the same DataSource in two different Comboboxes
            targetComboBox.BindingContext = new BindingContext();
            targetComboBox.DataSource = AvailableWorkstations;
            if (AvailableWorkstations.Count > 1)
            {
                targetComboBox.SelectedIndex = 1;
            }
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

        private void targetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            targetComboBox.BackColor = targetComboBox.SelectedIndex < 0 ? Color.Red : SystemColors.Window;
            TargetName = targetComboBox.SelectedItem.ToString();
            CanExecute_Start();
        }

        private void hopCountTextBox_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(hopCountTextBox.Text, out MaxHopCount))
            {
                hopCountTextBox.BackColor = SystemColors.Window;
            }
            else
            {
                hopCountTextBox.BackColor = Color.Red;
                MaxHopCount = -1;
            }
            CanExecute_Start();
        }

        private void resultCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExpectedResult = resultCombo.SelectedIndex == 0;
            CanExecute_Start();
        }

        private void CanExecute_Start()
        {
            startButton.Enabled = !string.IsNullOrWhiteSpace(SourceName) && !string.IsNullOrWhiteSpace(TargetName) && MaxHopCount > 0;
        }

        private void IntOnlyInput_TextboxKeyPress(object sender, KeyPressEventArgs E)
        {
            if (!char.IsControl(E.KeyChar) && !char.IsDigit(E.KeyChar))
            {
                E.Handled = true;
            }
        }
    }
}
