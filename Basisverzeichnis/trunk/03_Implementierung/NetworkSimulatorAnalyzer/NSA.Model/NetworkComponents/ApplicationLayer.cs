namespace NSA.Model.NetworkComponents
{
    class ApplicationLayer : ILayer
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
