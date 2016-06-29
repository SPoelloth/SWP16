using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView
{
    public partial class NetworkViewControl : UserControl
    {
        /// <summary>
        /// Occurs when [selection changed].
        /// </summary>
        public event Action<EditorElementBase> SelectionChanged;
        public event Action<VisualConnection> RemoveConnectionPressed;
        public event Action<EditorElementBase> RemoveElementPressed;
        public event Action<Control, int, Control, int> NewConnectionCreated;
        public event Action<string, string> QuickSimulation;
        public List<VisualConnection> connections = new List<VisualConnection>();
        public event Action<string, string> NodeRenamed;

        private MessageLoopFilter filter;

        public NetworkViewControl()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            DoubleBuffered = true;
            InitializeComponent();
            filter = new MessageLoopFilter();
            Application.AddMessageFilter(filter);
            filter.NewConnection += OnNewConnection;
            filter.NewSimulation += OnQuickSimulation;
            filter.Canceled += OnActionCanceled;
            filter.OnDeletePressed += Element_Delete;
            MouseMove += NetworkViewControl_MouseMove;
            MouseDown += NetworkViewControl_MouseDown;
        }

        private void NetworkViewControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                enableMoving = true;
                mouseLoc = e.Location;
            }
        }

        bool enableMoving = false;
        Point mouseLoc;
        private void NetworkViewControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (enableMoving && e.Button == MouseButtons.Left)
            {
                int diffX = e.X - mouseLoc.X;
                int diffY = e.Y - mouseLoc.Y;
                moveControls(new Point(diffX, diffY));
            }
            mouseLoc = e.Location;
        }

        private void moveControls(Point moveDiff)
        {
            foreach (Control c in Controls.OfType<IConfigurable>().OfType<Control>())
            {
                c.Location = new Point(c.Location.X + moveDiff.X, c.Location.Y + moveDiff.Y);
            }
        }

        private void OnNodeRenamed(string oldName, string newName)
        {
            NodeRenamed?.Invoke(oldName, newName);
        }

        private void OnQuickSimulation(Control control1, Point p1, Control control2, Point p2)
        {
            Cursor = Cursors.Default;
            foreach (var c in Controls.OfType<Control>()) c.Cursor = Cursor;
            ISimulationTarget ws1 = control1 as ISimulationTarget;
            ISimulationTarget ws2 = control2 as ISimulationTarget;
            if (ws1 == null || ws2 == null) return;
            QuickSimulation?.Invoke(((Control)ws1).Name, ((Control)ws2).Name);
        }

        private void OnNewConnection(Control control1, Point p1, Control control2, Point p2)
        {
            Cursor = Cursors.Default;
            foreach (var c in Controls.OfType<Control>()) c.Cursor = Cursor;
            IConfigurable ws1 = control1 as IConfigurable;
            IConfigurable ws2 = control2 as IConfigurable;
            if (ws1 == null || ws2 == null) return;
            int port1 = ws1.GetPortIDByPoint(p1);
            int port2 = ws2.GetPortIDByPoint(p2);
            if (port1 < 0 || port2 < 0) return;
            NewConnectionCreated?.Invoke((Control)ws1, port1, (Control)ws2, port2);
        }

        private void OnActionCanceled()
        {
            Cursor = Cursors.Default;
            foreach (var c in Controls.OfType<Control>()) c.Cursor = Cursor;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" />-event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" />that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            foreach (var c in Controls.OfType<IConfigurable>().OfType<Control>())
            {
                if (!Bounds.IntersectsWith(c.Bounds))
                {
                    Point center = new Point(Width / 2, Height / 2);
                    Point target = new Point(c.Location.X + c.Width / 2, c.Location.Y + c.Height / 2);

                    int intersectX = 0, intersectY = 0;
                    bool Xgegeben = false;

                    float m = (target.Y - center.Y) / ((float)target.X - center.X);
                    float mcrit = (Height - center.Y) / (float)(Width - center.X);

                    Xgegeben = Math.Abs(m) < mcrit;
                    if (Xgegeben) intersectX = target.X > center.X ? Width : 0;

                    if (!Xgegeben)
                        if (target.X > center.X)
                        {
                            if (m > mcrit) intersectY = Height;
                            else if (m < -mcrit) intersectY = 0;
                        }
                        else
                        {
                            if (m > mcrit) intersectY = 0;
                            else if (m < -mcrit) intersectY = Height;
                        }

                    if (Xgegeben)
                    {
                        intersectY = (int)(m * (intersectX - center.X)) + center.Y;
                    }
                    else
                    {
                        intersectX = (int)((intersectY - center.Y) / m) + center.X;
                    }

                    float winkelrad = (float)(target.X > center.X ? Math.Atan(m) : Math.PI + Math.Atan(m));

                    Point left = new Point(-20, -3);
                    Point right = new Point(-20, 3);
                    left = rotate_point(left, winkelrad);
                    right = rotate_point(right, winkelrad);
                    left.Offset(intersectX, intersectY);
                    right.Offset(intersectX, intersectY);

                    var points = new[]
                    {
                      new Point(intersectX,intersectY),
                      left,
                      right
                    };
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillPolygon(Brushes.Black, points);
                }
            }

            if (debug)
            {
                int i = 0;
                foreach (var c in Controls.OfType<EditorElementBase>())
                {
                    g.DrawString($"{c.GetType().Name + c.ZIndex} X:{c.Location.X} Y:{c.Location.Y} Selected:{c.IsSelected}", SystemFonts.DefaultFont, Brushes.Blue, 2, i++ * (SystemFonts.DefaultFont.Height + 2));
                }
            }

        }

        Point rotate_point(Point p, float angle)
        {
            var s = Math.Sin(angle);
            var c = Math.Cos(angle);

            var xnew = p.X * c - p.Y * s;
            var ynew = p.X * s + p.Y * c;

            return new Point((int)xnew, (int)ynew);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        public void AddElement(VisualConnection VisualConnection)
        {
            connections.Add(VisualConnection);
            VisualConnection.Selected += Connection_Selected;
        }

        public void AddElement(EditorElementBase element)
        {
            if (element is IConfigurable)
            {
                var label = new LabelControl(element);
                label.NameChanged += OnNodeRenamed;
                Controls.Add(label);
            }
            Controls.Add(element);
            element.Selected += Element_Selected;

            //Controls neu sortieren von hinten nach vorne
            // SetChildIndex(..) funktioniert nicht auf linux! :(

            // wir brauchen eine kopie der liste, wir können nicht im foreach die reihenfolge ändern
            var controls = Controls.OfType<EditorElementBase>().OrderBy(o => o.ZIndex).ToList();

            foreach (EditorElementBase c in controls)
            {
                Controls.Remove(c);
                Controls.Add(c);
            }
        }

        private void Connection_Selected(VisualConnection sender)
        {
            foreach (var c in Controls.OfType<EditorElementBase>())
            {
                if (c is ConnectionControl) continue;
                c.IsSelected = false;
            }
            foreach (var c in connections)
            {
                c.IsSelected = c == sender;
            }
        }

        private void Element_Selected(EditorElementBase element)
        {
            foreach (var c in Controls.OfType<EditorElementBase>())
            {
                if (c != element) c.IsSelected = false;
            }
            foreach (var c in connections)
            {
                c.IsSelected = false;
            }
            if (element != null) element.IsSelected = true;
            SelectionChanged?.Invoke(element);
        }

        bool debug = false;

        protected override void OnClick(EventArgs e)
        {
            Element_Selected(null);
            base.OnClick(e);
            var args = (MouseEventArgs)e;
            debug = args.X < 10 && args.Y < 10;
            if (debug) Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            enableMoving = false;
            Invalidate();
            Refresh();
            base.OnMouseUp(e);
        }

        private void Element_Delete()
        {
            foreach (var element in Controls.OfType<EditorElementBase>())
            {
                if (element.IsSelected)
                {
                    if (element is ConnectionControl) RemoveConnectionPressed?.Invoke(connections.First(c => c.Name == element.Name));
                    else RemoveElementPressed?.Invoke(element);
                }
            }
        }

        public void RemoveConnection(VisualConnection c)
        {
            c.Dispose();
            connections.Remove(c);
        }

        public void RemoveElement(EditorElementBase element)
        {
            foreach (var c in connections.Where(con => con.Element1 == element || con.Element2 == element).ToArray())
            {
                RemoveConnectionPressed?.Invoke(c);
            }
            Controls.Remove(element);
            element.Dispose();
            Element_Selected(null);
        }

        public void CreateNewConnection()
        {
            filter.ChangeStateNewConnection();
            Cursor = Cursors.Cross;
            foreach (var c in Controls.OfType<Control>()) c.Cursor = Cursor;
        }

        public void CreateNewQuickSimulation()
        {
            filter.ChangeStateQuickSimulation();
            Cursor = Cursors.Cross;
            foreach (var c in Controls.OfType<Control>()) c.Cursor = Cursor;
        }

        public void AddInterfaceToNode(string NodeName, string ifaceName)
        {
            ((IConfigurable)Controls.OfType<EditorElementBase>().First(n => n.Name == NodeName)).AddInterface(int.Parse(ifaceName.Replace("eth", "")));
        }

        public void RemoveInterfaceFromNode(string NodeName, string Eth1)
        {
            ((IConfigurable)Controls.OfType<EditorElementBase>().First(n => n.Name == NodeName)).RemoveInterface(int.Parse(Eth1.Replace("eth", "")));
        }

        internal bool NameExists(string name)
        {
            return Controls.OfType<Control>().Any(c => c.Name == name);
        }

        public Bitmap CreateScreenshot()
        {
            if (!Controls.OfType<EditorElementBase>().Any()) return null;
            var oldSize = Size;
            var area = Controls.OfType<EditorElementBase>().Aggregate(Controls.OfType<EditorElementBase>().First().Bounds, (current, c) => Rectangle.Union(current, c.Bounds));
            var bmp = new Bitmap(area.Width, area.Height);
            moveControls(new Point(-area.Location.X, -area.Location.Y));
            Size = Rectangle.Union(new Rectangle(), area).Size;
            ReverseZOrder();
            DrawToBitmap(bmp, new Rectangle(0, 0, Width, Height));
            ReverseZOrder();
            moveControls(area.Location);
            Size = oldSize;
           return bmp;
        }

        private void ReverseZOrder()
        {
            foreach (var c in Controls.OfType<EditorElementBase>()) c.ZIndex = int.MaxValue - c.ZIndex;
            var controls = Controls.OfType<EditorElementBase>().OrderBy(o => o.ZIndex).ToList();
            foreach (EditorElementBase c in controls)
            {
                Controls.Remove(c);
                Controls.Add(c);
            }
        }
    }
}
