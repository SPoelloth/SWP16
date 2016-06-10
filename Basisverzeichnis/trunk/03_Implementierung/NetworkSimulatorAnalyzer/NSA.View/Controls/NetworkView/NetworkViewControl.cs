﻿using System;
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
        List<EditorElementBase> testElements = new List<EditorElementBase>();

        public NetworkViewControl()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            DoubleBuffered = true;
            InitializeComponent();
            //testcode
            testElements.Add(new ComputerControl(new Point(20, 20), "Computer 1"));
            ((WorkstationControl)testElements[0]).NetworkPortCount = 4;
            testElements.Add(new ComputerControl(new Point(200, 20), "Computer 2"));
            testElements.Add(new ComputerControl(new Point(380, 20), "Computer 3"));
            ((WorkstationControl)testElements[2]).NetworkPortCount = 4;
            testElements.Add(new ComputerControl(new Point(200, 160), "Computer 4"));
            testElements.Add(new ComputerControl(new Point(500, 20), "Computer 5"));
            testElements.Add(new ComputerControl(new Point(280, 160), "Computer 6"));
            testElements.Add(new ComputerControl(new Point(525, 160), "Computer 7"));

            AddElement(new VisualConnection("a", (WorkstationControl)testElements[0], 1, (WorkstationControl)testElements[1], 0, this));
            AddElement(new VisualConnection("b", (WorkstationControl)testElements[1], 1, (WorkstationControl)testElements[2], 0, this));
            AddElement(new VisualConnection("c", (WorkstationControl)testElements[0], 3, (WorkstationControl)testElements[3], 0, this));
            AddElement(new VisualConnection("d", (WorkstationControl)testElements[2], 1, (WorkstationControl)testElements[4], 0, this));
            AddElement(new VisualConnection("e", (WorkstationControl)testElements[2], 2, (WorkstationControl)testElements[5], 1, this));
            AddElement(new VisualConnection("f", (WorkstationControl)testElements[2], 3, (WorkstationControl)testElements[6], 0, this));
            foreach (var e in testElements) AddElement(e);
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
            if (debug) Invalidate();
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
