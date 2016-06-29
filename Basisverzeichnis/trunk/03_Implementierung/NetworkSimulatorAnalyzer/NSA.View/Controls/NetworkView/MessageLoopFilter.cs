using System;
using System.Drawing;
using System.Windows.Forms;

namespace NSA.View.Controls.NetworkView
{
    /// <summary>
    /// Implements the logic to catch messages before they get dispatched to the NetworkViewControl and its childs.
    /// </summary>
    public class MessageLoopFilter : IMessageFilter
    {
        #region IMessageFilter Members

        const int WM_KEYDOWN = 0x0100;
        const int WM_LBUTTONDOWN = 0x201;
        const int WM_LBUTTONUP = 0x0202;

        private State currentState = State.Normal;
        /// <summary>
        /// The Action gets invoked when the user has made all input to create a new connection
        /// </summary>
        public Action<Control, Point, Control, Point> NewConnection;
        /// <summary>
        /// The Action gets invoked when the user has made all input to create a new simulation
        /// </summary>
        public Action<Control, Point, Control, Point> NewSimulation;
        /// <summary>
        /// The Action gets invoked when the user has pressed ESC to cancel the current input
        /// </summary>
        public Action Canceled;

        Point? firstConnectionPoint = null;
        Control firstConnectionControl = null;

        /// <summary>
        /// The Action gets invoked when the user pressed delete to delete an object in the NetworkViewControl
        /// </summary>
        public Action OnDeletePressed;


        /// <summary>
        /// Filters out a message before it is dispatched.
        /// Checks the different States and filters depending on the current state
        /// </summary>
        /// <param name="m">The message to be dispatched. You cannot modify this message.</param>
        /// <returns>
        /// true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.
        /// </returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (currentState == State.Normal)
            {
                Keys kc = (Keys)m.WParam.ToInt64() & Keys.KeyCode;
                if (m.Msg == WM_KEYDOWN && kc == Keys.Delete)
                {
                    var control = Control.FromHandle(m.HWnd);
                    if (!(control is TextBox || control is DataGridView))
                    {
                        OnDeletePressed?.Invoke();
                        return true;
                    }
                }

                return false;
            }

            if (currentState == State.Connection)
            {
                Keys kc = (Keys)m.WParam.ToInt64() & Keys.KeyCode;
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
                    var upPoint = new Point((short)(m.LParam.ToInt32() & 0xffff), (short)((m.LParam.ToInt32() >> 16) & 0xffff));
                    var distance = Point.Subtract(upPoint, (Size)firstConnectionPoint.Value);
                    var upControl = Control.FromHandle(m.HWnd);
                    if (distance.X * distance.X + distance.Y * distance.Y < 25 && firstConnectionControl == upControl) return true;
                    NewConnection?.Invoke(firstConnectionControl, firstConnectionPoint.Value, upControl, upPoint);
                    currentState = State.Normal;

                    return true;
                }
            }

            if (currentState == State.QuickSimulation)
            {
                Keys kc = (Keys)m.WParam.ToInt64() & Keys.KeyCode;
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
                    var distance = Point.Subtract(upPoint, (Size)firstConnectionPoint.Value);
                    var upControl = Control.FromHandle(m.HWnd);
                    if (distance.X * distance.X + distance.Y * distance.Y < 25 && firstConnectionControl == upControl) return true;

                    NewSimulation?.Invoke(firstConnectionControl, firstConnectionPoint.Value, upControl, upPoint);
                    currentState = State.Normal;

                    return true;
                }
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Sets the current State to Connection so the user can make inputs to create a new connection
        /// </summary>
        public void ChangeStateNewConnection()
        {
            currentState = State.Connection;
            firstConnectionPoint = null;
        }

        /// <summary>
        /// Sets the current State to Connection so the user can make inputs to create a new simulation
        /// </summary>
        public void ChangeStateQuickSimulation()
        {
            currentState = State.QuickSimulation;
            firstConnectionPoint = null;
        }

        enum State
        {
            Normal,
            Connection,
            QuickSimulation
        }
    }

}

