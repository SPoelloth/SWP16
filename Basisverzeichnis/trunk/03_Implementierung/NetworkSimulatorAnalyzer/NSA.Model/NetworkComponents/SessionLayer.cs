namespace NSA.Model.NetworkComponents
{
    class SessionLayer : ILayer
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
