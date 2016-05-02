using NSA.Model.NetworkComponents;
using System.Collections.Generic;
using System.Net;

namespace NetworkSimulatorAnalyzer.Model.NetworkComponents
{
    class Workstation : Hardwarenode
    {
        private List<Interface> interfaces;
        //        private subnetmask;   => Welcher Typ?
        private Routingtable routingtable;

        public Hardwarenode GetNextHardwarenode(Hardwarenode node)
        {
            return node;
        }
    }
}
