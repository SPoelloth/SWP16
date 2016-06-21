using System.Collections.Generic;
using NSA.Model.BusinessLogic.TestscenarioRunnables;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;
using System;

namespace NSA.Model.BusinessLogic
{
    internal class SimpleTestscenarioRunnable : ITestscenarioRunnable
    {
        private List<Hardwarenode> endNodes;
        private Rule rule;
        private Hardwarenode startNode;
        
        public int SimulationCount { get; }

        public SimpleTestscenarioRunnable(Rule rule)
        {
            this.rule = rule;
            this.startNode = rule.StartNode;
            this.endNodes = rule.EndNodes;
            this.SimulationCount = this.endNodes.Count; //TODO: change later
        }
  
        public List<Simulation> Run()
        {
            List<Simulation> failedSimulations = new List<Simulation>();
            //TODO: add endNodes for subnets

            foreach (var endNode in endNodes)
            {
                Simulation sim = new Simulation(Guid.NewGuid().ToString());
                Packet p = new Packet(startNode, endNode, rule.Options["TTL"], rule.ExpectedResult);
                sim.AddPacketSend(p);

                Result r = sim.Execute();
                if ((rule.ExpectedResult && r.ErrorId != 0) || (!rule.ExpectedResult && r.ErrorId == 0))
                    failedSimulations.Add(sim);
            }

            return failedSimulations;
        }
    }
}