using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
         */
        public int ErrorID { get; set; }
        public string Res { get; set; }
        public ILayer LayerError { get; set; }
        public bool SendError { get; set; }

        public Result()
        {
            ErrorID = 0;
            Res = "";
            LayerError = null;
        }
    }
}
