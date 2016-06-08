using NSA.Model.BusinessLogic;
using System.Collections.Generic;

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

        public void HopSelected()
        {
            // todo
        }

        public void StartSimulation(Simulation sim)
        {
            sim.Execute();
        }

        public void StartTestscenario()
        {
            // todo
        }

        public void AddSimulationToHistory(Simulation sim)
        {
            // todo
        }

        public void RunSimulationFromHistory()
        {
            // todo
        }

        public void RunLastSimulation()
        {
            // todo
        }
    }
}