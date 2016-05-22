namespace NSA.Model.NetworkComponents
{
    class TransportLayer : ILayer
    {
        public bool ValidateReceive()
        {
            return true;
        }

        public Hardwarenode ValidateSend()
        {
            return null;
        }
    }
}
