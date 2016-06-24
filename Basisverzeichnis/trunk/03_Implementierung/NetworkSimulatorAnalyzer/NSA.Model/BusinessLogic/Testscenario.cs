using System.Collections.Generic;
using NSA.Model.NetworkComponents;
using NSA.Model.BusinessLogic.TestscenarioRunnables;

namespace NSA.Model.BusinessLogic
{
    public class Testscenario
    {
        private readonly Network network;
        private readonly string text;

        public string FileName { get; private set; }
        public string Id { get; }
        public int SimulationCount { get; private set; } // wird jetzt nicht mehr angezeigt, kann raus, wenn sonst nicht mehr gebraucht.
        
        public Testscenario(string T, Network N, string FileName)
        {
            network = N;
            text = T;
            this.FileName = FileName;
        }

        /// <summary>
        /// parses the text and creates Testscenario runnables
        /// </summary>
        /// <returns>runnables</returns>
        public List<ITestscenarioRunnable> GetTestscenarioRunnables()
        {
            var runnables = new List<ITestscenarioRunnable>();
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                var rule = Rule.Parse(line, network);

                switch (rule.SimulType)
                {
                    case SimulationType.Simple:      runnables.Add(new SimpleTestscenarioRunnable(rule));        break;
                    case SimulationType.HasInternet: runnables.Add(new HasInternetTestscenarioRunnable(rule));   break;
                    case SimulationType.Only:        runnables.Add(new OnlyTestscenarioRunnable(rule, network)); break;
                }
            }

            SimulationCount = 0;
            foreach (var runnable in runnables)
            {
                SimulationCount += runnable.SimulationCount;
            }

            return runnables;
        }
    }
}
