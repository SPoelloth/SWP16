using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.Elements.Base;

namespace NSA.View.Controls.NetworkView.Elements
{
  public partial class SwitchControl : EditorElementBase, IConfigurable
  {
    public SwitchControl(Point location) : base(location)
    {
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);
    }
  }
}
