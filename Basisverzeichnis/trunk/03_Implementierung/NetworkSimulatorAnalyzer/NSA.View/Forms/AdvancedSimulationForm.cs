using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSA.View.Forms
{
    public partial class AdvancedSimulationForm : Form
    {
        public int AnimationTime = -1;
        public int MaxHopCount = -1;
        public string SourceName = "";
        public string TargetName = "";
        public int ExpectedResult = -1;

        public AdvancedSimulationForm()
        {
            InitializeComponent();
            resultCombo.SelectedIndex = 0;
            CanExecute_Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.OK;
        }

        private void sourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sourceComboBox.BackColor = sourceComboBox.SelectedIndex < 0 ? Color.Red : SystemColors.Window;
            SourceName = sourceComboBox.SelectedText;
            CanExecute_Start();
        }

        private void targetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            targetComboBox.BackColor = targetComboBox.SelectedIndex < 0 ? Color.Red : SystemColors.Window;
            TargetName = targetComboBox.SelectedText;
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

        private void animationTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            // todo
            CanExecute_Start();
        }

        private void resultCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExpectedResult = resultCombo.SelectedIndex;
            CanExecute_Start();
        }

        private void CanExecute_Start()
        {
            startButton.Enabled = !string.IsNullOrWhiteSpace(SourceName) && !string.IsNullOrWhiteSpace(TargetName) && MaxHopCount > 0 && AnimationTime > -1 && ExpectedResult > -1;
        }
    }
}
