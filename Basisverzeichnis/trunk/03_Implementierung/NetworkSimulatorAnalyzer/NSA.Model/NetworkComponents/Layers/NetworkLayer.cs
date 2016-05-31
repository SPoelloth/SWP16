using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    class NetworkLayer : ILayer
    {
        public bool ValidateReceive()
        {
            return true;
        }

        public void ValidateSend(ref Hardwarenode nextNode, ref IPAddress nextNodeIP, ref string interfaceName, Workstation destination, 
            Dictionary<string, Connection> connections, Routingtable routingtable)
        {
            List<Interface> interfaces = destination.GetInterfaces();
            foreach (Interface iface in interfaces)
            {
                for (int i = 0; i < routingtable.GetSize(); i++)
                {
                    Route r = routingtable.GetRouteAt(i);
                    if (iface.IpAddress.IsInSameSubnet(r.Destination, r.Subnetmask))
                    {
                        nextNodeIP = r.Gateway;
                        interfaceName = r.Iface.Name;
                    }
                }
            }
            //Falls vorhanden an Standard-Gateway schicken
            nextNode = null;
        }
    }
}
