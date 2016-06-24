namespace NSA.Model.NetworkComponents.Helper_Classes
{
    /// <summary>
    /// Result class for the packetresult
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets the error identifier.
        /// </summary>
        /// <value>
        /// The error identifier.
        /// </value>
        public Errors ErrorId { get; set; }

        /// <summary>
        /// Enum for the possible errors
        /// </summary>
        public enum Errors
        {
            /// <summary>
            /// No error
            /// </summary>
            NoError,
            /// <summary>
            /// No Route or Standard-Gateway was found
            /// </summary>
            NoRoute,
            /// <summary>
            /// No connection to the next node
            /// </summary>
            NoConnection,
            /// <summary>
            /// The packet was not for this node
            /// </summary>
            PacketNotForThisNode,
            /// <summary>
            /// The switch has no connection to the next node
            /// </summary>
            SwitchNoConnection,
            /// <summary>
            /// The source or destination is null
            /// </summary>
            SourceDestinationNull,
            /// <summary>
            /// The TTL is zero
            /// </summary>
            TtlError,
            /// <summary>
            /// Custom Layer Error
            /// </summary>
            CustomLayerError,
            /// <summary>
            /// No Packets in the simulation
            /// </summary>
            NoPackets
        };

        /// <summary>
        /// The possible result strings
        /// </summary>
        public static readonly string[] ResultStrings = { "Es ist kein Fehler bei der Simulation aufgetreten.",
            "Es gibt keine Route oder Standard-Gateway zum Zielrechner.",
            "Es gibt keine Verbindung zum nächsten Rechner.",
            "Das Packet war nicht für diesen Rechner bestimmt.",
            "Es gibt keine Verbindung zum nächsten Rechner.",
            "Quell- oder Zielrechner ist null.",
            "TTL ist 0, aber der Zielrechner wurde nicht erreicht.",
            "Layer {0} ist am Quell- aber nicht am Zielrechner enthalten.",
            "Keine Packete in der Simulation."
        };

        /// <summary>
        /// Gets or sets the result string.
        /// </summary>
        /// <value>
        /// The result string
        /// </value>
        public string Res { get; set; }
        /// <summary>
        /// Gets or sets the layer of the error.
        /// </summary>
        /// <value>
        /// The layer of the error.
        /// </value>
        public ILayer LayerError { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether it's a send error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if send error otherwise, <c>false</c>.
        /// </value>
        public bool SendError { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Result()
        {
            ErrorId = Result.Errors.NoError;
            Res = "";
            LayerError = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ErrId">The error identifier.</param>
        /// <param name="R">The result string.</param>
        /// <param name="L">The layer.</param>
        public Result(Errors ErrId, string R, ILayer L)
        {
            ErrorId = ErrId;
            Res = R;
            LayerError = L;
        }
    }
}
