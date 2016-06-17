using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public interface ILayer
    {
        /// <summary>
        /// Validates the layer while sending a packet.
        /// </summary>
        /// <param name="Destination"></param>
        /// <param name="CurrentNode"></param>
        /// <param name="ValInfo"></param>
        /// <param name="Tags"></param>
        void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags);

        /// <summary>
        /// Validates the layer while receiving a packet.
        /// </summary>
        /// <param name="CurrentNode"></param>
        /// <param name="ValInfo"></param>
        /// <param name="Tags"></param>
        /// <param name="Destination"></param>
        /// <returns>Boolean value indicating if the validation was successfull</returns>
        bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination);

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        /// <returns>The Layername</returns>
        string GetLayerName();

        /// <summary>
        /// Sets the name of the layer.
        /// </summary>
        /// <param name="NewName"></param>
        /// <returns></returns>
        bool SetLayerName(string NewName);
    }
}
