using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class DataLinkLayer : ILayer
    {
        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, int LayerIndex)
        {
            if (ValInfo.Iface == null)
                return;
            Connection c;
            if(CurrentNode.Connections.TryGetValue(ValInfo.Iface.Name, out c))
            {
                ValInfo.NextNodes.Add(c.Start.Equals(CurrentNode) ? c.End : c.Start);
                return;
            }
            ValInfo.Res.ErrorId = Result.Errors.NoConnection;
            ValInfo.Res.Res = Result.ResultStrings[(int)ValInfo.Res.ErrorId];
            ValInfo.Res.LayerError = new DataLinkLayer();
            ValInfo.Res.SendError = true;
            ValInfo.NextNodes = null;
        }

        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination, int LayerIndex)
        {
            if (ValInfo.NextNodeIp == null)
                return true;
            List<Interface> ifaces = CurrentNode.GetInterfaces();
            if (ifaces.Any(I => ValInfo.NextNodeIp.Equals(I.IpAddress)))
            {
                return true;
            }
            ValInfo.Res.ErrorId = Result.Errors.PacketNotForThisNode;
            ValInfo.Res.Res = Result.ResultStrings[(int) ValInfo.Res.ErrorId];
            ValInfo.Res.LayerError = new DataLinkLayer();
            ValInfo.Res.SendError = false;
            return false;
        }

        public string GetLayerName()
        {
            return "Sicherungsschicht";
        }

        public bool SetLayerName(string NewName)
        {
            return false;
        }
    }
}
