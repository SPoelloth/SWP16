﻿using System.Collections.Generic;
using System.Drawing;
using NSA.Model.NetworkComponents;


namespace NSA.Model.BusinessLogic
{
	public class Project
	{
	    public string Path { get; set; } // todo: Pfad nicht speichern
	    public Network Network { get; set; }
        // Locations of Víew Elements
        public List<NodeLocation> NodeLocations { get; set; }

        // Default Konstruktor
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            Network = new Network();
            Path = null;
            NodeLocations = new List<NodeLocation>();
        }

	    public void parseProjectViewDataToViewControlls()
	    {
	        
	    }
    }

    public class NodeLocation
    {
        public string Name { get; set; }
        public Point Point { get; set; }
    }
}
