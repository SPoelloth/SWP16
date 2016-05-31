using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Layers;

namespace NSA.Model.NetworkComponents
{
    public class Workstation : Hardwarenode
    {
        private List<Interface> interfaces = new List<Interface>();
        private Routingtable routingtable;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workstation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Workstation(string name) : base(name)
        {
            layerstack.AddLayer(new PhysicalLayer());
            layerstack.AddLayer(new DataLinkLayer());
            layerstack.AddLayer(new NetworkLayer());
            layerstack.AddLayer(new TransportLayer());
            layerstack.AddLayer(new SessionLayer());
            layerstack.AddLayer(new PresentationLayer());
            layerstack.AddLayer(new ApplicationLayer());
        }

        /// <summary>
        /// Gets the interfaces.
        /// </summary>
        /// <returns>The Interfaces</returns>
        public List<Interface> GetInterfaces()
        {
            return interfaces;
        }

        public override Hardwarenode Send(Hardwarenode destination, ref Dictionary<string, object> tags, ref string result)
        {
            Hardwarenode nextNode = this;
            IPAddress nextNodeIP = null;
            string interfaceName = "";
            for (int i = layerstack.GetSize() - 1; i >= 0; i--)
            {
                if (nextNode != null)
                    layerstack.GetLayer(i).ValidateSend(ref nextNode, ref nextNodeIP, ref interfaceName, destination as Workstation, connections, routingtable);
            }
            return nextNode == this ? null : nextNode;
        }

        public override bool Receive(ref Dictionary<string, object> tags, ref string result)
        {
            bool res = true;
            for (int i = 0; i < layerstack.GetSize(); i++)
            {
                if (res)
                    res = layerstack.GetLayer(i).ValidateReceive();
            }
            return res;
        }
    }
}
