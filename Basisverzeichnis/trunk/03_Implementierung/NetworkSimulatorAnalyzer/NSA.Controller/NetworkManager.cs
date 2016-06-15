using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Comparer for sorting the node names.
        /// Sorting order: A B C ... Z AA BB ... AAA BBB
        /// </summary>
        private class NodeNamesComparator : IComparer<string>
        {

            public int Compare(string X, string Y)
            {
                if (X.Length < Y.Length)
                {
                    // A < AA
                    return -1;
                }
                else if (X.Length > Y.Length)
                {
                    // AA > A
                    return 1;
                }
                else
                {
                    return String.Compare(X, Y, StringComparison.Ordinal);
                }
            }
        }

        private Network network; 
        // Unique names for each hardwarenode
        private SortedSet<string> uniqueNodeNames;
        // If we remove a node we will reuse its name for future nodes
        private string nextUniqueNodeName;

        // Default constructor:
        private NetworkManager()
        {
            Reset();
        }

        /// <summary>
        /// Helper method. Creates a unique node name for the node to be created next.
        /// </summary>
        private void CreateUniqueNameForNextNode()
        {
            // Create a unique name for the next hardwarenode that will be created.
            // Sorting order: A B C ... Z AA BB ... AAA BBB
            char nextLetter = (char)(uniqueNodeNames.Max[0] + 1);
            int letterRepeatTimes = uniqueNodeNames.Max.Length;
            if (nextLetter > 'Z')
            {
                nextLetter = 'A';
                letterRepeatTimes += 1;
            }
            nextUniqueNodeName = new string(nextLetter, letterRepeatTimes);
        }

        /// <summary>
        /// This method should be called if we want to reset the state of the NetworkManager, e.g.
        /// after we have loaded a new project.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Network contains nodes with the same name</exception>
        public void Reset()
        {
            Network oldNetwork = network;
            network = ProjectManager.Instance.CurrentProject.Network;
            if (network == oldNetwork)
            {
                // Reset() has already been called with this network object
                return;
            }

            uniqueNodeNames = new SortedSet<string>(new NodeNamesComparator());
            nextUniqueNodeName = "A";

            // Read in all the hardwarenode names
            List<Hardwarenode> nodes = network.GetAllHardwarenodes();
            foreach (Hardwarenode node in nodes)
            {
                if (uniqueNodeNames.Contains(nextUniqueNodeName))
                {
                    throw new InvalidOperationException("Network contains nodes with the same name!");
                }
                uniqueNodeNames.Add(node.Name);
            }

            if (uniqueNodeNames.Count > 0)
            {
                CreateUniqueNameForNextNode();
            }
        }

        #region Workstation-related methods

        /// <summary>
        /// Searches and returns a workstation
        /// </summary>
        /// <param name="Ip">The name of the workstation</param>
        /// <returns>The found workstation or null if it could not be found.</returns>
        public Hardwarenode GetWorkstationByIp(IPAddress Ip)
        {
            return network.GetWorkstationByIP(Ip) as Workstation;
        }

        /// <summary>
        /// Gets all the workstations
        /// </summary>
        /// <returns>The workstations</returns>
        public List<Workstation> GetAllWorkstations()
        {
            return network.GetAllWorkstations();
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
                }
                else if (null != (nodeSwitch = node as Switch))
                {
                    nodeSwitch.RemoveInterface(InterfaceName);
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

            Debug.Assert(!uniqueNodeNames.Contains(nextUniqueNodeName),
                "Could not create a unique name for a node!");

            switch (Type)
            {
                case HardwarenodeType.Switch:
                    node = new Switch(nextUniqueNodeName);
                    break;
                case HardwarenodeType.Workstation:
                    node = new Workstation(nextUniqueNodeName);
                    break;
                case HardwarenodeType.Router:
                    node = new Router(nextUniqueNodeName);
                    break;
            }

            uniqueNodeNames.Add(nextUniqueNodeName);
            // Add node to the Network and to the NetworkViewController
            network.AddHardwarenode(node);
            NetworkViewController.Instance.AddHardwarenode(node);

            CreateUniqueNameForNextNode();

            return node;
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

            if (uniqueNodeNames.Contains(Name))
            {
                uniqueNodeNames.Remove(Name);
                // Reuse the name for a future node.
                nextUniqueNodeName = Name;
            }
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

            network.RemoveConnection(Name);
        }

        #endregion

        /// <summary>
        /// Changes the gateway of a workstation.
        /// </summary>
        /// <param name="WorkstationName">The name of the workstation</param>
        /// <param name="Gateway">The new gateway</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void GatewayChanged(string WorkstationName, IPAddress Gateway)
        {
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
