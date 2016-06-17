using System.Collections.Generic;
using System.Linq;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
	public class Simulation
    {
        private List<Packet> packetsSend = new List<Packet>();
        private List<Packet> packetsReceived = new List<Packet>();
        public IPAddress Source { get; private set; }
        public IPAddress Destination { get; private set; }
	    public int Id { get; private set; }

        public Simulation(int I)
        {
            Id = I;
            Source = null;
            Destination = null;
        }

        public Simulation(int I, IPAddress S, IPAddress D)
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
        /// Executes this instance.
        /// </summary>
        public Result Execute()
	    {
            foreach (Packet sendpacket in packetsSend)
            {
                if (sendpacket.result.ErrorID == 0)
                {
                    Packet p = sendpacket.Send();

                    if (p != null)
                    {
                        packetsReceived.Add(p);
                    }
                    else
                        return sendpacket.result;
                }
            }

            foreach (Packet backpacket in packetsReceived)
            {
                if(backpacket.result.ErrorID == 0)
                    backpacket.Send();
            }
            if(packetsReceived.Count > 0)
                return packetsReceived[packetsReceived.Count - 1].result;
            return packetsSend[packetsSend.Count - 1].result;
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
