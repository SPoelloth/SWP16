using System.Drawing;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public interface IConfigurable
    {
        Rectangle GetPortBoundsByID(int port);
        int GetPortIDByPoint(Point p);
    }
}
