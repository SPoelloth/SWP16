namespace NSA.Model.BusinessLogic
{
    public class Rule
    {
        /**
         * Expected input:
         * PC_NAME | [PC_NAME, ...] | { OPTIONS } | TRUE/FALSE
         * PC_NAME | [SUBNET(PC_NAME), ...] | {TTL: 64, SSL: TRUE, ...} | TRUE/FALSE 
         * PC_NAME | ONLY([PC_NAME, ...]) | {TTL: 64, SSL: TRUE, ...} | TRUE/FALSE 
         * PC_NAME | HAS_INTERNET | TRUE/FALSE
         */
        
        [Flags]
        public enum SimulationType
        {
            Simple = 0,
            Only = 1,
            HasInternet = 2
        }
        
        private SimulationType simulationType;
        private string startNode;
        private ArrayList<string> endNodes;
        private ArrayList<Dictionary<string, string>> options;
        private bool expectedResult;
        
        private static string separator = "|";
        private static string optionSeparator = ":";
         
        /**
         * Method throws exceptions while parsing the data or retrieving nodes!
         */
        public static ArrayList<Simulation> GetSimulationForRule(string stringRule)
        {
            Rule rule = Rule.Parse(stringRule);
                
            NetworkManager.Instance.GetHardwarenodeByName(startNode); // find starting node
                
            if (simulationType & SimulationType.Only)
            {
                //TODO: find all end nodes, subnets
            }
                
            //TODO: create simulaitons
        }
        
        private Rule Parse(string rule)
        {
            int index = rule.indexOf(Rule.separator);
            int i = 0;
            while (index > 0)
            {
                int newIndex = rule.indexOf(Rule.separator, index);
                string info;
                if (newIndex == -1) info = rule.Substring(index, rule.Length - index);
                else                info = rule.Substring(index, newIndex - index);
                
                info = info.Trim();
                
                switch (i)
                {
                    case 0: startNode = info; break;
                    case 1: 
                    {
                        if (info.ToUpper().Contains("HAS_INTERNET"))    simulationType = SimulationType.HasInternet;
                        if (info.ToUpper().Contains("ONLY"))            simulationType = SimulationType.Only;
                        else                                            simulationType = SimulationType.Simple;
                        
                        if (info[0] == "[" && info[info.Length - 1] == "]")
                        {
                            info = info.Substring(1, info.Length - 2); // take everything betwenn the '[', ']' characters
                            endNodes = info.Split(",")
                        }
                        else
                        {
                            if (!info.Contains(",")) startNode = info;
                            else throw new ArgumentException("Rule expects only one element, but found a comma", rule);
                        }
                        
                        break;
                    }
                    case 2:
                    {
                        if (simulationType & SimulationType.HasInternet) this.expectedResult = Rule.CheckForTrueOrFalse(info);
                        else
                        {
                            if (info[0] == "{" && info[info.Length - 1] == "}")
                            {
                                info = info.Substring(1, info.Length - 2); // take everything betwenn the '{', '}' characters
                                ops = info.Split(",")
                                
                                foreach (string option in ops)
                                {
                                    int separatorIndex = option.IndexOf(Rule.optionSeparator);
                                    string key = option.Substring(0, separatorIndex);
                                    string val = option.Substring(separatorIndex).Trim();
                                    options[key] = val;
                                } 
                            }   
                        }
                        break;
                    }
                    case 3: this.expectedResult = Rule.CheckForTrueOrFalse(info); break;   
                    default: throw new ArgumentException("Rule doesn't contains too many separators '|' ", rule); 
                }
                    
                i++;
                index = newIndex;
            }
            
            if (this.startNode == null || this.endNodes == null || options == null)
            { throw new ArgumentException("Rule doesn't contain all the needed information", rule); }
        }
        
        public static bool CheckForTrueOrFalse(string text)
        {
            if (info.ToUpper().Contains("TRUE"))        return true;
            else if (info.ToUpper().Contains("FALSE"))  return false;
            else throw new ArgumentException("There is no such condition " + info + ". Expected TRUE or FALSE", rule);
        }
    }
}