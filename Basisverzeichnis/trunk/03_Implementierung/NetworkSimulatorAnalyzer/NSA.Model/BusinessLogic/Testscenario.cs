using System;
using System.Collections.Generic;

namespace NSA.Model.BusinessLogic
{
	public class Testscenario
    {
        private List<Rule> rules;

        public Testscenario()
        {
        }

        public Testscenario(string id)
	    {
	        Id = id;
	    }

	    public string Id { get; }
    }
}
