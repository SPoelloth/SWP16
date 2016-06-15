using System;
using NSA.View.Controls.PropertyControl.Misc;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class LayerstackConfigControl : ConfigControlBase
    {
        public LayerControl selectedLayerControl;
        public event Action LayerAdded;
        public event Action<LayerControl> LayerRemoved;
        public LayerstackConfigControl()
        {
            InitializeComponent();
        }

        public void AddLayer(string layerName)
        {
            LayerControl newLayerControl = new LayerControl(false);
            newLayerControl.LayerName = layerName;
            this.flpLayers.Controls.Add(newLayerControl);
        }

        public void AddCustomLayer(string layerName, string tag)
        {
            LayerControl newLayerControl = new LayerControl(true);
            newLayerControl.LayerName = layerName;
            newLayerControl.LayerTag = tag;
            newLayerControl.Selected += NewLayerControl_Selected;
            newLayerControl.NameChanged += NewLayerControl_NameChanged;
            newLayerControl.TagChanged += NewLayerControl_TagChanged;
            this.flpLayers.Controls.Add(newLayerControl);
        }

        private void NewLayerControl_TagChanged(string arg1, string arg2) {
            // TODO:
        }

        private void NewLayerControl_NameChanged(string arg1, string arg2) {
            // TODO:
        }

        private void NewLayerControl_Selected(LayerControl layerControl)
        {
            if (selectedLayerControl != null) selectedLayerControl.IsSelected = false;
            layerControl.IsSelected = true;
            selectedLayerControl = layerControl;
        }

        private void btAdd_Click(object sender, System.EventArgs e) {
            LayerAdded?.Invoke();
        }

        private void btDel_Click(object sender, System.EventArgs e) {
            if(selectedLayerControl.IsCustomLayer) LayerRemoved?.Invoke(selectedLayerControl);
        }

        private void btUp_Click(object sender, System.EventArgs e) {
            if (selectedLayerControl != null)
            {
                int currentIndex = flpLayers.Controls.GetChildIndex(selectedLayerControl);
                if (currentIndex > 0)
                {
                    int targetIndex = currentIndex - 1;
                    LayerControl targetControl = (LayerControl)flpLayers.Controls[targetIndex];
                    flpLayers.Controls.SetChildIndex(selectedLayerControl, targetIndex);
                    flpLayers.Controls.SetChildIndex(targetControl, currentIndex);
                }
            }
        }

        private void btDown_Click(object sender, System.EventArgs e) {
            if (selectedLayerControl != null) {
                int currentIndex = flpLayers.Controls.GetChildIndex(selectedLayerControl);
                if (currentIndex < flpLayers.Controls.Count - 1) {
                    int targetIndex = currentIndex + 1;
                    LayerControl targetControl = (LayerControl)flpLayers.Controls[targetIndex];
                    flpLayers.Controls.SetChildIndex(targetControl, currentIndex);
                    flpLayers.Controls.SetChildIndex(selectedLayerControl, targetIndex);
                }
            }
        }
    }
}
