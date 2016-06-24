using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents
{
    public class Hardwarenode
    {
        public Layerstack Layerstack { get; } = new Layerstack();
        public Dictionary<string, Connection> Connections { get; protected set; } = new Dictionary<string, Connection>();
        public string Name { get; set; }

        public List<Interface> Interfaces { get; protected set; } = new List<Interface>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Hardwarenode" /> class.
        /// </summary>
        /// <param name="N">The n.</param>
        public Hardwarenode(string N)
        {
            Name = N;
        }

        #region Methods

        #region interface methods
        /// <summary>
        /// Adds a new interface with the given IP and subnetmask
        /// </summary>
        /// <param name="Ip">The IP of the interface. Ignored if used with switch</param>
        /// <param name="Subnetmask">The subnetmask. Ignored if used with switch </param>
        /// <param name="PortNum">Number of port. Only for project loading purpose. </param>
        /// <returns>The newly added Interface</returns>
        public virtual Interface AddInterface(IPAddress Ip, IPAddress Subnetmask, int PortNum = -1)
        {
            if (PortNum == -1) PortNum = getNewInterfaceNumber();
            Interface interfaceObj = new Interface(Ip, Subnetmask, PortNum);
            Interfaces.Add(interfaceObj);
            return interfaceObj;
        }

        /// <summary>
        /// Removes the interface with the given name.
        /// </summary>
        /// <param name="InterfaceName">The Interfacename.</param>
        public void RemoveInterface(string InterfaceName)
        {
            Interfaces.Remove(Interfaces.Find(I => I.Name.Equals(InterfaceName)));
        }

        /// <summary>
        /// Gets the interface count.
        /// </summary>
        /// <returns>
        /// int: interface count
        /// </returns>
        public int GetInterfaceCount()
        {
            return Interfaces.Count;
        }

        /// <summary>
        /// Sets the interface.
        /// </summary>
        /// <param name="Ifacename">The name of the Interface.</param>
        /// <param name="Ip">The new ip.</param>
        /// <param name="Mask">The new subnetmask.</param>
        /// <returns>bool: false if the interface could not be found, otherwise true</returns>
        public virtual void SetInterface(string Ifacename, IPAddress Ip, IPAddress Mask)
        {
            if (!Interfaces.Exists(I => I.Name.Equals(Ifacename)))
            {
                AddInterface(Ip, Mask, int.Parse(Ifacename.Replace(Interface.NamePrefix, "")));
                return;
            }
            Interfaces.Find(I => I.Name.Equals(Ifacename)).SetInterface(Ip, Mask);
        }

        /// <summary>
        /// Gets the new interface number.
        /// </summary>
        /// <returns>int: number for next interface</returns>
        protected int getNewInterfaceNumber()
        {
            int newInterface = 0;
            bool found = false;

            while (!found)
            {
                if (!Interfaces.Exists(I => I.Name.Equals(Interface.NamePrefix + newInterface)))
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
        public virtual bool HasInterface(string IfaceName)
        {
            foreach (Interface i in Interfaces)
            {
                if (i.Name == IfaceName)
                    return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Adds a connection.
        /// </summary>
        /// <param name="IfaceName">Name of the interface where the connection should be added.</param>
        /// <param name="Con">The connection to be added.</param>
        public void AddConnection(string IfaceName, Connection Con)
        {
            if(HasInterface(IfaceName)) Connections.Add(IfaceName, Con);
            else
            {
                Debug.Assert(false, "Interface: " + IfaceName + " nicht vorhanden, aber es soll eine Verbindung daran gesetzt werden.");
            }
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
            return Connections.ContainsKey(InterfaceName);
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
            // If parameter cannot be cast to Hardwarenode return false.
            Hardwarenode h = Obj as Hardwarenode;
            if (h == null) return false;

            // Return true if the fields match:
            return string.Equals(Name, h.Name);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="Other">The other.</param>
        /// <returns></returns>
        public bool Equals(Hardwarenode Other)
        {
            if (Other == null) return false;
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
            if (ReferenceEquals(A, B)) return true;

            // If not casted to object, the Hardwarenode == Operator gets used => endless loop => Stackoverflow
            // If I do not check B for null, Resharper complains about possible  NullReferenceException
            if ((object) A == null || ((object)B == null)) return false;
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
