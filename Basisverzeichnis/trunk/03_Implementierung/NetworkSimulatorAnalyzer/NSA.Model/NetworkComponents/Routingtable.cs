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

        /// <summary>
        /// Removes the route at the given index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveRouteAtIndex(int index)
        {
            routes.RemoveAt(index);
        }

        /// <summary>
        /// Gets the size of the routingtable.
        /// </summary>
        /// <returns>The size</returns>
        public int GetSize()
        {
            return routes.Count;
        }

        /// <summary>
        /// Gets the route at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The route at the index</returns>
        public Route GetRouteAt(int index)
        {
            return routes[index];
        }
    }
}
