using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;
using NSA.Model.BusinessLogic.TestscenarioRunnables;

namespace NSA.Model.BusinessLogic
{
    public class Testscenario
    {
        private Dictionary<Rule, bool> results = new Dictionary<Rule, bool>();
        private Network network;
        private string text;

        public string fileName { get; private set; }
        public string Id { get; }
        public int SimulationCount { get; }
        
        public Testscenario(string t, Network n, string fileName)
        {
            network = n;
            text = t;
            this.fileName = fileName;
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
                {
                    Hardwarenode newNode = network.GetHardwarenodeByName(eNode);
                    if (newNode == null)
                    {
                        newNode = new Workstation(eNode);
                    }
                    endNodes.Add(newNode);
                }

                switch (rule.SimulType)
                {
                    case SimulationType.Simple: runnables.Add(new SimpleTestscenarioRunnable(rule, startNode, endNodes)); break;
                    case SimulationType.HasInternet: runnables.Add(new HasInternetTestscenarioRunnable(rule, startNode, network)); break;
                    default: break;
                }
            }

            this.SimulationCount = 0;
            foreach (var runnable in runnables) { this.SimulationCount += runnable.SimulationCount; }

            return runnables;
        }
    }
}
