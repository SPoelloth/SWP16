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
        List<EditorElementBase> testElements = new List<EditorElementBase>();

        public NetworkViewControl()
        {
            InitializeComponent();

            //testcode
            testElements.Add(new ComputerControl(new Point(20, 20), "Test1"));
            testElements.Add(new ComputerControl(new Point(200, 150), "Test2"));
            testElements.Add(new ComputerControl(new Point(400, 100), "Computer 1"));
            testElements.Add(new ConnectionControl(this, (WorkstationControl)testElements[0], 1, (WorkstationControl)testElements[1], 0));
            testElements.Add(new ConnectionControl(this, (WorkstationControl)testElements[1], 1, (WorkstationControl)testElements[2], 0));

            foreach (var e in testElements) AddElement(e);

            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (EditorElementBase c in Controls.OfType<EditorElementBase>().ToList())
            // wir brauchen eine kopie der liste, wir können nicht im foreach die reihenfolge ändern
            {
                Console.WriteLine(c.ZIndex);
                Controls.SetChildIndex(c, c.ZIndex);
            }
            Console.WriteLine("=======");

            base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.DodgerBlue, new Rectangle(0, 0, Size.Width - 1, Size.Height - 1));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
            base.OnSizeChanged(e);
        }

        public void AddElement(EditorElementBase element)
        {
            Console.WriteLine(element.ZIndex);
            if (element is IConfigurable)
            {
                var label = new LabelControl(element);
                Controls.Add(label);
            }
            Controls.Add(element);
            element.Selected += Element_Selected;
        }

        private void Element_Selected(EditorElementBase element)
        {
            foreach (var c in Controls)
            {
                var control = c as EditorElementBase;
                if (control == null) continue;
                control.IsSelected = false;
            }
            element.IsSelected = true;
        }

        protected override void OnClick(EventArgs e)
        {
            foreach (var c in Controls)
            {
                var control = c as EditorElementBase;
                if (control == null) continue;
                control.IsSelected = false;
            }
            base.OnClick(e);
        }
    }
}
