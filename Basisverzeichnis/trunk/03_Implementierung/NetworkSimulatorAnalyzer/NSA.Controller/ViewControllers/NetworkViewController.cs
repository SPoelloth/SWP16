using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;
using NSA.View.Controls.NetworkView;
using NSA.View.Controls.NetworkView.NetworkElements.Base;
using NSA.View.Forms;
using System.Drawing;
using NSA.View.Controls.NetworkView.NetworkElements;
using System.Linq;
using System.Windows.Forms;
using NSA.Model.BusinessLogic;

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
            networkViewControl.NewConnectionCreated += OnNewConnectionCreated;
        }

        private void OnNewConnectionCreated(Control c1, int port1, Control c2, int port2)
        {
            NetworkManager.Instance.CreateConnection(c1.Name, "eth" + port1, c2.Name, "eth" + port2);
            
        }

        public Point? GetLocationOfElementByName(string name)
        {
            return networkViewControl.Controls.OfType<EditorElementBase>().FirstOrDefault(s => s.Name == name)?.Location;
        }

        public List<NodeLocation> GetAllLocationsWithName()
        {
            List<NodeLocation> NodeLocations = new List<NodeLocation>();
            foreach (EditorElementBase element in networkViewControl.Controls.OfType<EditorElementBase>())
            {
                string nodeName = element.AccessibleName;
                NodeLocation nodeLocation = new NodeLocation();
                nodeLocation.Name = nodeName;
                nodeLocation.Point = element.Location;
                NodeLocations.Add(nodeLocation);
            }
            return NodeLocations;
        }

        public void ClearNodes()
        {
            foreach (EditorElementBase element in networkViewControl.Controls.OfType<EditorElementBase>())
            {
                if (element != null)
                {
                    try
                    {
                        networkViewControl.RemoveElement(element);
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
            }
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
            var node1 = GetControlByName(connection.Start.Name);
            var node2 = GetControlByName(connection.End.Name);
            if (node1 == null || node2 == null) throw new ArgumentNullException("referenced start or end of connection is null");
         //   networkViewControl.AddElement(new VisualConnection(connection.Name, node1, hierFehltDerPortIndex, node2, PORTINDEX));
         //TODO
        }

        public void CreateHardwarenodeRequest()
        {
            networkViewControl.CreateNewConnection();
        }

        private EditorElementBase GetControlByName(string name)
        {
            return networkViewControl.Controls.OfType<EditorElementBase>().First(s => s.Name == name);
        }
    }
}