﻿using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    public class PresentationLayer : ILayer
    {
        public bool ValidateReceive(Workstation currentNode, ValidationInfo valInfo, Dictionary<string, object> Tags, Hardwarenode destination)
        {
            return true;
        }

        public string GetLayerName()
        {
            return "Darstellungsschicht";
        }

        public void ValidateSend(Workstation destination, Workstation currentNode, ValidationInfo valInfo, Dictionary<string, object> Tags)
        {
            
        }
    }
}
