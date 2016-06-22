using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Network
    {
        private readonly List<Hardwarenode> nodes;
        private readonly List<Connection> connections;

	    public Network()
	    {
            nodes = new List<Hardwarenode>();
            connections = new List<Connection>();
	    }

        /// <summary>
        /// Returns the Hardwarenode with the name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>The Hardwarenode with this name or default value</returns>
        public Hardwarenode GetHardwarenodeByName(string Name)
	    {
	        return nodes.FirstOrDefault(N => N.Name == Name);
	    }

        /// <summary>
        /// Adds a hardwarenode.
        /// </summary>
        /// <param name="NewNode">The new node.</param>
        public void AddHardwarenode(Hardwarenode NewNode)
	    {
	        nodes.Add(NewNode);
	    }

        /// <summary>
        /// Adds the connection.
        /// </summary>
        /// <param name="StartNodeInterfaceName">Start name of the node interface.</param>
        /// <param name="EndNodeInterfaceName">End name of the node interface.</param>
        /// <param name="NewConnection">The new connection.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Connection already exists!
        /// or
        /// Interface of startnode is already used!
        /// or
        /// Interface of endnode is already used!
        /// </exception>
        public void AddConnection(string StartNodeInterfaceName, string EndNodeInterfaceName, Connection NewConnection)
	    {
	        if (!nodes.Contains(NewConnection.Start) || !nodes.Contains(NewConnection.End)) return;
	        if(connections.Count(C => C.Start == NewConnection.Start && C.End == NewConnection.End
	           || C.Start == NewConnection.End && C.End == NewConnection.Start) > 0
	           || connections.Contains(NewConnection))
	        {
	            // there's already a connection between the two nodes
	            throw new InvalidOperationException("Connection already exists!");
	        }
            
            if(NewConnection.Start.InterfaceIsUsed(StartNodeInterfaceName))
                throw new InvalidOperationException("Interface of startnode is already used!");
            if (NewConnection.End.InterfaceIsUsed(EndNodeInterfaceName))
                throw new InvalidOperationException("Interface of endnode is already used!");
 

                NewConnection.Start.AddConnection(StartNodeInterfaceName, NewConnection);
                NewConnection.End.AddConnection(EndNodeInterfaceName, NewConnection);
                connections.Add(NewConnection);
            
	    }

        /// <summary>
        /// Removes the hardwarnode.
        /// </summary>
        /// <param name="Name">The name.</param>
        public void RemoveHardwarnode(string Name)
        {
            nodes.RemoveAll(S => S.Name == Name);

            // das ist das gleiche:
            //foreach (Hardwarenode h in nodes)
            //{
            //    // A Hardwarenode is identified by its name. 
            //    // This loop is cancelled as soon as the correct hardwarenode which should be removed is reached. 
            //    if (h.Name == name)
            //    {
            //        nodes.Remove(h);
            //        return;
            //    }
            //}
        }

        /// <summary>
        /// Removes the connection.
        /// </summary>
        /// <param name="ConnectionName">Name of the connection.</param>
        public void RemoveConnection(string ConnectionName)
	    {
            var connection = connections.FirstOrDefault(C => C.Name == ConnectionName);

            if (connection == null) return;

            connections.Remove(connection);
            connection.Start.RemoveConnection(Interface.NamePrefix + connection.GetPortIndex(connection.Start));
            connection.End.RemoveConnection(Interface.NamePrefix + connection.GetPortIndex(connection.End));
        }

        /// <summary>
        /// Gets the workstation by ip.
        /// </summary>
        /// <param name="Ip">The ip.</param>
        /// <returns></returns>
        public Hardwarenode GetWorkstationByIp(IPAddress Ip)
        {
            return nodes.FirstOrDefault(H => H.HasIp(Ip));
        }

        /// <summary>
        /// Gets all hardwarenodes.
        /// </summary>
        /// <returns>all Hardwarenodes</returns>
        public List<Hardwarenode> GetAllHardwarenodes()
        {
            return nodes.ToList();
        }

        /// <summary>
        /// Gets all workstations.
        /// </summary>
        /// <returns>all Workstations</returns>
        public List<Workstation> GetAllWorkstations()
        {
            return nodes.OfType<Workstation>().ToList();
        }

        /// <summary>
        /// Gets the name of the connection by.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>the connection with its name</returns>
        public Connection GetConnectionByName(string Name)
        {
            return connections.FirstOrDefault(N => N.Name == Name);
        }

        /// <summary>
        /// Gets the routers with internetconnection.
        /// </summary>
        /// <returns>A List of routers</returns>
        public List<Router> GetRouters()
        {
            return nodes.OfType<Router>().Where(R => R.IsGateway).ToList();
        }

        public List<Connection> GetAllConnections()
	    {
            return connections.ToList();
	    }

        /// <summary>
        /// Gets all hardwarenodes belonging to a subnet.
        /// </summary>
        /// <param name="Subnetmask">The subnetmask.</param>
        /// <returns>A list of hardwarenodes who belong to the subnet.</returns>
        public List<Hardwarenode> GetHardwareNodesForSubnet(string Subnetmask)
        {
            List<Hardwarenode> resultNodes = new List<Hardwarenode>();
            IPAddress subnetAddress;
            bool ok = IPAddress.TryParse(Subnetmask, out subnetAddress);
            Debug.Assert(ok, "Invalid Subnetmask");

            List<Workstation> allWorkstations = GetAllWorkstations();
            // Iterate through all workstations
            foreach (Workstation w in allWorkstations)
            {
                List<Interface> ifaces = w.GetInterfaces();
                // Iterate through all interfaces of the current workstation.
                foreach (Interface iface in ifaces)
                {
                    if (subnetAddress.Equals(iface.Subnetmask))
                    {
                        // Workstation is in the same subnet.
                        resultNodes.Add(w);
                        break;
                    }
                }

            }

            return resultNodes;
        }
    }
}
