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
    /// <summary>
    /// Controller class handling the PropertyControl and its interaction with the model
    /// </summary>
    internal class PropertyController
    {
        #region Singleton
        /// <summary>
        /// Instance refrence for the singleton pattern.
        /// </summary>
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

                propertyControl.SwitchPortNumberChanged += PropertyControl_SwitchPortNumberChanged;
            }
        }

        #region Event Handling

        #region Interfaces

        private void PropertyControl_InterfaceAdded()
        {
            Workstation station = (Workstation) selectedNode;
            NetworkManager.Instance.AddInterfaceToWorkstation(station.Name, IPAddress.None, IPAddress.None);
            propertyControl.RetainScrollPosition = true;
            LoadElementProperties(selectedNode.Name);
            propertyControl.RetainScrollPosition = false;
        }

        private void PropertyControl_InterfaceRemoved(string Name)
        {
            NetworkManager.Instance.RemoveInterface(selectedNode.Name, Name);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControl_InterfaceChanged(string Name, IPAddress IpAddress, IPAddress SubnetMask)
        {
            NetworkManager.Instance.InterfaceChanged(selectedNode.Name, Name, IpAddress, SubnetMask);
        }

        #endregion Interfaces

        #region Routes

        private void PropertyControlAddRoute()
        {
            Workstation station = (Workstation) selectedNode;
            NetworkManager.Instance.AddRoute(station.Name, IPAddress.None, IPAddress.None, IPAddress.None, station.Interfaces.First());
            propertyControl.RetainScrollPosition = true;
            LoadElementProperties(selectedNode.Name);
            propertyControl.RetainScrollPosition = false;
        }

        private void PropertyControlRemoveRoute(string RouteName)
        {
            NetworkManager.Instance.RemoveRoute(selectedNode.Name, RouteName);
            LoadElementProperties(selectedNode.Name);
        }

        private void PropertyControl_RouteChanged(string RouteName, IPAddress Destination, IPAddress Gateway, IPAddress SubnetMask, string InterfaceName)
        {
            Workstation station = (Workstation) selectedNode;
            NetworkManager.Instance.RouteChanged(station.Name, RouteName, Destination, Gateway, SubnetMask, station.Interfaces.Find(i => i.Name == InterfaceName));
        }

        #endregion Routes

        #region Gateway

        private void PropertyControl_GatewayChanged(IPAddress GatewayAddress, string InterfaceName,
            bool HasInternetAccess)
        {
            NetworkManager.Instance.GatewayChanged(selectedNode.Name, GatewayAddress, InterfaceName, HasInternetAccess);
        }

        #endregion Gateway

        #region Layers

        private void PropertyControl_AddLayer()
        {
            Layerstack currentLayerStack = (selectedNode as Workstation).Layerstack;
            CustomLayer newCustomLayer = new CustomLayer(currentLayerStack.CreateUniqueName(), currentLayerStack.GetSize() - 1);
            currentLayerStack.AddLayer(newCustomLayer);
            propertyControl.AddLayerToLayerConfigControl(newCustomLayer.GetLayerName(), true);
        }

        private void PropertyControl_RemoveLayer(string LayerName)
        {
            Layerstack currentLayerStack = (selectedNode as Workstation).Layerstack;
            currentLayerStack.RemoveLayer(LayerName);
        }

        private void PropertyControl_LayerIndexChanged(string LayerName, int Index)
        {
            var selectedStation = selectedNode as Workstation;
            if (selectedStation == null)
            {
                throw new InvalidOperationException();
            }
            Layerstack layers = selectedStation.Layerstack;
            layers.SetIndex(LayerName, Index);
        }

        private void PropertyControl_LayerNameChanged(string FormerName, string NewName)
        {
            var selectedStation = selectedNode as Workstation;
            if (selectedStation == null)
            {
                throw new InvalidOperationException();
            }
            selectedStation.Layerstack.SetName(FormerName, NewName);
        }

        #endregion Layers

        #region Switch

        private void PropertyControl_SwitchPortNumberChanged(int NumberOfPorts)
        {
            NetworkManager.Instance.SetSwitchInterfaceCount((selectedNode as Switch).Name, NumberOfPorts);
        }

        #endregion Switch

        #endregion Event Handling

        #region Methods
        /// <summary>
        /// Load properties of a hardwarenode for editing in the PropertyConfigControl
        /// </summary>
        /// <param name="ElementName"></param>
        public void LoadElementProperties(string ElementName)
        {
            selectedNode = NetworkManager.Instance.GetHardwarenodeByName(ElementName);
            propertyControl.ClearControls();
            if (selectedNode is Workstation)
            {
                var station = selectedNode as Workstation;

                // load workstation ethernet interface config controls
                foreach (var eth in station.Interfaces)
                {
                    propertyControl.AddInterfaceConfigControl(eth.Name, eth.IpAddress, eth.Subnetmask);
                }
                
                // Set InterfaceList in RouteConfigControl
                RouteConfigControl.SetInterfaces(station.Interfaces.Select(i => i.Name).ToList());
                // load route controls
                foreach (var route in station.GetRoutes())
                {
                    propertyControl.AddRouteConfigControl(route.Name, route.Destination, route.Gateway, route.Subnetmask,
                        route.Iface.Name);
                }

                // load workstation Layerstack controls
                propertyControl.AddLayerStackConfigControl();
                foreach (ILayer layer in station.Layerstack.GetAllLayers())
                {
                    propertyControl.AddLayerToLayerConfigControl(layer.GetLayerName(), layer is CustomLayer);
                }

                GwConfigControl.SetInterfaces(station.Interfaces.Select(i => i.Name).ToList());
                if (selectedNode is Router)
                {
                    // load gateway config control
                    propertyControl.AddGatewayConfigControl(station.StandardGateway ?? IPAddress.None,
                        station.StandardGatewayPort?.Name, true, (selectedNode as Router).IsGateway);
                }
                else
                {
                    propertyControl.AddGatewayConfigControl(station.StandardGateway ?? IPAddress.None,
                        station.StandardGatewayPort?.Name, false);
                }
                propertyControl.DisplayElements();
            }
            else if (selectedNode is Switch)
            {
                Switch selectedSwitch = selectedNode as Switch;
                propertyControl.AddSwitchConfigControl(selectedSwitch.GetInterfaceCount());
            }
        }

        #endregion Methods
    }
}