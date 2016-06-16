using System;
using System.Drawing;
using System.Windows.Forms;
using NSA.View.Controls.NetworkView.NetworkElements.Base;

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
        
        public Action OnDeletePressed;


        public bool PreFilterMessage(ref Message m)
        {
            if (currentState == State.Normal)
            {
                Keys kc = (Keys)m.WParam.ToInt64() & Keys.KeyCode;
                if (m.Msg == WM_KEYDOWN && kc == Keys.Delete)
                {
                    if (!(Control.FromHandle(m.HWnd) is TextBox))
                    {
                        OnDeletePressed?.Invoke();
                        return true;
                    }
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

