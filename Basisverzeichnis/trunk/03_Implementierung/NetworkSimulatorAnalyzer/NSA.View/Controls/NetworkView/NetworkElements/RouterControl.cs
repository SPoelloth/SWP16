using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class RouterControl : EditorElementBase, IConfigurable, ISimulationTarget
    {
        #region Parameters
        #region Colors
        #region Workstation Colors
        private Pen borderPen = Pens.Black;
        private Pen separatorPen = new Pen(Color.DimGray, 2);
        private Pen selectedPen = Pens.Red;
        private LinearGradientBrush backgroundBrush = new LinearGradientBrush(new Point(), new Point(3, 0), Color.FromArgb(255,255,255), Color.FromArgb(175, 175, 175));
        private LinearGradientBrush backgroundGradientBrush = new LinearGradientBrush(new Point(), new Point(0, 200), Color.FromArgb(20, 0, 0, 0), Color.FromArgb(200, 0, 0, 0));
        private Brush dotBrush = new SolidBrush(Color.FromArgb(0, 0, 255));
        #endregion Workstation Colors
        #region Port Colors
        private Brush portBackgroundBrush = new SolidBrush(Color.FromArgb(200,200,200));
        private Brush portHighlightBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
        private LinearGradientBrush portOverlayBrush = new LinearGradientBrush(new Point(), new Point(5, 0), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(100, 0, 0, 0));
        private Pen portPins = Pens.White;
        #endregion Port Colors
        #endregion Colors
        #region Port Parameter

        int portsize = 10;
        const int portdistance = 2;
        const int portOffsetY = 30;
        const int portPinsLength = 10 / 2 - 1;
        const int portPinCount = 4;

        #endregion Port Parameter

        #endregion Parameters

        private List<Rectangle> portHitboxes = new List<Rectangle>();
        private List<int> interfaces = new List<int> { 0 };

        [Obsolete("Do not use! For Designer only!")]
        public RouterControl() : this(new Point(10, 10), "RouterControl")
        {
        }

        //protected override void ScaleCore(float dx, float dy)
        //{
        //    portsize = (int)(10 * dx);
        //    base.ScaleCore(dx, dy);
        //}

        public RouterControl(Point location, string name) : base(location, name)
        {
            backgroundGradientBrush.WrapMode = WrapMode.TileFlipX;
            portOverlayBrush.WrapMode = WrapMode.TileFlipX;
            InitializeComponent();
            Name = name;
            calculateDimension();
            calculateHitboxes();
        }

        private void calculateDimension()
        {
            int maxPort = interfaces.Count < 1 ? 0 : interfaces.Max();
            Width = 50;
            Height = 100 + 6 * (maxPort > 9 ? maxPort - 9 - (maxPort & 1) : 0);
        }

        private void calculateHitboxes()
        {
            portHitboxes = new List<Rectangle>();

            foreach (var i in interfaces)
            {
                var portRectangle = new Rectangle((Width - portsize) * (i % 2) + (1 - (i % 2) * 3), (i / 2) * (portsize + portdistance) + portOffsetY, portsize, portsize);
                portHitboxes.Add(portRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            calculateHitboxes();
            Graphics g = pe.Graphics;
            var offsetY = 0;
            g.FillRectangle(backgroundBrush, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
            g.FillRectangle(backgroundGradientBrush, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
            g.FillRectangle(dotBrush, new Rectangle(7, offsetY + 5, 2, 2));
            g.FillRectangle(dotBrush, new Rectangle(13, offsetY + 5, 2, 2));
            g.FillRectangle(dotBrush, new Rectangle(19, offsetY + 5, 2, 2));
            g.DrawLine(separatorPen, 3, offsetY + 11, Width - 4, offsetY + 11);

            foreach (var i in interfaces)
            {
                var portRectangle = portHitboxes[interfaces.IndexOf(i)];
                g.FillRectangle(portBackgroundBrush, portRectangle);

                for (int j = 1; j < portPinCount + 1; j++)
                {
                    float y = portRectangle.Y + (portsize - borderPen.Width * 2) / portPinCount * j;
                    int x = portRectangle.X + ((i + 1) % 2) * (portsize - portPinsLength);
                    g.DrawLine(portPins, new PointF(x, y), new PointF(x + portPinsLength, y));
                }
                g.FillRectangle(portRectangle.Contains(mouseLocation) ? portHighlightBrush : portOverlayBrush, portRectangle);
                g.DrawRectangle(borderPen, portRectangle);
            }
            g.DrawRectangle(IsSelected ? selectedPen : borderPen, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
        }

        Point mouseLocation = new Point();
        protected override void OnMouseMove(MouseEventArgs e)
        {
            mouseLocation = e.Location;
            foreach (var r in portHitboxes)
            {
                Invalidate(r);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            mouseLocation = new Point();
            base.OnMouseLeave(e);
        }


        public int GetPortIDByPoint(Point location)
        {
            for (int i = 0; i < portHitboxes.Count; i++)
            {
                if (portHitboxes[i].Contains(location)) return interfaces[i];
            }
            return -1;
        }

        public void RemoveInterface(int Ethernet)
        {
            interfaces.Remove(Ethernet);
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }

        public void AddInterface(int Ethernet)
        {
            interfaces.Add(Ethernet);
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }

        public override Rectangle GetPortBoundsByID(int port)
        {
            return portHitboxes[interfaces.IndexOf(port)];
        }
    }
}
