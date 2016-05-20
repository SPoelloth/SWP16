using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class RouterControl : WorkstationControl
    {
        public RouterControl(Point location) : base(location, "RouterControl")
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
