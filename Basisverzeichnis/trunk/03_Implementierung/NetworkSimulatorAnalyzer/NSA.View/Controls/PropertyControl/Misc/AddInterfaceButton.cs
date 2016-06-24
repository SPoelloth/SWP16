using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc
{
    /// <summary>
    /// Button for adding a new network interface
    /// </summary>
    public class AddInterfaceButton : Button
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AddInterfaceButton()
        {
            AutoSize = true;
            Text = "Schnittstelle hinzufügen";
            FontHeight = 12;
            DoubleBuffered = true;
        }

        /// <summary>
        /// Property for the button text
        /// </summary>
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
