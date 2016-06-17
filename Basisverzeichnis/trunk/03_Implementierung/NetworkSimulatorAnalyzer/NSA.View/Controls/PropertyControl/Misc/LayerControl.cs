using System;
using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc {
    public partial class LayerControl : UserControl {
        public bool IsCustomLayer = false;
        public event Action<LayerControl> Selected;
        public event Action<string> NameChanged;
        public string FormerName = "";

        public string LayerName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        public bool IsSelected
        {
            get { return selected; }
            set {
                if (!IsCustomLayer || value == selected)
                {
                    return;
                }
                selected = value;
            }
        }
        private bool selected;

        public LayerControl(bool CustomLayer = false) {
            InitializeComponent();
            IsCustomLayer = CustomLayer;
            if (CustomLayer)
            {
                FormerName = LayerName;
            }
        }

        private void LayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            Selected?.Invoke(this);
        }

        private void TextBoxName_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                textBoxName.Enabled = false;
                //this is to remove focus from the textbox
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e) {
            NameChanged?.Invoke(textBoxName.Text);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (IsSelected)
            {
                e.Graphics.DrawRectangle(Pens.DodgerBlue,
                    0,0,Width-1,Height-1);
            }
        }

        private void textBoxName_Click(object sender, EventArgs e)
        {
            if (IsCustomLayer) textBoxName.Enabled = true;
            IsSelected = true;
        }
    }
}
