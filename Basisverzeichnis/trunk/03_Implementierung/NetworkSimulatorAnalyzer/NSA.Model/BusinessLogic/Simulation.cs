using System.Collections.Generic;

namespace NSA.Model.BusinessLogic
{
	public class Simulation
    {
        private List<Packet> packetsSend;
        private List<Packet> packetsReceived;
	    private string id;
	    private bool expectedResult;

	    public Simulation(string _id, bool _result)
	    {
	        id = _id;
	        expectedResult = _result;
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
                Packet p = sendpacket.Send();

                if (p != null)
                {
                    packetsReceived.Add(p);
                }
            }

            foreach (Packet backpacket in packetsReceived)
            {
                backpacket.Send();
            }
        }
    }
}
