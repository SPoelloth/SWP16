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
            createConfigControls();
        }

        private void createConfigControls()
        {
            // todo
        }

        private Hardwarenode getWorkstationByIP(IPAddress ip)
        {
            Hardwarenode node = null;

            // todo

            return node;
        }

        public void HardwarenodeSelected()
        {
            // todo
        }

        public void InterfaceChanged(string workstationName, string interfaceName, IPAddress ipAddress, IPAddress subnetmask)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                // todo
               // List<Interface> interfaces = workstation.GetInterfaces();
            }
        }

        public void RouteChanged(string workstationName, IPAddress destination, IPAddress subnetmask,
            IPAddress gateway, Interface iface)
        {
            Workstation workstation = network.GetHardwarenodeByName(workstationName) as Workstation;
            if (null != workstation)
            {
                // todo: missing methods in network
                // make Routingtable in Network public
                // or
                // add "int GetRoutesCount()" and "Route GetRouteAt(int routeIndex)" to Network
            }
        }

        public void GatewayChanged(string workstationName, IPAddress gateway)
        {
            // todo
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
        /// Creates a new connection. Adds the connection to the Network and to the NetworkViewController.
        /// </summary>
        /// <param name="start">The start node of the connection</param>
        /// <param name="end">The end node of the connection</param>
        public void CreateConnection(string start, string end)
        {
            Hardwarenode A = GetHardwarenodeByName(start);
            Hardwarenode B = GetHardwarenodeByName(end);
            Connection newConnection = new Connection(A, B);

            network.AddConnection(newConnection);
            NetworkViewController.Instance.AddConnection(newConnection);
        }

        /// <summary>
        /// Removes the hardwarenode from the Network and from the NetworkViewController.
        /// </summary>
        /// <param name="name">The name of the node to be removed</param>
        public void RemoveHardwarenode(string name)
        {
            Hardwarenode node = network.GetHardwarenodeByName(name);

            network.RemoveHardwarnode(name);
            NetworkViewController.Instance.RemoveHardwarenode(node);
        }

        /// <summary>
        /// Removes the connection from the Network and from the NetworkViewController.
        /// </summary>
        /// <param name="name">The name of the connection to be removed</param>
        public void RemoveConnection(string name)
        {
            //Connection connection = network.GetConnectionByName(name); // todo: misssing method in Network
            //NetworkViewController.Instance.RemoveConnection(connection);
            //network.RemoveConnection(name); // todo: misssing method in Network
        }

        /// <summary>
        /// Returns a hardwarenode object.
        /// </summary>
        /// <param name="name">The name of the hardwarenode</param>
        /// <returns>The hardwarenode object</returns>
        public Hardwarenode GetHardwarenodeByName(string name)
        {
            return network.GetHardwarenodeByName(name);
        }
    }
}
