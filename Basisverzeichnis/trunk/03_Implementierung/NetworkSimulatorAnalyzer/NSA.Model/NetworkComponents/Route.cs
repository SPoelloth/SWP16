using System;
using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Route
    {
        public string Name { get; private set; }
        public IPAddress Destination { get; private set; }
        public IPAddress Subnetmask { get; private set; }
        public IPAddress Gateway { get; private set; }
        public Interface Iface { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Route" /> class.
        /// </summary>
        /// <param name="Destination">The Destination IP.</param>
        /// <param name="Subnetmask">The Mask.</param>
        /// <param name="Gateway">The Gateway.</param>
        /// <param name="Iface">The Interface to be used.</param>
        public Route(IPAddress Destination, IPAddress Subnetmask, IPAddress Gateway, Interface Iface)
        {
            Name = Guid.NewGuid().ToString("N");
            this.Destination = Destination;
            this.Subnetmask = Subnetmask;
            this.Gateway = Gateway;
            this.Iface = Iface;
        }


        /// <summary>
        /// Sets the route.
        /// </summary>
        /// <param name="DestinationIp">The destination.</param>
        /// <param name="Mask">The subnetmask.</param>
        /// <param name="GatewayAddress">The gateway.</param>
        /// <param name="Intface">The iface.</param>
        public void SetRoute(IPAddress DestinationIp, IPAddress Mask, IPAddress GatewayAddress, Interface Intface)
        {
            Destination = DestinationIp;
            Subnetmask = Mask;
            Gateway = GatewayAddress;
            Iface = Intface;
        }
    }
}
