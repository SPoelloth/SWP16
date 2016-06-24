using System;
using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;
using NSA.Model.NetworkComponents.Layers;

namespace NSA.Model.BusinessLogic
{
    /// <summary>
    /// Class for a packet
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// Returns the Source-Node
        /// </summary>
        /// <value>
        /// The source
        /// </value>
        public Hardwarenode Source { get; }
        /// <summary>
        /// Returns the Destination-Node
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public Hardwarenode Destination { get; }
        /// <summary>
        /// Returns the Hops of the packet
        /// </summary>
        /// <value>
        /// The Hops
        /// </value>
        public List<Hardwarenode> Hops { get; } = new List<Hardwarenode>();
        /// <summary>
        /// </summary>
        /// <value>
        /// The Time-To-Life
        /// </value>
        public int Ttl { get; private set; }
        /// <summary>
        /// Returns the result of the packet
        /// </summary>
        /// <value>
        /// Result of the packet
        /// </value>
        public Result Result { get; private set; } = new Result();
        /// <summary>
        /// Returns the expected result.
        /// </summary>
        /// <value>
        ///   Expected Result
        /// </value>
        public bool ExpectedResult { get; }
        private Dictionary<string, object> tags = new Dictionary<string, object>();

        /// <summary>
        /// Constructor for Packet
        /// </summary>
        /// <param name="Src">Source-Node</param>
        /// <param name="Dest">Destination-Node</param>
        /// <param name="T">TTL</param>
        /// <param name="ExpRes">Expected Result</param>
        public Packet(Hardwarenode Src, Hardwarenode Dest,
            int T, bool ExpRes)
        {
            Source = Src;
            Destination = Dest;
            if (Source == null || Destination == null)
            {
                Result.ErrorId = Result.Errors.SourceDestinationNull;
                Result.Res = Result.ResultStrings[(int)Result.ErrorId];
                Result.SendError = true;
            }
            Ttl = T;
            ExpectedResult = ExpRes;
            Hops.Add(Source);
        }

        /// <summary>
        /// Sends this packet to the destination.
        /// </summary>
        /// <returns>The Returnpacket if sending to destination was successfull or null</returns>
        public Packet Send()
        {
            ValidationInfo valInfo = new ValidationInfo
            {
                NextNodeIp = null,
                Res = Result,
                Source = Source as Workstation
            };
            while (!Hops[Hops.Count - 1].Equals(Destination) && valInfo.Res.ErrorId == 0 && Ttl > 0)
            {
                List<Hardwarenode> nextNodes = Hops[Hops.Count - 1].Send(Destination, tags, valInfo);
                if (nextNodes != null)
                {
                    if (nextNodes[nextNodes.Count - 1].Receive(tags, valInfo, Destination))
                        Ttl--;
                    foreach (Hardwarenode n in nextNodes)
                    {
                        Hops.Add(n);
                    }
                }
            }
            Result = valInfo.Res;
            if (!Hops[Hops.Count - 1].Equals(Destination) && Ttl == 0)
            {
                //TTL Error
                Result.ErrorId = Result.Errors.TtlError;
                Result.Res = Result.ResultStrings[(int)Result.ErrorId];
                Result.SendError = true;
            }
            if (Result.ErrorId == 0 && tags.Count != 0)
            {
                //Layer Error
                Result.ErrorId = Result.Errors.CustomLayerError;
                Result.Res = String.Format(Result.ResultStrings[(int)Result.ErrorId], tags.Keys.First());
                Result.LayerError = new CustomLayer(tags.Keys.First(), 0);
                Result.SendError = false;
            }
            if(Result.ErrorId == 0)
                return new Packet(Destination, Source, Ttl, ExpectedResult);
            return null;
        }
    }
}
