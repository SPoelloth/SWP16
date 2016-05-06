using NSA.Model.NetworkComponents;
using System.Collections.Generic;

namespace NetworkSimulatorAnalyzer.Model.NetworkComponents
{
    class Workstation : Hardwarenode
    {
        private List<Interface> interfaces;
        private Routingtable routingtable;

        public Workstation(string name) : base(name)
        {
        }

        public Hardwarenode GetNextHardwarenode(Hardwarenode node)
        {
            return node;
        }
    }
}
