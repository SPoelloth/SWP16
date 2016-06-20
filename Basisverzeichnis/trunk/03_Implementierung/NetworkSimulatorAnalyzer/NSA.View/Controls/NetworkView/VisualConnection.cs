using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NSA.View.Controls.NetworkView.NetworkElements;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView
{
    public class VisualConnection
    {
        private List<ConnectionControl> connectionControls = new List<ConnectionControl>();
        public Action<VisualConnection> Selected;
        public Action<VisualConnection> RemovePressed;
        public EditorElementBase Element1, Element2;
        public int Port1;
        public int Port2;
        NetworkViewControl Parent;
        public string Name;

        private bool isSelected = false;
        public bool IsSelected { get { return isSelected; } set { if (isSelected != value) { isSelected = value; if (!isSelected) Deselect(); else Select(); } } }

        public VisualConnection(string name, EditorElementBase element1, int port1, EditorElementBase element2, int port2, NetworkViewControl parent)
        {
            Name = name;
            Parent = parent;
            Element1 = element1;
            Element2 = element2;
            Port1 = port1;
            Port2 = port2;
            Element1.LocationChanged += Element_LocationChanged;
            Element2.LocationChanged += Element_LocationChanged;
            CalculateLineParts();
            parent.SelectionChanged += Deselect;
        }

        public void Deselect(EditorElementBase foo = null)
        {
            foreach (var c in connectionControls)
            {
                c.IsSelected = false;
                c.Invalidate();
            }
        }

        public void Select()
        {
            foreach (var c in connectionControls)
            {
                c.IsSelected = true;
                c.Invalidate();
            }
        }
        private void Connection_Click(object sender, EventArgs e)
        {
            IsSelected = true;
            foreach (var c in connectionControls)
            {
                c.IsSelected = true;
            }
            Selected?.Invoke(this);

        }

        private void Element_LocationChanged(object sender, EventArgs e)
        {
            CalculateLineParts();
        }

        private int SwitchConnectionDist = 4;

        private void CalculateLineParts()
        {
            if (Element1.Location.X > Element2.Location.X)
            {
                var e = Element2;
                Element2 = Element1;
                Element1 = e;
                int p = Port2;
                Port2 = Port1;
                Port1 = p;
            }

            var startElement = Element1.GetPortBoundsByID(Port1);
            var targetElement = Element2.GetPortBoundsByID(Port2);
            startElement.Offset(Element1.Location);
            targetElement.Offset(Element2.Location);

            List<Point> points = new List<Point>();
            ConnectionState state = ConnectionState.Straight;

            if ((Port1 % 2 == 0 || (targetElement.X - startElement.X) < 0) && !(Element1 is SwitchControl))
            {
                state = state | ConnectionState.LeftReverse;
            }
            if ((Port2 % 2 == 1 || (targetElement.X - startElement.X) < 0) && !(Element2 is SwitchControl))
            {
                state = state | ConnectionState.RightReverse;
            }

            if (state == ConnectionState.Straight)
            {
                int initialCableLength = Math.Max(15, Element1.Location.X - Element2.Location.X - Element1.Width) / 2;

                //startpunkt
                points.Add(new Point(startElement.X + startElement.Width / 2, startElement.Y + startElement.Height / 2));
                Point prevPoint = points.Last();
                var sw1 = Element1 as SwitchControl;
                var startDist = sw1 != null ? new Point(prevPoint.X, prevPoint.Y + 10 + Port1 * SwitchConnectionDist) : new Point(prevPoint.X + ((Port2 % 2) * 2 - 1) * initialCableLength, prevPoint.Y);

                // int a = 10;
                // for (int i = 0; i < 10; i++)
                // {
                //     Console.WriteLine(i > a / 2 ? a/2 - i % (a / 2) : i);
                // }

                points.Add(startDist);
                Point center = new Point((int)((startElement.Location.X + startElement.Width / 2f + targetElement.Location.X) / 2f), (int)(startElement.Location.Y + startElement.Height + targetElement.Location.Y / 2f));

                prevPoint = points.Last();
                points.Add(new Point(center.X, prevPoint.Y));

                Point end = new Point(targetElement.X + targetElement.Width / 2, targetElement.Y + targetElement.Height / 2);
                var sw2 = Element2 as SwitchControl;
                var endDist = sw2 != null ? new Point(end.X, end.Y + 10 + Port2 * SwitchConnectionDist) : new Point(end.X + ((Port2 % 2) * 2 - 1) * initialCableLength, end.Y);

                points.Add(new Point(center.X, endDist.Y));
                points.Add(endDist);
                points.Add(end);
            }
            else if (state == ConnectionState.LeftReverse)
            {
                int initialCableLength = 25;

                //startpunkt
                points.Add(new Point(startElement.X + startElement.Width / 2, startElement.Y + startElement.Height / 2));
                Point prevPoint = points.Last();
                points.Add(new Point(prevPoint.X + ((Port1 % 2) * 2 - 1) * initialCableLength, prevPoint.Y));
                Point end = new Point(targetElement.X + targetElement.Width / 2, targetElement.Y + targetElement.Height / 2);
                Point endDist;
                if (Element2 is SwitchControl) endDist = new Point(end.X, end.Y + 10 + Port2 * SwitchConnectionDist);
                else endDist = new Point(end.X + ((Port2 % 2) * 2 - 1) * initialCableLength, end.Y);
                prevPoint = points.Last();
                points.Add(new Point(prevPoint.X, endDist.Y));
                points.Add(endDist);
                points.Add(end);
            }
            else if (state == ConnectionState.RightReverse)
            {
                int initialCableLength = 25;

                //startpunkt
                points.Add(new Point(startElement.X + startElement.Width / 2, startElement.Y + startElement.Height / 2));
                Point prevPoint = points.Last();
                Point startDist;
                if (Element1 is SwitchControl) startDist = new Point(prevPoint.X, prevPoint.Y + 10 + Port1 * SwitchConnectionDist);
                else startDist = new Point(prevPoint.X + ((Port2 % 2) * 2 - 1) * initialCableLength, prevPoint.Y);
                points.Add(startDist);

                prevPoint = points.Last();
                Point end = new Point(targetElement.X + targetElement.Width / 2, targetElement.Y + targetElement.Height / 2);
                Point endDist = new Point(end.X + ((Port2 % 2) * 2 - 1) * initialCableLength, end.Y);
                points.Add(new Point(endDist.X, prevPoint.Y));

                points.Add(endDist);
                points.Add(end);
            }
            else if (state == ConnectionState.BothReverse)
            {
                int initialCableLength = 50;

                //startpunkt
                points.Add(new Point(startElement.X + startElement.Width / 2, startElement.Y + startElement.Height / 2));
                Point prevPoint = points.Last();
                points.Add(new Point(prevPoint.X + ((Port1 % 2) * 2 - 1) * initialCableLength, prevPoint.Y));

                prevPoint = points.Last();

                Point end = new Point(targetElement.X + targetElement.Width / 2, targetElement.Y + targetElement.Height / 2);
                Point endDist = new Point(end.X + ((Port2 % 2) * 2 - 1) * initialCableLength, end.Y);

                if (prevPoint.Y < endDist.Y)
                {
                    if (prevPoint.Y + Element1.Height + 25 < endDist.Y)
                    {
                        Point center = new Point((prevPoint.X + endDist.X) / 2, (prevPoint.Y + endDist.Y) / 2);
                        points.Add(new Point(prevPoint.X, center.Y));
                        prevPoint = points.Last();
                        points.Add(new Point(endDist.X, prevPoint.Y));
                    }
                    else
                    {
                        points.Add(new Point(prevPoint.X, Element1.Location.Y - 20));
                        prevPoint = points.Last();
                        points.Add(new Point(endDist.X, prevPoint.Y));
                    }
                }
                else
                {
                    if (endDist.Y + Element2.Height + 25 < prevPoint.Y)
                    {
                        Point center = new Point((prevPoint.X + endDist.X) / 2, (prevPoint.Y + endDist.Y) / 2);
                        points.Add(new Point(prevPoint.X, center.Y));
                        prevPoint = points.Last();
                        points.Add(new Point(endDist.X, prevPoint.Y));
                    }
                    else
                    {
                        points.Add(new Point(prevPoint.X, Element2.Location.Y - 20));
                        prevPoint = points.Last();
                        points.Add(new Point(endDist.X, prevPoint.Y));
                    }
                }

                points.Add(endDist);
                points.Add(end);
            }

            if (connectionControls.Count < points.Count - 1)
            {
                for (; connectionControls.Count < points.Count - 1;)
                {
                    var c = new ConnectionControl(Name, new Point(), new Point());
                    Parent.AddElement(c);
                    c.Click += Connection_Click;
                    connectionControls.Add(c);
                }
            }
            if (connectionControls.Count > points.Count - 1 && connectionControls.Count > 0)
            {
                for (int i = 0; i < connectionControls.Count - (points.Count - 1); i++)
                {
                    var c = connectionControls.Last();
                    Parent.RemoveElement(c);
                    c.Click -= Connection_Click;
                    connectionControls.Remove(c);
                }
            }

            for (int i = 0; i < points.Count - 1 && points.Count > 0; i++)
            {
                connectionControls[i].SetPoints(points[i], points[i + 1]);
            }
        }

        public void Dispose()
        {
            Element1.LocationChanged -= Element_LocationChanged;
            Element2.LocationChanged -= Element_LocationChanged;
            Parent.SelectionChanged -= Deselect;

            foreach (var c in connectionControls)
            {
                c.Parent.Controls.Remove(c);
                c.Dispose();
            }
        }

        [Flags]
        enum ConnectionState
        {
            Straight = 0,
            LeftReverse = 1,
            RightReverse = 2,
            BothReverse = 3,
        }
    }
}
