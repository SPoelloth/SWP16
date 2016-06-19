using System.Drawing;

namespace NSA.View.Controls.NetworkView.NetworkElements
{
    public interface IConfigurable
    {
        Rectangle GetPortBoundsByID(int Port);
        int GetPortIDByPoint(Point p);
        void RemoveInterface(int EthernetIndex);
        void AddInterface(int EthernetIndex);
    }
}
