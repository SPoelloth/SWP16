using System;
using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc
{
    /// <summary>
    /// Control representing a layer in the <see cref="NSA.View.Controls.PropertyControl.ConfigControls.LayerstackConfigControl"/>.
    /// </summary>
    public partial class LayerControl : UserControl
    {
        /// <summary>
        /// Flag indicating whether the represented layer is a custom layer.
        /// </summary>
        public bool IsCustomLayer = false;
        /// <summary>
        /// Is fired when the layer control is selected.
        /// </summary>
        public event Action<LayerControl> Selected;
        /// <summary>
        /// Is fired when the name of the layer has changed.
        /// </summary>
        public event Action<string> NameChanged;
        /// <summary>
        /// The former name of the control, when renamed.
        /// </summary>
        public string FormerName = "";

        /// <summary>
        /// The name of the layer.
        /// </summary>
        public string LayerName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        /// <summary>
        /// Flag indicating whether the control is currently selected.
        /// </summary>
        public bool IsSelected
        {
            get { return selected; }
            set
            {
                if (!IsCustomLayer || value == selected)
                {
                    return;
                }
                selected = value;
            }
        }
        private bool selected;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="LayerName">Name of the layer.</param>
        /// <param name="CustomLayer">Flag indicating whether the represented layer is a custom layer.</param>
        public LayerControl(string LayerName, bool CustomLayer = false)
        {
            InitializeComponent();
            // FIX for an issue where the textbox text was cut off
            textBoxName.Multiline = true;
            textBoxName.MinimumSize = new Size(0, 18);
            textBoxName.Size = new Size(textBoxName.Size.Width, 18);
            textBoxName.Multiline = false;

            textBoxName.LostFocus += TextBoxName_LostFocus;
            ;
            this.LayerName = LayerName;
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

        private void TextBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxName.Enabled = false;
                //this is to remove focus from the textbox
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            NameChanged?.Invoke(textBoxName.Text);
        }

        private void TextBoxName_LostFocus(object sender, EventArgs e)
        {
            textBoxName.Enabled = false;
        }

        private void textBoxName_Click(object sender, EventArgs e)
        {
            if (IsCustomLayer)
            {
                textBoxName.Enabled = true;
                textBoxName.Focus();
            }
            IsSelected = true;
            Refresh();
        }

        private void LayerControl_Click(object sender, EventArgs e)
        {
            if (IsCustomLayer)
            {
                textBoxName.Enabled = true;
                textBoxName.Focus();
            }
            IsSelected = true;
            Refresh();
        }

        /// <summary>
        /// Overrides the OnPain method for selection display purposes.
        /// </summary>
        /// <param name="e">The paint event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (IsSelected)
            {
                e.Graphics.DrawRectangle(Pens.DodgerBlue,
                    0, 0, Width - 1, Height - 1);
            }
        }
    }
}
