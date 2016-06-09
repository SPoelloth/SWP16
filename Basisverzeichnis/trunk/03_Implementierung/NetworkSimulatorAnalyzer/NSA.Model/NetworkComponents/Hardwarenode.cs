using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Hardwarenode
    {
        protected Layerstack Layerstack = new Layerstack();
        protected Dictionary<string, Connection> connections = new Dictionary<string, Connection>();
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hardwarenode" /> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        public Hardwarenode(string Name)
        {
            this.Name = Name;
        }

        #region Methods

        /// <summary>
        /// Adds a connection.
        /// </summary>
        /// <param name="IfaceName">Name of the interface where the connection should be added.</param>
        /// <param name="Con">The connection to be added.</param>
        public void AddConnection(string IfaceName, Connection Con)
        {
            connections.Add(IfaceName, Con);
        }

        /// <summary>
        /// Removes a connection.
        /// </summary>
        /// <param name="IfaceName">Name of the interface where the connection should be removed.</param>
        public virtual void RemoveConnection(string IfaceName)
        {
            connections.Remove(IfaceName);
        }

        /// <summary>
        /// Adds a layer to the layerstack.
        /// </summary>
        /// <param name="Lay">The layer to be added.</param>
        public void AddLayer(ILayer Lay)
        {
            Layerstack.AddLayer(Lay);
        }

        /// <summary>
        /// Removes a layer from the layerstack.
        /// </summary>
        /// <param name="Lay">The layer to be removed.</param>
        public void RemoveLayer(ILayer Lay)
        {
            Layerstack.RemoveLayer(Lay);
        }

        /// <summary>
        /// Checks if the Hardwarenode has the IP
        /// </summary>
        /// <param name="Ip">The ip.</param>
        /// <returns>A bool</returns>
        public virtual bool HasIP(IPAddress Ip)
        {
            return false;
        }

        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Res">The Result of the simulation</param>
        /// <param name="NextNodeIp">The IP of the next Node</param>
        /// <returns>The Hardwarenode which received the package or null if an error occured</returns>
        public virtual Hardwarenode Send(Hardwarenode Destination, Dictionary<string, object> Tags, Result Res, IPAddress NextNodeIp)
        {
            return null;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Res">The Result of the simulation</param>
        /// <param name="NextNodeIp"></param>
        /// <returns>If the Hardwarenode could receive the package</returns>
        public virtual bool Receive(Dictionary<string, object> Tags, Result Res, IPAddress NextNodeIp)
        {
            return true;
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <returns>Connections</returns>
        public Dictionary<string, Connection> GetConnections()
        {
            return connections;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Hardwarenode n = obj as Hardwarenode;
            if (n == null)
                return false;
            if (n.Name == Name)
                return true;
            return false;
        }

        #endregion
    }
}
