using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class ConnectionControl : EditorElementBase
    {
        public ConnectionControl(Point location) : base(location, "Connection")
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
