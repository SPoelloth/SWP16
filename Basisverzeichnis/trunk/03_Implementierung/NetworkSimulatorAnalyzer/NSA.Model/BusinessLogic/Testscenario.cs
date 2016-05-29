using System;
using System.Collections.Generic;

namespace NSA.Model.BusinessLogic
{
	public class Testscenario
    {
        private List<Rule> rules;
        public string Id { get; }

        public Testscenario()
        {
        }
        // todo: an id is needed
        // Vorschlag: Id = Guid.NewGuid().ToString("N"); (Tamara)
        // in the ProjectManager is a method to load the testscenario by id so the testscenario has to have an id
        public Testscenario(string id)
	    {
	        Id = id; 
	    }
    }
}
