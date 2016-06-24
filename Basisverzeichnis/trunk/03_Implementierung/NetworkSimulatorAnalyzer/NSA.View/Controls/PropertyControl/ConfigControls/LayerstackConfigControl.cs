using System;
using NSA.View.Controls.PropertyControl.Misc;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    /// <summary>
    /// Control for editing the layer stack of the current hardwarenode.
    /// </summary>
    public partial class LayerstackConfigControl : ConfigControlBase
    {
        private LayerControl selectedLayerControl;
        /// <summary>
        /// Is fired when a layer is added.
        /// </summary>
        public event Action LayerAdded;
        /// <summary>
        /// Is fired when a layer is removed.
        /// </summary>
        public event Action<LayerControl> LayerRemoved;
        /// <summary>
        /// Is fired when the name of a layer has changed.
        /// </summary>
        public event Action<string, string> LayerNameChanged;
        /// <summary>
        /// Is fired when the index of a layer has changed.
        /// </summary>
        public event Action<string, int> LayerIndexChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LayerstackConfigControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds a layer.
        /// </summary>
        /// <param name="LayerName">Name of the layer</param>
        /// <param name="IsCustom">Flag indicating whether the layer is a custom layer</param>
        public void AddLayer(string LayerName, bool IsCustom)
        {
            if (IsCustom)
            {
                var newLayerControl = new LayerControl(LayerName, true);
                newLayerControl.Selected += CustomLayerControl_Selected;
                newLayerControl.NameChanged += CustomLayerControl_NameChanged;
                flpLayers.Controls.Add(newLayerControl);
                flpLayers.Height += newLayerControl.Height + 6;
            } else
            {
                flpLayers.Controls.Add(new LayerControl(LayerName));
            }
            Refresh();
        }

        private void CustomLayerControl_NameChanged(string LayerName)
        {
            LayerNameChanged?.Invoke(selectedLayerControl.FormerName, LayerName);
            selectedLayerControl.FormerName = LayerName;
        }

        private void CustomLayerControl_Selected(LayerControl LayerControl)
        {
            if (selectedLayerControl != null)
            {
                selectedLayerControl.IsSelected = false;
                selectedLayerControl.Refresh();
            }
            LayerControl.IsSelected = true;
            LayerControl.Refresh();
            selectedLayerControl = LayerControl;
        }

        private void btAdd_Click(object Sender, System.EventArgs E)
        {
            LayerAdded?.Invoke();
        }

        private void btDel_Click(object Sender, System.EventArgs E)
        {
            if (selectedLayerControl == null) return;
            LayerRemoved?.Invoke(selectedLayerControl);
            int layerIndex = flpLayers.Controls.IndexOf(selectedLayerControl);
            flpLayers.Height -= selectedLayerControl.Height + 6;
            flpLayers.Controls.Remove(selectedLayerControl);
            int currentIndex = 0;
            foreach (LayerControl lc in flpLayers.Controls)
            {
                if (layerIndex <= currentIndex)
                {
                    LayerIndexChanged?.Invoke(lc.LayerName, currentIndex);
                }
                currentIndex++;
            }
            Refresh();
        }

        private void btUp_Click(object Sender, System.EventArgs E)
        {
            if (selectedLayerControl != null)
            {
                int currentIndex = flpLayers.Controls.GetChildIndex(selectedLayerControl);
                if (currentIndex > 0)
                {
                    int targetIndex = currentIndex - 1;
                    LayerControl targetControl = (LayerControl) flpLayers.Controls[targetIndex];
                    flpLayers.Controls.SetChildIndex(selectedLayerControl, targetIndex);
                    flpLayers.Controls.SetChildIndex(targetControl, currentIndex);
                    LayerIndexChanged?.Invoke(selectedLayerControl.LayerName,
                        flpLayers.Controls.IndexOf(selectedLayerControl));
                }
            }
        }

        private void btDown_Click(object Sender, System.EventArgs E)
        {
            if (selectedLayerControl != null)
            {
                int currentIndex = flpLayers.Controls.GetChildIndex(selectedLayerControl);
                if (currentIndex < flpLayers.Controls.Count - 1)
                {
                    int targetIndex = currentIndex + 1;
                    LayerControl targetControl = (LayerControl) flpLayers.Controls[targetIndex];
                    flpLayers.Controls.SetChildIndex(targetControl, currentIndex);
                    flpLayers.Controls.SetChildIndex(selectedLayerControl, targetIndex);
                    LayerIndexChanged?.Invoke(selectedLayerControl.LayerName,
                        flpLayers.Controls.IndexOf(selectedLayerControl));
                }
            }
        }
    }
}