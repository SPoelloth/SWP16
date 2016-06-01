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
        /// <param name="Destination">The Destination IP.</param>
        /// <param name="Subnetmask">The Mask.</param>
        /// <param name="Gateway">The Gateway.</param>
        /// <param name="Iface">The Interface to be used.</param>
        public Route(IPAddress Destination, IPAddress Subnetmask, IPAddress Gateway, Interface Iface)
        {
            this.Destination = Destination;
            this.Subnetmask = Subnetmask;
            this.Gateway = Gateway;
            this.Iface = Iface;
        }
    }
}
