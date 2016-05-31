using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Switch : Hardwarenode
    {
        private List<string> interfaces = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch"/> class.
        /// </summary>
        /// <param name="name">The name of the switch.</param>
        public Switch(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets the interfaces.
        /// </summary>
        /// <returns>
        /// The Interfaces
        /// </returns>
        public List<string> GetInterfaces()
        {
            return interfaces;
        }

        /// <summary>
        /// Adds the interface.
        /// </summary>
        public void AddInterface()
        {
            // ToDo auf geloeschte Interfaces achten!
            interfaces.Add("eth"+interfaces.Count);
        }

        /// <summary>
        /// Removes the interface.
        /// </summary>
        /// <param name="IfaceName">The name.</param>
        public void RemoveInterface(string IfaceName)
        {
            RemoveConnection(IfaceName);
            interfaces.Remove(IfaceName);
        }
    }
}
