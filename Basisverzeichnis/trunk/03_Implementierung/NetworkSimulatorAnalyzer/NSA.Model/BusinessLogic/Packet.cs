using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic
{
    public class Packet
    {
        private Hardwarenode source;
        private Hardwarenode destination;
        private List<Hardwarenode> hops;
        private int ttl;
        private string result = "";
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
            while (hops[hops.Count - 1] != destination && result == "")
            {
                Hardwarenode nextNode = hops[hops.Count - 1].Send(destination, ref tags, ref result);
                if (nextNode != null)
                {
                    nextNode.Receive(ref tags, ref result);
                    hops.Add(nextNode);
                }
            }
            return new Packet(destination, source, ttl, tags);
        }
    }
}
