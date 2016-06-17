using System;
using NSA.View.Controls.PropertyControl.Misc;

namespace NSA.View.Controls.PropertyControl.ConfigControls
{
    public partial class LayerstackConfigControl : ConfigControlBase
    {
        public LayerControl SelectedLayerControl;
        public event Action LayerAdded;
        public event Action<LayerControl> LayerRemoved;
        public event Action<string, string> LayerNameChanged;
        public event Action<string, int> LayerIndexChanged;

        public LayerstackConfigControl()
        {
            InitializeComponent();
        }

        public void AddLayer(string LayerName, bool IsCustom)
        {
            LayerControl newLayerControl;
            if (IsCustom)
            {
                newLayerControl = new LayerControl(LayerName, true);
                newLayerControl.Selected += CustomLayerControl_Selected;
                newLayerControl.NameChanged += CustomLayerControl_NameChanged;
            }
            else
            {
                newLayerControl = new LayerControl(LayerName);
            }
            flpLayers.Controls.Add(newLayerControl);
        }

        private void CustomLayerControl_NameChanged(string LayerName) {
            LayerNameChanged?.Invoke(SelectedLayerControl.FormerName, LayerName);
            SelectedLayerControl.FormerName = LayerName;
        }

        private void CustomLayerControl_Selected(LayerControl LayerControl)
        {
            if (SelectedLayerControl != null) SelectedLayerControl.IsSelected = false;
            LayerControl.IsSelected = true;
            LayerControl.Refresh();
            SelectedLayerControl = LayerControl;
        }

        private void btAdd_Click(object Sender, System.EventArgs E) {
            LayerAdded?.Invoke();
        }

        private void btDel_Click(object Sender, System.EventArgs E) {
            LayerRemoved?.Invoke(SelectedLayerControl);
            int layerIndex = flpLayers.Controls.IndexOf(SelectedLayerControl);
            flpLayers.Controls.Remove(SelectedLayerControl);
            int currentIndex = 0;
            foreach (LayerControl lc in flpLayers.Controls)
            {
                if (layerIndex <= currentIndex)
                {
                    LayerIndexChanged?.Invoke(lc.LayerName, currentIndex);
                }
                currentIndex++;
            }
        }

        private void btUp_Click(object Sender, System.EventArgs E) {
            if (SelectedLayerControl != null)
            {
                int currentIndex = flpLayers.Controls.GetChildIndex(SelectedLayerControl);
                if (currentIndex > 0)
                {
                    int targetIndex = currentIndex - 1;
                    LayerControl targetControl = (LayerControl)flpLayers.Controls[targetIndex];
                    flpLayers.Controls.SetChildIndex(SelectedLayerControl, targetIndex);
                    flpLayers.Controls.SetChildIndex(targetControl, currentIndex);
                    LayerIndexChanged?.Invoke(SelectedLayerControl.LayerName, flpLayers.Controls.IndexOf(SelectedLayerControl));
                }
            }
        }

        private void btDown_Click(object Sender, System.EventArgs E) {
            if (SelectedLayerControl != null) {
                int currentIndex = flpLayers.Controls.GetChildIndex(SelectedLayerControl);
                if (currentIndex < flpLayers.Controls.Count - 1) {
                    int targetIndex = currentIndex + 1;
                    LayerControl targetControl = (LayerControl)flpLayers.Controls[targetIndex];
                    flpLayers.Controls.SetChildIndex(targetControl, currentIndex);
                    flpLayers.Controls.SetChildIndex(SelectedLayerControl, targetIndex);
                    LayerIndexChanged?.Invoke(SelectedLayerControl.LayerName, flpLayers.Controls.IndexOf(SelectedLayerControl));
                }
            }
        }
    }
}
