using System;
using System.Collections.Generic;
using System.Linq;

namespace NSA.Model.NetworkComponents
{
	public class Network
    {
        private List<Hardwarenode> Nodes;
        private List<Connection> Connections;

        /// <summary>
        /// Returns the Hardwarenode with the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The Hardwarenode with this name or default value</returns>
        public Hardwarenode GetHardwarenodeByName(string name)
	    {
	        return Nodes.FirstOrDefault(n => n.Name == name);
	    }

        /// <summary>
        /// Adds a hardwarenode.
        /// </summary>
        /// <param name="newNode">The new node.</param>
        public void AddHardwarenode(Hardwarenode newNode)
	    {
	        Nodes.Add(newNode);
	    }

        /// <summary>
        /// Adds a connection.
        /// </summary>
        /// <param name="newConnection">The new connection.</param>
        /// <exception cref="System.InvalidOperationException">Connection already exists!</exception>
        public void AddConnection(Connection newConnection)
	    {
	        if (!Nodes.Contains(newConnection.start) || !Nodes.Contains(newConnection.end)) return;
	        if(Connections.Count(c => c.start == newConnection.start && c.end == newConnection.end
	           || c.start == newConnection.end && c.end == newConnection.start) > 0
	           || Connections.Contains(newConnection))
	        {
	            // there's already a connection between the two nodes
	            throw new InvalidOperationException("Connection already exists!");
	        }
	        Connections.Add(newConnection);
	    }
    }
}
