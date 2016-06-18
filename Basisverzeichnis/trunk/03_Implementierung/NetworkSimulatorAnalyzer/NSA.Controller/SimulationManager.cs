using System;
using NSA.Model.BusinessLogic;
using System.Collections.Generic;
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
            if (hops == null)
                return null;
            List<Result> res = new List<Result>();
            if (hops[hops.Count - 1].Equals(nodeOne))
            {
                Packet p = Simulations[Simulations.Count - 1].GetLastPacket();
                if (p == null)
                    return null;
                res.Add(p.Result);
                res.Add(new Result());
            }
            else if (hops[hops.Count - 1].Equals(nodeTwo))
            {
                res.Add(new Result());
                Packet p = Simulations[Simulations.Count - 1].GetLastPacket();
                if (p == null)
                    return null;
                res.Add(p.Result);
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
        /// <param name="Index">The index of the simulation in the history.</param>
        /// <returns>Null, if there is no packet. The Hop-List kann have count == 0</returns>
        public List<Hardwarenode> GetHopsOfLastPacket(int Index)
        {
            if (Index >= Simulations.Count)
                return null;
            Packet p = Simulations[Index].GetLastPacket();
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
        /// <param name="Index">The index.</param>
        /// <returns>True if it worked, false if not</returns>
        public bool GetSimulationResult(int Index)
        {
            Simulation sim = Simulations[Index];
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
            // todo Dennis & Oleg
        }

        public void AddSimulationToHistory(Simulation Sim)
        {
            Simulations.Add(Sim);
            InfoController.Instance.AddNewSimulationToHistory(Sim);
        }

        /// <summary>
        /// Runs the simulation from history.
        /// </summary>
        /// <param name="Index">The index of the simulation.</param>
        /// <returns></returns>
        public Result RunSimulationFromHistory(int Index)
        {
            Simulation sim = new Simulation(Simulations.Count, Simulations[Index].Source, Simulations[Index].Destination);
            foreach (Packet p in Simulations[Index].GetSendPackets())
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
            return RunSimulationFromHistory(Simulations.Count - 1);
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
                sim =  new Simulation(Simulations.Count, Source, "Broadcast");
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
                sim = new Simulation(Simulations.Count, Source, Destination);
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
    }
}