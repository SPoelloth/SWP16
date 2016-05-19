using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Route
    {
        private IPAddress destination;
        private IPAddress subnetmask;
        private IPAddress gateway;
        private Interface iface;
    }
}
