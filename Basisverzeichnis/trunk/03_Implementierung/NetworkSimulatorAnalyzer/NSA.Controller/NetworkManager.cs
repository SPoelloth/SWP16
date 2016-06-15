using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using NSA.Controller.ViewControllers;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;
using Switch = NSA.Model.NetworkComponents.Switch;

namespace NSA.Controller
{
    internal class NetworkManager
    {
        #region Singleton

        private static NetworkManager instance = null;
        private static readonly object padlock = new object();

        public static NetworkManager Instance
        {
            get
            {
                lock (padlock)
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

            public int Compare(string x, string y)
            {
                if (x.Length < y.Length)
                {
                    // A < AA
                    return -1;
                }
                else if (x.Length > y.Length)
                {
                    // AA > A
                    return 1;
                }
                else
                {
                    return x.CompareTo(y);
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
            char nextLetter = (char)((int)uniqueNodeNames.Max[0] + 1);
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
        /// <param name="ip">The name of the workstation</param>
        /// <returns>The found workstation or null if it could not be found.</returns>
        public Hardwarenode GetWorkstationByIP(IPAddress ip)
        {
            return network.GetWorkstationByIP(ip) as Workstation;
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
        /// <param name="workstationName">The name of the workstation</param>
        /// <param name="number">The name of the interface</param>
        /// <param name="ipAddress">The new IPAddress of the interface</param>
        /// <param name="subnetmask">The new subnetmask of the interface</param>
        /// <returns>False if the interface could not be found, otherwise true</returns>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public bool InterfaceChanged(string workstationName, string interfaceName, IPAddress ipAddress, IPAddress subnetmask)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                return workstation.SetInterface(interfaceName, ipAddress, subnetmask);
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
            }
        }

        /// <summary>
        /// Adds a new interface to the workstation.
        /// </summary>
        /// <param name="workstationName">The name of the workstation</param>
        /// <param name="ipAddress">The ipAddress</param>
        /// <param name="subnetmask">The subnetmask</param>
        /// <returns>The new created interface.</returns>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public Interface AddInterfaceToWorkstation(string workstationName, IPAddress ipAddress, IPAddress subnetmask)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                return workstation.AddInterface(ipAddress, subnetmask);
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
            }
        }

        /// <summary>
        /// Adds a new interface to the switch.
        /// </summary>
        /// <param name="switchName">The name of the switch</param>
        /// <returns>The name of the new interface.</returns>
        /// <exception cref="System.ArgumentException">Switch could not be found</exception>
        public string AddInterfaceToSwitch(string switchName)
        {
            Switch nodeSwitch = network.GetHardwarenodeByName(switchName) as Switch;
            if (null != nodeSwitch)
            {
                return nodeSwitch.AddInterface();
            }
            else
            {
                throw new ArgumentException("Switch with the name " + switchName + " could not be found");
            }
        }

        /// <summary>
        /// Removes an interface from the workstation or as switch.
        /// </summary>
        /// <param name="nodeName">The name of the workstation or a switch</param>
        /// <param name="interfaceName">The name of the interface.</param>
        /// <exception cref="System.ArgumentException">Node could not be found</exception>
        public void RemoveInterface(string nodeName, string interfaceName)
        {
            Hardwarenode node = network.GetHardwarenodeByName(nodeName);
            if (null != node)
            {
                Workstation workstation;
                Switch nodeSwitch;

                if (null != (workstation = node as Workstation))
                {
                    workstation.RemoveInterface(interfaceName);
                }
                else if (null != (nodeSwitch = node as Switch))
                {
                    nodeSwitch.RemoveInterface(interfaceName);
                }
            }
            else
            {
                throw new ArgumentException("Node with the name " + nodeName + " could not be found");
            }
        }

        #endregion

        #region Route-related methods

        /// <summary>
        /// Updates the changed route with new values
        /// </summary>
        /// <param name="workstationName">The name of the workstation</param>
        /// <param name="routeName">The name of the changed route</param>
        /// <param name="destination">The new destination address</param>
        /// <param name="subnetmask">The new subnetmask</param>
        /// <param name="gateway">The new gateway</param>
        /// <param name="iface">The new interface</param>
        /// <exception cref="System.ArgumentException">Workstation or route could not be found</exception>
        public void RouteChanged(string workstationName, string routeName, IPAddress destination, IPAddress subnetmask,
            IPAddress gateway, Interface iface)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                if (!workstation.SetRoute(routeName, destination, subnetmask, gateway, iface))
                {
                    throw new ArgumentException("Route with the name " + routeName + " could not be found");
                }
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
            }
        }

        /// <summary>
        /// Adds a route to the routingtable of the workstation.
        /// </summary>
        /// <param name="workstationName">The name of the workstation</param>
        /// <param name="destination">The destination address</param>
        /// <param name="subnetmask">The subnetmask</param>
        /// <param name="gateway">The gateway</param>
        /// <param name="iface">The interface</param>
        /// <returns>The new created route.</returns>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public Route AddRoute(string workstationName, IPAddress destination, IPAddress subnetmask,
            IPAddress gateway, Interface iface)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                Route route = new Route(destination, subnetmask, gateway, iface);
                workstation.AddRoute(route);
                return route;
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
            }
        }


        /// <summary>
        /// Removes a route from the routingtable of the workstation.
        /// </summary>
        /// <param name="workstationName">The name of the workstation</param>
        /// <param name="routeName">The name of the route</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void RemoveRoute(string workstationName, string routeName)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                workstation.RemoveRoute(routeName);
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
            }
        }

        #endregion

        #region Hardwarenode-related methods

        /// <summary>
        /// Returns a hardwarenode object.
        /// </summary>
        /// <param name="name">The name of the hardwarenode</param>
        /// <returns>The hardwarenode object or null if it does not exist</returns>
        public Hardwarenode GetHardwarenodeByName(string name)
        {
            return network.GetHardwarenodeByName(name);
        }

        /// <summary>
        /// Creates a hardwarenode and adds it to the Network and to the NetworkViewController.
        /// </summary>
        /// <param name="type">Type of the node</param>
        /// <returns>The new created hardwarenode.</returns>
        public Hardwarenode CreateHardwareNode(HardwarenodeType type)
        {
            Hardwarenode node = null;

            Debug.Assert(!uniqueNodeNames.Contains(nextUniqueNodeName),
                "Could not create a unique name for a node!");

            switch (type)
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
        /// <param name="name">The name of the node to be removed</param>
        /// <exception cref="System.ArgumentException">Hardwarenode could not be found</exception>
        public void RemoveHardwarenode(string name)
        {
            Hardwarenode node = network.GetHardwarenodeByName(name);
            if (null == node)
            {
                throw new ArgumentException("Hardwarenode with name " + name + "could not be found");
            }

            network.RemoveHardwarnode(name);

            if (uniqueNodeNames.Contains(name))
            {
                uniqueNodeNames.Remove(name);
                // Reuse the name for a future node.
                nextUniqueNodeName = name;
            }
        }

        #endregion

        #region Connection-related methods

        /// <summary>
        /// Creates a new connection. Adds the connection to the Network and to the NetworkViewController.
        /// </summary>
        /// <param name="start">The start node of the connection</param>
        /// <param name="StartNodeInterfaceName">The name of the Interface at which the Connection is pluged in at the start node</param>
        /// <param name="end">The end node of the connection</param>
        /// <param name="EndNodeInterfaceName">The name of the Interface at which the Connection is pluged in at the end node</param>
        /// <returns>The new created connection.</returns>
        /// <exception cref="System.ArgumentException">Start or end node could not be found</exception>
        public Connection CreateConnection(string start, string StartNodeInterfaceName, string end, string EndNodeInterfaceName)
        {
            Hardwarenode A = GetHardwarenodeByName(start);
            if (null == A)
            {
                throw new ArgumentException("Hardwarenode with the name " + start + " could not be found");
            }
            Hardwarenode B = GetHardwarenodeByName(end);
            if (null == B)
            {
                throw new ArgumentException("Hardwarenode with the name " + end + " could not be found");
            }
            if (A == B)
            {
                throw new ArgumentException("CreateConnection: start equals end");
            }

            Connection connection = new Connection(A, B);

            network.AddConnection(StartNodeInterfaceName, EndNodeInterfaceName, connection);
            NetworkViewController.Instance.AddConnection(connection);

            return connection;
        }

        /// <summary>
        /// Removes the connection from the Network and from the NetworkViewController.
        /// </summary>
        /// <param name="name">The name of the connection to be removed</param>
        /// <exception cref="System.ArgumentException">Connection could not be found</exception>
        public void RemoveConnection(string name)
        {
            Connection connection = network.GetConnectionByName(name);
            if (null == connection)
            {
                throw new ArgumentException("Connection with the name " + name + "could not be found");
            }

            network.RemoveConnection(name);
        }

        #endregion

        /// <summary>
        /// Changes the gateway of a workstation.
        /// </summary>
        /// <param name="workstationName">The name of the workstation</param>
        /// <param name="gateway">The new gateway</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void GatewayChanged(string workstationName, IPAddress gateway)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                workstation.StandardGateway = gateway;
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
            }
        }
    }
}
