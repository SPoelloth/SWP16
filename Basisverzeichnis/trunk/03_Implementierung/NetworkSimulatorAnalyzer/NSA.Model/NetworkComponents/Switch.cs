using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Switch : Hardwarenode
    {
        public List<string> Interfaces { get; private set; } = new List<string>();
        private readonly string namePrefix = "eth0";
        private int nextInterface;

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch"/> class.
        /// </summary>
        /// <param name="Name">The name of the switch.</param>
        public Switch(string Name) : base(Name)
        {
            
        }

        /// <summary>
        /// Gets the interface count.
        /// </summary>
        /// <returns>
        /// int: interface count
        /// </returns>
        public int GetInterfaceCount()
        {
            return Interfaces.Count;
        }

        /// <summary>
        /// Adds the interface.
        /// </summary>
        /// <returns>string: the name of the new interface</returns>
        public string AddInterface()
        {
            //ToDo Mehrere Lücken, d.h. mehrer gelöschte Interfaces beachten!
            string name = namePrefix + nextInterface;
            Interfaces.Add(name);

            if (nextInterface == (Interfaces.Count - 1))
                nextInterface++;
            else
                nextInterface = Interfaces.Count;

            return name;
        }

        /// <summary>
        /// Removes the interface with the given name.
        /// </summary>
        /// <param name="IfaceName">The name of the interface to be removed.</param>
        public void RemoveInterface(string IfaceName)
        {
            RemoveConnection(IfaceName);
            Interfaces.Remove(IfaceName);
            nextInterface = int.Parse(IfaceName.Substring(namePrefix.Length, IfaceName.Length - namePrefix.Length));
        }

        public override Hardwarenode Send(Hardwarenode Destination, Dictionary<string, object> Tags, Result Res, IPAddress nextNodeIP)
        {
            foreach (Connection c in connections.Values)
            {
                if (c.End.HasIP(nextNodeIP))
                {
                    return c.End;
                }
                if (c.Start.HasIP(nextNodeIP))
                {
                    return c.Start;
                }
            }
            Res.ErrorID = 4;
            Res.Res = "There is no Connection for the next Hardwarenode.";
            Res.SendError = true;
            return null;
        }

        public override bool Receive(Dictionary<string, object> Tags, Result Res, IPAddress nextNodeIP)
        {
            return base.Receive(Tags, Res, nextNodeIP);
        }
    }
}
