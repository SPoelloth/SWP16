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

        public override List<Hardwarenode> Send(Hardwarenode Destination, Dictionary<string, object> Tags, Result Res, IPAddress NextNodeIp)
        {
            List<Hardwarenode> nextNodes = new List<Hardwarenode>();
            foreach (Connection c in connections.Values)
            {
                if (c.End.HasIP(NextNodeIp))
                {
                    nextNodes.Add(c.End);
                    return nextNodes;
                }
                if (c.Start.HasIP(NextNodeIp))
                {
                    nextNodes.Add(c.Start);
                    return nextNodes;
                }
            }
            //Check if the next switch can send it
            foreach (Connection c in connections.Values)
            {
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s != null)
                    {
                        if (s.SendToIP(nextNodes, NextNodeIp))
                        {
                            nextNodes.Insert(0, s);
                            return nextNodes;
                        }
                    }
                }
                else
                {
                    Switch s = c.Start as Switch;
                    if (s != null)
                    {
                        if (s.SendToIP(nextNodes, NextNodeIp))
                        {
                            nextNodes.Insert(0, s);
                            return nextNodes;
                        }
                    }
                }
            }
            Res.ErrorID = 4;
            Res.Res = "There is no Connection for the next Hardwarenode.";
            Res.SendError = true;
            return null;
        }

        public bool SendToIP(List<Hardwarenode> NextNodes, IPAddress NextNodeIp)
        {
            foreach (Connection c in connections.Values)
            {
                if (c.End.HasIP(NextNodeIp))
                {
                    NextNodes.Add(c.End);
                    return true;
                }
                if (c.Start.HasIP(NextNodeIp))
                {
                    NextNodes.Add(c.Start);
                    return true;
                }
            }
            //check if the next switch can send it
            foreach (Connection c in connections.Values)
            {
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s != null)
                    {
                        if (s.SendToIP(NextNodes, NextNodeIp))
                        {
                            NextNodes.Insert(0, s);
                            return true;
                        }
                    }
                }
                else
                {
                    Switch s = c.Start as Switch;
                    if (s != null)
                    {
                        if (s.SendToIP(NextNodes, NextNodeIp))
                        {
                            NextNodes.Insert(0, s);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
