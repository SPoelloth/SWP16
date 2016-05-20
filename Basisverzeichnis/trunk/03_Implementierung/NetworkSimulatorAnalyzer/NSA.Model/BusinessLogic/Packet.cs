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
        private string result;
        private Dictionary<string, object> tags;

        public Packet(Hardwarenode _source, Hardwarenode _destination,
            int _ttl, Dictionary<string, object> _tags)
        {
            source = _source;
            destination = _destination;
            ttl = _ttl;
            tags = _tags;
        }
    }
}
