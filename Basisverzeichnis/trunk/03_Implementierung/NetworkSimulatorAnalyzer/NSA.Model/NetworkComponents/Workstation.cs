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
        private readonly List<Interface> interfaces = new List<Interface>();
        private readonly Dictionary<string, Route> routingtable = new Dictionary<string, Route>();
        public IPAddress StandardGateway { get; set; }
        public Interface StandardGatewayPort { get; set; }


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
            AddInterface(new IPAddress(new byte[] { 192, 168, 0, 1 }), new IPAddress(new byte[] { 255, 255, 255, 0 }));
            StandardGateway = null;
            StandardGatewayPort = null;
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
        /// <param name="PortNum">Number of port</param>
        /// <returns>The newly added Interface</returns>
        public Interface AddInterface(IPAddress Ip, IPAddress Subnetmask, int PortNum = -1)
        {
            if (PortNum == -1) PortNum = getNewInterfaceNumber();
            Interface interfaceObj = new Interface(Ip, Subnetmask, PortNum);
            interfaces.Add(interfaceObj);
            return interfaceObj;
        }

        /// <summary>
        /// Removes the interface with the given name.
        /// </summary>
        /// <param name="InterfaceName">The Interfacename.</param>
        public void RemoveInterface(string InterfaceName)
        {
            interfaces.Remove(interfaces.Find(I => I.Name.Equals(InterfaceName)));
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
        /// Sets the interface.
        /// </summary>
        /// <param name="Ifacename">The name of the Interface.</param>
        /// <param name="Ip">The new ip.</param>
        /// <param name="Mask">The new subnetmask.</param>
        /// <returns>bool: false if the interface could not be found, otherwise true</returns>
        public void SetInterface(string Ifacename, IPAddress Ip, IPAddress Mask)
        {
            if (!interfaces.Exists(I => I.Name.Equals(Ifacename)))
            {
                AddInterface(Ip, Mask, int.Parse(Ifacename.Replace(Interface.NamePrefix, "")));
                return;
            }
            interfaces.Find(I => I.Name.Equals(Ifacename)).SetInterface(Ip, Mask);
        }

        /// <summary>
        /// Gets the new interface number.
        /// </summary>
        /// <returns>int: number for next interface</returns>
        private int getNewInterfaceNumber()
        {
            int newInterface = 0;
            bool found = false;

            while (!found)
            {
                if (!interfaces.Exists(I => I.Name.Equals(Interface.NamePrefix + newInterface)))
                    found = true;
                else
                    newInterface++;
            }
            return newInterface;
        }

        /// <summary>
        /// Determines if there is an Interface with the specified name.
        /// </summary>
        /// <param name="IfaceName">Name of the iface.</param>
        /// <returns></returns>
        public override bool HasInterface(string IfaceName)
        {
            foreach (Interface i in interfaces)
            {
                if (i.Name == IfaceName)
                    return true;
            }
            return false;
        }

        #endregion
        #region routingtable methods
        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="Route">The route.</param>
        public void AddRoute(Route Route)
        {
            routingtable.Add(Route.Name, Route);
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
            if (!routingtable.ContainsKey(RouteName)) return false;
            routingtable[RouteName].SetRoute(Destination, Subnetmask, Gateway, Iface);
            return true;
        }
        #endregion

        /// <summary>
        /// Checks if the Hardwarenode has the IP
        /// </summary>
        /// <param name="Ip">The ip.</param>
        /// <returns>
        /// bool: true if workstation has the ip, otherwise false
        /// </returns>
        public override bool HasIp(IPAddress Ip)
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
        /// <param name="ValInfo"></param>
        /// <returns>
        /// The Hardwarenode which received the package or null if an error occured
        /// </returns>
        public override List<Hardwarenode> Send(Hardwarenode Destination, Dictionary<string, object> Tags, ValidationInfo ValInfo)
        {
            ValInfo.NextNodes = new List<Hardwarenode>();
            ValInfo.Iface = null;
            for (int i = Layerstack.GetSize() - 1; i >= 0; i--)
            {
                int customLayerCount = 0;
                //Calculate the custom layer count before this layer
                for (int j = 0; j < i; j++)
                {
                    if (Layerstack.GetLayer(j) is CustomLayer)
                        customLayerCount++;
                }
                if (ValInfo.NextNodes != null)
                {
                    Workstation dest = Destination as Workstation;
                    if (dest != null)
                    {
                        Layerstack.GetLayer(i).ValidateSend(dest, this, ValInfo, Tags, i - customLayerCount);
                    }
                    else
                    {
                        throw new ArgumentException("Destination is no Workstation.");
                    }
                }
            }
            return ValInfo.NextNodes;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="ValInfo">The validation Info</param>
        /// <param name="Destination">The destination</param>
        /// <returns>
        /// bool that indicates if the Hardwarenode received the package
        /// </returns>
        public override bool Receive(Dictionary<string, object> Tags, ValidationInfo ValInfo, Hardwarenode Destination)
        {
            bool res = true;
            int customLayerCount = 0;
            for (int i = 0; i < Layerstack.GetSize(); i++)
            {
                if (res)
                {
                    res = Layerstack.GetLayer(i).ValidateReceive(this, ValInfo, Tags, Destination, i - customLayerCount);
                    if (Layerstack.GetLayer(i) is CustomLayer)
                        customLayerCount++;
                }
            }
            return res;
        }
        #endregion
    }

}
