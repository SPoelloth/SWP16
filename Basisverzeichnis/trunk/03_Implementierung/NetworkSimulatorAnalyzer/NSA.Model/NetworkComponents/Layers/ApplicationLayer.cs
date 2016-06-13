using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class ApplicationLayer : ILayer
    {
        public bool ValidateReceive(IPAddress nextNodeIP, Workstation currentNode, Result Res)
        {
            return true;
        }

        public void ValidateSend(List<Hardwarenode> nextNodes, IPAddress nextNodeIP, Interface iface, Workstation destination, Workstation currentNode, Result Res)
        {
            
        }
    }
}
