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
        /// Liefert das Ergebnis der beiden Hardwarenodes des Hops zurück.
        /// </summary>
        /// <param name="IsSendPacket">Boolescher Wert, der angibt, ob es sich um ein Hinpacket handelt</param>
        /// <param name="PacketIndex">Der Index des Packets</param>
        /// <param name="NodeOneName">Der Name des ersten Hardwarenodes des Hops</param>
        /// <param name="NodeTwoName">Der Name des zweiten Hardwarenodes des Hops</param>
        /// <returns>Ein Tuple, welches die zwei Ergebnisse enthält. Item1 gehört zu NodeOneName, Item2 zu NodeTwoName</returns>
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
        /// Liefert das Ergbenis der Simulation, in Abhängigkeit des erwarteten Ergebnisses zurück
        /// </summary>
        /// <param name="Id">Die ID der Simulation</param>
        /// <returns>
        /// Erfolgreich (true) oder nicht erfolgreich (false)
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
        /// Fügt die Simulation der History hinzu und benachrichtigt die View.
        /// </summary>
        /// <param name="Sim">Die hinzuzufügende Simulation</param>
        public void AddSimulationToHistory(Simulation Sim)
        {
            Simulations.Add(Sim);
            InfoController.Instance.AddSimulationToHistoryTab(Sim);
        }

        /// <summary>
        /// Startet eine neue Simulation mit den gleichen Werten wie die angegebene Simulation aus der History
        /// </summary>
        /// <param name="Id">Die ID der alten Simulation</param>
        /// <returns>Das Ergebnis der Simulation</returns>
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
        /// Erstellt eine Simulation und startet sie.
        /// </summary>
        /// <param name="Source">Der Name der Quell-Workstation</param>
        /// <param name="Destination">Der Name der Ziel-Workstation</param>
        /// <param name="Ttl">Die Time-To-Life</param>
        /// <param name="ExpectedResult">Das erwartete Ergebnis der Simulation</param>
        /// <param name="Broadcast">Gibt an, ob es sich um einen Broadcast handelt</param>
        /// <param name="Subnet">Die Subnetzmaske des Broadcasts</param>
        /// <returns>Das Ergebnis der Simulation</returns>
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
        /// Erstellt und führt einen Broadcast in das angegebene Netz aus.
        /// </summary>
        /// <param name="Source">Die Quell-Workstation</param>
        /// <param name="Subnet">Die Subnetzaske des Subnetzes</param>
        /// <param name="ExpectedResult">Das erwartete Ergebnis</param>
        public void CreateAndExecuteBroadcast(string Source, string Subnet, bool ExpectedResult)
        {
            CreateAndExecuteSimulation(Source, null, 255, ExpectedResult, true, Subnet);
        }

        /// <summary>
        /// Leert die Simulationshistory
        /// </summary>
        public void ClearHistory()
        {
            Simulations.Clear();
        }

        /// <summary>
        /// Löscht eine Simulation aus der History
        /// </summary>
        /// <param name="Id">Die ID der Simulation</param>
        public void DeleteSimulationFromHistory(string Id)
        {
           // every simulation has a unique ID
            Simulations.RemoveAll(S => S.Id.Equals(Id));
        }
    }
}