using System;
using System.Collections.Generic;
using NetworkSimulatorAnalyzer.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic
{
    class Packet
    {
        private Hardwarenode source;
        private Hardwarenode destination;
        private List<Hardwarenode> hops;
        private int ttl;
        private Dictionary<String, Object> tags;
    }
}
