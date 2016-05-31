using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Interface
    {
        public string Name { get; private set; }
        public IPAddress IpAddress { get; private set; }
        private IPAddress subnetmask;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interface" /> class.
        /// </summary>
        /// <param name="ip">The ip address.</param>
        /// <param name="subnetmask">The subnetmask.</param>
        /// <param name="Number">The number (e.g. 0 for eth0).</param>
        public Interface(IPAddress ip, IPAddress subnetmask, int Number)
        {
            Name = "eth"+Number;
            IpAddress = ip;
            this.subnetmask = subnetmask;
        }
    }
}
