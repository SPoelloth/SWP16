using System;
using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView
{
    public partial class LabelControl : UserControl
    {
        private EditorElementBase parentElement;

        public LabelControl(EditorElementBase element)
        {
            element.LocationChanged += Element_LocationChanged;
            parentElement = element;
            InitializeComponent();
            textBox1.TextChanged += Label1_TextChanged;
            textBox1.KeyDown += TextBox1_KeyDown;
            textBox1.LostFocus += TextBox1_LostFocus;
            textBox1.Text = element.Name;
            Height = textBox1.Height;
            Label1_TextChanged(parentElement, null);
        }

        private void TextBox1_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) textBox1.Text = "Name";
        }

        private void Element_LocationChanged(object sender, EventArgs e)
        {
            Location = new Point(parentElement.Location.X + parentElement.Width / 2 - Width / 2 + 3, parentElement.Location.Y - textBox1.Height);
        }

        private void Label1_TextChanged(object sender, EventArgs e)
        {
            var measure = TextRenderer.MeasureText(textBox1.Text, textBox1.Font);
            Width = textBox1.Width = measure.Width;
            Height = measure.Height;
            parentElement.Name = textBox1.Text;
            Element_LocationChanged(parentElement, null);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Enabled = false;
                Enabled = true;
                //this is to remove focus from the textbox
            }
        }

    }
}
