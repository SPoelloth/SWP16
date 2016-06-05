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
        /// <param name="nextNode">The next node.</param>
        /// <param name="nextNodeIP">The next node ip.</param>
        /// <param name="iface">The interface of the connection to the next node.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="currentNode">The current node.</param>
        /// <param name="Res">Result.</param>
        void ValidateSend(Hardwarenode nextNode, IPAddress nextNodeIP, Interface iface, Workstation destination, Workstation currentNode, Result Res);

        /// <summary>
        /// Validates the layer while receiving a packet.
        /// </summary>
        /// <param name="nextNodeIP"></param>
        /// <param name="currentNode"></param>
        /// <param name="Res"></param>
        /// <returns>Boolean value indicating if the validation was successfull</returns>
        bool ValidateReceive(IPAddress nextNodeIP, Workstation currentNode, Result Res);
    }
}
