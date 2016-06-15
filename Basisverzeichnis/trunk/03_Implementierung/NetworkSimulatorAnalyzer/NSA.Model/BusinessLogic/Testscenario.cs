using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic
{
    public class Testscenario
    {
        public string Id { get; }
        public List<Rule> Rules { get; set; }

        public Testscenario()
	    {
            Id = Guid.NewGuid().ToString("N");
            Rules = new List<Rule>();
        }

        public Network ParseRulesToNetwork()
        {
            Network network = new Network();
            foreach (var rule in Rules)
            {
                // todo: parse rule
            }
            return network;
        }
    }

    public class Rule
    {
        public string Name { get; set; }
        public List<string> NodeNames { get; set; }
        public List<string> OnlyNodeNames { get; set; }
        public List<string> SubnetNames { get; set; }
        public bool HasInternet { get; set; }
        public int Ttl { get; set; }
        public bool Ssl { get; set; }
        public bool Applicable { get; set; }

        // Default Constructor
        public Rule()
        {
            Name = "";
            NodeNames = new List<string>();
            OnlyNodeNames = new List<string>();
            SubnetNames = new List<string>();
            HasInternet = false;
            Ttl = 0;
            Ssl = false;
            Applicable = false;
        }
    }
}
