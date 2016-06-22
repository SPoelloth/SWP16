using System;
using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView
{
    public partial class LabelControl : EditorElementBase
    {
        public event Action<string, string> NameChanged;

        public new static int ZIndexStart = 1000;

        private EditorElementBase parentElement;

        [Obsolete("Dont use this. This is for the Designer only.")]
        public LabelControl()
        {
            InitializeComponent();
            textBox.Text = "Text";
            Controls.Remove(textBox);
        }

        public LabelControl(EditorElementBase element)
        {
            ZIndex = ZIndexStart++;
            element.LocationChanged += Element_LocationChanged;
            parentElement = element;
            InitializeComponent();
            textBox.TextChanged += TextBox_TextChanged;
            textBox.KeyDown += TextBox1_KeyDown;
            textBox.LostFocus += TextBox1_LostFocus;
            textBox.Text = element.Name;
            label1.Text = element.Name;
            label1.Click += (s, e) => TextboxEnable(true);
            Click += LabelControl_Click;
            Height = textBox.Height;
            element.Disposed += Element_Disposed;
            textBox.ForeColor = Color.Red;
            var isLinux = Type.GetType("Mono.Runtime") != null;
            textBox.Location = new Point(textBox.Location.X + 3, textBox.Location.Y);
            label1.Location = new Point(label1.Location.X + (isLinux ? 3 : 0), label1.Location.Y);
            TextBox_TextChanged(parentElement, null);
        }

        private void LabelControl_Click(object sender, EventArgs e)
        {
            TextboxEnable(true);
        }

        private void Element_Disposed(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
            label1.Dispose();
            Dispose();
        }

        private void TextBox1_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Name";
                label1.Text = "Name";
            }
            var text = textBox.Text.Trim(' ');
            textBox.Text = text;
            label1.Text = text;
            TextBox_TextChanged(this, null);
            NameChanged?.Invoke(parentElement.Name, textBox.Text);
            parentElement.Name = textBox.Text;
            parentElement.Selected?.Invoke(parentElement);
        }

        private void Element_LocationChanged(object sender, EventArgs e)
        {
            Location = new Point(parentElement.Location.X + parentElement.Width / 2 - Width / 2, parentElement.Location.Y - textBox.Height);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var measure = TextRenderer.MeasureText(textBox.Text.Replace(' ', '|'), isTextboxEnabled ? textBox.Font : label1.Font);
            Width = measure.Width;
            textBox.Width = measure.Width;
            Height = measure.Height;
            label1.Size = Size;
            Element_LocationChanged(parentElement, null);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextboxEnable(false);
            }
        }

        bool isTextboxEnabled = false;
        private void TextboxEnable(bool isEnabled)
        {
            isTextboxEnabled = isEnabled;
            if (isEnabled)
            {
                if (!Controls.Contains(label1)) return;
                Controls.Remove(label1);
                Controls.Add(textBox);
                textBox.Focus();
                parentElement.Selected?.Invoke(parentElement);
            }
            else
            {
                if (!Controls.Contains(textBox)) return;
                Controls.Remove(textBox);
                Controls.Add(label1);
                label1.Text = textBox.Text;
                TextBox1_LostFocus(textBox, null);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

        }
    }
}
