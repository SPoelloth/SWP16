using System.Collections.Generic;
using NSA.Model.NetworkComponents;

namespace NSA.Model.BusinessLogic
{
	public class Project
    {
        // todo: decide if we need this class.
        // if not, then decide which type the member networkRepresantion
        // should have
        private class NetworkRepresantation
        {
        }

        private Network network;
        private NetworkRepresantation networkRepresantation;
        private List<Simulation> simulations;
    }
}
