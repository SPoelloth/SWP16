using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Interface
    {
        private string name;
        public IPAddress IpAddress { get; }
        private IPAddress subnetmask;
    }
}
