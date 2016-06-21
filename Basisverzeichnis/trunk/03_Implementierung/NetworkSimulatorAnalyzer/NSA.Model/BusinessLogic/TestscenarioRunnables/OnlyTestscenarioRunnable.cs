using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic.TestscenarioRunnables
{
    internal class OnlyTestscenarioRunnable : ITestscenarioRunnable
    {
        private readonly Network network;
        private readonly List<Hardwarenode> endNodes;
        private readonly Rule rule;
        private readonly Hardwarenode startNode;
        
        public int SimulationCount { get; }

        public OnlyTestscenarioRunnable(Rule Rule, Network N)
        {
            rule = Rule;
            startNode = Rule.StartNode;
            endNodes = Rule.EndNodes;
            SimulationCount = N.GetAllWorkstations().Count; 
            network = N;
        }

        /// <summary>
        /// runs all simulations for a given rule
        /// </summary>
        /// <returns>simulations that failed</returns>
        public List<Simulation> Run()
        {
            var failedSimulations = new List<Simulation>();

            foreach (var node in network.GetAllWorkstations())
            {
                if (startNode == node) continue;

                var expectedResult = rule.ExpectedResult;
                if (!endNodes.Contains(node)) expectedResult = !expectedResult;

                var sim = new Simulation(Guid.NewGuid().ToString(), startNode.Name, node.Name, expectedResult);

                var p = new Packet(startNode, node, rule.Options["TTL"], expectedResult);
                sim.AddPacketSend(p);

                var r = sim.Execute();
                if ((expectedResult && r.ErrorId != 0) || (!expectedResult && r.ErrorId == 0))
                    failedSimulations.Add(sim);
            }

            return failedSimulations;
        }
    }
}