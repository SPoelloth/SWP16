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

        WorkstationControl Element1, Element2;
        int Port1, Port2;

        public ConnectionControl(NetworkViewControl parent, WorkstationControl element1, int port1, WorkstationControl element2, int port2)
        {
            ZIndex = ZIndexStart++;
            //Parent = parent;
            Element1 = element1;
            Element2 = element2;
            Port1 = port1;
            Port2 = port2;
            CalculateSize();
            DoubleBuffered = true;
            InitializeComponent();
            Element1.LocationChanged += Element_LocationChanged;
            Element2.LocationChanged += Element_LocationChanged;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnParentBackColorChanged(e);
        }

        private void Element_LocationChanged(object sender, EventArgs e)
        {
            CalculateSize();
            Invalidate();
        }

        private void CalculateSize()
        {
            var startElement = Element1.GetPortBoundsByID(Port1);
            var targetElement = Element2.GetPortBoundsByID(Port2);
            startElement.Offset(Element1.Location);
            targetElement.Offset(Element2.Location);
            var boundingRectangle = Rectangle.Union(startElement, targetElement);
            Location = boundingRectangle.Location;
            Size = boundingRectangle.Size;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.FillRectangle(Brushes.Red, new Rectangle(new Point(), Size));
            var startElement = Element1.GetPortBoundsByID(Port1);
            var targetElement = Element2.GetPortBoundsByID(Port2);
            startElement.Offset(Element1.Location);
            targetElement.Offset(Element2.Location);

            var a = new Point(startElement.X + startElement.Width - Location.X, startElement.Y + startElement.Height / 2 - Location.Y);
            var b = new Point(targetElement.X - Location.X, targetElement.Y + targetElement.Height / 2 - Location.Y);

            Point am = new Point((a.X + b.X) / 2, a.Y);
            Point bm = new Point((a.X + b.X) / 2, b.Y);

            g.DrawLine(Pens.Black, a, am);
            g.DrawLine(Pens.Black, am, bm);
            g.DrawLine(Pens.Black, bm, b);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

        }
    }
}
