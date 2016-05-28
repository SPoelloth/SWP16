using System;

namespace NSA.Model.NetworkComponents
{
    public class Connection
    {
        public Hardwarenode Start { get; private set; }
        public Hardwarenode End { get; private set; }
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="source">The sourcenode.</param>
        /// <param name="target">The targetnode.</param>
        // Frage: nach welchem Schema sollen connections benannt werden (zwecks Eindeutigkeit)?
        public Connection(Hardwarenode source, Hardwarenode target)
        {
            Start = source;
            End = target;
            Name = Guid.NewGuid().ToString("N");
        }
    }
}
