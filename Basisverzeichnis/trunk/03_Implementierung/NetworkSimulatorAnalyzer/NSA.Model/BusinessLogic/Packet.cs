using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public Result result { get; private set; } = new Result();
        public bool ExpectedResult { get; }
        private Dictionary<string, object> tags = new Dictionary<string, object>();

        public Packet(Hardwarenode _source, Hardwarenode _destination,
            int _ttl, bool expRes)
        {
            Source = _source;
            Destination = _destination;
            if (Source == null || Destination == null)
            {
                result.ErrorID = 5;
                result.Res = "Source or destination node does not exist.";
                result.SendError = true;
            }
            Ttl = _ttl;
            ExpectedResult = expRes;
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
            valInfo.NextNodeIP = null;
            valInfo.Res = result;
            valInfo.Source = Source as Workstation;
            while (!hops[hops.Count - 1].Equals(Destination) && valInfo.Res.ErrorID == 0 && Ttl > 0)
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
            result = valInfo.Res;
            if (!hops[hops.Count - 1].Equals(Destination) && Ttl == 0)
            {
                //TTL Error
                result.ErrorID = 6;
                result.Res = "TTL is 0 but the destination was not reached.";
                result.SendError = true;
            }
            if (result.ErrorID == 0 && tags.Count != 0)
            {
                //Layer Error
                result.ErrorID = 7;
                result.Res = "No Layer " + tags.Keys.First() + " at the destination.";
                result.SendError = false;
            }
            if(result.ErrorID == 0)
                return new Packet(Destination, Source, Ttl, ExpectedResult);
            return null;
        }
    }
}
