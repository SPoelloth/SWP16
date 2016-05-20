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

	    public void AddPacketSend(Packet packet)
	    {
	    }

	    public void Execute()
	    {
	    }
    }
}
