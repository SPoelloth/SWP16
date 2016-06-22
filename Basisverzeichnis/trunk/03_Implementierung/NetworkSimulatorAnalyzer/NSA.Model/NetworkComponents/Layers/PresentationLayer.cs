using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class PresentationLayer : ILayer
    {
        private int index;

        public PresentationLayer(int I)
        {
            index = I;
        }

        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination, int LayerIndex)
        {
            return true;
        }

        public string GetLayerName()
        {
            return "Darstellungsschicht";
        }

        public bool SetLayerName(string NewName)
        {
            return false;
        }

        public int GetLayerIndex()
        {
            return index;
        }

        public void SetLayerIndex(int I)
        {
            index = I;
        }

        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, int LayerIndex)
        {
            
        }
    }
}
