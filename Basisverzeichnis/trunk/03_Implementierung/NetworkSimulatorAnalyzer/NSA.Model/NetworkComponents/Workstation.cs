using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Workstation : Hardwarenode
    {
        private List<Interface> interfaces = new List<Interface>();
        private Routingtable routingtable;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workstation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Workstation(string name) : base(name)
        {
        }
    }
}
