using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Hardwarenode
    {
        protected Layerstack layerstack = new Layerstack();
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
        /// <param name="Con">The connection to be added.</param>
        /// <param name="IfaceName">Name of the interface where the connection should be added.</param>
        public virtual void AddConnection(Connection Con, string IfaceName)
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
            layerstack.AddLayer(Lay);
        }

        /// <summary>
        /// Removes a layer from the layerstack.
        /// </summary>
        /// <param name="Lay">The layer to be removed.</param>
        public void RemoveLayer(ILayer Lay)
        {
            layerstack.RemoveLayer(Lay);
        }

        /// <summary>
        /// Checks if the Hardwarenode has the IP
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns>A bool</returns>
        public virtual bool HasIP(IPAddress ip)
        {
            return false;
        }

        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Result">String representing the result</param>
        /// <returns>The Hardwarenode which received the package or null if an error occured</returns>
        public virtual Hardwarenode Send(Hardwarenode Destination, ref Dictionary<string, object> Tags, ref string Result)
        {
            return null;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Result">String representing the result</param>
        /// <returns>If the Hardwarenode could receive the package</returns>
        public virtual bool Receive(ref Dictionary<string, object> Tags, ref string Result)
        {
            return false;
        }

        #endregion
    }
}
