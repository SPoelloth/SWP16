using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class WorkstationControl : EditorElementBase, IConfigurable
    {
        public WorkstationControl(Point location) : base(location)
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
