using System;
using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Hardwarenode
    {
        private Layerstack layerstack;
        private Dictionary<String, Connection> connections = new Dictionary<string, Connection>();
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hardwarenode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Hardwarenode(String name)
        {
            this.Name = name;
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
        /// <returns>The Hardwarenode which received the package or null if an error occured</returns>
        public Hardwarenode Send(Hardwarenode destination, Dictionary<string, Object> tags)
        {
            return this;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="tags">Optional tags.</param>
        /// <returns>If the Hardwarenode could receive the package</returns>
        public bool Receive(Dictionary<string, Object> tags)
        {
            return true;
        }

        #endregion
    }
}
