namespace NSA.Model.NetworkComponents
{
    class PresentationLayer : ILayer
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
