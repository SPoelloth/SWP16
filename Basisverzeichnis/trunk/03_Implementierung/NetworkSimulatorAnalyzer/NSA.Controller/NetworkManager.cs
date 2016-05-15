using System;
using NSA.Model.NetworkComponents;

namespace NSA.Controller
{
    internal class NetworkManager
    {
        public Network Network1 { get; }

        // Default constructor:
        public NetworkManager()
        {
            CreateConfigControls();
        }

        // Constructor:
        public NetworkManager(Network network)
        {
            Network1 = network;
            CreateConfigControls();
        }

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

        }

        public void RemoveHardwarenode(string name)
        {

        }

        public void RemoveConnection(string name)
        {

        }
    }
}
