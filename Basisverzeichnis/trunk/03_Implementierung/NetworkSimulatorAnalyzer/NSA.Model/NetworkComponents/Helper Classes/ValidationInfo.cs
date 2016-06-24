using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents.Helper_Classes
{
    /// <summary>
    /// Helper class for the parameters of the simulation
    /// </summary>
    public class ValidationInfo
    {
        /// <summary>
        /// Gets or sets the next nodes.
        /// </summary>
        /// <value>
        /// The next nodes.
        /// </value>
        public List<Hardwarenode> NextNodes { get; set; }
        /// <summary>
        /// Gets or sets the next node ip.
        /// </summary>
        /// <value>
        /// The next node ip.
        /// </value>
        public IPAddress NextNodeIp { get; set; }
        /// <summary>
        /// Gets or sets the interface.
        /// </summary>
        /// <value>
        /// The interface.
        /// </value>
        public Interface Iface { get; set; }
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Result Res { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public Workstation Source { get; set; }
    }
}
