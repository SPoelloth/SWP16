using System.Collections.Generic;
using System.Linq;
﻿using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
	public class Simulation
    {
        private List<Packet> packetsSend = new List<Packet>();
        private List<Packet> packetsReceived = new List<Packet>();
	    private int id;

	    public Simulation(int _id)
	    {
	        id = _id;
	    }

        /// <summary>
        /// Adds the packet send.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void AddPacketSend(Packet packet)
	    {
            packetsSend.Add(packet);
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
    }
}
