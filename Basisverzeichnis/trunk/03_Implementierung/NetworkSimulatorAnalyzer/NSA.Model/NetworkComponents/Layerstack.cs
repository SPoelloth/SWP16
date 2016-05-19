using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    public class Layerstack
    {
        private List<ILayer> layers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Layerstack"/> class.
        /// </summary>
        public Layerstack()
        {
            layers = new List<ILayer>();
        }

        /// <summary>
        /// Adds a layer to the stack.
        /// </summary>
        /// <param name="lay">The layer to be added.</param>
        public void AddLayer(ILayer lay)
        {
            layers.Add(lay);
        }

        /// <summary>
        /// Removes a layer from the stack.
        /// </summary>
        /// <param name="lay">The layer to be removed.</param>
        public void RemoveLayer(ILayer lay)
        {
            layers.Remove(lay);
        }

        /// <summary>
        /// Returns the size of the layerstack.
        /// </summary>
        /// <returns>The size</returns>
        public int GetSize()
        {
            return layers.Count;
        }

        /// <summary>
        /// Returns the layer at the index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The layer</returns>
        public ILayer GetLayer(int index)
        {
            return layers[index];
        }
    }
}
