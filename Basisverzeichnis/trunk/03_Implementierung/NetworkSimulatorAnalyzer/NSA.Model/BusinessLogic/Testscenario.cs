using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;
using NSA.Model.BusinessLogic.TestscenarioRunnables;

namespace NSA.Model.BusinessLogic
{
    public class Testscenario
    {
        public string Id { get; }
        private Dictionary<Rule, bool> results = new Dictionary<Rule, bool>();
        public static int SimulationId = 0;
        private Network network;
        private string text;

        public Testscenario()
        {
        }
        // todo: an id is needed
        // Vorschlag: Id = Guid.NewGuid().ToString("N"); (Tamara)
        // in the ProjectManager is a method to load the testscenario by id so the testscenario has to have an id
        public Testscenario(string t, Network n)
        {
            network = n;
            text = t;
        }

        public List<ITestscenarioRunnable> GetRunnables()
        {
            List<ITestscenarioRunnable> runnables = new List<ITestscenarioRunnable>();
            string[] lines = text.Split('\n');
            foreach (var line in lines)
            {
                Rule rule = Rule.Parse(line);
                Hardwarenode startNode = network.GetHardwarenodeByName(rule.StartNode);
                List<Hardwarenode> endNodes = new List<Hardwarenode>();

                foreach (var eNode in rule.EndNodes)
                { endNodes.Add(network.GetHardwarenodeByName(eNode)); }

                switch (rule.SimulType)
                {
                    case SimulationType.Simple: runnables.Add(new SimpleTestscenarioRunnable(rule, startNode, endNodes)); break;
                    case SimulationType.HasInternet: runnables.Add(new HasInternetTestscenarioRunnable(rule, startNode, network)); break;
                    default: break;
                }
            }

            return runnables;
        }
    }
}
