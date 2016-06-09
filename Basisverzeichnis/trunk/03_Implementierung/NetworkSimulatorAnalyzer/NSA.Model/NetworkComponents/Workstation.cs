using System;
using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;
using NSA.Model.NetworkComponents.Layers;

namespace NSA.Model.NetworkComponents
{
    public class Workstation : Hardwarenode
    {
        private List<Interface> interfaces = new List<Interface>();
        private Routingtable routingtable = new Routingtable();
        public IPAddress StandardGateway { get; set; }
        public Interface StandardGatewayPort { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Workstation" /> class.
        /// The IP address of the standardgateway must be set seperatly.
        /// </summary>
        /// <param name="Name">The Name.</param>
        public Workstation(string Name) : base(Name)
        {
            layerstack.AddLayer(new PhysicalLayer());
            layerstack.AddLayer(new DataLinkLayer());
            layerstack.AddLayer(new NetworkLayer());
            layerstack.AddLayer(new TransportLayer());
            layerstack.AddLayer(new SessionLayer());
            layerstack.AddLayer(new PresentationLayer());
            layerstack.AddLayer(new ApplicationLayer());
            StandardGateway = null;
        }

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
        /// Adds a new interface to the workstation
        /// </summary>
        /// <param name="Iface">The iface.</param>
        public void AddInterface(Interface Iface)
        {
            interfaces.Add(Iface);
        }

        /// <summary>
        /// Removes the given interface.
        /// </summary>
        /// <param name="Iface">The iface.</param>
        public void RemoveInterface(Interface Iface)
        {
            RemoveConnection(Iface.Name);
            interfaces.Remove(Iface);
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
        /// Adds the route.
        /// </summary>
        /// <param name="n">The name.</param>
        /// <param name="Route">The route.</param>
        public void AddRoute(string n, Route Route)
        {
            routingtable.AddRoute(n, Route);
        }

        /// <summary>
        /// Removes the route.
        /// </summary>
        /// <param name="n">The name.</param>
        public void RemoveRoute(string n)
        {
            routingtable.RemoveRoute(n);
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns>The Routes</returns>
        public Dictionary<string, Route>.ValueCollection GetRoutes()
        {
            return routingtable.GetRoutes();
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
            return routingtable.SetRoute(n, d, s, g, i);
        }

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
        /// <param name="nextNodeIP"></param>
        /// <returns>
        /// The Hardwarenode which received the package or null if an error occured
        /// </returns>
        public override Hardwarenode Send(Hardwarenode Destination, Dictionary<string, object> Tags, Result Result, IPAddress nextNodeIP)
        {
            Hardwarenode nextNode = this;
            Interface iface = null;
            for (int i = layerstack.GetSize() - 1; i >= 0; i--)
            {
                if (nextNode != null)
                {
                    Workstation dest = Destination as Workstation;
                    if(dest != null)
                        layerstack.GetLayer(i).ValidateSend(nextNode, nextNodeIP, iface, dest, this, Result);
                    else
                    {
                        throw new ArgumentException("Destination is no Workstation.");
                    }
                }
            }
            return nextNode.Equals(this) ? null : nextNode;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="Res"></param>
        /// <param name="nextNodeIP"></param>
        /// <returns>
        /// bool that indicates if the Hardwarenode received the package
        /// </returns>
        public override bool Receive(Dictionary<string, object> Tags, Result Res, IPAddress nextNodeIP)
        {
            bool res = true;
            for (int i = 0; i < layerstack.GetSize(); i++)
            {
                if (res)
                    res = layerstack.GetLayer(i).ValidateReceive(nextNodeIP, this, Res);
            }
            return res;
        }
    }
}
