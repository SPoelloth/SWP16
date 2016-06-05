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
        /// <param name="Name">The Name.</param>
        public Workstation(string Name) : base(Name)
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
        /// <param name="Iface">The iface.</param>
        public void AddInterface(Interface Iface)
        {
            interfaces.Add(Iface);
        }

        /// <summary>
        /// Removes the given interface.
        /// </summary>
        /// <param name="Iface">The iface.</param>
        public void RemoveInterface(Interface Iface)
        {
            RemoveConnection(Iface.Name);
            interfaces.Remove(Iface);
        }

        /// <summary>
        /// Gets the interface count.
        /// </summary>
        /// <returns>
        /// int: interface count
        /// </returns>
        public int GetInterfaceCount()
        {
            return interfaces.Count;
        }

        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="Route">The route.</param>
        public void AddRoute(Route Route)
        {
            routingtable.AddRoute(Route);
        }

        /// <summary>
        /// Removes the route.
        /// </summary>
        /// <param name="Route">The route.</param>
        public void RemoveRoute(Route Route)
        {
            routingtable.RemoveRoute(Route);
        }

        /// <summary>
        /// Gets the routingtable entry at the given index.
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <returns>
        /// The <see cref="Route"/> object at the given index.
        /// </returns>
        public Route GetRouteAt(int Index)
        {
            return routingtable.GetRouteAt(Index);
        }

        /// <summary>
        /// Gets number of route entries in the routing table.
        /// </summary>
        /// <returns>int: Number of route entries</returns>
        public int GetRouteCount()
        {
            return routingtable.GetSize();
        }


        /// <summary>
        /// Checks if the Hardwarenode has the IP
        /// </summary>
        /// <param name="Ip">The ip.</param>
        /// <returns>
        /// bool: true if workstation has the ip, otherwise false
        /// </returns>
        public override bool HasIP(IPAddress Ip)
        {
            bool hasIp = false;

            foreach (Interface iface in interfaces)
            {
                hasIp = iface.IpAddress.Equals(Ip);
                if (hasIp) break;
            }
            return hasIp;
        }

        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Result">String representing the result</param>
        /// <returns>
        /// The Hardwarenode which received the package or null if an error occured
        /// </returns>
        public override Hardwarenode Send(Hardwarenode Destination, ref Dictionary<string, object> Tags, ref string Result)
        {
            Hardwarenode nextNode = this;
            IPAddress nextNodeIp = null;
            string interfaceName = "";
            for (int i = layerstack.GetSize() - 1; i >= 0; i--)
            {
                if (nextNode != null)
                    layerstack.GetLayer(i).ValidateSend(ref nextNode, ref nextNodeIp, ref interfaceName, Destination as Workstation, connections, routingtable);
            }
            return nextNode == this ? null : nextNode;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Result">String representing the result</param>
        /// <returns>
        /// bool that indicates if the Hardwarenode received the package
        /// </returns>
        public override bool Receive(ref Dictionary<string, object> Tags, ref string Result)
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
