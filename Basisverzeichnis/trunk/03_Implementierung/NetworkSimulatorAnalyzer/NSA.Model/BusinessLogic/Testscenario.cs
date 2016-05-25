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
        // todo: an id is needed
        // in the ProjectManager is a method to load the testscenario by id so the testscenario has to have an id
        public Testscenario(string id)
	    {
	        Id = id;
	    }

	    public string Id { get; }
    }
}
