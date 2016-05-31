using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Interface
    {
        public string Name { get; }
        public IPAddress IpAddress { get; }
        private IPAddress subnetmask;
    }
}
