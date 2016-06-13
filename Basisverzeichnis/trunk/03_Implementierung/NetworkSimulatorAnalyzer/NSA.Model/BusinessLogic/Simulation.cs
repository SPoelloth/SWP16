using System.Collections.Generic;

namespace NSA.Model.BusinessLogic
{
	public class Simulation
    {
        private List<Packet> packetsSend;
        private List<Packet> packetsReceived;
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
        public void Execute()
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
                }
            }

            foreach (Packet backpacket in packetsReceived)
            {
                if(backpacket.result.ErrorID == 0)
                    backpacket.Send();
            }
        }
    }
}
