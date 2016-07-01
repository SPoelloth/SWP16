using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NSA.View.Controls.NetworkView.NetworkElements.Base
{
    /// <summary>
    /// The base element for every drawable element in the network editor.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class EditorElementBase : UserControl
    {
        private Image Image;
        internal Action<EditorElementBase> Selected;
        internal Action Deselected;

        internal static int ZIndexStart = 0;
        internal int ZIndex;

        private bool isSelected = false;
        internal bool IsSelected
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

        /// <summary>
        /// Initializes the element with default values so we can see mistakes in derived classes.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="name"></param>
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

        /// <summary>
        /// Draws an image onto the drawingarea
        /// 
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" />, that contains the input Parameters.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Image, 0, 0, Image.Width, Image.Height);
        }

        internal Point mouseDownOffset = new Point();
        bool dragging = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!e.Button.HasFlag(MouseButtons.Left)) return;
            dragging = true;
            mouseDownOffset = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!dragging) return;
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                var mouseLoc = Parent.PointToClient(Cursor.Position);
                Location = new Point(mouseLoc.X - mouseDownOffset.X, mouseLoc.Y - mouseDownOffset.Y);
                Invalidate();
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
            Invalidate();
        }

        /// <summary>
        /// Löst das <see cref="E:System.Windows.Forms.Control.MouseClick" />-Ereignis aus.
        /// </summary>
        /// <param name="e">Ein <see cref="T:System.Windows.Forms.MouseEventArgs" />, das die Ereignisdaten enthält.</param>
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
