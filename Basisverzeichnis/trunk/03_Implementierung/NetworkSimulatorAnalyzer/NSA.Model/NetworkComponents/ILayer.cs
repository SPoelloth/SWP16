using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public interface ILayer
    {
        /// <summary>
        /// Validates the layer while sending a packet.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="currentNode">The current node.</param>
        /// <param name="valInfo"></param>
        /// <param name="Tags"></param>
        void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo, Dictionary<string, object> Tags);

        /// <summary>
        /// Validates the layer while receiving a packet.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="valInfo"></param>
        /// <param name="Tags"></param>
        /// <param name="destination"></param>
        /// <returns>Boolean value indicating if the validation was successfull</returns>
        bool ValidateReceive(Workstation currentNode, ValidationInfo valInfo, Dictionary<string, object> Tags, Hardwarenode destination);

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        /// <returns>The Layername</returns>
        string GetLayerName();
    }
}
