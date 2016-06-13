using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView
{
    public partial class NetworkViewControl : UserControl
    {
        public event Action<EditorElementBase> SelectionChanged;
        public event Action<VisualConnection> RemoveConnectionPressed;
        public event Action<EditorElementBase> RemoveElementPressed;
        public List<VisualConnection> connections = new List<VisualConnection>();

        public NetworkViewControl()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            DoubleBuffered = true;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //e.Graphics.DrawRectangle(Pens.DodgerBlue, new Rectangle(0, 0, Size.Width - 1, Size.Height - 1));
            if (debug)
            {
                var g = e.Graphics;
                int i = 0;
                foreach (var c in Controls.OfType<EditorElementBase>())
                {
                    g.DrawString($"{c.GetType().Name + c.ZIndex} X:{c.Location.X} Y:{c.Location.Y} Selected:{c.IsSelected}", SystemFonts.DefaultFont, Brushes.Blue, 2, i++ * (SystemFonts.DefaultFont.Height + 2));
                }
            }
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
                Controls.Add(label);
            }
            Controls.Add(element);
            element.Selected += Element_Selected;
            element.RemovePressed += Element_Delete;

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
                c.IsSelected = false;
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
            foreach(var c in connections.Where(con => con.Element1 == element || con.Element2 == element).ToArray())
            {
                RemoveConnectionPressed?.Invoke(c);
            }
            Controls.Remove(element);
            element.Dispose();
        }
    }
}
