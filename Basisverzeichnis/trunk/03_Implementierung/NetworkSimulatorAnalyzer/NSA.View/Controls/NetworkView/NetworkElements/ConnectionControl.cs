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
        private const int LineWidth = 3;

        Point Point1, Point2;
        Control myParent;

        public ConnectionControl(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
            DoubleBuffered = true;
            ZIndex = ZIndexStart++;
            CalculateSize();
            InitializeComponent();
        }

        public void SetPoints(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
            CalculateSize();
            Invalidate();
        }

        private void CalculateSize()
        {
            Bounds = Rectangle.Union(new Rectangle(Point1.X, Point1.Y, LineWidth, LineWidth), new Rectangle(Point2.X, Point2.Y, LineWidth, LineWidth));
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics;
            g.FillRectangle(Brushes.Black, ClientRectangle);
        }
    }
}
