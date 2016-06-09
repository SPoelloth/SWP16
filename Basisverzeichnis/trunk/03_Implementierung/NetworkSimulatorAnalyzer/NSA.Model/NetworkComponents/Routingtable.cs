using System.Collections.Generic;
using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Routingtable
    {
        private Dictionary<string, Route> routes = new Dictionary<string, Route>();

        /// <summary>
        /// Adds a route to the table.
        /// </summary>
        /// <param name="n">The name.</param>
        /// <param name="Route">The route.</param>
        public void AddRoute(string n, Route Route)
        {
            routes.Add(n, Route);
        }

        /// <summary>
        /// Removes a route from the table.
        /// </summary>
        /// <param name="n">The name.</param>
        public void RemoveRoute(string n)
        {
            routes.Remove(n);
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns>The Routes</returns>
        public Dictionary<string, Route>.ValueCollection GetRoutes()
        {
            return routes.Values;
        }

        /// <summary>
        /// Sets the route.
        /// </summary>
        /// <param name="n">The name.</param>
        /// <param name="d">The destination.</param>
        /// <param name="s">The subnetmask.</param>
        /// <param name="g">The gateway.</param>
        /// <param name="i">The interface.</param>
        /// <returns></returns>
        public bool SetRoute(string n, IPAddress d, IPAddress s, IPAddress g, Interface i)
        {
            if (routes.ContainsKey(n))
            {
                routes[n] = new Route(n, d, s, g, i);
                return true;
            }
            return false;
        }
    }
}
