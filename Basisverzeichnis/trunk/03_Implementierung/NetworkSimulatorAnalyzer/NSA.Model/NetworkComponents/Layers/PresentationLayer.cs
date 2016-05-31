using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents.Layers
{
    class PresentationLayer : ILayer
    {
        public bool ValidateReceive()
        {
            return true;
        }

        public void ValidateSend(ref Hardwarenode nextNode, ref IPAddress nextNodeIP, ref string interfaceName, Workstation destination, Dictionary<string, Connection> connections, Routingtable routingtable)
        {
            nextNode = null;
        }
    }
}
