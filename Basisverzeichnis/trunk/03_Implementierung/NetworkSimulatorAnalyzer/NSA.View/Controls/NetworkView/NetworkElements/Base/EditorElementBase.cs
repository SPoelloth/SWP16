using System;
using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Controls.NetworkView.NetworkElements.Base
{
    public partial class EditorElementBase : UserControl
    {
        Image Image;
        public Action<EditorElementBase> Selected;
        internal Action Deselected;

        public static int ZIndexStart = 0;
        public int ZIndex;

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    Invalidate();
                    if (!IsSelected) Deselected?.Invoke();
                }
            }
        }

        internal EditorElementBase() 
        {

        }

        public EditorElementBase(Point location, string name)
        {
            ZIndex = ZIndexStart++;
            Name = name;
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
            // ReSharper disable once VirtualMemberCallInConstructor
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Image, 0, 0, Image.Width, Image.Height);
        }

        internal Point lastMouseLocation = new Point();
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
            Parent.Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            Cursor = Cursor == Cursors.Default ? Cursors.Hand : Cursor;
            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursor == Cursors.Hand ? Cursors.Default : Cursor;
            base.OnMouseLeave(e);
            lastMouseLocation = new Point();
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Selected?.Invoke(this);
            base.OnMouseClick(e);
        }

        public virtual Rectangle GetPortBoundsByID(int port)
        {
            throw new InvalidOperationException();
        }
    }
}
