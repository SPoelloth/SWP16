using System;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    /// <summary>
    /// Control for configuring a switch.
    /// </summary>
    public class SwitchConfigControl : ConfigControlBase
    {
        private System.Windows.Forms.NumericUpDown numericUpDownPorts;
        private System.Windows.Forms.Label labelPortNumber;

        /// <summary>
        /// Is fired when the number of ports has changed.
        /// </summary>
        public event System.Action<int> NumberOfPortsChanged;

        private void InitializeComponent()
        {
            this.numericUpDownPorts = new System.Windows.Forms.NumericUpDown();
            this.labelPortNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDownPorts)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Size = new System.Drawing.Size(65, 24);
            this.labelName.Text = "Switch";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(191, 3);
            this.buttonClose.Visible = false;
            // 
            // numericUpDownPorts
            // 
            this.numericUpDownPorts.Location = new System.Drawing.Point(168, 29);
            this.numericUpDownPorts.Maximum = new decimal(new int[]
            {
                32,
                0,
                0,
                0
            });
            this.numericUpDownPorts.Name = "numericUpDownPorts";
            this.numericUpDownPorts.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownPorts.TabIndex = 3;
            this.numericUpDownPorts.ValueChanged += new System.EventHandler(this.numericUpDownPorts_ValueChanged);
            // 
            // labelPortNumber
            // 
            this.labelPortNumber.AutoSize = true;
            this.labelPortNumber.Location = new System.Drawing.Point(5, 31);
            this.labelPortNumber.Name = "labelPortNumber";
            this.labelPortNumber.Size = new System.Drawing.Size(117, 13);
            this.labelPortNumber.TabIndex = 4;
            this.labelPortNumber.Text = "Anzahl an Anschlüssen";
            // 
            // SwitchConfigControl
            // 
            this.Controls.Add(this.labelPortNumber);
            this.Controls.Add(this.numericUpDownPorts);
            this.DoubleBuffered = true;
            this.Name = "SwitchConfigControl";
            this.Size = new System.Drawing.Size(214, 59);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.buttonClose, 0);
            this.Controls.SetChildIndex(this.numericUpDownPorts, 0);
            this.Controls.SetChildIndex(this.labelPortNumber, 0);
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDownPorts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="NumberOfPorts">The number of ports</param>
        public SwitchConfigControl(int NumberOfPorts)
        {
            InitializeComponent();
            numericUpDownPorts.Value = NumberOfPorts;
            initialized = true;
        }

        private void numericUpDownPorts_ValueChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                NumberOfPortsChanged?.Invoke((int) numericUpDownPorts.Value);
            }
        }
    }
}
