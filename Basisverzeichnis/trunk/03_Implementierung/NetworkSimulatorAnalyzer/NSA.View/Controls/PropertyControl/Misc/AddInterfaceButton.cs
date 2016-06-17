using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc {
    public class AddInterfaceButton : Button {
        public AddInterfaceButton()
        {
            AutoSize = true;
            Text = "Schnittstelle hinzufügen";
            FontHeight = 12;
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
