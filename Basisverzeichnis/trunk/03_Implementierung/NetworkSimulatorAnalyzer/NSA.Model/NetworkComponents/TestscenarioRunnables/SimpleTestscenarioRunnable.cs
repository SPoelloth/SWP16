using System.Collections.Generic;
using NSA.Model.BusinessLogic.TestscenarioRunnables;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic
{
    internal class SimpleTestscenarioRunnable : ITestscenarioRunnable
    {
        private List<Hardwarenode> endNodes;
        private Rule rule;
        private Hardwarenode startNode;

        public SimpleTestscenarioRunnable(Rule rule, Hardwarenode startNode, List<Hardwarenode> endNodes)
        {
            this.rule = rule;
            this.startNode = startNode;
            this.endNodes = endNodes;
        }
  
        public Result Run()
        {
            //TODO: add endNodes for subnets

            foreach (var endNode in endNodes)
            {
                Simulation sim = new Simulation(Testscenario.SimulationId++);
                Packet p = new Packet(startNode, endNode, rule.Options["TTL"], rule.ExpectedResult);
                sim.AddPacketSend(p);

                Result r = sim.Execute();
                if (rule.ExpectedResult) if (r.ErrorId != 0) return r;
                else                     if (r.ErrorId == 0) return r;
            }

            return new Result();
        }
    }
}