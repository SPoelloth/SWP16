using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Routingtable
    {
        private List<Route> routes = new List<Route>();

        /// <summary>
        /// Adds a route to the table.
        /// </summary>
        /// <param name="Route">The route.</param>
        public void AddRoute(Route Route)
        {
            routes.Add(Route);
        }

        /// <summary>
        /// Removes a route from the table.
        /// </summary>
        /// <param name="Route">The route.</param>
        public void RemoveRoute(Route Route)
        {
            routes.Remove(Route);
        }

        /// <summary>
        /// Removes the route at the given index.
        /// </summary>
        /// <param name="Index">The index.</param>
        public void RemoveRouteAtIndex(int Index)
        {
            routes.RemoveAt(Index);
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
        /// <param name="Index">The index.</param>
        /// <returns>The route at the index</returns>
        public Route GetRouteAt(int Index)
        {
            return routes[Index];
        }
    }
}
