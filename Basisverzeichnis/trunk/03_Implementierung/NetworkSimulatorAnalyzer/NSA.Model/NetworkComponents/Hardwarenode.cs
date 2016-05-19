using System;
using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Hardwarenode
    {
        private Layerstack layerstack;
        private Dictionary<String, Connection> connections = new Dictionary<string, Connection>();
        public string Name { get; set; }

        public Hardwarenode(String name)
        {
            this.Name = name;
            layerstack = new Layerstack();
        }

        #region Methods
        public virtual void AddConnection(Connection con) {}
        public virtual void RemoveConnection(Connection con) {}

        public void AddLayer(ILayer lay)
        {
            layerstack.AddLayer(lay);
        }

        public void RemoveLayer(ILayer lay)
        {
            layerstack.RemoveLayer(lay);
        }
        #endregion
    }
}
