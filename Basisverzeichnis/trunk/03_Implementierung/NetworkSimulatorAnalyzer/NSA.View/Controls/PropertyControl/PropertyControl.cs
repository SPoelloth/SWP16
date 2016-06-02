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
        public event Action InterfaceAdded;
        public event Action<string> InterfaceRemoved;
        public event Action<IPAddress> GatewayChanged;
        public event Action<List<Tuple<string, object>>> LayerStackChanged;
        public event Action RouteAdded;
        public event Action RouteRemoved;

        public PropertyControl()
        {
            InitializeComponent();
        }

        #region Methods

        public void AddInterfaceConfigControl(string InterfaceName, IPAddress IpAddress, IPAddress SubnetMask)
        {
            InterfaceConfigControl newControl = new InterfaceConfigControl(IpAddress, SubnetMask, InterfaceName);
            newControl.Closing += ConfigControl_Closing;
            newControl.InterfaceChanged += InterfaceConfigControl_InterfaceChanged;
            interfaceConfigControls.Add(newControl);
        }

        public void AddGatewayConfigControl(IPAddress DefaultGatewayAddress)
        {
            GWConfigControl newControl = new GWConfigControl(DefaultGatewayAddress);
            newControl.GatewayChanged += GWConfigControl_GatewayChanged;
            tempControls.Add(newControl);
        }

        public void AddRouteConfigControl(IPAddress Source, IPAddress Destination, IPAddress Route, string Parameters)
        {
            RouteConfigControl newControl = new RouteConfigControl(Source, Destination, Route, Parameters);
            newControl.Closing += ConfigControl_Closing;
            newControl.RouteChanged += RouteConfigControl_RouteChanged;
            routeConfigControls.Add(newControl);
        }

        public void DisplayElements()
        {
            // Add InterfaceConfigControls first
            foreach (InterfaceConfigControl icc in interfaceConfigControls)
            {
                flpContents.Controls.Add(icc);
            }
            // If the target HardwareNode has at least one networkInterface, add an AddButton for additional interfaces
            if (interfaceConfigControls.Count > 0)
            {
                AddButton addInterfaceButton = new AddButton();
                addInterfaceButton.Click += AddInterfaceButton_Click;
                flpContents.Controls.Add(addInterfaceButton);
            }
            // Add various singular controls
            foreach (Control c in tempControls)
            {
                flpContents.Controls.Add(c);
            }
            // Add RouteConfigControls and an AddButton, if necessary
            foreach (RouteConfigControl rcc in routeConfigControls)
            {
                flpContents.Controls.Add(rcc);
            }
            if (routeConfigControls.Count > 0)
            {
                AddButton addRouteButton = new AddButton();
                addRouteButton.Click += AddRouteButton_Click;
                flpContents.Controls.Add(addRouteButton);
            }
        }

        #endregion Methods

        #region Eventhandling

        private void AddInterfaceButton_Click(object Sender, EventArgs E)
        {
            InterfaceAdded?.Invoke();
        }

        private void AddRouteButton_Click(object Sender, EventArgs E)
        {
            RouteAdded?.Invoke();
        }


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

        private void GWConfigControl_GatewayChanged(IPAddress Obj)
        {
        }

        private void InterfaceConfigControl_InterfaceChanged(string Arg1, IPAddress Arg2, IPAddress Arg3)
        {
        }

        private void RouteConfigControl_RouteChanged(IPAddress Arg1, IPAddress Arg2, IPAddress Arg3, string Arg4)
        {
        }

        #endregion Eventhandling
    }
}
