using System;
using NSA.Model.BusinessLogic;
using System.Collections.Generic;
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
            if (Simulations.Count == 0)
                return null;
            if (IsSendPacket)
                p = Simulations[Simulations.Count - 1].GetSendPackets()?[PacketIndex];
            else
                p = Simulations[Simulations.Count - 1].GetReceivedPackets()?[PacketIndex];
            if (p == null)
                return null;
            Hardwarenode nodeOne = NetworkManager.Instance.GetHardwarenodeByName(NodeOneName);
            Hardwarenode nodeTwo = NetworkManager.Instance.GetHardwarenodeByName(NodeTwoName);
            if (nodeOne == null || nodeTwo == null)
                return null;
            List<Hardwarenode> hops = p.GetHops();
            if (hops.Count < 2)
                return null;
            if (hops[hops.Count].Equals(nodeTwo))
            {
                res.Add(new Result());
                res.Add(p.Result);
            }
            else if (hops[hops.Count].Equals(nodeTwo))
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
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// True if it worked, false if not
        /// </returns>
        public bool GetSimulationResult(string Id)
        {
            Simulation sim = null;
            foreach (Simulation s in Simulations)
            {
                if (s.Id == Id)
                    sim = s;
            }
            if (sim == null)
                return false;
            bool result = false;
            foreach (Packet p in sim.GetAllPackets())
            {
                if ((p.ExpectedResult && p.Result.ErrorId == 0) || (!p.ExpectedResult && p.Result.ErrorId != 0))
                    result = true;
                else
                    return false;
            }
            return result;
        }

        public void StartTestscenario(Testscenario T)
        {
            foreach (var runnable in T.GetRunnables())
            {
                Result r = runnable.Run();
                //todo FIXME: result is unused
            }
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
                if (s.Id == Id)
                {
                    sim = new Simulation(Guid.NewGuid().ToString("N"), s.Source, s.Destination);
                    oldSim = s;
                }
            }
            if (sim == null) return null;
            foreach (Packet p in oldSim.GetSendPackets())
            {
                sim.AddPacketSend(createPacket(p.Source, p.Destination, p.Ttl, p.ExpectedResult));
            }
            Result res = sim.Execute();
            AddSimulationToHistory(sim);
            return res;
        }

        /// <summary>
        /// Runs the last simulation.
        /// </summary>
        /// <returns></returns>
        public Result RunLastSimulation()
        {
            return RunSimulationFromHistory(Simulations[Simulations.Count - 1].Id);
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
        public Result CreateAndExecuteSimulation(string Source, string Destination, int Ttl, bool ExpectedResult, bool Broadcast)
        {
            Simulation sim;
            Hardwarenode start = NetworkManager.Instance.GetHardwarenodeByName(Source);
            if (Broadcast)
            {
                sim =  new Simulation(Guid.NewGuid().ToString("N"), Source, "Broadcast");
                List<Workstation> allWorkstations = NetworkManager.Instance.GetAllWorkstations();
                foreach (Workstation w in allWorkstations)
                {
                    if (w.Name != Source)
                    {
                        sim.AddPacketSend(createPacket(start, w, Ttl, ExpectedResult));
                    }
                }
            }
            else
            {
                sim = new Simulation(Guid.NewGuid().ToString("N"), Source, Destination);
                Hardwarenode end = NetworkManager.Instance.GetHardwarenodeByName(Destination);
                sim.AddPacketSend(createPacket(start, end, Ttl, ExpectedResult));
            }
            Result res = StartSimulation(sim);
            AddSimulationToHistory(sim);
            return res;
        }

        /// <summary>
        /// Creates the packet.
        /// </summary>
        /// <param name="Source">The source.</param>
        /// <param name="Destination">The destination.</param>
        /// <param name="Ttl">The TTL.</param>
        /// <param name="ExpectedResult">if set to <c>true</c> [expected result].</param>
        /// <returns>
        /// the packet
        /// </returns>
        /// <exception cref="System.ArgumentException">SimulationManager.createPacket: ttl kleiner-gleich 0</exception>
        /// <exception cref="ArgumentException">SimulationManager.createPacket: source or destination is null or ttl 0</exception>
        private Packet createPacket(Hardwarenode Source, Hardwarenode Destination, int Ttl, bool ExpectedResult)
        {
            if (Ttl <= 0)
                throw new ArgumentException("SimulationManager.createPacket: ttl <= 0");
            return new Packet(Source, Destination, Ttl, ExpectedResult);
        }

        /// <summary>
        /// Creates and Executes a quick simulation
        /// </summary>
        /// <param name="Source">The source.</param>
        /// <param name="Target">The target.</param>
        public void QuickSimulation(string Source, string Target)
        {
            CreateAndExecuteSimulation(Source, Target, 255, true, false);
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