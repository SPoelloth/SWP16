using System;
using System.Collections.Generic;
using System.Linq;

namespace NSA.Model.NetworkComponents
{
	public class Network
    {
        private List<Hardwarenode> nodes;
        private List<Connection> connections;

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

        /// <summary>
        /// Removes the hardwarnode.
        /// </summary>
        /// <param name="name">The name.</param>
        public void RemoveHardwarnode(string name)
        {
            nodes.RemoveAll(s => s.Name == name);

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
            foreach (Connection c in connections)
            {
                // This loop is cancelled as soon as the connection which should be removed is reached. 
                if (c.Name == ConnectionName)
                {
                    connections.Remove(c);
                    return;
                }
            }
	    }
    }
}
