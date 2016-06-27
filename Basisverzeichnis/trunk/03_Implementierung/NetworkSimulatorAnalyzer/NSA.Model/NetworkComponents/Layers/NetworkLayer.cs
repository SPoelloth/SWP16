using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    /// <summary>
    /// Network-Layer
    /// </summary>
    /// <seealso cref="NSA.Model.NetworkComponents.ILayer" />
    public class NetworkLayer : ILayer
    {
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkLayer"/> class.
        /// </summary>
        /// <param name="I">The index.</param>
        public NetworkLayer(int I)
        {
            index = I;
        }

        /// <summary>
        /// Validates the layer while receiving a packet.
        /// </summary>
        /// <param name="CurrentNode">Current node</param>
        /// <param name="ValInfo">Validation Info</param>
        /// <param name="Tags">Tags</param>
        /// <param name="Destination">Destinationnode</param>
        /// <param name="LayerIndex">Index of the Layer</param>
        /// <returns>
        /// Boolean value indicating if the validation was successfull
        /// </returns>
        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination, int LayerIndex)
        {
            return true;
        }

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        /// <returns>
        /// The Layername
        /// </returns>
        public string GetLayerName()
        {
            return "Vermittlungsschicht";
        }

        /// <summary>
        /// Sets the name of the layer.
        /// </summary>
        /// <param name="NewName">New Name</param>
        /// <returns></returns>
        public bool SetLayerName(string NewName)
        {
            return false;
        }

        /// <summary>
        /// Gets the index of the layer.
        /// </summary>
        /// <returns>The index</returns>
        public int GetLayerIndex()
        {
            return index;
        }

        /// <summary>
        /// Sets the index of the layer.
        /// </summary>
        /// <param name="I">The Index.</param>
        public void SetLayerIndex(int I)
        {
            index = I;
        }

        /// <summary>
        /// Validates the layer while sending a packet.
        /// </summary>
        /// <param name="Destination">The Destination</param>
        /// <param name="CurrentNode">Current Node</param>
        /// <param name="ValInfo">Validation Info</param>
        /// <param name="Tags">Tags</param>
        /// <param name="LayerIndex">The Layer index</param>
        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, int LayerIndex)
        {
            //if currentnode is connected to destination
            foreach (Connection c in CurrentNode.Connections.Values)
            {
                if (!c.Start.Equals(Destination) && !c.End.Equals(Destination)) continue;
                KeyValuePair<string, Connection> kvCurrent = CurrentNode.Connections.FirstOrDefault(S => S.Value.Equals(c));
                KeyValuePair<string, Connection> kvDest = Destination.Connections.FirstOrDefault(S => S.Value.Equals(c));
                Interface iCurr = CurrentNode.Interfaces.FirstOrDefault(I => I.Name.Equals(kvCurrent.Key));
                Interface iDest = Destination.Interfaces.FirstOrDefault(I => I.Name.Equals(kvDest.Key));
                if (iDest != null && (iCurr != null && iCurr.IpAddress.IsInSameSubnet(iDest.IpAddress, iCurr.Subnetmask)))
                {
                    ValInfo.NextNodes.Add(Destination);
                    ValInfo.Iface = null;
                    return;
                }
            }
            //if connected over switch
            foreach (Connection c in CurrentNode.Connections.Values)
            {
                Switch sw;
                KeyValuePair<string, Connection> kvCurrent = CurrentNode.Connections.FirstOrDefault(S => S.Value.Equals(c));
                Interface iCurr = CurrentNode.Interfaces.FirstOrDefault(I => I.Name.Equals(kvCurrent.Key));
                if (iCurr == null) continue;
                if (c.Start.Equals(CurrentNode))
                {
                    sw = c.End as Switch;
                    if (sw == null) continue;
                    if (!sw.SendToDestination(Destination, ValInfo, c, iCurr.IpAddress, iCurr.Subnetmask)) continue;
                    ValInfo.NextNodes.Insert(0, sw);
                    return;
                }
                sw = c.Start as Switch;
                if (sw == null) continue;
                if (!sw.SendToDestination(Destination, ValInfo, c, iCurr.IpAddress, iCurr.Subnetmask)) continue;
                ValInfo.NextNodes.Insert(0, sw);
                return;
            }
            //search in routes
            List<Interface> interfaces = Destination.Interfaces;
            foreach (Interface i in interfaces)
            {
                Dictionary<string, Route>.ValueCollection routes = CurrentNode.GetRoutes();
                foreach(Route r in routes)
                {
                    if (!i.IpAddress.IsInSameSubnet(r.Destination, r.Subnetmask)) continue;
                    ValInfo.NextNodeIp = r.Gateway;
                    ValInfo.Iface = r.Iface;
                }
            }
            if (CurrentNode.StandardGateway != null && ValInfo.NextNodeIp == null)
            {
                ValInfo.NextNodeIp = CurrentNode.StandardGateway;
                ValInfo.Iface = CurrentNode.StandardGatewayPort;
            }
            else if (ValInfo.NextNodeIp == null)
            {
                ValInfo.Res.ErrorId = Result.Errors.NoRoute;
                ValInfo.Res.Res = Result.ResultStrings[(int)ValInfo.Res.ErrorId];
                ValInfo.Res.LayerError = new NetworkLayer(index);
                ValInfo.Res.SendError = true;
                ValInfo.NextNodes = null;
            }
        }
    }
}
