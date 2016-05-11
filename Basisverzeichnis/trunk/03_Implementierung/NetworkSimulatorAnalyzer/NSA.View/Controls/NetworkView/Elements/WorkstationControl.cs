using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.Elements.Base;

namespace NSA.View.Controls.NetworkView.Elements
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
