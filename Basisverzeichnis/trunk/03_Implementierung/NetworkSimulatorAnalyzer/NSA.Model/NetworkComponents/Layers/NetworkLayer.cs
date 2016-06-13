using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class NetworkLayer : ILayer
    {
        public bool ValidateReceive(IPAddress nextNodeIP, Workstation currentNode, Result Res)
        {
            return true;
        }

        public void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo)
        {
            //Wenn destination direkt dran ist an einer Verbindung
            foreach (Connection c in currentNode.GetConnections().Values)
            {
                if (c.Start.Equals(destination) || c.End.Equals(destination))
                {
                    valInfo.NextNodes.Add(destination);
                    valInfo.Iface = null;
                    return;
                }
            }

            //In der Routingtabelle nachgucken
            List<Interface> interfaces = destination.GetInterfaces();
            foreach (Interface i in interfaces)
            {
                Dictionary<string, Route>.ValueCollection routes = currentNode.GetRoutes();
                foreach(Route r in routes)
                {
                    if (valInfo.Iface.IpAddress.IsInSameSubnet(r.Destination, r.Subnetmask))
                    {
                        valInfo.NextNodeIP = r.Gateway;
                        valInfo.Iface = r.Iface;
                    }
                }
            }
            if (currentNode.StandardGateway != null && valInfo.NextNodeIP == null)
            {
                valInfo.NextNodeIP = currentNode.StandardGateway;
                valInfo.Iface = currentNode.StandardGatewayPort;
            }
            else if (valInfo.NextNodeIP == null)
            {
                valInfo.Res.ErrorID = 1;
                valInfo.Res.Res = "There is no Route or Standard-Gateway for the specified destination.";
                valInfo.Res.LayerError = new NetworkLayer();
                valInfo.Res.SendError = true;
                valInfo.NextNodes = null;
            }
        }
    }
}
