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
        void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo);

        /// <summary>
        /// Validates the layer while receiving a packet.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="valInfo"></param>
        /// <returns>Boolean value indicating if the validation was successfull</returns>
        bool ValidateReceive(Workstation currentNode, ValidationInfo valInfo);
    }
}
