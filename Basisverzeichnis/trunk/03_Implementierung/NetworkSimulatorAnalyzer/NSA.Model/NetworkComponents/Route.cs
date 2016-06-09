using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Route
    {
        public string Name { get; set; }
        public IPAddress Destination { get; private set; }
        public IPAddress Subnetmask { get; private set; }
        public IPAddress Gateway { get; private set; }
        public Interface Iface { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Route" /> class.
        /// </summary>
        /// <param name="n">The Name.</param>
        /// <param name="d">The Destination IP.</param>
        /// <param name="s">The Mask.</param>
        /// <param name="g">The Gateway.</param>
        /// <param name="i">The Interface to be used.</param>
        public Route(string n, IPAddress d, IPAddress s, IPAddress g, Interface i)
        {
            Name = n;
            Destination = d;
            Subnetmask = s;
            Gateway = g;
            Iface = i;
        }
    }
}
