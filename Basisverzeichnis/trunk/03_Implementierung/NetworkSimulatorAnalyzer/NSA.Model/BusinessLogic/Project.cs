using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic
{
	public class Project
    {
        private Network network { get; set; }
        private string path { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="_network">The _network.</param>
        /// <param name="_path">The _path.</param>
        public Project(Network _network, string _path)
        {
            network = _network;
            path = _path;
        }
    }
}
