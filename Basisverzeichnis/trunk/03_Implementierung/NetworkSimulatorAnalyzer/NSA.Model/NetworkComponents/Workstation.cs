using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Layers;

namespace NSA.Model.NetworkComponents
{
    public class Workstation : Hardwarenode
    {
        private List<Interface> interfaces = new List<Interface>();
        private Routingtable routingtable = new Routingtable();
        private IPAddress standardGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workstation" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="gateway">The gateway address.</param>
        public Workstation(string name, byte[] gateway) : base(name)
        {
            standardGateway = new IPAddress(gateway);
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

        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="tags">Optional tags.</param>
        /// <param name="result">String representing the result</param>
        /// <returns>
        /// The Hardwarenode which received the package or null if an error occured
        /// </returns>
        public override Hardwarenode Send(Hardwarenode destination, ref Dictionary<string, object> tags, ref string result)
        {
            Hardwarenode nextNode = this;
            IPAddress nextNodeIP = null;
            for (int i = layerstack.GetSize() - 1; i >= 0; i--)
            {
                if (nextNode != null)
                    layerstack.GetLayer(i).ValidateSend(ref nextNode, ref nextNodeIP, destination as Workstation, connections, routingtable);
            }
            return nextNode == this ? null : nextNode;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="tags">Optional tags.</param>
        /// <param name="result">String representing the result</param>
        /// <returns>
        /// bool that indicates if the Hardwarenode received the package
        /// </returns>
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
