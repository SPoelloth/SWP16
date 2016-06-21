using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic.TestscenarioRunnables
{
    class HasInternetTestscenarioRunnable: ITestscenarioRunnable
    {
        private Rule rule;
        private Hardwarenode startNode;
        private List<Hardwarenode> endNodes
        public int SimulationCount { get; }

        public HasInternetTestscenarioRunnable(Rule rule)
        {
            this.rule = rule;
            this.startNode = rule.StartNode;
            this.endNodes = rule.EndNodes;
            this.SimulationCount = 0;
        }

        public List<Simulation> Run()
        {
            string l = Guid.NewGuid().ToString();
            Simulation sim = new Simulation(l);
            List<Simulation> passedSimulations = new List<Simulation>();

            foreach (var router in endNodes)
            {
                Packet p = new Packet(startNode, router, rule.Options["TTL"], rule.ExpectedResult);
                sim.AddPacketSend(p);
                if (sim.Execute().ErrorId == 0)
                {
                    passedSimulations.Add(sim);
                }
                // else break;
            }

            if (passedSimulations.Count == 0)
                passedSimulations.Add(new Simulation(l, startNode.Name, "NoInternetRouter")); //potential BUG
            else
                passedSimulations.Clear();

            return passedSimulations;
        }
    }
}
