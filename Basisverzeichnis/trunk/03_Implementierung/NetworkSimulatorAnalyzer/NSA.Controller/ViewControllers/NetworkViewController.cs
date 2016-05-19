using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using NSA.Model.NetworkComponents;
using NSA.View.Controls.NetworkView;
using NSA.View.Controls.NetworkView.NetworkElements.Base;
using NSA.View.Forms;

namespace NSA.Controller.ViewControllers
{
    public class NetworkViewController
    {
        #region Singleton

        private static NetworkViewController instance = null;
        private static readonly object padlock = new object();

        public static NetworkViewController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NetworkViewController();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton

        private NetworkViewControl networkViewControl;
        

        NetworkViewController()
        {
            networkViewControl = MainForm.Instance.GetComponent("NetworkviewControl") as NetworkViewControl;
        }

        public void EditorElement_Selected(EditorElementBase selectedElement)
        {
            
        }

        public void AddHardwarenode()
        {
            // Get new Hardwarenode from Networkmanager
            // Subscribe to BL events

            // Create Hardwarenoderepresentation
            // Subscribe to UI events
            // Give Representation to NetworkViewControl.AddElement(EditorElementbase newElement)
        }
    }
}