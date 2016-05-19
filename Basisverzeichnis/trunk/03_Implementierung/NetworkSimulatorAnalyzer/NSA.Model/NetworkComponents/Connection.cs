namespace NSA.Model.NetworkComponents
{
    public class Connection
    {
        public Hardwarenode start;
        public Hardwarenode end;

        public Connection(Hardwarenode source, Hardwarenode target)
        {
            start = source;
            end = target;
        }
    }
}
