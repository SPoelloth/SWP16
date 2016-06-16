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
        public event Action<string, string, int> LayerChanged;

        public LayerstackConfigControl()
        {
            InitializeComponent();
        }

        public void AddLayer(string LayerName)
        {
            LayerControl newLayerControl = new LayerControl(false);
            newLayerControl.LayerName = LayerName;
            this.flpLayers.Controls.Add(newLayerControl);
        }

        public void AddCustomLayer(string LayerName, string LayerTag, int Index)
        {
            LayerControl customLayerControl = new LayerControl(true);
            customLayerControl.LayerName = LayerName;
            customLayerControl.LayerTag = LayerTag;
            customLayerControl.Selected += CustomLayerControl_Selected;
            customLayerControl.NameChanged += CustomLayerControl_NameChanged;
            customLayerControl.TagChanged += CustomLayerControl_TagChanged;
            flpLayers.Controls.Add(customLayerControl);
            flpLayers.Controls.SetChildIndex(customLayerControl, Index);
        }

        private void CustomLayerControl_TagChanged(string LayerTag) {
            LayerChanged?.Invoke(SelectedLayerControl.LayerName, LayerTag, flpLayers.Controls.IndexOf(SelectedLayerControl));
        }

        private void CustomLayerControl_NameChanged(string LayerName) {
            LayerNameChanged?.Invoke(SelectedLayerControl.FormerName, LayerName);
            SelectedLayerControl.FormerName = LayerName;
        }

        private void CustomLayerControl_Selected(LayerControl LayerControl)
        {
            if (SelectedLayerControl != null) SelectedLayerControl.IsSelected = false;
            LayerControl.IsSelected = true;
            SelectedLayerControl = LayerControl;
        }

        private void btAdd_Click(object Sender, System.EventArgs E) {
            LayerAdded?.Invoke();
        }

        private void btDel_Click(object Sender, System.EventArgs E) {
            if(SelectedLayerControl.IsCustomLayer) LayerRemoved?.Invoke(SelectedLayerControl);
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
                    LayerChanged?.Invoke(SelectedLayerControl.LayerName, SelectedLayerControl.LayerTag, flpLayers.Controls.IndexOf(SelectedLayerControl));
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
                    LayerChanged?.Invoke(SelectedLayerControl.LayerName, SelectedLayerControl.LayerTag, flpLayers.Controls.IndexOf(SelectedLayerControl));
                }
            }
        }
    }
}
