using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class DataLinkLayer : ILayer
    {
        public void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo)
        {
            if (valInfo.Iface == null)
                return;
            if (currentNode.GetConnections().ContainsKey(valInfo.Iface.Name))
            {
                valInfo.NextNodes.Add(currentNode.GetConnections()[valInfo.Iface.Name].Start.Equals(currentNode) ? currentNode.GetConnections()[valInfo.Iface.Name].End : currentNode.GetConnections()[valInfo.Iface.Name].Start);
                return;
            }
            valInfo.Res.ErrorID = 2;
            valInfo.Res.Res = "There is no Connection at the Interface from choosen the Route.";
            valInfo.Res.LayerError = new DataLinkLayer();
            valInfo.Res.SendError = true;
            valInfo.NextNodes = null;
        }

        public bool ValidateReceive(IPAddress nextNodeIP, Workstation currentNode, Result Res)
        {
            if (nextNodeIP == null)
                return true;
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
