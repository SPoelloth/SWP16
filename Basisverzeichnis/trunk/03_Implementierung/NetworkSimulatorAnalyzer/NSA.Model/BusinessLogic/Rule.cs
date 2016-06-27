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

        public SimulationType SimulType { get; }

        public string StartNodeString { get; }

        public Hardwarenode StartNode
        {
            get
            {
                var node = network.GetHardwarenodeByName(StartNodeString);
                if (node == null)
                {
                    node = new Workstation(this.StartNodeString);
                }
                return node;
            }
        }
        public List<string> EndNodesString { get; }

        public List<Hardwarenode> EndNodes
        {
            get
            {
                var nodes = new List<Hardwarenode>();
                if (SimulType == SimulationType.HasInternet)
                {
                    nodes.CopyTo(network.GetRouters().ToArray());
                    return nodes;
                }

                foreach (var node in EndNodesString)
                {
                    if (node.ToUpper().Contains("SUBNET")) {}
                        //NOTE: Functionality is not there yet
                        // nodes.CopyTo(network.GetHardwarenodeBySubnetName(node));
                    else
                    {
                        var n = network.GetHardwarenodeByName(node) ?? new Workstation(node);
                        nodes.Add(n);
                    } 
                }

                return nodes;
            }
        }

        public Dictionary<string, int> Options { get; }

        public bool ExpectedResult { get; }

        private const string Separator = "|";
        private const string OptionSeparator = ":";

        public static List<string> Parameters = new List<string> 
        {
            "TTL",
            "SSL"
        };

        private readonly Network network;
        public Rule(string StartNode, List<string> EndNodes, Dictionary<string, int> Options, SimulationType SimulationType, bool ExpectedResult, Network N)
        {
            StartNodeString = StartNode;
            EndNodesString = EndNodes;
            SimulType = SimulationType;
            
            if (!Options.ContainsKey("TTL")) Options["TTL"] = 64;
            this.Options = Options;

            this.ExpectedResult = ExpectedResult;
            network = N;
        }


        /// <summary>
        /// parses a string and creates a rule from it
        /// </summary>
        /// <param name="Rule"></param>
        /// <param name="N"></param>
        /// <returns>Rule object</returns>
        public static Rule Parse(string Rule, Network N)
        {
            if (Rule == null) throw new ArgumentNullException(nameof(Rule));
            var simulationType = SimulationType.Simple;
            var startNode = "";
            var endNodes = new List<string>();
            var options = new Dictionary<string, int>();
            var expectedResult = CheckForTrueOrFalse(Rule.Substring(Rule.LastIndexOf(BusinessLogic.Rule.Separator, StringComparison.Ordinal)), "GARBAGE");

            var index = -1;
            var i = 0;
            do
            {
                var newIndex = Rule.IndexOf(BusinessLogic.Rule.Separator, index + 1, StringComparison.Ordinal);
                var info = newIndex == -1 ? Rule.Substring(index + 1, Rule.Length - index - 1) : Rule.Substring(index + 1, newIndex - index - 1);

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
                                var ops = info.Split(',');

                                foreach (var option in ops)
                                {
                                    var separatorIndex = option.IndexOf(BusinessLogic.Rule.OptionSeparator, StringComparison.Ordinal);
                                    var key = option.Substring(0, separatorIndex);
                                    var val = option.Substring(separatorIndex + 1).Trim();

                                    if (Parameters.Contains(key))
                                    {
                                        if (val == "TRUE")       val = "1";
                                        else if (val == "FALSE") val = "0";

                                        options[key] = int.Parse(val);
                                    }
                                    else
                                        throw new ArgumentException("Invalid option", Rule);
                                }
                            }
                            break;
                        }
                }

                i++;
                index = newIndex;
            } while (index != -1);

            if (startNode == "")
            { throw new ArgumentException("Rule doesn't contain all the needed information", Rule); }

            return new Rule(startNode, endNodes, options, simulationType, expectedResult, N);
        }

        public static bool CheckForTrueOrFalse(string Text, string Rule)
        {
            if (Text.ToUpper().Contains("TRUE")) return true;
            else if (Text.ToUpper().Contains("FALSE")) return false;
            else throw new ArgumentException("There is no such condition " + Text + ". Expected TRUE or FALSE", Rule);
        }
    }
}
