using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}