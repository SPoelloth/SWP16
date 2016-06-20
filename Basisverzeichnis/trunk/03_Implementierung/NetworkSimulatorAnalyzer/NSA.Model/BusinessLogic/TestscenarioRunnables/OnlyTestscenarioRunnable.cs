using System.Collections.Generic;
using NSA.Model.BusinessLogic.TestscenarioRunnables;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;
using System;

namespace NSA.Model.BusinessLogic
{
    internal class OnlyTestscenarioRunnable : ITestscenarioRunnable
    {
        private Network network;
        private List<Hardwarenode> restNodes;
        private Rule rule;
        private Hardwarenode startNode;
        
        public int SimulationCount { get; }

        public OnlyTestscenarioRunnable(Rule rule, Hardwarenode startNode, List<Hardwarenode> endNodes, Network n)
        {
            this.rule = rule;
            this.startNode = startNode;
            this.endNodes = endNodes;
            this.SimulationCount = network.GetAllWorkstations().Count; //TODO: change later
            this.network = n
        }
  
        public List<Simulation> Run()
        {
            List<Simulation> failedSimulations = new List<Simulation>();
            //TODO: add endNodes for subnets

            foreach (var node in network.GetAllWorkstations())
            {
                Simulation sim = new Simulation(Guid.NewGuid().ToString());

                bool expectedResult = rule.ExpectedResult
                if (!endNodes.Contains(node)) expectedResult = !expectedResult; 

                Packet p = new Packet(startNode, node, rule.Options["TTL"], expectedResult);
                sim.AddPacketSend(p);

                Result r = sim.Execute();
                if ((rule.ExpectedResult && r.ErrorId != 0) || (!rule.ExpectedResult && r.ErrorId == 0))
                    failedSimulations.Add(sim);
            }

            return failedSimulations;
        }
    }
}