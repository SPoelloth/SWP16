using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic
{
    [Flags]
    public enum SimulationType
    {
        Simple = 0,
        Only = 1,
        HasInternet = 2
    }

    public class Rule
    {
        /**
         * Expected input:
         * PC_NAME | (PC_NAME, ...) | { OPTIONS } | TRUE/FALSE
         * PC_NAME | (SUBNET(PC_NAME), ...) | {TTL: 64, SSL: TRUE, ...} | TRUE/FALSE 
         * PC_NAME | ONLY(PC_NAME, ...) | {TTL: 64, SSL: TRUE, ...} | TRUE/FALSE 
         * PC_NAME | HAS_INTERNET | TRUE/FALSE
         */

        private SimulationType simulationType;
        public SimulationType SimulType { get { return simulationType; } }
        private string startNode;
        public string StartNodeString { get { return startNode; } }
        public Hardwarenode StartNode { get { return network.GetHardwarenodeByName(startNode); } }
        private List<string> endNodes;
        public List<string> EndNodesString { get { return endNodes; } }
        public List<Hardwarenode> EndNodes
        {
            get
            {
                List<Hardwarenode> nodes = new List<Hardwarenode>();

                foreach (var node in endNodes)
                {
                    if (node.ToUpper().Contains("SUBNET")) {}
                        //NOTE: Functionality is not there yet
                        // nodes.CopyTo(network.GetHardwarenodeBySubnetName(node));
                    else
                    {
                        Hardwarenode n = GetHardwarenodeByName(node);
                        if (n == null)  newNode = new Workstation(eNode);
                        nodes.Add(n);
                    } 
                }

                return nodes;
            }
        }
        private Dictionary<string, int> options;
        public Dictionary<string, int> Options { get { return options; } }
        private bool expectedResult;
        public bool ExpectedResult { get { return expectedResult; } }

        private static string separator = "|";
        private static string optionSeparator = ":";
        public static List<string> PARAMETERS = new List<string> 
        {
            "TTL",
            "SSL"
        };

        public Rule(string startNode, List<string> endNodes, Dictionary<string, int> options, SimulationType simulationType, bool expectedResult, Network n)
        {
            this.startNode = startNode;
            this.endNodes = endNodes;
            this.simulationType = simulationType;
            
            if (!options.ContainsKey("TTL")) option["TTL"] = 64;
            this.options = options;

            this.expectedResult = expectedResult;
            this.network = n;
        }

        public static Rule Parse(string rule, Network n)
        {
            SimulationType simulationType = SimulationType.Simple;
            string startNode = "";
            List<string> endNodes = new List<string>();
            Dictionary<string, int> options = new Dictionary<string, int>();
            bool expectedResult = CheckForTrueOrFalse(rule.Substring(rule.LastIndexOf(Rule.separator)), "GARBAGE");

            int index = -1;
            int i = 0;
            do
            {
                int newIndex = rule.IndexOf(Rule.separator, index + 1);
                string info;
                if (newIndex == -1) info = rule.Substring(index + 1, rule.Length - index - 1);
                else info = rule.Substring(index + 1, newIndex - index - 1);

                info = info.Trim();

                switch (i)
                {
                    case 0: startNode = info; break;
                    case 1:
                        {
                            if (info.ToUpper().Contains("HAS_INTERNET")) { simulationType = SimulationType.HasInternet; break; }
                            else if (info.ToUpper().Contains("ONLY"))    { simulationType = SimulationType.Only; info = info.ToUpper().Replace("ONLY", ""); }
                            else simulationType = SimulationType.Simple;

                            if (info[0] == '(' && info[info.Length - 1] == ')')
                            {
                                info = info.Substring(1, info.Length - 2); // take everything between the '(', ')' characters
                                endNodes.AddRange(info.Split(','));
                            }

                            break;
                        }
                    case 2:
                        {
                            if (info[0] == '{' && info[info.Length - 1] == '}')
                            {
                                info = info.Substring(1, info.Length - 2); // take everything between the '{', '}' characters
                                string[] ops = info.Split(',');

                                foreach (string option in ops)
                                {
                                    int separatorIndex = option.IndexOf(Rule.optionSeparator);
                                    string key = option.Substring(0, separatorIndex);
                                    string val = option.Substring(separatorIndex + 1).Trim();

                                    if (PARAMETERS.Contains(key))
                                    {
                                        if (val == "TRUE")       val = "1";
                                        else if (val == "FALSE") val = "0";

                                        options[key] = Int32.Parse(val);
                                    }
                                    else
                                        throw new ArgumentException("Invalid option", rule);
                                }
                            }
                            break;
                        }
                }

                i++;
                index = newIndex;
            } while (index != -1);

            if (startNode == "")
            { throw new ArgumentException("Rule doesn't contain all the needed information", rule); }

            return new Rule(startNode, endNodes, options, simulationType, expectedResult, n);
        }

        public static bool CheckForTrueOrFalse(string text, string rule)
        {
            if (text.ToUpper().Contains("TRUE")) return true;
            else if (text.ToUpper().Contains("FALSE")) return false;
            else throw new ArgumentException("There is no such condition " + text + ". Expected TRUE or FALSE", rule);
        }
    }
}
