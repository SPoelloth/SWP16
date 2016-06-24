using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    /// <summary>
    /// Base class for ConfigControls.
    /// </summary>
    public partial class ConfigControlBase : UserControl
    {
        /// <summary>
        /// Set to true once initialization has finished.
        /// </summary>
        protected bool initialized = false;
        /// <summary>
        /// Is fired when the control is being closed
        /// </summary>
        public event Action<ConfigControlBase> Closing;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigControlBase()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Closing?.Invoke(this);
        }

        /// <summary>
        /// Checks whether a string ist a valid IP address
        /// </summary>
        /// <param name="addr">The address</param>
        /// <returns>True if valid, false otherwise</returns>
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

        /// <summary>
        /// Checks whether an IP address is a valid subnet mask.
        /// </summary>
        /// <param name="Subnetmask">The subnetmask</param>
        /// <returns>True if valid, false otherwise</returns>
        protected static bool IsValidSubnetMask(IPAddress Subnetmask) {
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
