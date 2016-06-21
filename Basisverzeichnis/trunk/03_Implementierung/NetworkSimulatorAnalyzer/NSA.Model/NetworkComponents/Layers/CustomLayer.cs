using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class CustomLayer : ILayer
    {
        private string name;

        public CustomLayer(string N)
        {
            name = N;
        }

        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, int LayerIndex)
        {
            if (ValInfo.Source != null && ValInfo.Source.Equals(CurrentNode))
            {
                Tags.Add(name, LayerIndex);
            }
        }

        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination, int LayerIndex)
        {
            if (CurrentNode.Equals(Destination) && Tags.ContainsKey(name) && LayerIndex == (int)Tags[name])
                Tags.Remove(name);
            return true;
        }

        public string GetLayerName()
        {
            return name;
        }

        public bool SetLayerName(string NewName)
        {
            name = NewName;
            return true;
        }
    }
}
