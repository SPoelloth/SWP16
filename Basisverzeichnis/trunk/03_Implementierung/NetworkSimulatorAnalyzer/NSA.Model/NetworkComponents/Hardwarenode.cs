using NSA.Model.NetworkComponents;
using System;
using System.Collections.Generic;

namespace NetworkSimulatorAnalyzer.Model.NetworkComponents
{
    class Hardwarenode
    {
        private Layerstack layerstack;
        private Dictionary<String, Connection> connections = new Dictionary<string, Connection>();
    }
}
