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

        public void HopSelected(int IndexOfHop)
        {
            // todo
            // Jeremy: zu einem Hop gehören immer zwei Rechner, indexOfHop identifiziert den ersten Rechner in der Liste an Hops
            // klären: zweiter Rechner der davor oder der danach?
            // ebenfalls klären: Hop selektieren nur bei aktueller Simulation möglich (würde sagen JA)
        }

        public void StartSimulation(Simulation Sim)
        {
            Sim.Execute();
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
        public void CreateSimulation(IPAddress Source, IPAddress Destination, int Ttl, Dictionary<string, Object> Tags, bool ExpectedResult)
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
                StartSimulation(sim);
                AddSimulationToHistory(sim);
            }
            else
            {
                // Create Packets for all destinations.
                foreach (Hardwarenode destinationNode in destinationList)
                {
                    Simulation sim = new Simulation(Simulations.Count);
                    sim.AddPacketSend(createPacket(NetworkManager.Instance.GetWorkstationByIp(Source),
                        destinationNode, Ttl, Tags, ExpectedResult));
                    StartSimulation(sim);
                    AddSimulationToHistory(sim);
                }

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