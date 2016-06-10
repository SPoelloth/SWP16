using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class ConnectionControl : EditorElementBase
    {
        public new static int ZIndexStart = 10000;
        private const int LineWidth = 3;

        Point Point1, Point2;

        public ConnectionControl(string name, Point point1, Point point2)
        {
            Name = name;
            Point1 = point1;
            Point2 = point2;
            // ReSharper disable once VirtualMemberCallInConstructor
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
            g.Clear(IsSelected ? Color.Red : Color.Black);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
        }
    }
}
