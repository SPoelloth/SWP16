namespace NSA.Model.NetworkComponents
{
    class DataLinkLayer : ILayer
    {
        public Hardwarenode ValidateSend()
        {
            // Jeremy: hier muss überprüft werden, ob eine Connection zum nächsten Rechner existiert
            return null;
        }

        public bool ValidateReceive()
        {
            return true;
        }
    }
}
