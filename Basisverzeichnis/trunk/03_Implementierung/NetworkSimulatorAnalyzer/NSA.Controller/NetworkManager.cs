using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Computer,
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
        private readonly SortedSet<string> uniqueNodeNames = new SortedSet<string>(new NodeNamesComparator());
        // If we remove a node we will reuse its name for future nodes
        private string nextUniqueNodeName = "A";

        // Default constructor:
        private NetworkManager()
        {
            network = new Network();
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
        /// <param name="number">The number of the interface(e.g. 0 for eth0).</param>
        /// <param name="ipAddress">The new IPAddress of the interface</param>
        /// <param name="subnetmask">The new subnetmask of the interface</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void InterfaceChanged(string workstationName, int number, IPAddress ipAddress, IPAddress subnetmask)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                Interface myInterface = workstation.GetInterfaces().Single(i => i.Name == Interface.NamePrefix+number);
                myInterface.IpAddress = ipAddress;
                myInterface.Subnetmask = subnetmask;
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
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void AddInterface(string workstationName, IPAddress ipAddress, IPAddress subnetmask)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                workstation.AddInterface(ipAddress, subnetmask);
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
            }
        }

        /// <summary>
        /// Removes an interface from the workstation.
        /// </summary>
        /// <param name="workstationName">The name of the workstation</param>
        /// <param name="number">The number of the interface(e.g. 0 for eth0).</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void RemoveInterface(string workstationName, int number)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                workstation.RemoveInterface(number);
            }
            else
            {
                throw new ArgumentException("Workstation with the name " + workstationName + " could not be found");
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
        /// <param name="routeName">The name of the route</param>
        /// <param name="destination">The destination address</param>
        /// <param name="subnetmask">The subnetmask</param>
        /// <param name="gateway">The gateway</param>
        /// <param name="iface">The interface</param>
        /// <exception cref="System.ArgumentException">Workstation could not be found</exception>
        public void AddRoute(string workstationName, string routeName, IPAddress destination, IPAddress subnetmask,
            IPAddress gateway, Interface iface)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                Route route = new Route(routeName, destination, subnetmask, gateway, iface);
                workstation.AddRoute(route);
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
        public void CreateHardwareNode(HardwarenodeType type)
        {
            Hardwarenode node = null;

            if (uniqueNodeNames.Contains(nextUniqueNodeName))
            {
                throw new InvalidOperationException("Could not create a unique name for a node!");
            }

            switch (type)
            {
                case HardwarenodeType.Switch:
                    node = new Switch(nextUniqueNodeName);
                    break;
                case HardwarenodeType.Workstation:
                    node = new Workstation(nextUniqueNodeName);
                    break;
                case HardwarenodeType.Computer:
                    node = new Computer(nextUniqueNodeName);
                    break;
                case HardwarenodeType.Router:
                    node = new Router(nextUniqueNodeName);
                    break;
            }

            uniqueNodeNames.Add(nextUniqueNodeName);
            // Add node to the Network and to the NetworkViewController
            network.AddHardwarenode(node);
            NetworkViewController.Instance.AddHardwarenode(node);

            // Create a unique name for the next hardwarenode that will be created.
            // Sorting order: A B C ... Z AA BB ... AAA BBB
            char nextLetter = (char)((int)uniqueNodeNames.Max[0]+1);
            int letterRepeatTimes = uniqueNodeNames.Max.Length;
            if (nextLetter > 'Z')
            {
                nextLetter = 'A';
                letterRepeatTimes += 1;
            }
            nextUniqueNodeName = new string(nextLetter, letterRepeatTimes);
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
        /// <exception cref="System.ArgumentException">Start or end node could not be found</exception>
        public void CreateConnection(string start, string StartNodeInterfaceName, string end, string EndNodeInterfaceName)
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

            Connection newConnection = new Connection(A, B);

            network.AddConnection(StartNodeInterfaceName, EndNodeInterfaceName, newConnection);
            NetworkViewController.Instance.AddConnection(newConnection);
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
