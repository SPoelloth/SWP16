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

        public void ValidateSend(ref Hardwarenode nextNode, ref IPAddress nextNodeIP, Workstation destination, Dictionary<string, Connection> connections, Routingtable routingtable)
        {
            // Jeremy: hier muss die nächste Rechner-IP mithilfe der Routingtabelle ermittelt werden
            // Zuerst schauen ob die Zieladresse  der Route das Netz oder die IP des Ziels ist
            List<Interface> interfaces = destination.GetInterfaces();
            foreach (Interface iface in interfaces)
            {
                for (int i = 0; i < routingtable.GetSize(); i++)
                {
                    Route r = routingtable.GetRouteAt(i);
                    if (IPAddressExtensions.IsInSameSubnet(iface.IpAddress, r.Destination, r.Subnetmask))
                    {
                        nextNodeIP = r.Gateway;
                    }
                }
            }
            nextNode = null;
        }
    }
}
