using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Layers;
using NSA.View.Controls.PropertyControl;
using NSA.View.Controls.PropertyControl.ConfigControls;
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
                propertyControl.InterfaceAdded += PropertyControl_InterfaceAdded;
                propertyControl.InterfaceRemoved += PropertyControl_InterfaceRemoved;
                propertyControl.InterfaceChanged += PropertyControl_InterfaceChanged;

                propertyControl.GatewayChanged += PropertyControl_GatewayChanged;

                propertyControl.AddRoute += PropertyControlAddRoute;
                propertyControl.RemoveRoute += PropertyControlRemoveRoute;
                propertyControl.RouteChanged += PropertyControl_RouteChanged;

                propertyControl.LayerIndexChanged += PropertyControl_LayerIndexChanged;
                propertyControl.LayerNameChanged += PropertyControl_LayerNameChanged;
                propertyControl.AddLayer += PropertyControl_AddLayer;
                propertyControl.RemoveLayer += PropertyControl_RemoveLayer;

            }
        }




        #region Event Handling

        #region Interfaces
        private void PropertyControl_InterfaceAdded() {
            Workstation station = (Workstation)selectedNode;
            NetworkManager.Instance.AddInterfaceToWorkstation(station.Name, IPAddress.None, IPAddress.None);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControl_InterfaceRemoved(string Name) {
            NetworkManager.Instance.RemoveInterface(selectedNode.Name, Name);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControl_InterfaceChanged(string Name, IPAddress IpAddress, IPAddress SubnetMask) {
            NetworkManager.Instance.InterfaceChanged(selectedNode.Name, Name, IpAddress, SubnetMask);
        }
        #endregion Interfaces

        #region Routes
        private void PropertyControlAddRoute() {
            Workstation station = (Workstation)selectedNode;
            Route newRoute = NetworkManager.Instance.AddRoute(station.Name, IPAddress.None, IPAddress.None, IPAddress.None, station.GetInterfaces()[0]);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControlRemoveRoute(string RouteName) {
            NetworkManager.Instance.RemoveRoute(selectedNode.Name, RouteName);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControl_RouteChanged(string RouteName, IPAddress Destination, IPAddress Gateway, IPAddress SubnetMask, string InterfaceName) {
            Workstation station = (Workstation)selectedNode;
            NetworkManager.Instance.RouteChanged(station.Name, RouteName, Destination, Gateway, SubnetMask, station.GetInterfaces().Find(i => i.Name == InterfaceName));
        }
        #endregion Routes

        #region Gateway

        private void PropertyControl_GatewayChanged(IPAddress GatewayAddress, string InterfaceName, bool HasInternetAccess) {
            NetworkManager.Instance.GatewayChanged(selectedNode.Name, GatewayAddress, HasInternetAccess);
        }
        #endregion Gateway

        #region Layers
        private void PropertyControl_AddLayer() {
            Layerstack currentLayerStack = (selectedNode as Workstation).GetLayerstack();
            CustomLayer newCustomLayer = new CustomLayer(currentLayerStack.CreateUniqueName());
            currentLayerStack.AddLayer(newCustomLayer);
            propertyControl.AddLayerToLayerConfigControl(newCustomLayer.GetLayerName(), true);
        }

        private void PropertyControl_RemoveLayer(string LayerName) {
            Layerstack currentLayerStack = (selectedNode as Workstation).GetLayerstack();
            currentLayerStack.RemoveLayer(LayerName);
        }

        private void PropertyControl_LayerIndexChanged(string LayerName, int Index)
        {
            var selectedStation = selectedNode as Workstation;
            if (selectedStation == null)
            {
                throw new InvalidOperationException();
            }
            Layerstack layers = selectedStation.GetLayerstack();
            layers.SetIndex(LayerName, Index);
        }

        private void PropertyControl_LayerNameChanged(string FormerName, string NewName) {
            var selectedStation = selectedNode as Workstation;
            if (selectedStation == null) {
                throw new InvalidOperationException();
            }
            selectedStation.GetLayerstack().SetName(FormerName, NewName);
        }
        #endregion Layers

        #endregion Event Handling

        #region Methods
        public void LoadElementProperties(string ElementName)
        {
            selectedNode = NetworkManager.Instance.GetHardwarenodeByName(ElementName);
            propertyControl.ClearControls();
            if (selectedNode is Workstation)
            {
                var station = selectedNode as Workstation;

                // load workstation ethernet interface config controls
                foreach (var eth in station.GetInterfaces())
                {
                    propertyControl.AddInterfaceConfigControl(eth.Name, eth.IpAddress, eth.Subnetmask);
                }

                // Set InterfaceList in RouteConfigControl
                RouteConfigControl.SetInterfaces(station.GetInterfaces().Select(i => i.Name).ToList());
                // load route controls
                foreach (var route in station.GetRoutes())
                {
                    propertyControl.AddRouteConfigControl(route.Name, route.Destination, route.Gateway, route.Subnetmask, route.Iface.Name);
                }

                // load workstation Layerstack controls
                propertyControl.AddLayerStackConfigControl();
                foreach (ILayer layer in station.GetLayerstack().GetAllLayers())
                {
                    propertyControl.AddLayerToLayerConfigControl(layer.GetLayerName(), layer is CustomLayer);
                }

                if (selectedNode is Router)
                {
                    // load gateway config control
                    // propertyControl.AddGatewayConfigControl(station.StandardGateway, station.StandardGatewayPort.Name, true, (selectedNode as Router).IsGateway);
                }
                else
                {
                    // propertyControl.AddGatewayConfigControl(station.StandardGateway, station.StandardGatewayPort.Name, true);
                }
                propertyControl.DisplayElements();
            }
        }
        #endregion Methods
    }
}