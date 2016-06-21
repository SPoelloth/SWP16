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
        /// Gets the hop result.
        /// </summary>
        /// <param name="IsSendPacket">if set to <c>true</c> [is send packet].</param>
        /// <param name="PacketIndex">Index of the packet.</param>
        /// <param name="NodeOneName">Name of the node one.</param>
        /// <param name="NodeTwoName">Name of the node two.</param>
        /// <returns></returns>
        public List<Result> GetHopResult(bool IsSendPacket, int PacketIndex, string NodeOneName, string NodeTwoName)
        {
            List<Result> res = new List<Result>();
            Packet p = null;
            p = IsSendPacket ? Simulations[Simulations.Count - 1].PacketsSend?[PacketIndex] : Simulations[Simulations.Count - 1].PacketsReceived?[PacketIndex];
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
                res.Add(new Result());
                res.Add(p.Result);
            }
            else if (hops.Last().Equals(nodeOne))
            {
                res.Add(p.Result);
                res.Add(new Result());
            }
            else
            {
                res.Add(new Result());
                res.Add(new Result());
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

            foreach (var runnable in T.GetRunnables())
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
            InfoController.Instance.AddNewSimulationToHistory(Sim);
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
        /// <param name="Index">The index of the simulation.</param>
        public void DeleteSimulationFromHistory(int Index)
        {
            Simulations.RemoveAt(Index);
        }
    }
}