using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public partial class WorkstationControl : EditorElementBase, IConfigurable
    {
        private Pen borderPen = Pens.White;
        private Pen separatorPen = Pens.Black;
        private Pen selectedPen = Pens.Red;
        private Brush backgroundBrush = new SolidBrush(Color.FromArgb(80, 80, 80));
        private Brush dotBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        public int NetworPortCount = 1;

        public WorkstationControl() : this(new Point(10, 10))
        {
        }

        public WorkstationControl(Point location) : base(location)
        {
            InitializeComponent();
            Width = 50;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            var offsetY = label1.Height + 3;
            g.FillRectangle(backgroundBrush, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));
            g.DrawRectangle(IsSelected ? selectedPen : borderPen, new Rectangle(0, offsetY, Width - 1, Height - 1 - offsetY));

            g.FillRectangle(dotBrush, new Rectangle(5, offsetY + 5, 2, 2));
            g.FillRectangle(dotBrush, new Rectangle(10, offsetY + 5, 2, 2));
            g.FillRectangle(dotBrush, new Rectangle(15, offsetY + 5, 2, 2));
            g.DrawLine(separatorPen, 3, offsetY + 10, Width - 4, offsetY + 10);


            //base.OnPaint(pe);
        }
    }
}
