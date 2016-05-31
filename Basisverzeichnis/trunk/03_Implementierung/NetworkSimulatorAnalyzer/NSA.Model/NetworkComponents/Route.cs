using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Route
    {
        public IPAddress Destination { get; private set; }
        public IPAddress Subnetmask { get; private set; }
        public IPAddress Gateway { get; private set; }
        public Interface Iface { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Route" /> class.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="subnetmask">The subnetmask.</param>
        /// <param name="gateway">The gateway.</param>
        /// <param name="intface">The intface.</param>
        public Route(IPAddress destination, IPAddress subnetmask, IPAddress gateway, Interface intface)
        {
            Destination = destination;
            Subnetmask = subnetmask;
            Gateway = gateway;
            Iface = intface;
        }
    }
}
