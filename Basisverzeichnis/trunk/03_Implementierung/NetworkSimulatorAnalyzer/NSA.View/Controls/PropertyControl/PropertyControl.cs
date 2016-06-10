using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using NSA.View.Controls.PropertyControl.ConfigControls;
using NSA.View.Controls.PropertyControl.Misc;

namespace NSA.View.Controls.PropertyControl
{
    public partial class PropertyControl : UserControl
    {
        private readonly List<InterfaceConfigControl> interfaceConfigControls = new List<InterfaceConfigControl>();
        private readonly List<RouteConfigControl> routeConfigControls = new List<RouteConfigControl>();
        private readonly List<Control> tempControls = new List<Control>();

        public event Action<string, IPAddress, IPAddress> InterfaceChanged;
        public event Action<IPAddress, string> GatewayChanged;
        public event Action<IPAddress, IPAddress, IPAddress, string> RouteChanged;
        public event Action<List<Tuple<string, object>>> LayerStackChanged;

        public event Action InterfaceAdded;
        public event Action<string> InterfaceRemoved;
        public event Action RouteAdded;
        public event Action<string> RouteRemoved;

        public PropertyControl()
        {
            InitializeComponent();
            DisplayElements();
        }

        #region Methods

        /// <summary>
        ///     Creates an InterfaceConfigControl and adds it to the list of interfaces to be displayed.
        /// </summary>
        /// <param name="InterfaceName">The name of the interface</param>
        /// <param name="IpAddress">The current IP address of the interface</param>
        /// <param name="SubnetMask">The current subnet mask of the interface</param>
        public void AddInterfaceConfigControl(string InterfaceName, IPAddress IpAddress, IPAddress SubnetMask)
        {
            var newControl = new InterfaceConfigControl(IpAddress, SubnetMask, InterfaceName);
            newControl.Closing += ConfigControl_Closing;
            newControl.InterfaceChanged += InterfaceConfigControl_InterfaceChanged;
            interfaceConfigControls.Add(newControl);
        }

        /// <summary>
        ///     Creates a GateWayConfigControl and adds it to the list of controls to be displayed.
        /// </summary>
        /// <param name="DefaultGatewayAddress">The IP adress of the default gateway</param>
        /// <param name="AssignedInterfaceName">The name of the ethernet interface to be used for the default gateway</param>
        public void AddGatewayConfigControl(IPAddress DefaultGatewayAddress, string AssignedInterfaceName)
        {
            var newControl = new GWConfigControl(DefaultGatewayAddress, AssignedInterfaceName);
            newControl.GatewayChanged += GWConfigControl_GatewayChanged;
            tempControls.Add(newControl);
        }

        /// <summary>
        ///     Creates a RouteConfigControl and adds it to the list of routes to be displayed.
        /// </summary>
        /// <param name="Source">IP address of the route source</param>
        /// <param name="Destination">IP address of the route destination</param>
        /// <param name="Route">IP address of the route</param>
        /// <param name="Parameters">Parameters for the route</param>
        public void AddRouteConfigControl(IPAddress Source, IPAddress Destination, IPAddress Route, string Parameters)
        {
            var newControl = new RouteConfigControl(Source, Destination, Route, Parameters);
            newControl.Closing += ConfigControl_Closing;
            newControl.RouteChanged += RouteConfigControl_RouteChanged;
            routeConfigControls.Add(newControl);
        }

        /// <summary>
        ///     Puts the config controls in the flowlayoutpanel, where they are displayed for the user.
        ///     Is called after all elements have been created and added.
        /// </summary>
        public void DisplayElements()
        {
            // Add InterfaceConfigControls first
            foreach (var icc in interfaceConfigControls)
            {
                flpContents.Controls.Add(icc);
            }
            // If the target HardwareNode has at least one networkInterface, add an AddButton for additional interfaces
            if (interfaceConfigControls.Count > 0)
            {
                var addInterfaceButton = new AddButton();
                addInterfaceButton.Click += AddInterfaceButton_Click;
                flpContents.Controls.Add(addInterfaceButton);
            }
            // Add various singular controls
            foreach (var c in tempControls)
            {
                flpContents.Controls.Add(c);
            }
            // Add RouteConfigControls and an AddButton, if necessary
            foreach (var rcc in routeConfigControls)
            {
                flpContents.Controls.Add(rcc);
            }
            if (routeConfigControls.Count > 0)
            {
                var addRouteButton = new AddButton();
                addRouteButton.Click += AddRouteButton_Click;
                flpContents.Controls.Add(addRouteButton);
            }
        }

        #endregion Methods

        #region Eventhandling

        /// <summary>
        ///     Handler for the click of the "Add Interface" button
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void AddInterfaceButton_Click(object Sender, EventArgs E)
        {
            InterfaceAdded?.Invoke();
        }

        /// <summary>
        ///     Handler for the click of the "Add Route" button
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void AddRouteButton_Click(object Sender, EventArgs E)
        {
            RouteAdded?.Invoke();
        }

        /// <summary>
        ///     Handler for the Closing event of a ConfigControl
        /// </summary>
        /// <param name="Control">The closing control</param>
        private void ConfigControl_Closing(ConfigControlBase Control)
        {
            var control = Control as InterfaceConfigControl;
            if (control != null)
            {
                control.InterfaceChanged -= InterfaceConfigControl_InterfaceChanged;
                InterfaceRemoved?.Invoke(control.Name);
            }
            else if (Control is RouteConfigControl)
            {
                ((RouteConfigControl) Control).RouteChanged -= RouteConfigControl_RouteChanged;
            }
            Control.Closing -= ConfigControl_Closing;
            flpContents.Controls.Remove(Control);
        }

        /// <summary>
        ///     Handler for the GateWayChanged event of a GWConfigControl
        /// </summary>
        /// <param name="GwAddress">The IP address of the default gateway</param>
        /// <param name="InterfaceName">The name of the assigned interface</param>
        private void GWConfigControl_GatewayChanged(IPAddress GwAddress, string InterfaceName)
        {
            GatewayChanged?.Invoke(GwAddress, InterfaceName);
        }

        /// <summary>
        ///     Handler for the InterfaceChanged event of an InterfaceConfigControl
        /// </summary>
        /// <param name="InterfaceName">The name of the interface</param>
        /// <param name="IpAddress">The IP address of the interface</param>
        /// <param name="SubnetMask">The subnet mask of the interface</param>
        private void InterfaceConfigControl_InterfaceChanged(string InterfaceName, IPAddress IpAddress,
            IPAddress SubnetMask)
        {
            InterfaceChanged?.Invoke(InterfaceName, IpAddress, SubnetMask);
        }

        /// <summary>
        ///     Handler for the RouteChanged event of a RouteConfigControl
        /// </summary>
        /// <param name="Source">IP address of the route source</param>
        /// <param name="Destination">IP address of the route destination</param>
        /// <param name="Route">IP address of the route</param>
        /// <param name="Parameters">Parameters for the route</param>
        private void RouteConfigControl_RouteChanged(IPAddress Source, IPAddress Destination, IPAddress Route,
            string Parameters)
        {
            RouteChanged?.Invoke(Source, Destination, Route, Parameters);
        }

        #endregion Eventhandling
    }
}
