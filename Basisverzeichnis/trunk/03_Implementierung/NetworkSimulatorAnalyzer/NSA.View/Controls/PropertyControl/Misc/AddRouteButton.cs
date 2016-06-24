using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc
{
    /// <summary>
    /// Button for adding a route.
    /// </summary>
    public class AddRouteButton : Button
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AddRouteButton()
        {
            AutoSize = true;
            Text = "Route hinzufügen";
            FontHeight = 12;
            DoubleBuffered = true;
        }

        /// <summary>
        /// Property for the button text.
        /// </summary>
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
