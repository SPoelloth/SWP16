using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic.TestscenarioRunnables
{
    internal class SimpleTestscenarioRunnable : ITestscenarioRunnable
    {
        private readonly List<Hardwarenode> endNodes;
        private readonly Rule rule;
        private readonly Hardwarenode startNode;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="NSA.Model.BusinessLogic.TestscenarioRunnables.SimpleTestscenarioRunnable"/> class.
		/// </summary>
		/// <param name="Rule">Rule object</param>
        public SimpleTestscenarioRunnable(Rule Rule)
        {
            rule = Rule;
            startNode = Rule.StartNode;
            endNodes = Rule.EndNodes;
        }
        
        /// <summary>
        /// runs all simulations for a given rule
        /// </summary>
        /// <returns>simulations that failed</returns>
        public List<Simulation> Run()
        {
            var failedSimulations = new List<Simulation>();

            foreach (var endNode in endNodes)
            {
                var sim = new Simulation(Guid.NewGuid().ToString(), startNode.Name, endNode.Name, rule.ExpectedResult);
                var p = new Packet(startNode, endNode, rule.Options["TTL"], rule.ExpectedResult);
                sim.AddPacketSend(p);

                var r = sim.Execute();
                if ((rule.ExpectedResult && r.ErrorId != 0) || (!rule.ExpectedResult && r.ErrorId == 0))
                    failedSimulations.Add(sim);
            }

            return failedSimulations;
        }
    }
}