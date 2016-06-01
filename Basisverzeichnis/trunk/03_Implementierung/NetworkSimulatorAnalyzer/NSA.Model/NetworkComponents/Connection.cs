using System;

namespace NSA.Model.NetworkComponents
{
    public class Connection
    {
        public Hardwarenode Start { get; private set; }
        public Hardwarenode End { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="Source">The sourcenode.</param>
        /// <param name="Target">The targetnode.</param>
        public Connection(Hardwarenode Source, Hardwarenode Target)
        {
            Start = Source;
            End = Target;
            Name = Guid.NewGuid().ToString("N");
        }
    }
}
