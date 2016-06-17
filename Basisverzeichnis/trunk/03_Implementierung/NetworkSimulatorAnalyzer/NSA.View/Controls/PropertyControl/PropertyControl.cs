using System;
using System.Collections.Generic;
using System.Linq;
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

        public event Action InterfaceAdded;
        public event Action<string> InterfaceRemoved;
        public event Action<string, IPAddress, IPAddress> InterfaceChanged;

        public event Action<IPAddress, string, bool> GatewayChanged;

        public event Action AddRoute;
        public event Action<string> RemoveRoute;
        public event Action<string, IPAddress, IPAddress, IPAddress, string> RouteChanged;

        public event Action AddLayer;
        public event Action<string> RemoveLayer;
        public event Action<string, int> LayerIndexChanged;
        public event Action<string, string> LayerNameChanged;

        public PropertyControl()
        {
            InitializeComponent();
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
        public void AddGatewayConfigControl(IPAddress DefaultGatewayAddress, string AssignedInterfaceName, bool IsRouter, bool HasInternetAccess = true)
        {
            var gwConfigControl = new GwConfigControl(DefaultGatewayAddress, AssignedInterfaceName, IsRouter, HasInternetAccess);
            gwConfigControl.GatewayChanged += GWConfigControl_GatewayChanged;
            tempControls.Add(gwConfigControl);
        }

        /// <summary>
        ///     Creates a RouteConfigControl and adds it to the list of routes to be displayed.
        /// </summary>
        /// <param name="Source">IP address of the route source</param>
        /// <param name="Destination">IP address of the route destination</param>
        /// <param name="Route">IP address of the route</param>
        /// <param name="Parameters">Parameters for the route</param>
        public void AddRouteConfigControl(string RouteName, IPAddress Source, IPAddress Destination, IPAddress Route, string Parameters)
        {
            var newControl = new RouteConfigControl(RouteName, Source, Destination, Route, Parameters);
            newControl.Closing += ConfigControl_Closing;
            newControl.RouteChanged += RouteConfigControl_RouteChanged;
            routeConfigControls.Add(newControl);
        }

        public void AddLayerStackConfigControl()
        {
            var layerStackConfigControl = new LayerstackConfigControl();
            layerStackConfigControl.LayerAdded += LayerStackConfigControl_LayerAdded;
            layerStackConfigControl.LayerRemoved += LayerStackConfigControl_LayerRemoved;
            layerStackConfigControl.LayerNameChanged += LayerStackConfigControl_LayerNameChanged;
            layerStackConfigControl.LayerIndexChanged += LayerStackConfigControl_LayerIndexChanged;
            tempControls.Add(layerStackConfigControl);
        }

        public void AddLayerToLayerConfigControl(string LayerName, bool IsCustom)
        {
            var lscc = Controls.OfType<LayerstackConfigControl>().FirstOrDefault() ?? tempControls.OfType<LayerstackConfigControl>().FirstOrDefault();
            if (lscc == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                lscc.AddLayer(LayerName, IsCustom);
            }
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
            var addInterfaceButton = new AddInterfaceButton();
            addInterfaceButton.Click += AddInterfaceButton_Click;
            flpContents.Controls.Add(addInterfaceButton);

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
            var addRouteButton = new AddRouteButton();
            addRouteButton.Click += AddRouteButton_Click;
            flpContents.Controls.Add(addRouteButton);
            ResumeLayout();
        }

        public void ClearControls()
        {
            SuspendLayout();
            foreach (Control c in flpContents.Controls)
            {
                if (c is InterfaceConfigControl)
                {
                    InterfaceConfigControl icc = c as InterfaceConfigControl;
                    icc.InterfaceChanged -= InterfaceConfigControl_InterfaceChanged;
                    icc.Closing -= ConfigControl_Closing;
                } else if (c is RouteConfigControl)
                {
                    RouteConfigControl rcc = c as RouteConfigControl;
                    rcc.RouteChanged -= RouteConfigControl_RouteChanged;
                    rcc.Closing -= ConfigControl_Closing;
                } else if (c is GwConfigControl)
                {
                    GwConfigControl gcc = c as GwConfigControl;
                    gcc.GatewayChanged -= GWConfigControl_GatewayChanged;
                    gcc.Closing -= ConfigControl_Closing;
                } else if (c is LayerstackConfigControl)
                {
                    LayerstackConfigControl lscc = c as LayerstackConfigControl;
                    lscc.LayerAdded -= LayerStackConfigControl_LayerAdded;
                    lscc.LayerRemoved -= LayerStackConfigControl_LayerRemoved;
                    lscc.LayerNameChanged -= LayerStackConfigControl_LayerNameChanged;
                    lscc.LayerIndexChanged -= LayerStackConfigControl_LayerIndexChanged;
                    lscc.Closing -= ConfigControl_Closing;
                } else if (c is AddInterfaceButton)
                {
                    AddInterfaceButton aib = c as AddInterfaceButton;
                    aib.Click -= AddInterfaceButton_Click;
                } else if (c is AddRouteButton)
                {
                    AddRouteButton arb = c as AddRouteButton;
                    arb.Click -= AddRouteButton_Click;
                }
            }
            flpContents.Controls.Clear();
            interfaceConfigControls.Clear();
            routeConfigControls.Clear();
            tempControls.Clear();
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
            AddRoute?.Invoke();
        }

        /// <summary>
        ///     Handler for the Closing event of a ConfigControl
        /// </summary>
        /// <param name="Control">The closing control</param>
        private void ConfigControl_Closing(ConfigControlBase Control)
        {
            if (Control is InterfaceConfigControl)
            {
                var control = Control as InterfaceConfigControl;
                control.InterfaceChanged -= InterfaceConfigControl_InterfaceChanged;
                InterfaceRemoved?.Invoke(control.InterfaceName);
            }
            else if (Control is RouteConfigControl)
            {
                var rcc = Control as RouteConfigControl;
                ((RouteConfigControl) Control).RouteChanged -= RouteConfigControl_RouteChanged;
                RemoveRoute?.Invoke(rcc.RouteName);
            }
            Control.Closing -= ConfigControl_Closing;
            flpContents.Controls.Remove(Control);
        }

        #region Interface
        /// <summary>
        ///     Handler for the InterfaceChanged event of an InterfaceConfigControl
        /// </summary>
        /// <param name="InterfaceName">The name of the interface</param>
        /// <param name="IpAddress">The IP address of the interface</param>
        /// <param name="SubnetMask">The subnet mask of the interface</param>
        private void InterfaceConfigControl_InterfaceChanged(string InterfaceName, IPAddress IpAddress,
            IPAddress SubnetMask) {
            InterfaceChanged?.Invoke(InterfaceName, IpAddress, SubnetMask);
        }
        #endregion Interface

        #region Gateway
        /// <summary>
        ///     Handler for the GateWayChanged event of a GWConfigControl
        /// </summary>
        /// <param name="GwAddress">The IP address of the default gateway</param>
        /// <param name="InterfaceName">The name of the assigned interface</param>
        /// <param name="HasInternetAccess">Flag indicating whether the workstation has internet access</param>
        private void GWConfigControl_GatewayChanged(IPAddress GwAddress, string InterfaceName, bool HasInternetAccess) {
            GatewayChanged?.Invoke(GwAddress, InterfaceName, HasInternetAccess);
        }
        #endregion Gateway

        #region Routes
        /// <summary>
        ///     Handler for the RouteChanged event of a RouteConfigControl
        /// </summary>
        /// <param name="RouteName">The name of the route</param>
        /// <param name="Source">IP address of the route source</param>
        /// <param name="Destination">IP address of the route destination</param>
        /// <param name="Route">IP address of the route</param>
        /// <param name="Parameters">Parameters for the route</param>
        private void RouteConfigControl_RouteChanged(string RouteName, IPAddress Source, IPAddress Destination, IPAddress Route,
            string Parameters) {
            RouteChanged?.Invoke(RouteName, Source, Destination, Route, Parameters);
        }
        #endregion Routes

        #region Layers
        private void LayerStackConfigControl_LayerAdded() {
            AddLayer?.Invoke();
        }

        private void LayerStackConfigControl_LayerRemoved(LayerControl LayerControl) {
            RemoveLayer?.Invoke(LayerControl.LayerName);
        }

        private void LayerStackConfigControl_LayerNameChanged(string FormerName, string NewName) {
            LayerNameChanged?.Invoke(FormerName, NewName);
        }

        private void LayerStackConfigControl_LayerIndexChanged(string LayerName, int LayerIndex) {
            LayerIndexChanged?.Invoke(LayerName, LayerIndex);
        }
        #endregion Layers
        #endregion Eventhandling
    }
}
