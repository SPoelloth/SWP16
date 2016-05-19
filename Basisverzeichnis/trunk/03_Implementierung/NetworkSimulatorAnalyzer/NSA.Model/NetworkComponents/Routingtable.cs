using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Routingtable
    {
        private List<Route> routes = new List<Route>();

        /// <summary>
        /// Adds a route to the table.
        /// </summary>
        /// <param name="route">The route.</param>
        public void AddRoute(Route route)
        {
            routes.Add(route);
        }

        /// <summary>
        /// Removes a route from the table.
        /// </summary>
        /// <param name="route">The route.</param>
        public void RemoveRoute(Route route)
        {
            routes.Remove(route);
        }
    }
}
