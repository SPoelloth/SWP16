using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class ApplicationLayer : ILayer
    {
        public bool ValidateReceive(Workstation currentNode, ValidationInfo valInfo)
        {
            return true;
        }

        public void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo)
        {
            
        }
    }
}
