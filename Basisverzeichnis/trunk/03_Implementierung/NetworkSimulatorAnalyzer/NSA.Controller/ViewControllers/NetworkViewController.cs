using System;
using NSA.Model.NetworkComponents;
using NSA.View.Controls.NetworkView;
using NSA.View.Controls.NetworkView.NetworkElements.Base;
using NSA.View.Forms;
using System.Drawing;
using NSA.View.Controls.NetworkView.NetworkElements;
using System.Linq;

namespace NSA.Controller.ViewControllers
{
    public class NetworkViewController
    {
        #region Singleton

        public static NetworkViewController Instance = new NetworkViewController();

        #endregion Singleton

        private NetworkViewControl networkViewControl;


        public void Initialize()
        {
            networkViewControl = MainForm.Instance.GetComponent("NetworkviewControl") as NetworkViewControl;
            if (networkViewControl == null) throw new NullReferenceException("NetworkviewControl is null");
            networkViewControl.SelectionChanged += EditorElement_Selected;
            networkViewControl.RemoveConnectionPressed += RemoveConnection;
            networkViewControl.RemoveElementPressed += RemoveHardwarenode;
        }

        public Point? GetLocationOfElementByName(string name)
        {
            return networkViewControl.Controls.OfType<EditorElementBase>().FirstOrDefault(s => s.Name == name)?.Location;
        }

        private void RemoveHardwarenode(EditorElementBase e)
        {
            networkViewControl.RemoveElement(e);
            NetworkManager.Instance.RemoveHardwarenode(e.Name);
        }

        private void RemoveConnection(VisualConnection c)
        {
            networkViewControl.RemoveConnection(c);
            NetworkManager.Instance.RemoveConnection(c.Name);
        }

        public void EditorElement_Selected(EditorElementBase selectedElement)
        {
            PropertyController.Instance.LoadElementProperties(selectedElement?.Name);
        }

        public void AddHardwarenode(Hardwarenode node)
        {
            if (node is Workstation) networkViewControl.AddElement(new WorkstationControl(new Point(100, 100), node.Name));
            if (node is Switch) networkViewControl.AddElement(new SwitchControl(new Point(100, 100), node.Name));
        }

        public void AddConnection(Connection connection)
        {
            // Get new Connection from Networkmanager
            // Subscribe to BL events

            // Create Connectionrepresentation
            // Subscribe to UI events
            // Give Representation to NetworkViewControl.AddElement(EditorElementbase newElement)
        }

    }
}