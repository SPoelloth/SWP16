using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSA.View.Controls.NetworkView
{

    public class MessageLoopFilter : IMessageFilter
    {
        #region IMessageFilter Members

        const int WM_KEYDOWN = 0x0100;
        const int WM_LBUTTONDOWN = 0x201;
        const int WM_LBUTTONUP = 0x0202;

        public State currentState = State.Normal;
        public Action<Control, Point, Control, Point> NewConnection;
        public Action Canceled;

        Point? firstConnectionPoint = null;
        Control firstConnectionControl = null;

        public MessageLoopFilter()
        {

        }

        public bool PreFilterMessage(ref Message m)
        {
            if (currentState == State.Normal)
            {
                Keys kc = (Keys)m.WParam.ToInt64() & Keys.KeyCode;
                if (m.Msg == WM_KEYDOWN && kc == Keys.Delete)
                {
                    // todo blub

                }

                return false;
            }

            if (currentState == State.Connection)
            {
                Keys kc = (Keys)(int)m.WParam & Keys.KeyCode;
                if (m.Msg == WM_KEYDOWN && kc == Keys.Escape)
                {
                    currentState = State.Normal;
                    Canceled?.Invoke();
                    return true;
                }

                if (m.Msg == WM_LBUTTONDOWN)
                {
                    if (firstConnectionPoint == null)
                    {
                        firstConnectionPoint = new Point(m.LParam.ToInt32() & 0xffff, (m.LParam.ToInt32() >> 16) & 0xffff);
                        firstConnectionControl = Control.FromHandle(m.HWnd);
                    }
                    return true;
                }

                if (m.Msg == WM_LBUTTONUP && firstConnectionPoint != null)
                {
                    var upPoint = new Point(m.LParam.ToInt32() & 0xffff, (m.LParam.ToInt32() >> 16) & 0xffff);
                    if (upPoint == firstConnectionPoint)
                    {
                        return true;
                    }
                    else
                    {
                        NewConnection?.Invoke(firstConnectionControl, firstConnectionPoint.Value, Control.FromHandle(m.HWnd), upPoint);
                        currentState = State.Normal;
                    }
                    return true;
                }
            }

            if (currentState == State.QuickSimulation)
            {

            }
            return false;
        }

        #endregion

        public void ChangeStateNewConnection()
        {
            currentState = State.Connection;
            firstConnectionPoint = null;
        }
    }

    public enum State
    {
        Normal,
        Connection,
        QuickSimulation
    }
}

