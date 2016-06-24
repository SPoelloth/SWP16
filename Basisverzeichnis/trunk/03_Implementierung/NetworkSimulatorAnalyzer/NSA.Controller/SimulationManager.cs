using System;
using NSA.Model.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
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
        /// Returns the result for both nodes of one hop
        /// </summary>
        /// <param name="IsSendPacket">bool, which indicates, if its a sendpacket</param>
        /// <param name="PacketIndex">The index of the packet</param>
        /// <param name="NodeOneName">Name of the first node of the hop</param>
        /// <param name="NodeTwoName">Name of the second node of the Hop</param>
        /// <returns>A Tuple, which contains the two results. Item1 is for NodeOneName, Item2 for NodeTwoName</returns>
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
        /// Returns the result of the simulation
        /// </summary>
        /// <param name="Id">The ID of the simulation</param>
        /// <returns>
        /// The result
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

        /// <summary>
        /// Adds a simulation to the history
        /// </summary>
        /// <param name="Sim">The simulation</param>
        public void AddSimulationToHistory(Simulation Sim)
        {
            Simulations.Add(Sim);
            InfoController.Instance.AddSimulationToHistoryTab(Sim);
        }

        /// <summary>
        /// Starts a new simulation with the parameters of one in the history
        /// </summary>
        /// <param name="Id">The ID of the old simulation</param>
        /// <returns>The result of the simulation</returns>
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
        /// Creates and executes a simulation
        /// </summary>
        /// <param name="Source">The name of the sourcenode</param>
        /// <param name="Destination">The name of the destinationnode</param>
        /// <param name="Ttl">Time-To-Life</param>
        /// <param name="ExpectedResult">The expected result</param>
        /// <param name="Broadcast">Indicates if it is a broadcast</param>
        /// <param name="Subnet">The subnetmask of the broadcast</param>
        /// <returns>The result of this simulation</returns>
        public Result CreateAndExecuteSimulation(string Source, string Destination, int Ttl = 255, bool ExpectedResult = true, bool Broadcast = false, string Subnet = "")
        {
            Simulation sim;
            Hardwarenode start = NetworkManager.Instance.GetHardwarenodeByName(Source);
            if (Broadcast)
            {
                sim =  new Simulation(Guid.NewGuid().ToString("N"), Source, "Broadcast", ExpectedResult);
                List<Workstation> allWorkstations = NetworkManager.Instance.GetHardwareNodesForSubnet(Subnet);
                foreach (Workstation w in allWorkstations)
                {
                    if (w.Name.Equals(Source)) continue;
                    sim.AddPacketSend(new Packet(start, w, Ttl, ExpectedResult));
                }
            }
            else
            {
                sim = new Simulation(Guid.NewGuid().ToString("N"), Source, Destination, ExpectedResult);
                Hardwarenode end = NetworkManager.Instance.GetHardwarenodeByName(Destination);
                sim.AddPacketSend(new Packet(start, end, Ttl, ExpectedResult));
            }
            var res = sim.GetAllPackets().Any() ? sim.Execute() : new Result(Result.Errors.NoPackets, Result.ResultStrings[(int)Result.Errors.NoPackets], null);
            AddSimulationToHistory(sim);
            return res;
        }

        /// <summary>
        /// Creates and executes a broadcast.
        /// </summary>
        /// <param name="Source">The name of the sourcenode</param>
        /// <param name="Subnet">The subnetmask of the broadcast</param>
        /// <param name="ExpectedResult">Expected Result</param>
        public void CreateAndExecuteBroadcast(string Source, string Subnet, bool ExpectedResult)
        {
            CreateAndExecuteSimulation(Source, null, 255, ExpectedResult, true, Subnet);
        }

        /// <summary>
        /// Clears the simulationhistory.
        /// </summary>
        public void ClearHistory()
        {
            Simulations.Clear();
        }

        /// <summary>
        /// Deletes a simulation from the history.
        /// </summary>
        /// <param name="Id">The ID of the simulation</param>
        public void DeleteSimulationFromHistory(string Id)
        {
           // every simulation has a unique ID
            Simulations.RemoveAll(S => S.Id.Equals(Id));
        }

        /// <summary>
        /// Highlights the connections packet.
        /// </summary>
        /// <param name="IsSendPacket">Indicates, if it's a sendpacket</param>
        /// <param name="PacketIndex">Index of the packet.</param>
        public void HighlightPacketConnections(bool IsSendPacket, int PacketIndex)
        {
            List<string> connectionNames = new List<string>();
            Simulation sim = Simulations.LastOrDefault();
            if (sim == null) return;
            Packet p;
            if (IsSendPacket)
            {
                if (PacketIndex >= sim.PacketsSend.Count) return;
                p = sim.PacketsSend[PacketIndex];
            }
            else
            {
                if (PacketIndex >= sim.PacketsReceived.Count) return;
                p = sim.PacketsReceived[PacketIndex];
            }
            for (int i = 1; i < p.Hops.Count; i++)
            {
                Connection c = NetworkManager.Instance.GetConnectionByNodes(p.Hops[i - 1], p.Hops[i]);
                if (c != null)
                    connectionNames.Add(c.Name);
            }
            NetworkViewController.Instance.HighlightConnections(connectionNames);
        }

        /// <summary>
        /// Unhighlights the connections.
        /// </summary>
        public void UnhighlightConnections()
        {
            NetworkViewController.Instance.HighlightConnections(null);
        }
    }
}