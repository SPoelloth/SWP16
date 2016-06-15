﻿using System.Net;

namespace NSA.Model.NetworkComponents
{
    public class Interface
    {
        public string Name { get; private set; }
        public IPAddress IpAddress { get; set; }
        public IPAddress Subnetmask { get; set; }

        public const string NamePrefix = "eth";

        /// <summary>
        /// Initializes a new instance of the <see cref="Interface" /> class.
        /// </summary>
        /// <param name="Ip">The ip address of the interface.</param>
        /// <param name="Mask">The corresponding subnetmask.</param>
        /// <param name="Number">The number (e.g. 0 for eth0).</param>
        public Interface(IPAddress Ip, IPAddress Mask, int Number)
        {
            Name = NamePrefix+Number;
            IpAddress = Ip;
            Subnetmask = Mask;
        }

        /// <summary>
        /// Sets the interface.
        /// </summary>
        /// <param name="Ip">The new ip.</param>
        /// <param name="Mask">The new subnetmask.</param>
        public void SetInterface(IPAddress Ip, IPAddress Mask)
        {
            IpAddress = Ip;
            Subnetmask = Mask;
        }
    }
}
