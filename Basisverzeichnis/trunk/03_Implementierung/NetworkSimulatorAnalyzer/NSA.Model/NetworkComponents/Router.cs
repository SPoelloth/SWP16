namespace NSA.Model.NetworkComponents
{
    /// <summary>
    /// Implements the network component Router.
    /// </summary>
    /// <seealso cref="NSA.Model.NetworkComponents.Workstation" />
    public class Router : Workstation
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is an internet gateway.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an internet gateway; otherwise, <c>false</c>.
        /// </value>
        public bool IsGateway { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Router"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        public Router(string Name) : base(Name)
        {
            IsGateway = false;
        }
    }
}
