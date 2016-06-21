using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
	public class Simulation
    {
        private readonly List<Packet> packetsSend = new List<Packet>();
        private readonly List<Packet> packetsReceived = new List<Packet>();
        public string Source { get; private set; }
        public string Destination { get; private set; }
	    public string Id { get; private set; }

        public Simulation(string I)
        {
            Id = I;
            Source = null;
            Destination = null;
        }

        public Simulation(string I, string S, string D)
	    {
	        Id = I;
	        Source = S;
	        Destination = D;
	    }

        /// <summary>
        /// Adds the packet send.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        public void AddPacketSend(Packet Packet)
	    {
            packetsSend.Add(Packet);
        }

        /// <summary>
        /// Gets the send packets.
        /// </summary>
        /// <returns></returns>
        public List<Packet> GetSendPackets()
	    {
	        return packetsSend;
	    }

        /// <summary>
        /// Gets the received packets.
        /// </summary>
        /// <returns></returns>
        public List<Packet> GetReceivedPackets()
        {
            return packetsReceived;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public Result Execute()
	    {
            foreach (Packet sendpacket in packetsSend)
            {
                if (sendpacket.Result.ErrorId == 0)
                {
                    Packet p = sendpacket.Send();

                    if (p != null)
                    {
                        packetsReceived.Add(p);
                    }
                    else
                        return sendpacket.Result;
                }
            }

            foreach (Packet backpacket in packetsReceived)
            {
                if(backpacket.Result.ErrorId == 0)
                    backpacket.Send();
            }
            if(packetsReceived.Count > 0)
                return packetsReceived[packetsReceived.Count - 1].Result;
            return packetsSend[packetsSend.Count - 1].Result;
	    }

        /// <summary>
        /// Gets all packets.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Packet> GetAllPackets()
	    {
	        return packetsSend.Concat(packetsReceived);
	    }

        /// <summary>
        /// Gets the last packet.
        /// </summary>
        /// <returns>Null if there is no packet.</returns>
        public Packet GetLastPacket()
	    {
	        if (packetsReceived.Count == 0 && packetsSend.Count == 0)
	            return null;
	        if (packetsReceived.Count == 0)
	            return packetsSend[packetsSend.Count - 1];
	        return packetsReceived[packetsReceived.Count - 1];
	    }
    }
}
