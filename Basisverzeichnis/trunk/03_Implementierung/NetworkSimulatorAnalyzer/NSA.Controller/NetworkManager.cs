using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NSA.Controller.ViewControllers;
using NSA.Model.NetworkComponents;
using Switch = NSA.Model.NetworkComponents.Switch;

namespace NSA.Controller
{
    internal class NetworkManager
    {
        #region Singleton

        private static NetworkManager instance;
        private static readonly object Padlock = new object();

        public static NetworkManager Instance
        {
            get
            {
                lock (Padlock)
                {
                    return instance ?? (instance = new NetworkManager());
                }
            }
        }

        #endregion Singleton

        /// <summary>
        /// Type of the hardwarenode
        /// </summary>
        public enum HardwarenodeType
        {
            Switch,
            Workstation,
            Router
        };

        private Network network; 

        // Default constructor:
        private NetworkManager()
        {
            network = ProjectManager.Instance.CurrentProject.Network;
        }

        #region Workstation-related methods

        /// <summary>
        /// Searches and returns a workstation
        /// </summary>
        /// <param name="Ip">The name of the workstation</param>
        /// <returns>The found workstation or null if it could not be found.</returns>
        public Hardwarenode GetWorkstationByIp(IPAddress Ip)
        {
            return network.GetWorkstationByIp(Ip) as Workstation;
        }

        /// <summary>
        /// Gets all the workstations
        /// </summary>
        /// <returns>The workstations</returns>
        public List<Workstation> GetAllWorkstations()
        {
            return network.GetAllWorkstations();
        }

        public List<Hardwarenode> GetAllHardwareNodes()
        {
            return network.GetAllHardwarenodes();
        }

        public List<Connection> GetAllConnections()
        {
            return network.GetAllConnections();
        }

        #endregion

        #region Interface-related methods

        /// <summary>
        /// Changes the interface of the workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="InterfaceName"></param>
        /// <param name="IpAddress">The new IPAddress of the interface</param>
        /// <param name="Subnetmask">The new subnetmask of the interface</param>
        /// <returns>False if the interface could not be found, otherwise true</returns>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public bool InterfaceChanged(string WorkstationName, string InterfaceName, IPAddress IpAddress, IPAddress Subnetmask)
        {
            Workstation workstation = network.GetHardwarenodeByName(WorkstationName) as Workstation;
            if (null != workstation)
            {
                return workstation.SetInterface(InterfaceName, IpAddress, Subnetmask);
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + WorkstationName + " could not be found");
            }
        }

        /// <summary>
        /// Adds a new interface to the workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="IpAddress">The ipAddress</param>
        /// <param name="Subnetmask">The subnetmask</param>
        /// <returns>The new created interface.</returns>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public Interface AddInterfaceToWorkstation(string WorkstationName, IPAddress IpAddress, IPAddress Subnetmask)
        {
            Workstation workstation = network.GetHardwarenodeByName(WorkstationName) as Workstation;
            if (null != workstation)
            {
                return workstation.AddInterface(IpAddress, Subnetmask);

            }
            else
            {
                throw new ArgumentException("Workstation with the name " + WorkstationName + " could not be found");
            }
        }

        /// <summary>
        /// Adds a new interface to the switch.
        /// </summary>
        /// <param name="SwitchName">The name of the switch</param>
        /// <returns>The name of the new interface.</returns>
        /// <exception cref="System.ArgumentException">Switch could not be found</exception>
        public string AddInterfaceToSwitch(string SwitchName)
        {
            Switch nodeSwitch = network.GetHardwarenodeByName(SwitchName) as Switch;
            if (null != nodeSwitch)
            {
                return nodeSwitch.AddInterface();
            }
            else
            {
                throw new ArgumentException("Switch with the name " + SwitchName + " could not be found");
            }
        }

        /// <summary>
        /// Removes an interface from the workstation or as switch.
        /// </summary>
        /// <param name="NodeName">The name of the workstation or a switch</param>
        /// <param name="InterfaceName">The name of the interface.</param>
        /// <exception cref="System.ArgumentException">Node could not be found</exception>
        public void RemoveInterface(string NodeName, string InterfaceName)
        {
            Hardwarenode node = network.GetHardwarenodeByName(NodeName);
            if (null != node)
            {
                Workstation workstation;
                Switch nodeSwitch;

                if (null != (workstation = node as Workstation))
                {
                    workstation.RemoveInterface(InterfaceName);
                    Connection c = workstation.GetConnectionAtPort(InterfaceName);
                    if(c != null)
                        RemoveConnection(c.Name);
                }
                else if (null != (nodeSwitch = node as Switch))
                {
                    nodeSwitch.RemoveInterface(InterfaceName);
                    Connection c = nodeSwitch.GetConnectionAtPort(InterfaceName);
                    if (c != null)
                        RemoveConnection(c.Name);
                }
            }
            else
            {
                throw new ArgumentException("Node with the name " + NodeName + " could not be found");
            }
        }

        /// <summary>
        /// Sets the switch interface count.
        /// </summary>
        /// <param name="NodeName">Name of the node.</param>
        /// <param name="NewCount">The new count.</param>
        /// <exception cref="System.ArgumentException">
        /// Node with the name  + NodeName +  is no switch
        /// or
        /// Node with the name  + NodeName +  could not be found
        /// </exception>
        public void SetSwitchInterfaceCount(string NodeName, int NewCount)
        {
            Hardwarenode node = network.GetHardwarenodeByName(NodeName);
            if (null != node)
            {
                Switch nodeSwitch;

                if (null != (nodeSwitch = node as Switch))
                {
                    if (NewCount > nodeSwitch.GetInterfaceCount())
                    {
                        for (int i = nodeSwitch.GetInterfaceCount(); i < NewCount; i++)
                            nodeSwitch.AddInterface();
                        NetworkViewController.Instance.NodeChanged(nodeSwitch);
                    }
                    else if (NewCount < nodeSwitch.GetInterfaceCount())
                    {
                        for(int i = nodeSwitch.GetInterfaceCount(); i > NewCount; i--)
                            nodeSwitch.RemoveInterface("eth" + (i - 1));
                        NetworkViewController.Instance.NodeChanged(nodeSwitch);
                    }
                }
                else
                {
                    throw new ArgumentException("Node with the name " + NodeName + " is no switch");
                }
            }
            else
            {
                throw new ArgumentException("Node with the name " + NodeName + " could not be found");
            }

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
        /// <exception cref="System.ArgumentException">Workstation or route could not be found</exception>
        public void RouteChanged(string WorkstationName, string RouteName, IPAddress Destination, IPAddress Subnetmask,
            IPAddress Gateway, Interface Iface)
        {
            Workstation workstation = network.GetHardwarenodeByName(WorkstationName) as Workstation;
            if (null != workstation)
            {
                if (!workstation.SetRoute(RouteName, Destination, Subnetmask, Gateway, Iface))
                {
                    throw new ArgumentException("Route with the name " + RouteName + " could not be found");
                }
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + WorkstationName + " could not be found");
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
        /// <returns>The new created route.</returns>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public Route AddRoute(string WorkstationName, IPAddress Destination, IPAddress Subnetmask,
            IPAddress Gateway, Interface Iface)
        {
            Workstation workstation = network.GetHardwarenodeByName(WorkstationName) as Workstation;
            if (null != workstation)
            {
                Route route = new Route(Destination, Subnetmask, Gateway, Iface);
                workstation.AddRoute(route);
                return route;
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + WorkstationName + " could not be found");
            }
        }


        /// <summary>
        /// Removes a route from the routingtable of the workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="RouteName">The name of the route</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void RemoveRoute(string WorkstationName, string RouteName)
        {
            Workstation workstation = network.GetHardwarenodeByName(WorkstationName) as Workstation;
            if (null != workstation)
            {
                workstation.RemoveRoute(RouteName);
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + WorkstationName + " could not be found");
            }
        }

        #endregion

        #region Hardwarenode-related methods

        /// <summary>
        /// Returns a hardwarenode object.
        /// </summary>
        /// <param name="Name">The name of the hardwarenode</param>
        /// <returns>The hardwarenode object or null if it does not exist</returns>
        public Hardwarenode GetHardwarenodeByName(string Name)
        {
            return network.GetHardwarenodeByName(Name);
        }

        /// <summary>
        /// Creates a hardwarenode and adds it to the Network and to the NetworkViewController.
        /// </summary>
        /// <param name="Type">Type of the node</param>
        /// <returns>The new created hardwarenode.</returns>
        public Hardwarenode CreateHardwareNode(HardwarenodeType Type)
        {
            Hardwarenode node = null;

            switch (Type)
            {
                case HardwarenodeType.Switch:

                    node = new Switch(CreateUniqueName(Type));
                    break;
                case HardwarenodeType.Workstation:
                    node = new Workstation(CreateUniqueName(Type));
                    break;
                case HardwarenodeType.Router:
                    node = new Router(CreateUniqueName(Type));
                    break;
            }
            
            // Add node to the Network and to the NetworkViewController
            network.AddHardwarenode(node);
            NetworkViewController.Instance.AddHardwarenode(node);

            return node;
        }

        private string CreateUniqueName(HardwarenodeType type)
        {
            for(int i = 1; ; i++)
            {
                string name = $"{type} {i}";
                if (GetAllHardwareNodes().All(n => n.Name != name)) return name;
            }
        }

        /// <summary>
        /// Removes the hardwarenode from the Network and from the NetworkViewController.
        /// </summary>
        /// <param name="Name">The name of the node to be removed</param>
        /// <exception cref="System.ArgumentException">Hardwarenode could not be found</exception>
        public void RemoveHardwarenode(string Name)
        {
            Hardwarenode node = network.GetHardwarenodeByName(Name);
            if (null == node)
            {
                throw new ArgumentException("Hardwarenode with name " + Name + "could not be found");
            }

            network.RemoveHardwarnode(Name);

            NetworkViewController.Instance.RemoveHardwarenode(Name);
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
        /// <returns>The new created connection.</returns>
        /// <exception cref="System.ArgumentException">Start or end node could not be found</exception>
        public Connection CreateConnection(string Start, string StartNodeInterfaceName, string End, string EndNodeInterfaceName)
        {
            Hardwarenode a = GetHardwarenodeByName(Start);
            if (null == a)
            {
                throw new ArgumentException("Hardwarenode with the name " + Start + " could not be found");
            }
            Hardwarenode b = GetHardwarenodeByName(End);
            if (null == b)
            {
                throw new ArgumentException("Hardwarenode with the name " + End + " could not be found");
            }
            if (a == b)
            {
                throw new ArgumentException("CreateConnection: start equals end");
            }

            Connection connection = new Connection(a, b);

            network.AddConnection(StartNodeInterfaceName, EndNodeInterfaceName, connection);
            NetworkViewController.Instance.AddConnection(connection);

            return connection;
        }

        /// <summary>
        /// Removes the connection from the Network and from the NetworkViewController.
        /// </summary>
        /// <param name="Name">The name of the connection to be removed</param>
        /// <exception cref="System.ArgumentException">Connection could not be found</exception>
        public void RemoveConnection(string Name)
        {
            Connection connection = network.GetConnectionByName(Name);
            if (null == connection)
            {
                throw new ArgumentException("Connection with the name " + Name + "could not be found");
            }
            connection.End.RemoveConnection(Name);
            connection.Start.RemoveConnection(Name);
            network.RemoveConnection(Name);
            NetworkViewController.Instance.RemoveConnection(Name);
        }

        #endregion

        /// <summary>
        /// Changes the gateway of a workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="Gateway">The new gateway</param>
        /// <param name="InterfaceName">Name of the assigned interface</param>
        /// <param name="HasInternetAccess">Indicates whether this node has internet access</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void GatewayChanged(string WorkstationName, IPAddress Gateway, string InterfaceName, bool HasInternetAccess)
        {
            // TODO: Do something with HasInternetAccess and InterfaceName
            Workstation workstation = network.GetHardwarenodeByName(WorkstationName) as Workstation;
            if (null != workstation)
            {
                workstation.StandardGateway = Gateway;
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + WorkstationName + " could not be found");
            }
        }
    }
}
