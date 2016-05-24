using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents.Layers
{
    class DataLinkLayer : ILayer
    {
        public void ValidateSend(ref Hardwarenode nextNode, ref IPAddress nextNodeIP, Workstation destination, Dictionary<string, Connection> connections, Routingtable routingtable)
        {
            foreach (Connection c in connections.Values)
            {
                if (c.End.HasIP(nextNodeIP))
                {
                    nextNode = c.End;
                    return;
                }
                else if (c.Start.HasIP(nextNodeIP))
                {
                    nextNode = c.Start;
                    return;
                }
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
