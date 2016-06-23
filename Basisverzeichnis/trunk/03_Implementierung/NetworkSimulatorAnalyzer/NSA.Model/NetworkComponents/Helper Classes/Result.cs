namespace NSA.Model.NetworkComponents.Helper_Classes
{
    public class Result
    {
        /*
         * Possible ErrorIDs:
         * 0: No Error.
         * 1: NetworkLayer:  No Route or Standard-Gateway for the destination.
         * 2: DataLinkLayer: There is no Connection for the next Node.
         * 3: DataLinkLayer: The packet was not for this Node.
         * 4: Switch:        No Connection to the next Node.
         * 5: Packet:        Source or Destination is null.
         * 6: Packet:        TTL is 0 but the destination was not reached
         * 7: Tags:          CustomLayer at source but not at destination
         */
        public Errors ErrorId { get; set; }

        public enum Errors
        {
            NoError,
            NoRoute,
            NoConnection,
            PacketNotForThisNode,
            SwitchNoConnection,
            SourceDestinationNull,
            TtlError,
            CustomLayerError,
            NoPackets
        };

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

        public string Res { get; set; }
        public ILayer LayerError { get; set; }
        public bool SendError { get; set; }

        public Result()
        {
            ErrorId = Result.Errors.NoError;
            Res = "";
            LayerError = null;
        }

        public Result(Errors ErrId, string R, ILayer L)
        {
            ErrorId = ErrId;
            Res = R;
            LayerError = L;
        }
    }
}
