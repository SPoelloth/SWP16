using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class ApplicationLayer : ILayer
    {
        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination)
        {
            return true;
        }

        public string GetLayerName()
        {
            return "Anwendungsschicht";
        }

        public bool SetLayerName(string NewName)
        {
            return false;
        }

        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags)
        {
            
        }
    }
}
