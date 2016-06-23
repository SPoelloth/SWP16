using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using NSA.Controller.ViewControllers;
using NSA.Model.NetworkComponents;
using Switch = NSA.Model.NetworkComponents.Switch;
// ReSharper disable HeuristicUnreachableCode

namespace NSA.Controller
{
    internal class NetworkManager
    {
        public static NetworkManager Instance = new NetworkManager();
        private readonly Network network;

        private NetworkManager()
        {
            network = ProjectManager.Instance.CurrentProject.Network;
        }

        #region Workstation-related methods

        /// <summary>
        /// Searches and returns a workstation
        /// </summary>
        /// <param name="Ip">The name of the workstation</param>
        /// <returns>
        /// The found workstation or null if it could not be found.
        /// </returns>
        public Hardwarenode GetWorkstationByIp(IPAddress Ip)
        {
            return network.GetWorkstationByIp(Ip) as Workstation;
        }

        /// <summary>
        /// Gets all the workstations
        /// </summary>
        /// <returns>
        /// The workstations
        /// </returns>
        public List<Workstation> GetAllWorkstations()
        {
            return network.GetAllWorkstations();
        }

        /// <summary>
        /// Gets all hardware nodes.
        /// </summary>
        /// <returns></returns>
        public List<Hardwarenode> GetAllHardwareNodes()
        {
            return network.GetAllHardwarenodes();
        }

        /// <summary>
        /// Gets all connections.
        /// </summary>
        /// <returns></returns>
        public List<Connection> GetAllConnections()
        {
            return network.Connections;
        }

        /// <summary>
        /// Gets the hardware nodes of the subnet.
        /// </summary>
        /// <param name="Subnetmask">The subnetmask.</param>
        /// <returns></returns>
        public List<Workstation> GetHardwareNodesForSubnet(string Subnetmask)
        {
            return network.GetHardwareNodesForSubnet(Subnetmask);
        }

        #endregion

        #region Interface-related methods

        /// <summary>
        /// Changes the interface of the workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="InterfaceName">Name of the interface.</param>
        /// <param name="IpAddress">The new IPAddress of the interface</param>
        /// <param name="Subnetmask">The new subnetmask of the interface</param>
        public void InterfaceChanged(string WorkstationName, string InterfaceName, IPAddress IpAddress, IPAddress Subnetmask)
        {
            GetWorkstationByName(WorkstationName)?.SetInterface(InterfaceName, IpAddress, Subnetmask);
        }

        /// <summary>
        /// Adds a new interface to the workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="IpAddress">The ipAddress</param>
        /// <param name="Subnetmask">The subnetmask</param>
        /// <param name="Portnum">The portnumber.</param>
        public void AddInterfaceToWorkstation(string WorkstationName, IPAddress IpAddress, IPAddress Subnetmask, int Portnum = -1)
        {
            Workstation workstation = GetWorkstationByName(WorkstationName);
            var iface = workstation.AddInterface(IpAddress, Subnetmask, Portnum);
            NetworkViewController.Instance.AddInterfaceToHardwareNode(workstation.Name, iface.Name);
        }

        /// <summary>
        /// Adds a new interface to the switch.
        /// </summary>
        /// <param name="SwitchName">The name of the switch</param>
        public void AddInterfaceToSwitch(string SwitchName)
        {
            Switch nodeSwitch = network.GetHardwarenodeByName(SwitchName) as Switch;
            if (nodeSwitch == null)
            {
                Debug.Assert(nodeSwitch != null, "Switch with the name " + SwitchName + " could not be found");
                return;
            }
            var iface = nodeSwitch.AddInterface();
            NetworkViewController.Instance.AddInterfaceToHardwareNode(nodeSwitch.Name, iface);
        }

        /// <summary>
        /// Removes an interface from the workstation or as switch.
        /// </summary>
        /// <param name="NodeName">The name of the workstation or a switch</param>
        /// <param name="InterfaceName">The name of the interface.</param>
        public void RemoveInterface(string NodeName, string InterfaceName)
        {
            Hardwarenode node = network.GetHardwarenodeByName(NodeName);
            if (node == null) { Debug.Assert(node != null, "Node with the name " + NodeName + " could not be found"); return; }
            Workstation workstation = node as Workstation;
            Switch nodeSwitch = node as Switch;

            if (workstation != null)
            {
                workstation.RemoveInterface(InterfaceName);
                NetworkViewController.Instance.RemoveInterfaceFromNode(workstation.Name, InterfaceName);
                Connection c = workstation.GetConnectionAtPort(InterfaceName);
                if (c != null)
                {
                    RemoveConnection(c.Name);
                }
            }
            else if (nodeSwitch != null)
            {
                nodeSwitch.RemoveInterface(InterfaceName);
                NetworkViewController.Instance.RemoveInterfaceFromNode(nodeSwitch.Name, InterfaceName);
                Connection c = nodeSwitch.GetConnectionAtPort(InterfaceName);
                if (c != null)
                {
                    RemoveConnection(c.Name);
                }
            }
        }

        /// <summary>
        /// Sets the switch interface count.
        /// </summary>
        /// <param name="NodeName">Name of the node.</param>
        /// <param name="NewCount">The new count.</param>
        public void SetSwitchInterfaceCount(string NodeName, int NewCount)
        {
            Switch nodeSwitch = network.GetHardwarenodeByName(NodeName) as Switch;
            if (nodeSwitch == null)
            {
                Debug.Assert(nodeSwitch != null, "Node with the name " + NodeName + " could not be found");
                return;
            }
            if (NewCount == nodeSwitch.GetInterfaceCount()) return;
            var interfaceDiff = nodeSwitch.Interfaces.ToList();
            nodeSwitch.SetInterfaceCount(NewCount);
            interfaceDiff = interfaceDiff.Except(nodeSwitch.Interfaces).ToList();
            foreach (var i in interfaceDiff)
            {
                string connnectionName = nodeSwitch.Connections.FirstOrDefault(C => C.Key.Equals(i)).Value?.Name;
                if (connnectionName != null) RemoveConnection(connnectionName);
            }

            NetworkViewController.Instance.SwitchChanged(nodeSwitch);
        }

        #endregion

        #region Route-related methods

        /// <summary>
        /// Updates the changed route with new values
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="RouteName">The name of the changed route</param>
        /// <param name="Destination">The new destination address</param>
        /// <param name="Subnetmask">The new subnetmask</param>
        /// <param name="Gateway">The new gateway</param>
        /// <param name="Iface">The new interface</param>
        public void RouteChanged(string WorkstationName, string RouteName, IPAddress Destination, IPAddress Subnetmask, IPAddress Gateway, Interface Iface)
        {
            if (GetWorkstationByName(WorkstationName)?.SetRoute(RouteName, Destination, Subnetmask, Gateway, Iface) != true)
            {
                Debug.Assert(false, "Route with the name " + RouteName + " could not be found");
            }
        }

        /// <summary>
        /// Adds a route to the routingtable of the workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="Destination">The destination address</param>
        /// <param name="Subnetmask">The subnetmask</param>
        /// <param name="Gateway">The gateway</param>
        /// <param name="Iface">The interface</param>
        public void AddRoute(string WorkstationName, IPAddress Destination, IPAddress Subnetmask, IPAddress Gateway, Interface Iface)
        {
            GetWorkstationByName(WorkstationName)?.AddRoute(new Route(Destination, Subnetmask, Gateway, Iface));
        }


        /// <summary>
        /// Removes a route from the routingtable of the workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="RouteName">The name of the route</param>
        public void RemoveRoute(string WorkstationName, string RouteName)
        {
            GetWorkstationByName(WorkstationName)?.RemoveRoute(RouteName);
        }

        #endregion

        #region Hardwarenode-related methods

        private Workstation GetWorkstationByName(string Name)
        {
            Workstation workstation = network.GetHardwarenodeByName(Name) as Workstation;
            Debug.Assert(workstation != null, "Workstation with the name " + Name + " could not be found");
            return workstation;
        }

        /// <summary>
        /// Returns a hardwarenode object.
        /// </summary>
        /// <param name="Name">The name of the hardwarenode</param>
        /// <returns>
        /// The hardwarenode object or null if it does not exist
        /// </returns>
        public Hardwarenode GetHardwarenodeByName(string Name)
        {
            return network.GetHardwarenodeByName(Name);
        }

        /// <summary>
        /// Creates a hardwarenode and adds it to the Network and to the NetworkViewController.
        /// </summary>
        /// <param name="Type">Type of the node</param>
        /// <param name="Name">The name.</param>
        /// <returns>
        /// The new created hardwarenode.
        /// </returns>
        public Hardwarenode CreateHardwareNode(HardwarenodeType Type, string Name = null)
        {
            Hardwarenode node = null;
            string nodeName = Name ?? CreateUniqueName(Type);

            switch (Type)
            {
                case HardwarenodeType.Switch:
                    node = new Switch(nodeName);
                    break;
                case HardwarenodeType.Workstation:
                    node = new Workstation(nodeName);
                    break;
                case HardwarenodeType.Router:
                    node = new Router(nodeName);
                    break;
            }

            // Add node to the Network and to the NetworkViewController
            network.AddHardwarenode(node);
            NetworkViewController.Instance.AddHardwarenode(node);

            return node;
        }

        /// <summary>
        /// Creates a unique name.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <returns></returns>
        private string CreateUniqueName(HardwarenodeType Type)
        {
            for (int i = 1; ; i++)
            {
                string name = $"{Type} {i}";
                if (GetAllHardwareNodes().All(N => N.Name != name)) return name;
            }
        }

        /// <summary>
        /// Removes the hardwarenode from the Network and from the NetworkViewController.
        /// </summary>
        /// <param name="Name">The name of the node to be removed</param>
        public void RemoveHardwarenode(string Name)
        {
            Hardwarenode node = network.GetHardwarenodeByName(Name);
            if (node == null)
            {
                Debug.Assert(node != null, "Hardwarenode with name " + Name + "could not be found");
                return;
            }
            network.RemoveHardwarnode(Name);
            NetworkViewController.Instance.RemoveHardwarenode(Name);
        }

        /// <summary>
        /// Changes a HardwareNode's name and makes the PropertyController reload its properties.
        /// </summary>
        /// <param name="OldName">The old name.</param>
        /// <param name="NewName">The new name.</param>
        public void RenameHardwarenode(string OldName, string NewName)
        {
            if (GetAllHardwareNodes().FirstOrDefault(N => N.Name == NewName) == null)
            {
                Debug.Assert(false);
                return;
            }
            GetAllHardwareNodes().First(N => N.Name == OldName).Name = NewName;
            PropertyController.Instance.LoadElementProperties(NewName);
        }
        #endregion

        #region Connection-related methods

        /// <summary>
        /// Creates a new connection. Adds the connection to the Network and to the NetworkViewController.
        /// </summary>
        /// <param name="Start">The start node of the connection</param>
        /// <param name="StartNodeInterfaceName">The name of the Interface at which the Connection is pluged in at the start node</param>
        /// <param name="End">The end node of the connection</param>
        /// <param name="EndNodeInterfaceName">The name of the Interface at which the Connection is pluged in at the end node</param>
        public void CreateConnection(string Start, string StartNodeInterfaceName, string End, string EndNodeInterfaceName)
        {
            Hardwarenode a = GetHardwarenodeByName(Start);
            Hardwarenode b = GetHardwarenodeByName(End);
            if (a == null || b == null)
            {
                Debug.Assert(false, "Hardwarenode could not be found");
                return;
            }
            if (a == b) return;
            Connection connection = new Connection(a, b);
            network.AddConnection(StartNodeInterfaceName, EndNodeInterfaceName, connection);
            NetworkViewController.Instance.AddConnection(connection);
        }

        /// <summary>
        /// Removes the connection from the Network and from the NetworkViewController.
        /// </summary>
        /// <param name="Name">The name of the connection to be removed</param>
        public void RemoveConnection(string Name)
        {
            Connection connection = network.GetConnectionByName(Name);
            if (connection == null)
            {
                Debug.Assert(connection != null, "Connection with the name " + Name + "could not be found");
                return;
            }
            network.RemoveConnection(Name);
            NetworkViewController.Instance.RemoveConnection(Name);
        }

        #endregion

        /// <summary>
        /// Changes the gateway of a workstation or router.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="Gateway">The new gateway</param>
        /// <param name="InterfaceName">Name of the assigned interface</param>
        /// <param name="HasInternetAccess">Indicates whether this node has internet access</param>
        public void GatewayChanged(string WorkstationName, IPAddress Gateway, string InterfaceName, bool HasInternetAccess)
        {
            // TODO: Do something with HasInternetAccess and InterfaceName
            Workstation ws = GetWorkstationByName(WorkstationName);
            ws.StandardGateway = Gateway;
            ws.StandardGatewayPort = ws.GetInterfaces().First(I => I.Name == InterfaceName);
            var r = ws as Router;
            if (r != null) r.IsGateway = HasInternetAccess;
        }

        /// <summary>
        /// Type of the hardwarenode
        /// </summary>
        public enum HardwarenodeType
        {
            Switch,
            Workstation,
            Router
        }
    }
}
