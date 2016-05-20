namespace NSA.Model.NetworkComponents
{
    public class Connection
    {
        public Hardwarenode start;
        public Hardwarenode end;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="source">The sourcenode.</param>
        /// <param name="target">The targetnode.</param>
        public Connection(Hardwarenode source, Hardwarenode target)
        {
            start = source;
            end = target;
        }
    }
}
