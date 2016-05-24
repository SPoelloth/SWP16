using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Route
    {
        public IPAddress Destination { get; }
        public IPAddress Subnetmask { get; }
        public IPAddress Gateway { get; }
        private Interface iface;
    }
}
