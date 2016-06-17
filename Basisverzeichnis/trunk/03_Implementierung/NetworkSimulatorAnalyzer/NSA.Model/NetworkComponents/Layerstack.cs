using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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
        /// <param name="name">The name.</param>
        /// <exception cref="System.InvalidOperationException">Layer with the name:  + name +  does not exist.</exception>
        public void RemoveLayer(string name)
        {
            foreach (ILayer l in layers)
            {
                if (l.GetLayerName() != name) continue;
                layers.Remove(l);
                return;
            }
            throw new InvalidOperationException("Layer with the name: " + name + " does not exist.");
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

        /// <summary>
        /// Inserts at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="layer">The layer.</param>
        public void InsertAt(int index, ILayer layer)
        {
            layers.Insert(index, layer);
        }

        /// <summary>
        /// Sets the index.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="newIndex">The new index.</param>
        public void SetIndex(string name, int newIndex)
        {
            ILayer layer = null;
            foreach (ILayer l in layers)
            {
                if (l.GetLayerName() == name)
                {
                    layer = l;
                    break;
                }
            }
            if (layer != null)
            {
                layers.Remove(layer);
                InsertAt(newIndex, layer);
            }
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <returns>True if it worked. False if the newName is already taken by another layer or when there is no layer with the old name</returns>
        public bool SetName(string oldName, string newName)
        {
            foreach (ILayer l in layers)
            {
                if (l.GetLayerName() == newName)
                    return false;
            }
            foreach (ILayer l in layers)
            {
                if (l.GetLayerName() == oldName)
                {
                    return l.SetLayerName(newName);
                }
            }
            return false;
        }

        /// <summary>
        /// Gets all layers.
        /// </summary>
        /// <returns></returns>
        public List<ILayer> GetAllLayers()
        {
            return layers.ToList();
        }

        /// <summary>
        /// Determines whether the name is taken or not.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool IsNameTaken(string name)
        {
            foreach (ILayer l in layers)
            {
                if (l.GetLayerName() == name)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Creates a new unique name.
        /// </summary>
        /// <returns></returns>
        public string CreateUniqueName()
        {
            for (int i = 1; ; i++)
            {
                string name = $"Neues Layer {i}";
                if (layers.All(l => l.GetLayerName() != name)) return name;
            }
        }
    }
}
