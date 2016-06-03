using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSA.View.Controls.NetworkView.NetworkElements;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

namespace NSA.View.Controls.NetworkView
{
    public class VisualConnection
    {
        private List<ConnectionControl> connectionControls = new List<ConnectionControl>();
        private EditorElementBase Element1, Element2;
        private int Port1;
        private int Port2;
        NetworkViewControl Parent;

        public VisualConnection(EditorElementBase element1, int port1, EditorElementBase element2, int port2, NetworkViewControl parent)
        {
            Parent = parent;
            Element1 = element1;
            Element2 = element2;
            Port1 = port1;
            Port2 = port2;
            Element1.LocationChanged += Element_LocationChanged;
            Element2.LocationChanged += Element_LocationChanged;
            connectionControls.Add(new ConnectionControl(new Point(), new Point()));
            connectionControls.Add(new ConnectionControl(new Point(), new Point()));
            connectionControls.Add(new ConnectionControl(new Point(), new Point()));
            connectionControls.Add(new ConnectionControl(new Point(), new Point()));
            connectionControls.Add(new ConnectionControl(new Point(), new Point()));
            CalculateLineParts();
            foreach (var c in connectionControls) Parent.Controls.Add(c);
        }

        private void Element_LocationChanged(object sender, EventArgs e)
        {
            CalculateLineParts();
        }

        private void CalculateLineParts()
        {
            var startElement = Element1.GetPortBoundsByID(Port1);
            var targetElement = Element2.GetPortBoundsByID(Port2);
            startElement.Offset(Element1.Location);
            targetElement.Offset(Element2.Location);

            var Location = Rectangle.Union(startElement, targetElement).Location;
            var a = new Point(startElement.X + startElement.Width, startElement.Y + startElement.Height / 2);
            var b = new Point(targetElement.X , targetElement.Y + targetElement.Height / 2 );

            Point Element1Start = a;
            Point Element1PixelMargin = a;
            Element1PixelMargin.X += Element1 is WorkstationControl ? -5 + 10 * (Port1 % 2) : 0;
            Element1PixelMargin.Y += Element1 is SwitchControl ? 5 : 0;
            Point Element2Start = b;
            Point Element2PixelMargin = b;
            Element2PixelMargin.X += Element2 is WorkstationControl ? -5 + 10 * (Port2 % 2) : 0;
            Element2PixelMargin.Y += Element2 is SwitchControl ? 5 : 0;
            
            Point am = new Point((Element1PixelMargin.X + Element2PixelMargin.X) / 2, Element1PixelMargin.Y);
            Point bm = new Point((Element1PixelMargin.X + Element2PixelMargin.X) / 2, Element2PixelMargin.Y);

            if (DirectConnectionPossible(Element1PixelMargin, Element2PixelMargin))
            {
               connectionControls[0].SetPoints(Element1Start, Element1PixelMargin);
               connectionControls[1].SetPoints(Element1PixelMargin, am);
               connectionControls[2].SetPoints(am, bm);
               connectionControls[3].SetPoints(bm, Element2PixelMargin);
               connectionControls[4].SetPoints(Element2PixelMargin, Element2Start);
            }
            //else 
            //{
            //  TODO
            //  beide linien nach unten zeichnen bis verbindung möglich ist
            //  
            //  
            //  
            //  
            //}
        }

        private bool DirectConnectionPossible(Point point1, Point point2)
        {
            // TODO
            return true;
        }
    }
}
