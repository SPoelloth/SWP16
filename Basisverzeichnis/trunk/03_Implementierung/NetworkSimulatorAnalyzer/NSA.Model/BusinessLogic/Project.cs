using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic
{
	public class Project
	{
	    public string Path { get; set; }
	    public Network Network { get; set; }
        public Dictionary<string, Point?> NodeLocation { get; set; }

        // Default Konstruktor
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            Network = new Network();
            Path = null;
            
        }
    }
}
