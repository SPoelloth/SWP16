using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class CustomLayer : ILayer
    {
        private string name;

        public CustomLayer(string n)
        {
            name = n;
        }

        public void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo, Dictionary<string, object> Tags)
        {
            if(valInfo.Source != null && valInfo.Source.Equals(currentNode))
                Tags.Add(name, name);
        }

        public bool ValidateReceive(Workstation currentNode, ValidationInfo valInfo, Dictionary<string, object> Tags, Hardwarenode destination)
        {
            if (currentNode.Equals(destination) && Tags.ContainsKey(name))
                Tags.Remove(name);
            return true;
        }

        public string GetLayerName()
        {
            return name;
        }
    }
}
