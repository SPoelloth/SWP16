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

        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags)
        {
            if(ValInfo.Source != null && ValInfo.Source.Equals(CurrentNode))
                Tags.Add(name, name);
        }

        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination)
        {
            if (CurrentNode.Equals(Destination) && Tags.ContainsKey(name))
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
