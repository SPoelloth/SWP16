using System.Collections.Generic;
using System.Linq;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{

    /// <summary>
    /// Implements the network component switch.
    /// </summary>
    /// <seealso cref="NSA.Model.NetworkComponents.Hardwarenode" />
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
        /// <param name="Count">The count.</param>
        public List<Interface> SetInterfaceCount(int Count)
        {
            List<Interface> removedInterfaces = new List<Interface>();
            for (int i = Interfaces.Count; i < Count; i++)
            {
                Interfaces.Add(new Interface(null, null, getNewInterfaceNumber()));
            }
            for (int i = Interfaces.Count; i > Count; i--)
            {
                var last = Interfaces.Last();
                removedInterfaces.Add(last);
                Interfaces.Remove(last);
            }

            return removedInterfaces;
        }

        #endregion


        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="ValInfo">Validation Info</param>
        /// <returns>
        /// The Hardwarenode which received the package or null if an error occured
        /// </returns>
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
                if (!c.Start.HasIp(ValInfo.NextNodeIp)) continue;
                nextNodes.Add(c.Start);
                return nextNodes;
            }
            //Check if the next switch can send it
            foreach (Connection c in Connections.Values)
            {
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s == null) continue;
                    if (!s.SendToIp(ValInfo, c)) continue;
                    nextNodes.Insert(0, s);
                    return nextNodes;
                }
                else
                {
                    Switch s = c.Start as Switch;
                    if (s == null) continue;
                    if (!s.SendToIp(ValInfo, c)) continue;
                    nextNodes.Insert(0, s);
                    return nextNodes;
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
        /// <returns>Bool if it could send</returns>
        public bool SendToIp(ValidationInfo ValInfo, Connection ComingConn)
        {
            foreach (Connection c in Connections.Values)
            {
                if (c.End.HasIp(ValInfo.NextNodeIp))
                {
                    ValInfo.NextNodes.Add(c.End);
                    return true;
                }
                if (!c.Start.HasIp(ValInfo.NextNodeIp)) continue;
                ValInfo.NextNodes.Add(c.Start);
                return true;
            }
            //check if the next switch can send it
            foreach (Connection c in Connections.Values)
            {
                if (c == ComingConn) continue;
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s == null) continue;
                    if (!s.SendToIp(ValInfo, c)) continue;
                    ValInfo.NextNodes.Insert(0, s);
                    return true;
                }
                else
                {
                    Switch s = c.Start as Switch;
                    if (s == null) continue;
                    if (!s.SendToIp(ValInfo, c)) continue;
                    ValInfo.NextNodes.Insert(0, s);
                    return true;
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
        /// <returns>Bool if it could send</returns>
        public bool SendToDestination(Workstation Destination, ValidationInfo ValInfo, Connection ComingCon)
        {
            foreach (Connection c in Connections.Values)
            {
                if (c.End.Equals(Destination))
                {
                    ValInfo.NextNodes.Add(c.End);
                    return true;
                }
                if (!c.Start.Equals(Destination)) continue;
                ValInfo.NextNodes.Add(c.Start);
                return true;
            }
            //check if the next switch can send it
            foreach (Connection c in Connections.Values)
            {
                if (c == ComingCon) continue;
                if (c.Start.Equals(this))
                {
                    Switch s = c.End as Switch;
                    if (s == null) continue;
                    if (!s.SendToDestination(Destination, ValInfo, c)) continue;
                    ValInfo.NextNodes.Insert(0, s);
                    return true;
                }
                else
                {
                    Switch s = c.Start as Switch;
                    if (s == null) continue;
                    if (!s.SendToDestination(Destination, ValInfo, c)) continue;
                    ValInfo.NextNodes.Insert(0, s);
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
