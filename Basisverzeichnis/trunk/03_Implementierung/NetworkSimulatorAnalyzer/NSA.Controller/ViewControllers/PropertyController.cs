using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSA.Model.NetworkComponents;
using NSA.View.Controls.NetworkView;
using NSA.View.Controls.PropertyControl;
using NSA.View.Forms;

namespace NSA.Controller.ViewControllers
{
    public class PropertyController
    {
        #region Singleton

        private static PropertyController instance = null;
        private static readonly object padlock = new object();

        public static PropertyController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PropertyController();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton

        private PropertyControl propertyControl;
        PropertyController() {
            propertyControl = MainForm.Instance.GetComponent("PropertyControl") as PropertyControl;
        }

        public void LoadElementProperties(string elementName)
        {
            Hardwarenode node = NetworkManager.Instance.GetHardwarenodeByName(elementName);
            if (node != null)
            {
                if (node is Workstation)
                {
                    Workstation station = node as Workstation;

                    // load workstation ethernet interface config controls
                    foreach (Interface eth in station.GetInterfaces())
                    {
                        propertyControl.AddInterfaceConfigControl(eth.Name, eth.IpAddress, eth.Subnetmask);
                    }
                    // load workstation gateway config controls
                    // load workstation Layerstack controls
                    if (node is Router) {
                        // load gateway config control
                    }
                } else if (node is Switch)
                {
                    
                }
            }
        }
    }
}