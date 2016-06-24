using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    /// <summary>
    /// Transport-Layer
    /// </summary>
    /// <seealso cref="NSA.Model.NetworkComponents.ILayer" />
    public class TransportLayer : ILayer
    {
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransportLayer"/> class.
        /// </summary>
        /// <param name="I">The i.</param>
        public TransportLayer(int I)
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
            return "Transportschicht";
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
        /// <returns>Index</returns>
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
            
        }
    }
}
