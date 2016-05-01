using System.Net;

namespace NetworkSimulatorAnalyzer.Model.NetworkComponents
{
    class Workstation : Hardwarenode
    {
        private IPAddress ipAddress;
//        private subnetmask;   => Welcher Typ?
//        private Routingtable routingtable;

        public Hardwarenode GetNextHardwarenode(Hardwarenode node)
        {
            return node;
        }
    }
}
