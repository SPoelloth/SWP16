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
        List<EditorElementBase> testElements = new List<EditorElementBase>();

        public NetworkViewControl()
        {
            InitializeComponent();
            DoubleBuffered = true;

            //testcode
            testElements.Add(new ComputerControl(new Point(20, 20), "Computer 1"));
            ((WorkstationControl)testElements[0]).NetworkPortCount = 4;
            testElements.Add(new ComputerControl(new Point(200, 20), "Computer 2"));
            testElements.Add(new ComputerControl(new Point(380, 20), "Computer 3"));
            testElements.Add(new ConnectionControl((WorkstationControl)testElements[0], 1, (WorkstationControl)testElements[1], 0));
            testElements.Add(new ConnectionControl((WorkstationControl)testElements[1], 1, (WorkstationControl)testElements[2], 0));

            testElements.Add(new ComputerControl(new Point(200, 180), "Computer 4"));
            testElements.Add(new ConnectionControl((WorkstationControl)testElements[0], 3, (WorkstationControl)testElements[5], 0));
            foreach (var e in testElements) AddElement(e);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
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
            if (element is IConfigurable)
            {
                var label = new LabelControl(element);
                Controls.Add(label);
            }
            Controls.Add(element);
            element.Selected += Element_Selected;

            //Controls neu sortieren von hinten nach vorne
            // SetChildIndex(..) funktioniert nicht auf linux! :(
            SuspendLayout();

            // wir brauchen eine kopie der liste, wir können nicht im foreach die reihenfolge ändern
            var controls = Controls.OfType<EditorElementBase>().OrderBy(o => o.ZIndex).ToList();

            foreach (EditorElementBase c in controls)
            {
                Controls.Remove(c);
                Controls.Add(c);
            }
            ResumeLayout();
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
            SelectionChanged?.Invoke(element);
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
