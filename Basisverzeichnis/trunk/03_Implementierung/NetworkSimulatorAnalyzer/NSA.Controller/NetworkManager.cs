using System;
using NSA.Model.NetworkComponents;

namespace NSA.Controller
{
    class NetworkManager
    {
        public Network Network1 { get; }

        // Default constructor:
        public NetworkManager()
        {
            createConfigControls();
        }

        // Constructor:
        public NetworkManager(Network network)
        {
            Network1 = network;
            createConfigControls();
        }

        private void createConfigControls()
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

        public void CreateConnection(String start, String end)
        {

        }

        public void RemoveHardwarenode(String name)
        {

        }

        public void RemoveConnection(String name)
        {

        }
    }
}
