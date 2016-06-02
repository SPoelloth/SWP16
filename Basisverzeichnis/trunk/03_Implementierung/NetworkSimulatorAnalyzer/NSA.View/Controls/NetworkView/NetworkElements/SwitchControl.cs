using System;
using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class SwitchControl : EditorElementBase, IConfigurable
    {
        public SwitchControl(Point location, string name) : base(location, name)
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public override Rectangle GetPortBoundsByID(int port)
        {
            throw new NotImplementedException();
        }
    }
}
