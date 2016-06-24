using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Switch : Hardwarenode
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch"/> class.
        /// </summary>
        /// <param name="Name">The name of the switch.</param>
        public Switch(string Name) : base(Name)
        {
            for (int i = 0; i < 5; i++)
            {
                Interfaces.Add(new Interface(null, null, getNewInterfaceNumber()));
            }
        }

        #region methods
        #region interface methods

        /// <summary>
        /// Adds a new interface with the given IP and subnetmask
        /// </summary>
        /// <param name="Ip">The IP of the interface. Ignored if used with switch</param>
        /// <param name="Subnetmask">The subnetmask. Ignored if used with switch </param>
        /// <param name="PortNum">Number of port. Only for project loading purpose. </param>
        /// <returns>The newly added Interface</returns>
        public override Interface AddInterface(IPAddress Ip, IPAddress Subnetmask, int PortNum = -1)
        {
            if (PortNum == -1) PortNum = getNewInterfaceNumber();

            Interface interfaceObj = new Interface(null, null, PortNum);
            Interfaces.Add(interfaceObj);
            return interfaceObj;
        }

        /// <summary>
        /// Sets the interface count to the given value.
        /// </summary>
        /// <param name="count">The count.</param>
        public void SetInterfaceCount(int count)
        {
            Interfaces.Clear();
            for (int i = 0; i < count; i++)
            {
                Interfaces.Add(new Interface(null, null, getNewInterfaceNumber()));
            }
        }

        #endregion


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
                        if (s.SendToIp(ValInfo, c))
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
                        if (s.SendToIp(ValInfo, c))
                        {
                            nextNodes.Insert(0, s);
                            return nextNodes;
                        }
                    }
                }
            }
            ValInfo.Res.ErrorId = Result.Errors.SwitchNoConnection;
            ValInfo.Res.Res = Result.ResultStrings[(int)ValInfo.Res.ErrorId];
            ValInfo.Res.SendError = true;
            return null;
        }

        /// <summary>
        /// Sends to ip.
        /// </summary>
        /// <param name="ValInfo">The value information.</param>
        /// <param name="ComingConn">The coming connection.</param>
        /// <returns></returns>
        public bool SendToIp(ValidationInfo ValInfo, Connection ComingConn)
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
                if (c == ComingConn) continue;
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s != null)
                    {
                        if (s.SendToIp(ValInfo, c))
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
                        if (s.SendToIp(ValInfo, c))
                        {
                            ValInfo.NextNodes.Insert(0, s);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Sends to destination.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="ValInfo">The value information.</param>
        /// <param name="ComingCon">The coming connection.</param>
        /// <returns></returns>
        public bool SendToDestination(Workstation Destination, ValidationInfo ValInfo, Connection ComingCon)
        {
            foreach (Connection c in Connections.Values)
            {
                if (c.End.Equals(Destination))
                {
                    ValInfo.NextNodes.Add(c.End);
                    return true;
                }
                if (c.Start.Equals(Destination))
                {
                    ValInfo.NextNodes.Add(c.Start);
                    return true;
                }
            }
            //check if the next switch can send it
            foreach (Connection c in Connections.Values)
            {
                if (c == ComingCon) continue;
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s != null)
                    {
                        if (s.SendToDestination(Destination, ValInfo, c))
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
                        if (s.SendToDestination(Destination, ValInfo, c))
                        {
                            ValInfo.NextNodes.Insert(0, s);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
