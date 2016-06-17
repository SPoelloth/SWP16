using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc {
    public class AddRouteButton : Button {
        public AddRouteButton()
        {
            AutoSize = true;
            Text = "Route hinzufügen";
            FontHeight = 12;
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
