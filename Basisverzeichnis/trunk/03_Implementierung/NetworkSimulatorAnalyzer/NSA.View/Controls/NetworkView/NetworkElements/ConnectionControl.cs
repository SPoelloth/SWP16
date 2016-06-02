using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class ConnectionControl : EditorElementBase
    {
        public new static int ZIndexStart = 10000;

        Point Point1, Point2;

        public ConnectionControl(Point point1, Point point2, Control parent)
        {
            Parent = parent;
            Point1 = point1;
            Point2 = point2;
            DoubleBuffered = true;
            ZIndex = ZIndexStart++;
            CalculateSize();
            InitializeComponent();
        }

        private void CalculateSize()
        {
            var boundingRectangle = Rectangle.Union(new Rectangle(Point1, new Size(1, 1)), new Rectangle(Point2, new Size(1, 1)));
            Location = boundingRectangle.Location;
            Size = boundingRectangle.Size;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics;
            g.Clear(Color.Black);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

        }
    }
}
