using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    /// <summary>
    /// This control displays a switch.
    /// </summary>
    public partial class SwitchControl : EditorElementBase, IConfigurable
    {
        #region Parameters
        #region Colors
        #region Workstation Colors
        private Pen borderPen = Pens.Black;
        private Pen selectedPen = Pens.Red;
        private LinearGradientBrush backgroundBrush = new LinearGradientBrush(new Point(), new Point(3, 0), Color.FromArgb(80, 110, 173), Color.FromArgb(101, 130, 193));
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

        /// <summary>
        /// This constructor is for designer only, do not use it.
        /// </summary>
        [Obsolete("Do not use! For Designer only!")]
        public SwitchControl() : this(new Point(10, 10), "SwitchControl")
        {
        }

        //protected override void ScaleCore(float dx, float dy)
        //{
        //    portsize = (int)(10 * dx);
        //    base.ScaleCore(dx, dy);
        //}

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SwitchControl"/> class.
        /// </summary>
        /// <param name="location">The start location of the element in the parent control.</param>
        /// <param name="name"></param>
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
            Location = new Point(Location.X + 1, Location.Y);
            Location = new Point(Location.X - 1, Location.Y);
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

        /// <summary>
        /// Removes an interface from the switch, identified by the interface ID.
        /// </summary>
        /// <param name="Ethernet">The ID of the ethernet port to remove.</param>
        public void RemoveInterface(int Ethernet)
        {
            interfaces.Remove(Ethernet);
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }

        /// <summary>
        /// Adds an interface to the switch, with a given interface ID.
        /// </summary>
        /// <param name="Ethernet">The ID of the ethernet port to add.</param>
        public void AddInterface(int Ethernet)
        {
            interfaces.Add(Ethernet);
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }

        /// <summary>
        /// Replaces all interfaces of the switch with new ones.
        /// </summary>
        /// <param name="ifaces">The new interface ID list.</param>
        public void SetInterfaces(List<int> ifaces)
        {
            interfaces = ifaces;
            calculateDimension();
            calculateHitboxes();
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:Paint" /> event.
        /// Draws the switch and its interfaces.
        /// </summary>
        /// <param name="pe">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
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

            foreach (var portRectangle in portHitboxes)
            {
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

        Point mouseLocation;
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


        public int GetInterfaceCount()
        {
            return interfaces.Count;
        }


        /// <summary>
        /// Gets the port bounds by port ID.
        /// </summary>
        /// <param name="port">The port ID.</param>
        /// <returns></returns>
        public override Rectangle GetPortBoundsByID(int port)
        {
            return portHitboxes[interfaces.IndexOf(port)];
        }
    }
}
