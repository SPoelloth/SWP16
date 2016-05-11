using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.Elements.Base;

namespace NSA.View.Controls.NetworkView
{
  public partial class NetworkViewControl : UserControl
  {
    List<EditorElementBase> drawableElements = new List<EditorElementBase>();

    public NetworkViewControl()
    {
      drawableElements.Add(new EditorElementBase(new Point(10, 10)));
      drawableElements.Add(new EditorElementBase(new Point(200, 150)));
      InitializeComponent();
      Controls.AddRange(drawableElements.ToArray());
      DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.DrawRectangle(Pens.DodgerBlue, new Rectangle(0,0, Size.Width-1, Size.Height-1));
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      Invalidate();
      base.OnSizeChanged(e);
    }
  }
}
