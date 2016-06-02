using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class ComputerControl : WorkstationControl
    {
        public ComputerControl(Point location, string name) : base(location, name)
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
