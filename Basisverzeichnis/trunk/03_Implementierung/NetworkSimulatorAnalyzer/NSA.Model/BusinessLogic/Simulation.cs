using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
    /// <summary>
    /// Class for the simulation
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// Returns the sendpackets
        /// </summary>
        /// <value>
        /// The sendpackets
        /// </value>
        public List<Packet> PacketsSend { get; } = new List<Packet>();
        /// <summary>
        /// Returns the receivedpackets
        /// </summary>
        /// <value>
        /// The receivedpackets
        /// </value>
        public List<Packet> PacketsReceived { get; } = new List<Packet>();
        /// <summary>
        /// Returns the name of the sourcenode
        /// </summary>
        /// <value>
        /// Name of the sourcenode
        /// </value>
        public string Source { get; private set; }
        /// <summary>
        /// Returns the name of the destinationnode
        /// </summary>
        /// <value>
        /// Name of the destinationnode
        /// </value>
        public string Destination { get; private set; }
        /// <summary>
        /// Returns the ID
        /// </summary>
        /// <value>
        /// ID of the simulation
        /// </value>
        public string Id { get; private set; }
        /// <summary>
        /// Returns the expected result
        /// </summary>
        /// <value>
        ///   The expected result
        /// </value>
        public bool ExpectedResult { get; private set; }

        /// <summary>
        /// Constructor for the simultion used in testscenarios
        /// </summary>
        /// <param name="I">The ID</param>
        public Simulation(string I)
        {
            Id = I;
            Source = null;
            Destination = null;
        }

        /// <summary>
        /// Constructor used for normal simulation
        /// </summary>
        /// <param name="I">ID</param>
        /// <param name="S">Name of sourcenode</param>
        /// <param name="D">Name of destinationnode</param>
        /// <param name="ExpRes">Expected result</param>
        public Simulation(string I, string S, string D, bool ExpRes)
	    {
	        Id = I;
	        Source = S;
	        Destination = D;
            ExpectedResult = ExpRes;
	    }

        /// <summary>
        /// Adds a packet to the sendpackets
        /// </summary>
        /// <param name="Packet">The packet to be added</param>
        public void AddPacketSend(Packet Packet)
	    {
            PacketsSend.Add(Packet);
        }

        /// <summary>
        /// Executes this simulation
        /// </summary>
        public Result Execute()
	    {
            foreach (Packet sendpacket in PacketsSend)
            {
                if (sendpacket.Result.ErrorId == 0)
                {
                    Packet p = sendpacket.Send();

                    if (p != null)
                    {
                        PacketsReceived.Add(p);
                    }
                    else
                        return sendpacket.Result;
                }
            }

            foreach (Packet backpacket in PacketsReceived)
            {
                if(backpacket.Result.ErrorId == 0)
                    backpacket.Send();
            }
            if(PacketsReceived.Count > 0)
                return PacketsReceived[PacketsReceived.Count - 1].Result;
            return PacketsSend[PacketsSend.Count - 1].Result;
	    }

        /// <summary>
        /// Returns all packets
        /// </summary>
        /// <returns>All Packets</returns>
        public IEnumerable<Packet> GetAllPackets()
	    {
	        return PacketsSend.Concat(PacketsReceived);
	    }

        /// <summary>
        /// Returns the last packet
        /// </summary>
        /// <returns>Null (if there is no packet) or the last packet</returns>
        public Packet GetLastPacket()
	    {
	        if (PacketsReceived.Count == 0 && PacketsSend.Count == 0)
	            return null;
	        if (PacketsReceived.Count == 0)
	            return PacketsSend[PacketsSend.Count - 1];
	        return PacketsReceived[PacketsReceived.Count - 1];
	    }
    }
}
