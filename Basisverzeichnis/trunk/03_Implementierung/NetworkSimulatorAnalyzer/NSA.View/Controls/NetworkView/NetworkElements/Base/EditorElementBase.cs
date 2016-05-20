using System;
using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Controls.NetworkView.NetworkElements.Base
{
    public partial class EditorElementBase : UserControl
    {
        Image Image;
        public Action<EditorElementBase>  Selected;

        private bool isSelected = false;
        public bool IsSelected { get { return isSelected; } set { if (isSelected != value) { isSelected = value; Invalidate(); } } }

        public EditorElementBase() : this(new Point(0, 0))
        {

        }

        public EditorElementBase(Point location)
        {
            Location = location;
            Size = new Size(100, 100);
            Image = new Bitmap(100, 100);
            using (var g = Graphics.FromImage(Image))
            {
                g.Clear(Color.White);
                g.DrawLine(Pens.Red, 0, 0, 100, 100);
                g.DrawLine(Pens.Red, 100, 0, 0, 100);
            }
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Image, 0, 0, Image.Width, Image.Height);
        }

        Point lastMouseLocation = new Point();
        bool dragging = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                dragging = true;
                lastMouseLocation = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!dragging) return;
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                Location = new Point(Location.X + e.X - lastMouseLocation.X, Location.Y + e.Y - lastMouseLocation.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            dragging = false;
        }

        protected override void OnMouseHover(System.EventArgs e)
        {
            Cursor = Cursors.Hand;
            base.OnMouseHover(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Selected?.Invoke(this);
            base.OnMouseClick(e);
        }
    }
}
