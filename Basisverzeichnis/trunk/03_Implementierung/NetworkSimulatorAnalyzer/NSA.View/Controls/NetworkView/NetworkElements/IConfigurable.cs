using System.Drawing;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public interface IConfigurable
    {
        Rectangle GetPortBoundsByID(int port);
        int GetPortIDByPoint(Point p);
        void RemoveInterface(int ethernet);
        void AddInterface(int ethernet);
    }
}
