using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    class DataLinkLayer : ILayer
    {
        public void ValidateSend(Hardwarenode nextNode, IPAddress nextNodeIP, Interface iface, Workstation destination, Workstation currentNode, Result Res)
        {
            if (currentNode.GetConnections().ContainsKey(iface.Name))
            {
                nextNode = currentNode.GetConnections()[iface.Name].Start.Equals(currentNode) ? currentNode.GetConnections()[iface.Name].End : currentNode.GetConnections()[iface.Name].Start;
                return;
            }
            Res.ErrorID = 2;
            Res.Res = "There is no Connection at the Interface from choosen the Route.";
            Res.LayerError = new DataLinkLayer();
            Res.SendError = true;
            nextNode = null;
        }

        public bool ValidateReceive(IPAddress nextNodeIP, Workstation currentNode, Result Res)
        {
            List<Interface> ifaces = currentNode.GetInterfaces();
            foreach (Interface i in ifaces)
            {
                if (nextNodeIP.Equals(i.IpAddress))
                    return true;
            }
            Res.ErrorID = 3;
            Res.Res = "The Connection is to the wrong node.";
            Res.LayerError = new DataLinkLayer();
            Res.SendError = false;
            return false;
        }
    }
}
