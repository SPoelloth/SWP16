using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc {
    public class AddButton : Button {
        public AddButton()
        {
            Height = 25;
            Width = 25;
            Text = "+";
            FontHeight = 12;
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
