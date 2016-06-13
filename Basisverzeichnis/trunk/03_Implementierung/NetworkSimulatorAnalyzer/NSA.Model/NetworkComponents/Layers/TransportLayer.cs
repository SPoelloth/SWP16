using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class TransportLayer : ILayer
    {
        public bool ValidateReceive(IPAddress nextNodeIP, Workstation currentNode, Result Res)
        {
            return true;
        }

        public void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo)
        {
            
        }
    }
}
