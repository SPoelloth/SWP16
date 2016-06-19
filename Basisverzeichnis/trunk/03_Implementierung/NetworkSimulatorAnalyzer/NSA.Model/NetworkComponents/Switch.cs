using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Switch : Hardwarenode
    {
        public List<string> Interfaces { get; } = new List<string>();
        public static readonly string InterfaceNamePrefix = "eth";

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch"/> class.
        /// </summary>
        /// <param name="Name">The name of the switch.</param>
        public Switch(string Name) : base(Name)
        {
            AddInterface();
            AddInterface();
            AddInterface();
            AddInterface();
            AddInterface();
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
            string name = InterfaceNamePrefix + GetNewInterfaceNumber();
            Interfaces.Add(name);
            return name;
        }

        /// <summary>
        /// Removes the interface with the given name.
        /// </summary>
        /// <param name="InterfaceName">The name.</param>
        public void RemoveInterface(string InterfaceName)
        {
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
                if (Interfaces.Exists(I => I.Equals(InterfaceNamePrefix + newInterface)))
                    newInterface++;
                else
                    found = true;
            }

            return newInterface;
        }

        /// <summary>
        /// Determines if there is an Interface with the specified name.
        /// </summary>
        /// <param name="IfaceName">Name of the iface.</param>
        /// <returns></returns>
        public override bool HasInterface(string IfaceName)
        {
            foreach (string i in Interfaces)
            {
                if (i == IfaceName)
                    return true;
            }
            return false;
        }

        public override List<Hardwarenode> Send(Hardwarenode Destination, Dictionary<string, object> Tags, ValidationInfo ValInfo)
        {
            List<Hardwarenode> nextNodes = new List<Hardwarenode>();
            foreach (Connection c in Connections.Values)
            {
                if (c.End.HasIp(ValInfo.NextNodeIp))
                {
                    nextNodes.Add(c.End);
                    return nextNodes;
                }
                if (c.Start.HasIp(ValInfo.NextNodeIp))
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
                        if (s.SendToIp(ValInfo))
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
                        if (s.SendToIp(ValInfo))
                        {
                            nextNodes.Insert(0, s);
                            return nextNodes;
                        }
                    }
                }
            }
            ValInfo.Res.ErrorId = 4;
            ValInfo.Res.Res = "There is no Connection for the next Hardwarenode.";
            ValInfo.Res.SendError = true;
            return null;
        }

        /// <summary>
        /// Sends to ip.
        /// </summary>
        /// <param name="ValInfo">The value information.</param>
        /// <returns></returns>
        public bool SendToIp(ValidationInfo ValInfo)
        {
            foreach (Connection c in Connections.Values)
            {
                if (c.End.HasIp(ValInfo.NextNodeIp))
                {
                    ValInfo.NextNodes.Add(c.End);
                    return true;
                }
                if (c.Start.HasIp(ValInfo.NextNodeIp))
                {
                    ValInfo.NextNodes.Add(c.Start);
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
                        if (s.SendToIp(ValInfo))
                        {
                            ValInfo.NextNodes.Insert(0, s);
                            return true;
                        }
                    }
                }
                else
                {
                    Switch s = c.Start as Switch;
                    if (s != null)
                    {
                        if (s.SendToIp(ValInfo))
                        {
                            ValInfo.NextNodes.Insert(0, s);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
