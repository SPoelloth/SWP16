using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class ConfigControlBase : UserControl
    {
        protected bool initialized = false;
        public event Action<ConfigControlBase> Closing;

        public ConfigControlBase()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Closing?.Invoke(this);
        }

        // TODO: We should probably move this someplace else
        protected bool IsValidIP(string addr)
        {
            //create our match pattern
            var pattern = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            //create our Regular Expression object
            var check = new Regex(pattern);
            //boolean variable to hold the status
            var valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }
    }
}
