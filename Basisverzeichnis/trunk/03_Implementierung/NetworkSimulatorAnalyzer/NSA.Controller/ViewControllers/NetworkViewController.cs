using System;
using NSA.Model.NetworkComponents;
using NSA.View.Controls.NetworkView;
using NSA.View.Controls.NetworkView.NetworkElements.Base;
using NSA.View.Forms;
using System.Drawing;
using NSA.View.Controls.NetworkView.NetworkElements;

namespace NSA.Controller.ViewControllers
{
    public class NetworkViewController
    {
        #region Singleton

        public static NetworkViewController Instance= new NetworkViewController();

        #endregion Singleton

        private NetworkViewControl networkViewControl;


        NetworkViewController()
        {
            networkViewControl = MainForm.Instance.GetComponent("NetworkviewControl") as NetworkViewControl;
            if (networkViewControl == null) throw new NullReferenceException("NetworkviewControl is null");
            networkViewControl.SelectionChanged += EditorElement_Selected;
        }

        public void EditorElement_Selected(EditorElementBase selectedElement)
        {
            if (selectedElement == null) return;
            PropertyController.Instance.LoadElementProperties(selectedElement.Name);
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

        public void RemoveHardwarenode(Hardwarenode node)
        {
            // Get Hardwarenode from Networkmanager
            // UnSubscribe to BL events

            // Remove Hardwarenoderepresentation
            // UnSubscribe from UI events
            // Remove Representation from NetworkViewControl
        }

        public void RemoveConnection(Connection connection)
        {
            // Get new Connection from Networkmanager
            // Subscribe to BL events

            // Create Connectionrepresentation
            // UnSubscribe from UI events
            // Remove Representation from NetworkViewControl
        }
    }
}