using System;
using NSA.Model.BusinessLogic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using NSA.Controller.ViewControllers;
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

        /// <summary>
        /// Gets the hop result.
        /// </summary>
        /// <param name="IsSendPacket">if set to <c>true</c> [is send packet].</param>
        /// <param name="PacketIndex">Index of the packet.</param>
        /// <param name="NodeOneName">Name of the node one.</param>
        /// <param name="NodeTwoName">Name of the node two.</param>
        /// <returns></returns>
        public Tuple<Result, Result> GetHopResult(bool IsSendPacket, int PacketIndex, string NodeOneName, string NodeTwoName)
        {
            Tuple<Result, Result> res;
            Packet p = IsSendPacket ? Simulations[Simulations.Count - 1].PacketsSend?[PacketIndex] : Simulations[Simulations.Count - 1].PacketsReceived?[PacketIndex];
            if (p == null || Simulations.Count == 0)
                return null;
            Hardwarenode nodeOne = NetworkManager.Instance.GetHardwarenodeByName(NodeOneName);
            Hardwarenode nodeTwo = NetworkManager.Instance.GetHardwarenodeByName(NodeTwoName);
            if (nodeOne == null || nodeTwo == null)
                return null;
            List<Hardwarenode> hops = p.Hops;
            if (hops.Count < 2)
                return null;
            if (hops.Last().Equals(nodeTwo))
            {
                res = new Tuple<Result, Result>(new Result(), p.Result);
            }
            else if (hops.Last().Equals(nodeOne))
            {
                res = new Tuple<Result, Result>(p.Result, new Result());
            }
            else
            {
                res = new Tuple<Result, Result>(new Result(), new Result());
            }
            return res;
        }

        /// <summary>
        /// Gets the simulation result.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// True if it worked, false if not
        /// </returns>
        public bool GetSimulationResult(string Id)
        {
            Simulation sim = Simulations.First(S => S.Id == Id);
            if (sim == null)
                return false;
            foreach (Packet p in sim.GetAllPackets())
            {
                if ((!p.ExpectedResult || p.Result.ErrorId != 0) && (p.ExpectedResult || p.Result.ErrorId == 0))
                    return false;
            }
            return true;
        }

        public List<Simulation> StartTestscenario(Testscenario T)
        {
            List<Simulation> failedSimulations = new List<Simulation>();

            foreach (var runnable in T.GetTestscenarioRunnables())
            {
                foreach (var simulation in runnable.Run())
                {
                    failedSimulations.Add(simulation);  
                }
            }

            return failedSimulations;
        }

        public void AddSimulationToHistory(Simulation Sim)
        {
            Simulations.Add(Sim);
            InfoController.Instance.AddSimulationToHistoryTab(Sim);
        }

        /// <summary>
        /// Runs the simulation from history.
        /// </summary>
        /// <param name="Id">The id of the simulation.</param>
        /// <returns></returns>
        public Result RunSimulationFromHistory(string Id)
        {
            Simulation sim = null, oldSim = null;
            foreach (Simulation s in Simulations)
            {
                if (s.Id != Id) continue;
                sim = new Simulation(Guid.NewGuid().ToString("N"), s.Source, s.Destination, s.ExpectedResult);
                oldSim = s;
            }
            if (sim == null) return null;
            foreach (Packet p in oldSim.PacketsSend)
            {
                sim.AddPacketSend(new Packet(p.Source, p.Destination, p.Ttl, p.ExpectedResult));
            }
            Result res = sim.Execute();
            AddSimulationToHistory(sim);
            return res;
        }

        /// <summary>
        /// Creates the simulation and starts it.
        /// </summary>
        /// <param name="Source">The source.</param>
        /// <param name="Destination">The destination.</param>
        /// <param name="Ttl">The TTL.</param>
        /// <param name="ExpectedResult">the expected result of the simulation.</param>
        /// <param name="Broadcast">if set to <c>true</c>: broadcast.</param>
        /// <returns></returns>
        public Result CreateAndExecuteSimulation(string Source, string Destination, int Ttl = 255, bool ExpectedResult = true, bool Broadcast = false)
        {
            Simulation sim;
            Hardwarenode start = NetworkManager.Instance.GetHardwarenodeByName(Source);
            if (Broadcast)
            {
                sim =  new Simulation(Guid.NewGuid().ToString("N"), Source, "Broadcast", ExpectedResult);
                List<Workstation> allWorkstations = NetworkManager.Instance.GetAllWorkstations();
                foreach (Workstation w in allWorkstations)
                {
                    if (w.Name == Source) continue;
                    sim.AddPacketSend(new Packet(start, w, Ttl, ExpectedResult));
                }
            }
            else
            {
                sim = new Simulation(Guid.NewGuid().ToString("N"), Source, Destination, ExpectedResult);
                Hardwarenode end = NetworkManager.Instance.GetHardwarenodeByName(Destination);
                sim.AddPacketSend(new Packet(start, end, Ttl, ExpectedResult));
            }
            Result res = sim.Execute();
            AddSimulationToHistory(sim);
            return res;
        }

        /// <summary>
        /// Clears the history.
        /// </summary>
        public void ClearHistory()
        {
            Simulations.Clear();
        }

        /// <summary>
        /// Deletes the simulation from history.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        public void DeleteSimulationFromHistory(string Id)
        {
           // every simulation has a unique ID
            Simulations.RemoveAll(S => S.Id.Equals(Id));
        }


        /// <summary>
        /// Gets all hardwarenodes belonging to a subnet.
        /// </summary>
        /// <param name="Subnetmask">The subnetmask.</param>
        /// <returns>A list of hardwarenodes who belong to the subnet.</returns>
        public List<Hardwarenode> GetHardwareNodesForSubnet(string Subnetmask)
        {
            List<Hardwarenode> nodes = new List<Hardwarenode>();

            IPAddress subnetAddress;
            bool ok = IPAddress.TryParse(Subnetmask, out subnetAddress);
            Debug.Assert(ok, "Invalid Subnetmask");

            List<Workstation> allWorkstations = NetworkManager.Instance.GetAllWorkstations();
            // Iterate through all workstations
            foreach (Workstation w in allWorkstations)
            {
                List<Interface> ifaces = w.GetInterfaces();
                // Iterate through all interfaces of the current workstation.
                foreach (Interface iface in ifaces)
                {
                    if (subnetAddress.Equals(iface.Subnetmask))
                    {
                        // Workstation is in the same subnet.
                        nodes.Add(w);
                        break;
                    }
                }

            }

            return nodes;
        }
    }
}