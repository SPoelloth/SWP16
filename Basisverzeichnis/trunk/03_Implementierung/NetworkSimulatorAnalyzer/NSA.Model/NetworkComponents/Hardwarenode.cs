using System;
using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Hardwarenode
    {
        protected Layerstack layerstack;
        protected Dictionary<string, Connection> connections = new Dictionary<string, Connection>();
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hardwarenode"/> class.
        /// </summary>
        /// <param name="n">The name.</param>
        public Hardwarenode(string n)
        {
            Name = n;
            layerstack = new Layerstack();
        }

        #region Methods
        /// <summary>
        /// Adds a connection.
        /// </summary>
        /// <param name="con">The connection to be removed.</param>
        public virtual void AddConnection(Connection con) {}

        /// <summary>
        /// Removes a connection.
        /// </summary>
        /// <param name="con">The connection to be removed.</param>
        public virtual void RemoveConnection(Connection con) {}

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
        /// Adds a layer to the layerstack.
        /// </summary>
        /// <param name="lay">The layer to be added.</param>
        public void AddLayer(ILayer lay)
        {
            layerstack.AddLayer(lay);
        }

        /// <summary>
        /// Removes a layer from the layerstack.
        /// </summary>
        /// <param name="lay">The layer to be removed.</param>
        public void RemoveLayer(ILayer lay)
        {
            layerstack.RemoveLayer(lay);
        }

        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="tags">Optional tags.</param>
        /// <param name="result">String representing the result</param>
        /// <returns>The Hardwarenode which received the package or null if an error occured</returns>
        public virtual Hardwarenode Send(Hardwarenode destination, ref Dictionary<string, Object> tags, ref string result)
        {
            return null;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="tags">Optional tags.</param>
        /// <param name="result">String representing the result</param>
        /// <returns>If the Hardwarenode could receive the package</returns>
        public virtual bool Receive(ref Dictionary<string, Object> tags, ref string result)
        {
            return false;
        }

        #endregion
    }
}
