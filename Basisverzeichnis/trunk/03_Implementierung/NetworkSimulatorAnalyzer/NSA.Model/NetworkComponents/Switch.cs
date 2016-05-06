using System.Collections.Generic;

namespace NetworkSimulatorAnalyzer.Model.NetworkComponents
{
    class Switch : Hardwarenode
    {
        private List<string> interfaces;

        public Switch(string name) : base(name)
        {
        }
    }
}
