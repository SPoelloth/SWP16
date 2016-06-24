using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    /// <summary>
    /// DataLink-Layer
    /// </summary>
    /// <seealso cref="NSA.Model.NetworkComponents.ILayer" />
    public class DataLinkLayer : ILayer
    {
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataLinkLayer"/> class.
        /// </summary>
        /// <param name="I">The index.</param>
        public DataLinkLayer(int I)
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
            if (ValInfo.Iface == null)
                return;
            Connection c;
            if(CurrentNode.Connections.TryGetValue(ValInfo.Iface.Name, out c))
            {
                ValInfo.NextNodes.Add(c.Start.Equals(CurrentNode) ? c.End : c.Start);
                return;
            }
            ValInfo.Res.ErrorId = Result.Errors.NoConnection;
            ValInfo.Res.Res = Result.ResultStrings[(int)ValInfo.Res.ErrorId];
            ValInfo.Res.LayerError = new DataLinkLayer(index);
            ValInfo.Res.SendError = true;
            ValInfo.NextNodes = null;
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
            if (ValInfo.NextNodeIp == null)
                return true;
            List<Interface> ifaces = CurrentNode.Interfaces;
            if (ifaces.Any(I => ValInfo.NextNodeIp.Equals(I.IpAddress)))
            {
                return true;
            }
            ValInfo.Res.ErrorId = Result.Errors.PacketNotForThisNode;
            ValInfo.Res.Res = Result.ResultStrings[(int) ValInfo.Res.ErrorId];
            ValInfo.Res.LayerError = new DataLinkLayer(index);
            ValInfo.Res.SendError = false;
            return false;
        }

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        /// <returns>
        /// The Layername
        /// </returns>
        public string GetLayerName()
        {
            return "Sicherungsschicht";
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
    }
}
