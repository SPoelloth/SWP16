using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class SwitchControl : EditorElementBase, IConfigurable
    {
        #region Parameters
        #region Colors
        #region Workstation Colors
        private Pen borderPen = Pens.Black;
        private Pen selectedPen = Pens.Red;
        private LinearGradientBrush backgroundBrush = new LinearGradientBrush(new Point(), new Point(3, 0), Color.FromArgb(101, 130, 193), Color.FromArgb(101, 130, 193));
        private LinearGradientBrush backgroundGradientBrush = new LinearGradientBrush(new Point(), new Point(0, 50), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 0, 0));
        private Brush dotBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        #endregion Workstation Colors
        #region Port Colors
        private Brush portBackgroundBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        private Brush portHighlightBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
        private LinearGradientBrush portOverlayBrush = new LinearGradientBrush(new Point(), new Point(0, 5), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(100, 0, 0, 0));
        private Pen portPins = Pens.White;
        #endregion Port Colors
        #endregion Colors
        #region Port Parameter

        int portsize = 10;
        const int portdistance = 2;
        const int portOffsetX = 9;
        const int portPinsLength = 10 / 2 - 1;
        const int portPinCount = 4;

        #endregion Port Parameter

        #endregion Parameters

        private List<Rectangle> portHitboxes = new List<Rectangle>();
        private List<int> interfaces = new List<int> { 0, 1, 2, 3, 4 };

        [Obsolete("Do not use! For Designer only!")]
        public SwitchControl() : this(new Point(10, 10), "SwitchControl")
        {
        }

        //protected override void ScaleCore(float dx, float dy)
        //{
        //    portsize = (int)(10 * dx);
        //    base.ScaleCore(dx, dy);
        //}

        public SwitchControl(Point location, string name) : base(location, name)
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
            Height = 20;
            Width = 2 * portOffsetX + (portsize + portdistance) * (maxPort + 1);
        }

        private void calculateHitboxes()
        {
            portHitboxes = new List<Rectangle>();

            foreach (var i in interfaces)
            {
                var portRectangle = new Rectangle(portOffsetX + (portsize + portdistance) * i, Height - portsize - 1, portsize, portsize);
                portHitboxes.Add(portRectangle);
            }
        }

        public void RemoveInterface(int iface)
        {
            interfaces.Add(iface);
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }

        public void AddInterface(int iface)
        {
            interfaces.Remove(iface);
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }

        public void SetInterfaces(List<int> ifaces)
        {
            interfaces = ifaces;
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            calculateHitboxes();
            Graphics g = pe.Graphics;
            var offsetY = 0;
            g.FillRectangle(backgroundBrush, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
            g.FillRectangle(backgroundGradientBrush, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
            //g.FillRectangle(dotBrush, new Rectangle(5, offsetY + 5, 2, 2));
            //g.FillRectangle(dotBrush, new Rectangle(10, offsetY + 5, 2, 2));
            //g.FillRectangle(dotBrush, new Rectangle(15, offsetY + 5, 2, 2));

            for (int i = 0; i < portHitboxes.Count; i++)
            {
                var portRectangle = portHitboxes[i];
                g.FillRectangle(portBackgroundBrush, portRectangle);

                for (int j = 1; j < portPinCount + 1; j++)
                {
                    float y = portRectangle.Y + 1;
                    int x = portRectangle.X + (int)((portsize - borderPen.Width * 2) / portPinCount * j);
                    g.DrawLine(portPins, new PointF(x, y), new PointF(x, y + portPinsLength));
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

        public int GetPortIDByPoint(Point location)
        {
            for (int i = 0; i < portHitboxes.Count; i++)
            {
                if (portHitboxes[i].Contains(location)) return i;
            }
            return -1;
        }

        public override Rectangle GetPortBoundsByID(int port)
        {
            return portHitboxes[port];
        }
    }
}
