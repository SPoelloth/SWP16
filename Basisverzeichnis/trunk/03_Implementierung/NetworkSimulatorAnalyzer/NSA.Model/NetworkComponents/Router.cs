namespace NSA.Model.NetworkComponents
{
    public class Router : Workstation
    {
        public bool IsGateway { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Router"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Router(string name) : base(name)
        {
        }
    }
}
