using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Layers;

namespace NSA.Model.NetworkComponents
{
    public class Workstation : Hardwarenode
    {
        private List<Interface> interfaces = new List<Interface>();
        private Routingtable routingtable = new Routingtable();
        public IPAddress StandardGateway { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Workstation" /> class.
        /// The IP address of the standardgateway must be set seperatly.
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
        /// <returns>
        /// The Interfaces
        /// </returns>
        public List<Interface> GetInterfaces()
        {
            return interfaces;
        }

        /// <summary>
        /// Adds a new interface to the workstation
        /// </summary>
        /// <param name="iface">The iface.</param>
        public void AddInterface(Interface iface)
        {
            interfaces.Add(iface);
        }

        /// <summary>
        /// Removes the given interface.
        /// </summary>
        /// <param name="iface">The iface.</param>
        public void RemoveInterface(Interface iface)
        {
            interfaces.Remove(iface);
        }

        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="route">The route.</param>
        public void AddRoute(Route route)
        {
            routingtable.AddRoute(route);
        }

        /// <summary>
        /// Removes the route.
        /// </summary>
        /// <param name="route">The route.</param>
        public void RemoveRoute(Route route)
        {
            routingtable.RemoveRoute(route);
        }


        /// <summary>
        /// Checks if the Hardwarenode has the IP
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns>
        /// bool: true if workstation has the ip, otherwise false
        /// </returns>
        public override bool HasIP(IPAddress ip)
        {
            bool hasIp = false;

            foreach (Interface iface in interfaces)
            {
                hasIp = iface.IpAddress.Equals(ip);
                if (hasIp) break;
            }
            return hasIp;
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
