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

        }

        // Constructor:
        public SimulationManager(List<Simulation> simulations)
        {
            Simulations = simulations;
        }

        public void OnHopSelected()
        {

        }

        public void StartSimulation(Simulation sim)
        {

        }

        public void StartTestscenario()
        {

        }

        public void AddSimulationToHistory(Simulation sim)
        {

        }

        public void RunSimulationFromHistory()
        {

        }

        public void RunLastSimulation()
        {

        }
    }

    class SimulationManagerImpl : SimulationManager
    {
    }
}