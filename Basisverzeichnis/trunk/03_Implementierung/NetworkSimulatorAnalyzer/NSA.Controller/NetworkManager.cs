using System;
using System.Collections.Generic;
using NSA.Model.BusinessLogic;
using NSA.Model.NetworkComponents;

namespace NSA.Controller
{
	class NetworkManager
	{
		private Network network;

		// Default constructor:
		public NetworkManager()
		{
			createConfigControls();
		}

		// Constructor:
		public NetworkManager(Network network)
		{
			this.network = network;
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

		public void CreateHardwareNode(String name, Enum typ)
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
