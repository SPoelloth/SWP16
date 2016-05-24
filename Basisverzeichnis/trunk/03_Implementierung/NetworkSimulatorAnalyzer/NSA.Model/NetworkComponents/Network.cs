using System;
using System.Collections.Generic;
using System.Linq;

namespace NSA.Model.NetworkComponents
{
	public class Network
    {
        private List<Hardwarenode> nodes;
        private List<Connection> connections;

        /// <summary>
        /// Returns the Hardwarenode with the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The Hardwarenode with this name or default value</returns>
        public Hardwarenode GetHardwarenodeByName(string name)
	    {
	        return nodes.FirstOrDefault(n => n.Name == name);
	    }

        /// <summary>
        /// Adds a hardwarenode.
        /// </summary>
        /// <param name="newNode">The new node.</param>
        public void AddHardwarenode(Hardwarenode newNode)
	    {
	        nodes.Add(newNode);
	    }

        /// <summary>
        /// Adds a connection.
        /// </summary>
        /// <param name="newConnection">The new connection.</param>
        /// <exception cref="System.InvalidOperationException">Connection already exists!</exception>
        public void AddConnection(Connection newConnection)
	    {
	        if (!nodes.Contains(newConnection.Start) || !nodes.Contains(newConnection.End)) return;
	        if(connections.Count(c => c.Start == newConnection.Start && c.End == newConnection.End
	           || c.Start == newConnection.End && c.End == newConnection.Start) > 0
	           || connections.Contains(newConnection))
	        {
	            // there's already a connection between the two nodes
	            throw new InvalidOperationException("Connection already exists!");
	        }
	        connections.Add(newConnection);
	    }
    }
}
