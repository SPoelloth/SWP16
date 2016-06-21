using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic.TestscenarioRunnables
{
    class HasInternetTestscenarioRunnable: ITestscenarioRunnable
    {
        private readonly Rule rule;
        private readonly Hardwarenode startNode;
        private readonly List<Hardwarenode> endNodes;
        public int SimulationCount { get; }

        public HasInternetTestscenarioRunnable(Rule Rule)
        {
            rule = Rule;
            startNode = Rule.StartNode;
            endNodes = Rule.EndNodes;
            SimulationCount = 0;
        }

        /// <summary>
        /// runs all simulations for a given rule
        /// </summary>
        /// <returns>simulations that failed</returns>
        public List<Simulation> Run()
        {
            var l = Guid.NewGuid().ToString();
            var sim = new Simulation(l);
            var passedSimulations = new List<Simulation>();

            foreach (var router in endNodes)
            {
                var p = new Packet(startNode, router, rule.Options["TTL"], rule.ExpectedResult);
                sim.AddPacketSend(p);
                if (sim.Execute().ErrorId == 0)
                {
                    passedSimulations.Add(sim);
                }
                // else break;
            }

            if (passedSimulations.Count == 0)
                passedSimulations.Add(new Simulation(l, startNode.Name, "NoInternetRouter", rule.ExpectedResult)); //potential BUG
            else
                passedSimulations.Clear();

            return passedSimulations;
        }
    }
}
