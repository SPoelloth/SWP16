using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Switch : Hardwarenode
    {
        public List<string> Interfaces { get; } = new List<string>();
        private readonly string interfaceNamePrefix = "eth";

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
            string name = interfaceNamePrefix + GetNewInterfaceNumber();
            Interfaces.Add(name);
            return name;
        }

        /// <summary>
        /// Removes the interface with the given name.
        /// </summary>
        /// <param name="InterfaceName">The name.</param>
        public void RemoveInterface(string InterfaceName)
        {
            RemoveConnection(InterfaceName);
            Interfaces.Remove(InterfaceName);
        }

        /// <summary>
        /// Gets the new interface number.
        /// </summary>
        /// <returns>int: number for next interface</returns>
        private int GetNewInterfaceNumber()
        {
            int newInterface = 0;
            bool found = false;

            while (!found)
            {
                if (Interfaces.Exists(I => I.Equals(interfaceNamePrefix + newInterface)))
                    newInterface++;
                else
                    found = true;
            }

            return newInterface;
        }

        public override List<Hardwarenode> Send(Hardwarenode Destination, Dictionary<string, object> Tags, Result Res, IPAddress NextNodeIp)
        {
            List<Hardwarenode> nextNodes = new List<Hardwarenode>();
            foreach (Connection c in Connections.Values)
            {
                if (c.End.HasIp(NextNodeIp))
                {
                    nextNodes.Add(c.End);
                    return nextNodes;
                }
                if (c.Start.HasIp(NextNodeIp))
                {
                    nextNodes.Add(c.Start);
                    return nextNodes;
                }
            }
            //Check if the next switch can send it
            foreach (Connection c in Connections.Values)
            {
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s != null)
                    {
                        if (s.SendToIp(nextNodes, NextNodeIp))
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
                        if (s.SendToIp(nextNodes, NextNodeIp))
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

        public bool SendToIp(List<Hardwarenode> NextNodes, IPAddress NextNodeIp)
        {
            foreach (Connection c in Connections.Values)
            {
                if (c.End.HasIp(NextNodeIp))
                {
                    NextNodes.Add(c.End);
                    return true;
                }
                if (c.Start.HasIp(NextNodeIp))
                {
                    NextNodes.Add(c.Start);
                    return true;
                }
            }
            //check if the next switch can send it
            foreach (Connection c in Connections.Values)
            {
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s != null)
                    {
                        if (s.SendToIp(NextNodes, NextNodeIp))
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
                        if (s.SendToIp(NextNodes, NextNodeIp))
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
