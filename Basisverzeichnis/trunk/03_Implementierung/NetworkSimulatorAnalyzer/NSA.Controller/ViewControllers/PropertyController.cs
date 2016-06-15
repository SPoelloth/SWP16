using System;
using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents;
using NSA.View.Controls.PropertyControl;
using NSA.View.Forms;

namespace NSA.Controller.ViewControllers
{
    public class PropertyController
    {
        #region Singleton

        public static PropertyController Instance = new PropertyController();

        #endregion Singleton

        private readonly PropertyControl propertyControl;
        private Hardwarenode selectedNode;

        private PropertyController()
        {
            propertyControl = MainForm.Instance.GetComponent("PropertyControl") as PropertyControl;
            if (propertyControl == null)
            {
                throw new InvalidOperationException("PropertyControl was null/not found");
            }
            else
            {
                propertyControl.InterfaceChanged += PropertyControl_InterfaceChanged;
                propertyControl.GatewayChanged += PropertyControl_GatewayChanged;
                propertyControl.RouteChanged += PropertyControl_RouteChanged;
                propertyControl.LayerStackChanged += PropertyControl_LayerStackChanged;

                propertyControl.InterfaceAdded += PropertyControl_InterfaceAdded;
                propertyControl.InterfaceRemoved += PropertyControl_InterfaceRemoved;
                propertyControl.RouteAdded += PropertyControl_RouteAdded;
                propertyControl.RouteRemoved += PropertyControl_RouteRemoved;
            }
        }

        #region Event Handling
        private void PropertyControl_RouteRemoved(string routeName)
        {
            NetworkManager.Instance.RemoveRoute(selectedNode.Name, routeName);
        }

        private void PropertyControl_RouteAdded()
        {
            Workstation station = (Workstation)selectedNode;
            //Route newRoute = NetworkManager.Instance.AddRoute(station.Name, IPAddress.None, IPAddress.None, null);
            // propertyControl.AddInterfaceConfigControl(newInterface.Name, newInterface.IpAddress, newInterface.Subnetmask);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControl_InterfaceRemoved(string name)
        {
            NetworkManager.Instance.RemoveInterface(selectedNode.Name, name);
        }

        private void PropertyControl_InterfaceAdded()
        {
            Workstation station = (Workstation) selectedNode;
            Interface newInterface = NetworkManager.Instance.AddInterfaceToWorkstation(station.Name, IPAddress.None, IPAddress.None); 
            // propertyControl.AddInterfaceConfigControl(newInterface.Name, newInterface.IpAddress, newInterface.Subnetmask);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControl_LayerStackChanged(List<Tuple<string, object>> obj)
        {
            // TODO: Implement Layerstack in UI first
        }

        private void PropertyControl_RouteChanged(System.Net.IPAddress Destination, System.Net.IPAddress Gateway, System.Net.IPAddress SubnetMask, string interfaceName)
        {
            // TODO: Get interface by name
            // NetworkManager.Instance.RouteChanged(selectedNode.Name, Destination, Gateway, SubnetMask, interfaceName);
        }

        private void PropertyControl_GatewayChanged(System.Net.IPAddress GatewayAddress, string InterfaceName)
        {
            NetworkManager.Instance.GatewayChanged(selectedNode.Name, GatewayAddress);
        }

        private void PropertyControl_InterfaceChanged(string name, System.Net.IPAddress IpAddress, System.Net.IPAddress SubnetMask)
        {
            NetworkManager.Instance.InterfaceChanged(selectedNode.Name, name, IpAddress, SubnetMask);
        }
        #endregion Event Handling

        public void LoadElementProperties(string elementName)
        {
            selectedNode = NetworkManager.Instance.GetHardwarenodeByName(elementName);
            propertyControl.ClearControls();
            if (selectedNode is Workstation)
            {
                var station = selectedNode as Workstation;

                // load workstation ethernet interface config controls
                foreach (var eth in station.GetInterfaces())
                {
                    propertyControl.AddInterfaceConfigControl(eth.Name, eth.IpAddress, eth.Subnetmask);
                }
                // load workstation gateway config control
                // TODO: Does(n't) every workstation have a gw?
                // propertyControl.AddGatewayConfigControl(station.StandardGateway, station.StandardGatewayPort.Name);

                // load route controls
                foreach (var route in station.GetRoutes())
                {
                    propertyControl.AddRouteConfigControl(route.Destination, route.Gateway, route.Subnetmask, route.Iface.Name);
                }

                // load workstation Layerstack controls
                // TODO: Integrate once finished
                //propertyControl.AddLayerStackControl(station.GetLayers());

                if (selectedNode is Router)
                {
                    // load gateway config control
                    propertyControl.AddGatewayConfigControl(station.StandardGateway, station.StandardGatewayPort.Name);
                }
                propertyControl.DisplayElements();
            }
        }
    }
}