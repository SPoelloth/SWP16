using System;
using System.Collections.Generic;
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

        private PropertyController()
        {
            propertyControl = MainForm.Instance.GetComponent("PropertyControl") as PropertyControl;
            if (propertyControl != null)
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
            else
            {
                throw new InvalidOperationException("PropertyControl was null/not found");
            }
        }

        #region Event Handling
        private void PropertyControl_RouteRemoved(string routeName)
        {
            // TODO: 
            NetworkManager.Instance.RemoveRoute("IrgendwerSagtMirSicherWoIchDenNamenHerkrieg", "routeName");
        }

        private void PropertyControl_RouteAdded()
        {
            // TODO: Get route from somewhere and add to propertyControl
        }

        private void PropertyControl_InterfaceRemoved(string name)
        {
            // TODO: Get actual name of the selected station from NetworkManager or sth.
            NetworkManager.Instance.RemoveInterface("IrgendwerSagtMirSicherWoIchDenNamenHerkrieg", name);
        }

        private void PropertyControl_InterfaceAdded()
        {
            // TODO: Get interface
            // Interface newInterface = NetworkManager.GetNewInterface(); 
            // propertyControl.AddInterfaceConfigControl(newInterface.Name, newInterface);
        }

        private void PropertyControl_LayerStackChanged(List<System.Tuple<string, object>> obj)
        {
            // TODO: Implement Layerstack in UI first
        }

        private void PropertyControl_RouteChanged(System.Net.IPAddress Destination, System.Net.IPAddress Gateway, System.Net.IPAddress SubnetMask, string interfaceName)
        {
            // TODO: Get interface by name
            // NetworkManager.Instance.RouteChanged("IrgendwerSagtMirSicherWoIchDenNamenHerkrieg", Destination, Gateway, SubnetMask, interfaceName);
        }

        private void PropertyControl_GatewayChanged(System.Net.IPAddress GatewayAddress, string InterfaceName)
        {
            // TODO: Get actual name of the selected station from NetworkManager or sth., what about InterfaceName?
            NetworkManager.Instance.GatewayChanged("IrgendwerSagtMirSicherWoIchDenNamenHerkrieg", GatewayAddress);
        }

        private void PropertyControl_InterfaceChanged(string name, System.Net.IPAddress IpAddress, System.Net.IPAddress SubnetMask)
        {
            // TODO: Get actual name of the selected station from NetworkManager or sth.
            NetworkManager.Instance.InterfaceChanged("IrgendwerSagtMirSicherWoIchDenNamenHerkrieg", Convert.ToInt16(name.Remove(0,3)), IpAddress, SubnetMask);
        }
        #endregion Event Handling

        public void LoadElementProperties(string elementName)
        {
            var node = NetworkManager.Instance.GetHardwarenodeByName(elementName);
            propertyControl.ClearControls();
            if (node != null)
            {
                if (node is Workstation)
                {
                    var station = node as Workstation;

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

                    if (node is Router)
                    {
                        // load gateway config control
                        propertyControl.AddGatewayConfigControl(station.StandardGateway, station.StandardGatewayPort.Name);
                    }
                    propertyControl.DisplayElements();
                }
            }
        }
    }
}