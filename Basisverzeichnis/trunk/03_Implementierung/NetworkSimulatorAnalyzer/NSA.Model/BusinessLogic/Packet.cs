using System;
using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
    public class Packet
    {
        private Hardwarenode source;
        private Hardwarenode destination;
        private List<Hardwarenode> hops = new List<Hardwarenode>();
        private int ttl;
        public Result result { get; private set; }
        public bool expectedResult { get; }
        private Dictionary<string, object> tags;

        public Packet(Hardwarenode _source, Hardwarenode _destination,
            int _ttl, Dictionary<string, object> _tags, bool expRes)
        {
            source = _source;
            destination = _destination;
            if (source == null || destination == null)
            {
                result.ErrorID = 5;
                result.Res = "Source or destination node does not exist.";
                result.SendError = true;
            }
            ttl = _ttl;
            tags = _tags;
            expectedResult = expRes;
            hops.Add(source);
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
            while (!hops[hops.Count - 1].Equals(destination) && valInfo.Res.ErrorID == 0 && ttl > 0)
            {
                List<Hardwarenode> nextNodes = hops[hops.Count - 1].Send(destination, tags, valInfo);
                if (nextNodes != null)
                {
                    if (nextNodes[nextNodes.Count - 1].Receive(tags, valInfo))
                        ttl--;
                    foreach (Hardwarenode n in nextNodes)
                    {
                        hops.Add(n);
                    }
                }
            }
            result = valInfo.Res;
            if (!hops[hops.Count - 1].Equals(destination) && ttl == 0)
            {
                //TTL Error
                result.ErrorID = 6;
                result.Res = "TTL is 0 but the destination was not reached.";
                result.SendError = true;
            }
            if(result.ErrorID == 0)
                return new Packet(destination, source, ttl, tags, expectedResult);
            return null;
        }
    }
}
