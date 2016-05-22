namespace NSA.Model.NetworkComponents
{
    class NetworkLayer : ILayer
    {
        public bool ValidateReceive()
        {
            return true;
        }

        public Hardwarenode ValidateSend()
        {
            // Jeremy: hier muss der nächste Rechner mithilfe der Routingtabelle ermittelt werden
            return null;
        }
    }
}
