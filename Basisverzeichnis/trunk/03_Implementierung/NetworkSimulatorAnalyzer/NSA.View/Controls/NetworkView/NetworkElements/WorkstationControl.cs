using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class WorkstationControl : EditorElementBase, IConfigurable
    {
        #region Colors
        #region Workstation Colors
        private Pen borderPen = Pens.Black;
        private Pen separatorPen = new Pen(Color.Black, 2);
        private Pen selectedPen = Pens.Red;
        private LinearGradientBrush backgroundBrush = new LinearGradientBrush(new Point(), new Point(3, 0), Color.FromArgb(60, 60, 60), Color.FromArgb(175, 175, 175));
        private LinearGradientBrush backgroundGradientBrush = new LinearGradientBrush(new Point(), new Point(0, 200), Color.FromArgb(50, 0, 0, 0), Color.FromArgb(255, 0, 0, 0));
        private Brush dotBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        #endregion Workstation Colors
        #region Port Colors
        private Brush portBackgroundBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        private LinearGradientBrush portOverlayBrush = new LinearGradientBrush(new Point(), new Point(5, 0), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(100, 0, 0, 0));
        private Pen portPins = Pens.White;
        #endregion Port Colors
        #endregion Colors

        int portcount = 1;
        public int NetworkPortCount { get { return portcount; } set { portcount = value; CalculateDimension(); } }

        public WorkstationControl() : this(new Point(10, 10), "WorkstationControl")
        {
        }

        public WorkstationControl(Point location, string name) : base(location, name)
        {
            backgroundGradientBrush.WrapMode = WrapMode.TileFlipX;
            portOverlayBrush.WrapMode = WrapMode.TileFlipX;
            InitializeComponent();
            Name = name;
            CalculateDimension();
        }

        private void CalculateDimension()
        {
            Width = 50;
            Height = 100 + 6 * (NetworkPortCount > 9 ? NetworkPortCount - 9 - (NetworkPortCount & 1) : 0);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            var offsetY = 0;
            g.FillRectangle(backgroundBrush, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
            g.FillRectangle(backgroundGradientBrush, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
            g.FillRectangle(dotBrush, new Rectangle(5, offsetY + 5, 2, 2));
            g.FillRectangle(dotBrush, new Rectangle(10, offsetY + 5, 2, 2));
            g.FillRectangle(dotBrush, new Rectangle(15, offsetY + 5, 2, 2));
            g.DrawLine(separatorPen, 3, offsetY + 11, Width - 4, offsetY + 11);

            int portsize = 10;
            int portdistance = 2;
            int portOffsetY = 30;
            int portPinsLength = portsize / 2 - 1;
            int portPinCount = 4;

            for (int i = 0; i < portcount; i++)
            {
                var portRectangle = new Rectangle((Width - portsize) * (i % 2) + (1 - (i % 2) * 3), (i / 2) * (portsize + portdistance) + portOffsetY, portsize, portsize);
                g.FillRectangle(portBackgroundBrush, portRectangle);

                for (int j = 1; j < portPinCount + 1; j++)
                {
                    float y = portRectangle.Y + (portsize - borderPen.Width * 2) / portPinCount * j;
                    int x = portRectangle.X + ((i + 1) % 2) * (portsize - portPinsLength);
                    g.DrawLine(portPins, new PointF(x, y), new PointF(x + portPinsLength, y));
                }
                g.FillRectangle(portOverlayBrush, portRectangle);
                g.DrawRectangle(borderPen, portRectangle);
            }
            g.DrawRectangle(IsSelected ? selectedPen : borderPen, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
        }
    }
}
