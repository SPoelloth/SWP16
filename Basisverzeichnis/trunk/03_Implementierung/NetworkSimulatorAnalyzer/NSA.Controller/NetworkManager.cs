using System;
using NSA.Model.NetworkComponents;

namespace NSA.Controller
{
    internal class NetworkManager
    {
        #region Singleton

        private static NetworkManager instance = null;
        private static readonly object padlock = new object();

        public static NetworkManager Instance {
            get {
                lock (padlock) {
                    if (instance == null) {
                        instance = new NetworkManager();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton

        public Network Network { get; }

        // Default constructor:
        NetworkManager()
        {
            CreateConfigControls();
        }

        //// Constructor:
        //public NetworkManager(Network network)
        //{
        //    Network = network;
        //    CreateConfigControls();
        //}

        private static void CreateConfigControls()
        {

        }

        public void OnHardwarenodeSelected()
        {

        }

        public void OnInterfaceChanged()
        {

        }

        public void OnRouteChanged()
        {

        }

        public void OnGatewayChanged()
        {

        }

        public void CreateHardwareNode(string name, Enum typ)
        {

        }

        public void CreateConnection(string start, string end)
        {
            Hardwarenode A = GetHardwarenodeByName(start);
            Hardwarenode B = GetHardwarenodeByName(end);
            Connection newConnection = new Connection(A, B);
        }

        public void RemoveHardwarenode(string name)
        {

        }

        public void RemoveConnection(string name)
        {

        }

        public Hardwarenode GetHardwarenodeByName(string name)
        {
            return this.Network?.GetHardwarenodeByName(name);
        }
    }
}
