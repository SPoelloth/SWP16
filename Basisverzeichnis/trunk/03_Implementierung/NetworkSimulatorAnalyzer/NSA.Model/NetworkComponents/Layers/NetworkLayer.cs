using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    class NetworkLayer : ILayer
    {
        public bool ValidateReceive(IPAddress nextNodeIP, Workstation currentNode, Result Res)
        {
            return true;
        }

        public void ValidateSend(List<Hardwarenode> nextNodes, IPAddress nextNodeIP, Interface iface, Workstation destination, Workstation currentNode, Result Res)
        {
            List<Interface> interfaces = destination.GetInterfaces();
            foreach (Interface i in interfaces)
            {
                Dictionary<string, Route>.ValueCollection routes = currentNode.GetRoutes();
                foreach(Route r in routes)
                {
                    if (iface.IpAddress.IsInSameSubnet(r.Destination, r.Subnetmask))
                    {
                        nextNodeIP = r.Gateway;
                        iface = r.Iface;
                    }
                }
            }
            if (currentNode.StandardGateway != null && nextNodeIP == null)
            {
                nextNodeIP = currentNode.StandardGateway;
                iface = currentNode.StandardGatewayPort;
            }
            else if (nextNodeIP == null)
            {
                Res.ErrorID = 1;
                Res.Res = "There is no Route or Standard-Gateway for the specified destination.";
                Res.LayerError = new NetworkLayer();
                Res.SendError = true;
                nextNodes = null;
            }
        }
    }
}
