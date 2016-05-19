using System;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class ConfigControlBase : UserControl
    {
        public event Action Closing;

        public ConfigControlBase()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void buttonClose_Click(object sender, System.EventArgs e)
        {
            Closing?.Invoke();
        }
    }
}
