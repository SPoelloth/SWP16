namespace NSA.Model.NetworkComponents
{
    class PhysicalLayer : ILayer
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
