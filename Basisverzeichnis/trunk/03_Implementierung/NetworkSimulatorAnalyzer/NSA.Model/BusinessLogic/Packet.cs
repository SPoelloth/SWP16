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
        private Result result;
        private Dictionary<string, object> tags;

        public Packet(Hardwarenode _source, Hardwarenode _destination,
            int _ttl, Dictionary<string, object> _tags)
        {
            source = _source;
            destination = _destination;
            ttl = _ttl;
            tags = _tags;
            hops.Add(source);
        }

        /// <summary>
        /// Sends this packet to the destination.
        /// </summary>
        /// <returns>The Returnpacket if sending to destination was successfull</returns>
        public Packet Send()
        {
            IPAddress nextNodeIP = null;
            while (!hops[hops.Count - 1].Equals(destination) && result.ErrorID == 0)
            {
                List<Hardwarenode> nextNodes = hops[hops.Count - 1].Send(destination, tags, result, nextNodeIP);
                if (nextNodes != null)
                {
                    nextNodes[nextNodes.Count - 1].Receive(tags, result, nextNodeIP);
                    foreach (Hardwarenode n in nextNodes)
                    {
                        hops.Add(n);
                    }
                }
            }
            if(result.ErrorID == 0)
                return new Packet(destination, source, ttl, tags);
            return null;
        }
    }
}
