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
        public SimulationManager(List<Simulation> simulations)
        {
            Simulations = simulations;
        }

        public void HopSelected(int indexOfHop)
        {
            // todo
            // Jeremy: zu einem Hop gehören immer zwei Rechner, indexOfHop identifiziert den ersten Rechner in der Liste an Hops
            // klären: zweiter Rechner der davor oder der danach?
            // ebenfalls klären: Hop selektieren nur bei aktueller Simulation möglich (würde sagen JA)
        }

        public void StartSimulation(Simulation sim)
        {
            sim.Execute();
        }

        public void StartTestscenario(Testscenario t)
        {
            // todo Dennis & Oleg
        }

        public void AddSimulationToHistory(Simulation sim)
        {
            Simulations.Add(sim);
        }

        public void RunSimulationFromHistory(int index)
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
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="ttl">The TTL.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="expectedResult">the expected result of the simulation.</param>
        public void CreateSimulation(IPAddress source, IPAddress destination, int ttl, Dictionary<string, Object> tags, bool expectedResult)
        {
            List<Workstation> allWorkstations = NetworkManager.Instance.GetAllWorkstations();
            List<Hardwarenode> destinationList = new List<Hardwarenode>();

            bool isBroadcast = false;
            bool addWorkstationAsDestination = false;
            // Iterate through all workstations and add the destinations to te destinationList
            for (int workstationIndex = 0; workstationIndex < allWorkstations.Count; workstationIndex++)
            {
                addWorkstationAsDestination = false;
                List<Interface> ifaces = allWorkstations[workstationIndex].GetInterfaces();
                // Iterate through all interfaces of the current workstation.
                foreach (Interface iface in ifaces)
                {
                    if (!isBroadcast && destination.Equals(iface.IpAddress))
                    {
                        // We have found a workstation whose ip matches our destination ip
                        addWorkstationAsDestination = true;
                        break;
                    }
                    else
                    {
                        IPAddress ifaceBroadcastAddress = IPAddressExtensions.GetBroadcastAddress(iface.IpAddress,
                            iface.Subnetmask);
                        if (destination.Equals(ifaceBroadcastAddress))
                        {
                            // Our destination is a broadcast ip and the current workstation is in the same subnet.
                            isBroadcast = true;
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
                sim.AddPacketSend(createPacket(NetworkManager.Instance.GetWorkstationByIP(source), null, ttl, tags, expectedResult));
                StartSimulation(sim);
                AddSimulationToHistory(sim);
            }
            else
            {
                // Create Packets for all destinations.
                foreach (Hardwarenode destinationNode in destinationList)
                {
                    Simulation sim = new Simulation(Simulations.Count);
                    sim.AddPacketSend(createPacket(NetworkManager.Instance.GetWorkstationByIP(source),
                        destinationNode, ttl, tags, expectedResult));
                    StartSimulation(sim);
                    AddSimulationToHistory(sim);
                }

            }
        }

        /// <summary>
        /// Creates the packet.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="ttl">The TTL.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>the packet</returns>
        /// <exception cref="System.ArgumentException">SimulationManager.createPacket: source or destination is null or ttl <= 0</exception>
        private Packet createPacket(Hardwarenode source, Hardwarenode destination, int ttl, Dictionary<string, Object> tags, bool expectedResult)
        {
            if(ttl <= 0)
                throw new ArgumentException("SimulationManager.createPacket: ttl <= 0");
            return new Packet(source, destination, ttl, tags, expectedResult);
        }
    }
}