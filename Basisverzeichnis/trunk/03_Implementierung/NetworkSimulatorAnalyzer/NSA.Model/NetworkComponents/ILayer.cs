namespace NSA.Model.NetworkComponents
{
    public interface ILayer
    {
        /// <summary>
        /// Validates the layer while sending a packet.
        /// </summary>
        /// <returns>null if not successfull or the next Hardwarenode if it was</returns>
        Hardwarenode ValidateSend();

        /// <summary>
        /// Validates the layer while receiving a packet.
        /// </summary>
        /// <returns>Boolean value indicating if the validation was successfull</returns>
        bool ValidateReceive();
    }
}
