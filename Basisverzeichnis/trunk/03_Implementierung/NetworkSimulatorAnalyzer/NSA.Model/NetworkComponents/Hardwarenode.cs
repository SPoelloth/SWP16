using System;
using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    class Hardwarenode
    {
        private Layerstack layerstack;
        private Dictionary<String, Connection> connections = new Dictionary<string, Connection>();
        private string name;

        public Hardwarenode(String name)
        {
            this.name = name;
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
