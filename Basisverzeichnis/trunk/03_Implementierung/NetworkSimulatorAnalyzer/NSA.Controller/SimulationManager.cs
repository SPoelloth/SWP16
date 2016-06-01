using NSA.Model.BusinessLogic;
using System.Collections.Generic;

namespace NSA.Controller
{
    internal class SimulationManager
    {
        public List<Simulation> Simulations { get; }

        // Default constructor:
        public SimulationManager()
        {
            // todo
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
            // todo
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