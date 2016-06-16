using System;
using NSA.Model.BusinessLogic;
using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Controller
{
    internal class SimulationManager
    {
        public List<Simulation> Simulations { get; }

        public static SimulationManager Instance = new SimulationManager();
        /// <summary>
        /// Default Constructor.
        /// </summary>
        private SimulationManager()
        {
            Simulations = new List<Simulation>();
        }

        // Constructor:
        public SimulationManager(List<Simulation> Simulations)
        {
            this.Simulations = Simulations;
        }

        /// <summary>
        /// Gets the result for the selected Hop. Only use with NodeNames of the Last Simulation!
        /// </summary>
        /// <param name="NodeOneName">Name of the node one of the hop.</param>
        /// <param name="NodeTwoName">Name of the node two of the hop.</param>
        /// <returns>Null if any error occured. If not: res[0] == result of node one & res[1] == result of node two</returns>
        public List<Result> HopSelected(string NodeOneName, string NodeTwoName)
        {
            Hardwarenode nodeOne = NetworkManager.Instance.GetHardwarenodeByName(NodeOneName);
            Hardwarenode nodeTwo = NetworkManager.Instance.GetHardwarenodeByName(NodeTwoName);
            if (nodeOne == null || nodeTwo == null)
                return null;
            if (Simulations.Count == 0)
                return null;
            List<Hardwarenode> hops = GetHopsOfLastPacket(Simulations.Count - 1);
            if(hops == null)
                return null;
            List<Result> res = new List<Result>();
            if (hops[hops.Count - 1].Equals(nodeOne))
            {
                Packet p = Simulations[Simulations.Count - 1].GetLastPacket();
                if (p == null)
                    return null;
                res.Add(p.result);
                res.Add(new Result());
            }
            else if (hops[hops.Count - 1].Equals(nodeTwo))
            {
                res.Add(new Result());
                Packet p = Simulations[Simulations.Count - 1].GetLastPacket();
                if (p == null)
                    return null;
                res.Add(p.result);
            }
            else
            {
                res.Add(new Result());
                res.Add(new Result());
            }
            return res;
        }

        /// <summary>
        /// Gets the hops of last packet.
        /// </summary>
        /// <param name="index">The index of the simulation in the history.</param>
        /// <returns>Null, if there is no packet. The Hop-List kann have count == 0</returns>
        public List<Hardwarenode> GetHopsOfLastPacket(int index)
        {
            if (index >= Simulations.Count)
                return null;
            Packet p = Simulations[index].GetLastPacket();
            return p?.GetHops();
        }

        /// <summary>
        /// Starts the simulation.
        /// </summary>
        /// <param name="Sim">The sim.</param>
        /// <returns>Result of the last packet</returns>
        public Result StartSimulation(Simulation Sim)
        {
            return Sim.Execute();
        }

        /// <summary>
        /// Gets the simulation result.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>True if it worked, false if not</returns>
        public bool GetSimulationResult(int index)
        {
            Simulation sim = Simulations[index];
            bool result = false;
            foreach (Packet p in sim.GetAllPackets())
            {
                if ((p.expectedResult && p.result.ErrorID == 0) || (!p.expectedResult && p.result.ErrorID != 0))
                    result = true;
                else
                    result = false;
                if (result = false)
                    return result;
            }
            return result;
        }

        public void StartTestscenario(Testscenario T)
        {
            // todo Dennis & Oleg
        }

        public void AddSimulationToHistory(Simulation Sim)
        {
            Simulations.Add(Sim);
        }

        public void RunSimulationFromHistory(int Index)
        {
            // todo create a new Simulation Object with the same parameters as the simulation with the index
            // Jeremy: i don't know if we should add this simulation to the History... i would say yes
            // Beispiel: Google Chrome/Firefox nimmt eine Seite, die man aus dem Verlauf/Chronik aus aufruft auch wieder in die Chronik auf
        }

        public void RunLastSimulation()
        {
            // todo create a new Simulation Object with the same parameters as the Last simulation
            // Jeremy: i don't know if we should add this simulation to the History... i would say yes
        }

        /// <summary>
        /// Creates the simulation and starts it.
        /// </summary>
        /// <param name="Source">The source.</param>
        /// <param name="Destination">The destination.</param>
        /// <param name="Ttl">The TTL.</param>
        /// <param name="Tags">The tags.</param>
        /// <param name="ExpectedResult">the expected result of the simulation.</param>
        public Result CreateSimulation(IPAddress Source, IPAddress Destination, int Ttl, Dictionary<string, Object> Tags, bool ExpectedResult)
        {
            List<Workstation> allWorkstations = NetworkManager.Instance.GetAllWorkstations();
            List<Hardwarenode> destinationList = new List<Hardwarenode>();

            bool isBroadcast = false;
            // Iterate through all workstations and add the destinations to te destinationList
            for (int workstationIndex = 0; workstationIndex < allWorkstations.Count; workstationIndex++)
            {
                var addWorkstationAsDestination = false;
                List<Interface> ifaces = allWorkstations[workstationIndex].GetInterfaces();
                // Iterate through all interfaces of the current workstation.
                foreach (Interface iface in ifaces)
                {
                    if (!isBroadcast && Destination.Equals(iface.IpAddress))
                    {
                        // We have found a workstation whose ip matches our destination ip
                        addWorkstationAsDestination = true;
                        break;
                    }
                    else
                    {
                        if (!isBroadcast)
                        {
                            IPAddress ifaceBroadcastAddress = IPAddressExtensions.GetBroadcastAddress(iface.IpAddress,
                                iface.Subnetmask);
                            if (Destination.Equals(ifaceBroadcastAddress))
                            {
                                // Our destination is a broadcast ip
                                isBroadcast = true;
                            }
                        }
                        if (isBroadcast && IPAddressExtensions.IsInSameSubnet(Destination, iface.IpAddress, iface.Subnetmask))
                        {
                            // Current workstation is in the destination broadcast subnet.
                            addWorkstationAsDestination = true;
                            break;
                        }
                    }
                }

                if (addWorkstationAsDestination)
                {
                    // Add the current workstation to the destinationList.
                    destinationList.Add(allWorkstations[workstationIndex]);

                    if (!isBroadcast)
                    {
                        // Break because we already have found the single destination workstation.
                        break;
                    }
                }
            }

            if (destinationList.Count < 1)
            {
                Simulation sim = new Simulation(Simulations.Count);
                // Our destination list is empty. -> Create an error packet
                sim.AddPacketSend(createPacket(NetworkManager.Instance.GetWorkstationByIp(Source), null, Ttl, Tags, ExpectedResult));
                Result res = StartSimulation(sim);
                AddSimulationToHistory(sim);
                return res;
            }
            else
            {
                // Create Packets for all destinations.
                Simulation sim = new Simulation(Simulations.Count);
                foreach (Hardwarenode destinationNode in destinationList)
                {
                    sim.AddPacketSend(createPacket(NetworkManager.Instance.GetWorkstationByIp(Source),
                        destinationNode, Ttl, Tags, ExpectedResult));
                }
                Result res = StartSimulation(sim);
                AddSimulationToHistory(sim);
                return res;
            }
        }

        /// <summary>
        /// Creates the packet.
        /// </summary>
        /// <param name="Source">The source.</param>
        /// <param name="Destination">The destination.</param>
        /// <param name="Ttl">The TTL.</param>
        /// <param name="Tags">The tags.</param>
        /// <param name="ExpectedResult"></param>
        /// <returns>the packet</returns>
        /// <exception cref="ArgumentException">SimulationManager.createPacket: source or destination is null or ttl 0</exception>
        private Packet createPacket(Hardwarenode Source, Hardwarenode Destination, int Ttl, Dictionary<string, Object> Tags, bool ExpectedResult)
        {
            if(Ttl <= 0)
                throw new ArgumentException("SimulationManager.createPacket: ttl <= 0");
            return new Packet(Source, Destination, Ttl, Tags, ExpectedResult);
        }
    }
}