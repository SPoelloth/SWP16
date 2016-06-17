using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Hardwarenode
    {
        protected Layerstack Layerstack = new Layerstack();
        protected Dictionary<string, Connection> Connections = new Dictionary<string, Connection>();
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hardwarenode" /> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        public Hardwarenode(string Name)
        {
            this.Name = Name;
        }

        #region Methods

        /// <summary>
        /// Adds a connection.
        /// </summary>
        /// <param name="IfaceName">Name of the interface where the connection should be added.</param>
        /// <param name="Con">The connection to be added.</param>
        public void AddConnection(string IfaceName, Connection Con)
        {
            Connections.Add(IfaceName, Con);
        }

        /// <summary>
        /// Removes a connection.
        /// </summary>
        /// <param name="IfaceName">Name of the interface where the connection should be removed.</param>
        public virtual void RemoveConnection(string IfaceName)
        {
            Connections.Remove(IfaceName);
        }

        /// <summary>
        /// Gets the connection at port.
        /// </summary>
        /// <param name="IfaceName">Name of the port.</param>
        /// <returns></returns>
        public Connection GetConnectionAtPort(string IfaceName)
        {
            if (!Connections.ContainsKey(IfaceName))
                return null;
            return Connections[IfaceName];
        }

        /// <summary>
        /// Interfaces the is used.
        /// </summary>
        /// <param name="InterfaceName">Name of the interface.</param>
        /// <returns></returns>
        public bool InterfaceIsUsed(string InterfaceName)
        {
            if (Connections.ContainsKey(InterfaceName))
                return true;
            return false;
        }

        /// <summary>
        /// Checks if the Hardwarenode has the IP
        /// </summary>
        /// <param name="Ip">The ip.</param>
        /// <returns>A bool</returns>
        public virtual bool HasIp(IPAddress Ip)
        {
            return false;
        }

        /// <summary>
        /// Hardwarenode sends the package to specified destination.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="ValInfo"></param>
        /// <returns>The Hardwarenode which received the package or null if an error occured</returns>
        public virtual List<Hardwarenode> Send(Hardwarenode Destination, Dictionary<string, object> Tags, ValidationInfo ValInfo)
        {
            return null;
        }

        /// <summary>
        /// Hardwarenode receives the package.
        /// </summary>
        /// <param name="Tags">Optional tags.</param>
        /// <param name="ValInfo">The validation Info</param>
        /// <param name="Destination">The destination.</param>
        /// <returns>
        /// If the Hardwarenode could receive the package
        /// </returns>
        public virtual bool Receive(Dictionary<string, object> Tags, ValidationInfo ValInfo, Hardwarenode Destination)
        {
            return true;
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <returns>Connections</returns>
        public Dictionary<string, Connection> GetConnections()
        {
            return Connections;
        }

        #region Equality

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="Obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object Obj)
        {
            // If parameter is null return false.
            if (Obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Hardwarenode h = Obj as Hardwarenode;
            if ((object)h == null)
            {
                return false;
            }

            // Return true if the fields match:
            return string.Equals(Name, h.Name);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="Other">The other.</param>
        /// <returns></returns>
        protected bool Equals(Hardwarenode Other)
        {
            // auto-generated method
            return string.Equals(Name, Other.Name);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            // auto-generated method
            return (Name != null ? Name.GetHashCode() : 0);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="A">a.</param>
        /// <param name="B">The b.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Hardwarenode A, Hardwarenode B)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(A, B))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)A == null) || ((object)B == null))
            {
                return false;
            }

            // Return true if the fields match:
            return A.Name == B.Name;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="A">a.</param>
        /// <param name="B">The b.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Hardwarenode A, Hardwarenode B)
        {
            // auto-generated method
            return !(A == B);
        }
        #endregion

        /// <summary>
        /// Gets the port index of connection.
        /// </summary>
        /// <param name="C">The connection.</param>
        /// <returns>Portindex</returns>
        public int GetPortIndexOfConnection(Connection C)
        {
            KeyValuePair<string, Connection> pair = Connections.FirstOrDefault(S => S.Value == C);
            if(pair.Equals(default(KeyValuePair<string, Connection>)))
                return -1;
            string str = pair.Key;
            str = str.Remove(0, 3);
            return Int32.Parse(str);
        }
        #endregion
    }
}
