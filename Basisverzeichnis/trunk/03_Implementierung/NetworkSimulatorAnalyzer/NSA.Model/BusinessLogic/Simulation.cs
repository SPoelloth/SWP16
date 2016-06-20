using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
	public class Simulation
    {
        public List<Packet> PacketsSend { get; } = new List<Packet>();
        public List<Packet> PacketsReceived { get; } = new List<Packet>();
        public string Source { get; private set; }
        public string Destination { get; private set; }
	    public string Id { get; private set; }
        public bool ExpectedResult { get; private set; }

        public Simulation(string I)
        {
            Id = I;
            Source = null;
            Destination = null;
        }

        public Simulation(string I, string S, string D, bool ExpRes)
	    {
	        Id = I;
	        Source = S;
	        Destination = D;
            ExpectedResult = ExpRes;
	    }

        /// <summary>
        /// Adds the packet send.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        public void AddPacketSend(Packet Packet)
	    {
            PacketsSend.Add(Packet);
        }

        /// <summary>
        /// Executes this instance.
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
        /// Gets all packets.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Packet> GetAllPackets()
	    {
	        return PacketsSend.Concat(PacketsReceived);
	    }

        /// <summary>
        /// Gets the last packet.
        /// </summary>
        /// <returns>Null if there is no packet.</returns>
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
