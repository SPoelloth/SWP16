using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
    public class Packet
    {
        public Hardwarenode Source { get; private set; }
        public Hardwarenode Destination { get; private set; }
        private List<Hardwarenode> hops = new List<Hardwarenode>();
        public int Ttl { get; private set; }
        public Result Result { get; private set; } = new Result();
        public bool ExpectedResult { get; }
        private Dictionary<string, object> tags = new Dictionary<string, object>();

        public Packet(Hardwarenode Src, Hardwarenode Dest,
            int T, bool ExpRes)
        {
            Source = Src;
            Destination = Dest;
            if (Source == null || Destination == null)
            {
                Result.ErrorId = 5;
                Result.Res = "Source or destination node does not exist.";
                Result.SendError = true;
            }
            Ttl = T;
            ExpectedResult = ExpRes;
            hops.Add(Source);
        }

        /// <summary>
        /// Gets the hops.
        /// </summary>
        /// <returns></returns>
        public List<Hardwarenode> GetHops()
        {
            return hops;
        }

        /// <summary>
        /// Sends this packet to the destination.
        /// </summary>
        /// <returns>The Returnpacket if sending to destination was successfull</returns>
        public Packet Send()
        {
            ValidationInfo valInfo = new ValidationInfo();
            valInfo.NextNodeIp = null;
            valInfo.Res = Result;
            valInfo.Source = Source as Workstation;
            while (!hops[hops.Count - 1].Equals(Destination) && valInfo.Res.ErrorId == 0 && Ttl > 0)
            {
                List<Hardwarenode> nextNodes = hops[hops.Count - 1].Send(Destination, tags, valInfo);
                if (nextNodes != null)
                {
                    if (nextNodes[nextNodes.Count - 1].Receive(tags, valInfo, Destination))
                        Ttl--;
                    foreach (Hardwarenode n in nextNodes)
                    {
                        hops.Add(n);
                    }
                }
            }
            Result = valInfo.Res;
            if (!hops[hops.Count - 1].Equals(Destination) && Ttl == 0)
            {
                //TTL Error
                Result.ErrorId = 6;
                Result.Res = "TTL is 0 but the destination was not reached.";
                Result.SendError = true;
            }
            if (Result.ErrorId == 0 && tags.Count != 0)
            {
                //Layer Error
                Result.ErrorId = 7;
                Result.Res = "No Layer " + tags.Keys.First() + " at the destination.";
                Result.SendError = false;
            }
            if(Result.ErrorId == 0)
                return new Packet(Destination, Source, Ttl, ExpectedResult);
            return null;
        }
    }
}
