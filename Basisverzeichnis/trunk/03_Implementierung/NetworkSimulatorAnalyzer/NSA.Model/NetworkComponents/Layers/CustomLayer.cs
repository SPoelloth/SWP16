using System;
using System.Collections.Generic;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.NetworkComponents.Layers
{
    /// <summary>
    /// Custom-Layer
    /// </summary>
    /// <seealso cref="NSA.Model.NetworkComponents.ILayer" />
    public class CustomLayer : ILayer
    {
        private string name;
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLayer"/> class.
        /// </summary>
        /// <param name="N">The name.</param>
        /// <param name="I">The index.</param>
        public CustomLayer(string N, int I)
        {
            name = N;
            index = I;
        }

        /// <summary>
        /// Validates the layer while sending a packet.
        /// </summary>
        /// <param name="Destination">The Destination</param>
        /// <param name="CurrentNode">Current Node</param>
        /// <param name="ValInfo">Validation Info</param>
        /// <param name="Tags">Tags</param>
        /// <param name="LayerIndex">The Layer index</param>
        public void ValidateSend(Workstation Destination, Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, int LayerIndex)
        {
            if (ValInfo.Source != null && ValInfo.Source.Equals(CurrentNode))
            {
                Tags.Add(name, LayerIndex);
            }
        }

        /// <summary>
        /// Validates the layer while receiving a packet.
        /// </summary>
        /// <param name="CurrentNode"></param>
        /// <param name="ValInfo"></param>
        /// <param name="Tags"></param>
        /// <param name="Destination"></param>
        /// <param name="LayerIndex"></param>
        /// <returns>
        /// Boolean value indicating if the validation was successfull
        /// </returns>
        public bool ValidateReceive(Workstation CurrentNode, ValidationInfo ValInfo, Dictionary<string, object> Tags, Hardwarenode Destination, int LayerIndex)
        {
            if (CurrentNode.Equals(Destination) && Tags.ContainsKey(name) && LayerIndex == (int)Tags[name])
                Tags.Remove(name);
            if (CurrentNode.Equals(Destination) && Tags.ContainsKey(name) && LayerIndex != (int) Tags[name])
            {
                ValInfo.Res.ErrorId = Result.Errors.CustomLayerIndexError;
                ValInfo.Res.SendError = false;
                ValInfo.Res.LayerError = this;
                ValInfo.Res.Res = String.Format(Result.ResultStrings[(int)Result.Errors.CustomLayerIndexError], name);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        /// <returns>
        /// The Layername
        /// </returns>
        public string GetLayerName()
        {
            return name;
        }

        /// <summary>
        /// Sets the name of the layer.
        /// </summary>
        /// <param name="NewName">The new Name</param>
        /// <returns></returns>
        public bool SetLayerName(string NewName)
        {
            name = NewName;
            return true;
        }

        /// <summary>
        /// Gets the index of the layer.
        /// </summary>
        /// <returns>The index</returns>
        public int GetLayerIndex()
        {
            return index;
        }

        /// <summary>
        /// Sets the index of the layer.
        /// </summary>
        /// <param name="I">The Index.</param>
        public void SetLayerIndex(int I)
        {
            index = I;
        }
    }
}
