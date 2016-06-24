using NSA.Model.NetworkComponents;


namespace NSA.Model.BusinessLogic
{
    /// <summary>
    /// Class for project
    /// </summary>
    public class Project
	{
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the network.
        /// </summary>
        /// <value>
        /// The network.
        /// </value>
        public Network Network { get; set; }

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
