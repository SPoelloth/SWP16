using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;
using NSA.Model.BusinessLogic.TestscenarioRunnables;

namespace NSA.Model.BusinessLogic
{
    public class Testscenario
    {
		/// <summary>
		/// The network.
		/// </summary>
        private readonly Network network;
        
		/// <summary>
		/// The text.
		/// </summary>
		private readonly string text;

		/// <summary>
		/// Gets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
        public string FileName { get; private set; }

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
        public string Id { get; }
        
		/// <summary>
		/// Initializes a new instance of the <see cref="NSA.Model.BusinessLogic.Testscenario"/> class.
		/// </summary>
		/// <param name="T">Text to parse</param>
		/// <param name="N">Network</param>
		/// <param name="FileName">File name</param>
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
                try
                {
                    var rule = Rule.Parse(line, network);
                    switch (rule.SimulType)
                    {
                        case SimulationType.Simple: runnables.Add(new SimpleTestscenarioRunnable(rule)); break;
                        case SimulationType.HasInternet: runnables.Add(new HasInternetTestscenarioRunnable(rule)); break;
                        case SimulationType.Only: runnables.Add(new OnlyTestscenarioRunnable(rule, network)); break;
                    }
                }
                catch{}
            }

            return runnables;
        }
    }
}
