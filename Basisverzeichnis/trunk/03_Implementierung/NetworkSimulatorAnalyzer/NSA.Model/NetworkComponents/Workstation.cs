using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;
using NSA.Model.NetworkComponents.Layers;

namespace NSA.Model.NetworkComponents
{
    public class Workstation : Hardwarenode
    {
        private List<Interface> interfaces = new List<Interface>();
        // Warum nochmal Dictionary bzw. was ist der string ?
        private Dictionary<string, Route> routingtable = new Dictionary<string, Route>();
        public IPAddress StandardGateway { get; set; }
        public Interface StandardGatewayPort { get; set; }
        
        private int nextInterface;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workstation" /> class.
        /// The IP address of the standardgateway must be set seperatly.
        /// </summary>
        /// <param name="Name">The Name.</param>
        public Workstation(string Name) : base(Name)
        {
            Layerstack.AddLayer(new PhysicalLayer());
            Layerstack.AddLayer(new DataLinkLayer());
            Layerstack.AddLayer(new NetworkLayer());
            Layerstack.AddLayer(new TransportLayer());
            Layerstack.AddLayer(new SessionLayer());
            Layerstack.AddLayer(new PresentationLayer());
            Layerstack.AddLayer(new ApplicationLayer());
            StandardGateway = null;
        }


        #region methods
        #region interface methods
        /// <summary>
        /// Gets the interfaces.
        /// </summary>
        /// <returns>
        /// The Interfaces
        /// </returns>
        public List<Interface> GetInterfaces()
        {
            return interfaces;
        }

        /// <summary>
        /// Adds a new interface with the given IP and subnetmask
        /// </summary>
        /// <param name="Ip">The IP of the interface.</param>
        /// <param name="Subnetmask">The subnetmask.</param>
        /// <returns>The newly added Interface</returns>
        public Interface AddInterface(IPAddress Ip, IPAddress Subnetmask)
        {
            //ToDo Mehrere Lücken, d.h. mehrere gelöschte Interfaces beachten!
            Interface interfaceObj = new Interface(Ip, Subnetmask, nextInterface);

            interfaces.Add(interfaceObj);

            if (nextInterface == (interfaces.Count - 1))
                nextInterface++;
            else
                nextInterface = interfaces.Count;

            return interfaceObj;
        }

        /// <summary>
        /// Removes the interface with the given number.
        /// </summary>
        /// <param name="number">The number.</param>
        public void RemoveInterface(int number)
        {
            string name = Interface.NamePrefix + number;
            RemoveConnection(name);
            interfaces.Remove(interfaces.Find(I => I.Name.Equals(name)));
            nextInterface = int.Parse(name.Substring(Interface.NamePrefix.Length, name.Length - Interface.NamePrefix.Length));
        }

        /// <summary>
        /// Gets the interface count.
        /// </summary>
        /// <returns>
        /// int: interface count
        /// </returns>
        public int GetInterfaceCount()
        {
            return interfaces.Count;
        }

        /// <summary>
        /// Changes the interface.
        /// </summary>
        /// <param name="number">The number of the interface.</param>
        /// <param name="ipAddress">The new IPAddress of the interface.</param>
        /// <param name="subnetmask">The new subnetmask of the interface.</param>
        public void SetInterface(int number, IPAddress ipAddress, IPAddress subnetmask)
        {
            // todo
            // Vorschlag für diese Methode. 
            // Grund: wenn im NetworkManager "InterfaceChanged(...)" aufgerufen wird, müsste ansonsten
            // Workstation sowiese benachrigtigt werden. Und auch die Connections in Workstation, die ja 
            // protected sind, müssten aktualisíert werden (?)
            // s. a. das todo in "InterfaceChanged()" für mehr Details
        }

        #endregion
        #region routingtable methods
        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="N">The name.</param>
        /// <param name="Route">The route.</param>
        public void AddRoute(string N, Route Route)
        {
            // todo: Route-Name wird offenbar doppelt abgespeichert (in der Route und in Dictionary)
            // -> ein Mal sollte reichen, weil sonst könnte es passieren, dass man der methode 
            // unterschiedliche Namen übergibt.
            routingtable.Add(N, Route);
        }

        /// <summary>
        /// Removes the route.
        /// </summary>
        /// <param name="N">The name.</param>
        public void RemoveRoute(string N)
        {
            routingtable.Remove(N);
        }


        /// <summary>
        /// Gets the route count.
        /// </summary>
        /// <returns>int: number of routes in the routingtable</returns>
        public int GetRouteCount()
        {
            return routingtable.Count;
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns>The Routes</returns>
        public Dictionary<string, Route>.ValueCollection GetRoutes()
        {
            return routingtable.Values;
        }

        /// <summary>
        /// Gets the route at the given index.
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <returns></returns>
        public Route GetRouteAt(int Index)
        {
            return routingtable.ElementAt(Index).Value;
        }

        /// <summary>
        /// Sets the route.
        /// </summary>
        /// <param name="RouteName">The name of the route.</param>
        /// <param name="Destination">The new destination.</param>
        /// <param name="Subnetmask">The new subnetmask.</param>
        /// <param name="Gateway">The new gateway.</param>
        /// <param name="Iface">The new interface.</param>
        /// <returns>bool: false if the route could not be found, otherwise true</returns>
        public bool SetRoute(string RouteName, IPAddress Destination, IPAddress Subnetmask, IPAddress Gateway, Interface Iface)
        {
            if (routingtable.ContainsKey(RouteName))
            {
                routingtable[RouteName] = new Route(RouteName, Destination, Subnetmask, Gateway, Iface);
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Checks if the Hardwarenode has the IP
        /// </summary>
        /// <param name="Ip">The ip.</param>
        /// <returns>
        /// bool: true if workstation has the ip, otherwise false
        /// </returns>
        public override bool HasIP(IPAddress Ip)
        {
            bool hasIp = false;

            foreach (Interface iface in interfaces)
            {
                hasIp = iface.IpAddress.Equals(Ip);
                if (hasIp) break;
            }
            return hasIp;
        }

        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Result">String representing the result</param>
        /// <param name="NextNodeIp"></param>
        /// <returns>
        /// The Hardwarenode which received the package or null if an error occured
        /// </returns>
        public override List<Hardwarenode> Send(Hardwarenode Destination, Dictionary<string, object> Tags, Result Result, IPAddress NextNodeIp)
        {
            List<Hardwarenode> nextNodes = new List<Hardwarenode>();
            Interface iface = null;
            for (int i = Layerstack.GetSize() - 1; i >= 0; i--)
            {
                if (nextNodes != null)
                {
                    Workstation dest = Destination as Workstation;
                    if(dest != null)
                        Layerstack.GetLayer(i).ValidateSend(nextNodes, NextNodeIp, iface, dest, this, Result);
                    else
                    {
                        throw new ArgumentException("Destination is no Workstation.");
                    }
                }
            }
            return nextNodes;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Res"></param>
        /// <param name="NextNodeIp"></param>
        /// <returns>
        /// bool that indicates if the Hardwarenode received the package
        /// </returns>
        public override bool Receive(Dictionary<string, object> Tags, Result Res, IPAddress NextNodeIp)
        {
            bool res = true;
            for (int i = 0; i < Layerstack.GetSize(); i++)
            {
                if (res)
                    res = Layerstack.GetLayer(i).ValidateReceive(NextNodeIp, this, Res);
            }
            return res;
        }

        #endregion
    }
    
}
