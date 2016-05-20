using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class SwitchControl : EditorElementBase, IConfigurable
    {
        public SwitchControl(Point location) : base(location, "SwitchControl")
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
