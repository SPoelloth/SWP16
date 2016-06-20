using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class NetworkLayer : ILayer
    {
        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination, int LayerIndex)
        {
            return true;
        }

        public string GetLayerName()
        {
            return "Vermittlungsschicht";
        }

        public bool SetLayerName(string NewName)
        {
            return false;
        }

        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, int LayerIndex)
        {
            //Wenn destination direkt dran ist an einer Verbindung
            foreach (Connection c in CurrentNode.Connections.Values)
            {
                if (c.Start.Equals(Destination) || c.End.Equals(Destination))
                {
                    ValInfo.NextNodes.Add(Destination);
                    ValInfo.Iface = null;
                    return;
                }
            }
            //Wenn übr Switch direkt dran, dann kann man so auch senden
            foreach (Connection c in CurrentNode.Connections.Values)
            {
                Switch sw;
                if (c.Start.Equals(CurrentNode))
                {
                    sw = c.End as Switch;
                    if (sw != null)
                    {
                        if (sw.SendToDestination(Destination, ValInfo, c))
                        {
                            ValInfo.NextNodes.Insert(0, sw);
                            return;
                        }
                    }
                }
                else
                {
                    sw = c.Start as Switch;
                    if (sw != null)
                    {
                        if (sw.SendToDestination(Destination, ValInfo, c))
                        {
                            ValInfo.NextNodes.Insert(0, sw);
                            return;
                        }
                    }
                }
            }
            //In der Routingtabelle nachgucken
            List<Interface> interfaces = Destination.GetInterfaces();
            foreach (Interface i in interfaces)
            {
                Dictionary<string, Route>.ValueCollection routes = CurrentNode.GetRoutes();
                foreach(Route r in routes)
                {
                    if (i.IpAddress.IsInSameSubnet(r.Destination, r.Subnetmask))
                    {
                        ValInfo.NextNodeIp = r.Gateway;
                        ValInfo.Iface = r.Iface;
                    }
                }
            }
            if (CurrentNode.StandardGateway != null && ValInfo.NextNodeIp == null)
            {
                ValInfo.NextNodeIp = CurrentNode.StandardGateway;
                ValInfo.Iface = CurrentNode.StandardGatewayPort;
            }
            else if (ValInfo.NextNodeIp == null)
            {
                ValInfo.Res.ErrorId = Result.Errors.NoRoute;
                ValInfo.Res.Res = Result.ResultStrings[(int)ValInfo.Res.ErrorId];
                ValInfo.Res.LayerError = new NetworkLayer();
                ValInfo.Res.SendError = true;
                ValInfo.NextNodes = null;
            }
        }
    }
}
