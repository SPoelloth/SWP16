using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Switch : Hardwarenode
    {
        public List<string> Interfaces { get; private set; } = new List<string>();
        private readonly string interfaceNamePrefix = "eth";
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
            string name = interfaceNamePrefix + nextInterface;
            Interfaces.Add(name);

            if (nextInterface == (Interfaces.Count - 1))
                nextInterface++;
            else
                nextInterface = Interfaces.Count;

            return name;
        }

        /// <summary>
        /// Removes the interface with the given number.
        /// </summary>
        /// <param name="number">The number.</param>
        public void RemoveInterface(int number)
        {
            string ifaceName = interfaceNamePrefix + number;
            RemoveConnection(ifaceName);
            Interfaces.Remove(ifaceName);
            nextInterface = int.Parse(ifaceName.Substring(interfaceNamePrefix.Length, ifaceName.Length - interfaceNamePrefix.Length));
        }

        public override Hardwarenode Send(Hardwarenode Destination, Dictionary<string, object> Tags, Result Res, IPAddress NextNodeIp)
        {
            foreach (Connection c in connections.Values)
            {
                if (c.End.HasIP(NextNodeIp))
                {
                    return c.End;
                }
                if (c.Start.HasIP(NextNodeIp))
                {
                    return c.Start;
                }
            }
            Res.ErrorID = 4;
            Res.Res = "There is no Connection for the next Hardwarenode.";
            Res.SendError = true;
            return null;
        }
    }
}
