using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Layerstack
    {
        private List<ILayer> layers;


        public void AddLayer(ILayer lay)
        {
            layers.Add(lay);
        }

        public void RemoveLayer(ILayer lay)
        {
        }
    }
}
