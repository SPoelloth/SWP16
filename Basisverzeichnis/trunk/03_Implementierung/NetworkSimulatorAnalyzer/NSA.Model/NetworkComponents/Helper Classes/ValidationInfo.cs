using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents.Helper_Classes
{
    public class ValidationInfo
    {
        public List<Hardwarenode> NextNodes { get; set; }
        public IPAddress NextNodeIp { get; set; }
        public Interface Iface { get; set; }
        public Result Res { get; set; }
        public Workstation Source { get; set; }
    }
}
