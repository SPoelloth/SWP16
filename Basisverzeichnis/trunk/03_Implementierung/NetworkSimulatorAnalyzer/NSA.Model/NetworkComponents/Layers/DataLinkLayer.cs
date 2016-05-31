using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents.Layers
{
    class DataLinkLayer : ILayer
    {
        public void ValidateSend(ref Hardwarenode nextNode, ref IPAddress nextNodeIP, ref string interfaceName, Workstation destination,
            Dictionary<string, Connection> connections, Routingtable routingtable)
        {
            if (connections[interfaceName].end.HasIP(nextNodeIP))
            {
                nextNode = connections[interfaceName].end;
                return;
            }
            else if (connections[interfaceName].start.HasIP(nextNodeIP))
            {
                nextNode = connections[interfaceName].start;
                return;
            }
            //result string muss hier noch auf Fehlerfall gesetzt werden
            nextNode = null;
        }

        public bool ValidateReceive()
        {
            return true;
        }
    }
}
