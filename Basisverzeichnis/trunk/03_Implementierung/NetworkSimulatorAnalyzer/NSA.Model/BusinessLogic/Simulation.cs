using System.Collections.Generic;
using System.Linq;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
    /// <summary>
    /// Klasse um die Simulation darzustellen
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// Liefert die Hinpackete zurück
        /// </summary>
        /// <value>
        /// Die Hinpackete
        /// </value>
        public List<Packet> PacketsSend { get; } = new List<Packet>();
        /// <summary>
        /// Liefert die Rückpackete zurück
        /// </summary>
        /// <value>
        /// Die Rückpackete
        /// </value>
        public List<Packet> PacketsReceived { get; } = new List<Packet>();
        /// <summary>
        /// Liefert den Namen der Quell-Workstation zurück
        /// </summary>
        /// <value>
        /// Der Name der Quell-Workstation
        /// </value>
        public string Source { get; private set; }
        /// <summary>
        /// Liefert den Namen der Ziel-Workstation zurück
        /// </summary>
        /// <value>
        /// Der Name der Ziel-Workstation
        /// </value>
        public string Destination { get; private set; }
        /// <summary>
        /// Liefert die ID zurück
        /// </summary>
        /// <value>
        /// Die ID der Simulation
        /// </value>
        public string Id { get; private set; }
        /// <summary>
        /// Liefert das erwartete Ergebnis zurück
        /// </summary>
        /// <value>
        ///   Das erwartete Ergebnis
        /// </value>
        public bool ExpectedResult { get; private set; }

        /// <summary>
        /// Konstruktor der für die Simulationen der Testszenarien verwendet wird.
        /// </summary>
        /// <param name="I">Die ID</param>
        public Simulation(string I)
        {
            Id = I;
            Source = null;
            Destination = null;
        }

        /// <summary>
        /// Konstruktor, der für die normalen Simulationen verwendet wird.
        /// </summary>
        /// <param name="I">Die ID</param>
        /// <param name="S">Der Name der Quell-Workstation</param>
        /// <param name="D">Der Name der Ziel-Workstation</param>
        /// <param name="ExpRes">Das erwartete Ergebnis</param>
        public Simulation(string I, string S, string D, bool ExpRes)
	    {
	        Id = I;
	        Source = S;
	        Destination = D;
            ExpectedResult = ExpRes;
	    }

        /// <summary>
        /// Fügt ein Hinpacket in die Liste ein.
        /// </summary>
        /// <param name="Packet">Das hinzuzufügende Packet</param>
        public void AddPacketSend(Packet Packet)
	    {
            PacketsSend.Add(Packet);
        }

        /// <summary>
        /// Führt die Simulation durch.
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
        /// Liefert alle Packete zurück
        /// </summary>
        /// <returns>Alle Packete</returns>
        public IEnumerable<Packet> GetAllPackets()
	    {
	        return PacketsSend.Concat(PacketsReceived);
	    }

        /// <summary>
        /// Liefert das letzte Packet zurück.
        /// </summary>
        /// <returns>Null wenn es kein Packet gibt, sonst das letzte Packet</returns>
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
