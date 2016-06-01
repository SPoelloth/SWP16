using System;
using System.Drawing;
using System.Windows.Forms;
using NSA.Logging;
using NSA.View.Controls.NetworkView;

namespace NSA.View.Forms
{
    public partial class MainForm : Form
    {
        #region Singleton

        private static MainForm instance = null;
        private static readonly object padlock = new object();

        public static MainForm Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MainForm();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton


        MainForm()
        {
            
            LogManager.WriteToLog("MainForm Constructor");
            InitializeComponent();
            MainForm_Resize(null, null);
        }

        public UserControl GetComponent(string name)
        {
            foreach (UserControl control in Controls)
                if (control.Tag?.Equals(name) == true) return control;
            return null;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            int margin = 5;
            ToolbarControl.Location = new Point(margin, margin);
            ToolbarControl.Size = new Size(Width - margin * 2, ToolbarControl.Height);
            networkViewControl.Location = new Point(margin, ToolbarControl.Location.Y + ToolbarControl.Height + margin);
            networkViewControl.Width = Width - propertyControl.Width - margin * 4 - 12;
            networkViewControl.Height = Height - ToolbarControl.Height - infoControl.Height - margin * 4 - 35;
            propertyControl.Location = new Point(networkViewControl.Width + networkViewControl.Location.X + margin, networkViewControl.Location.Y);
            propertyControl.Height = (networkViewControl.Height + infoControl.Height);
            infoControl.Location = new Point(margin, networkViewControl.Location.Y + networkViewControl.Height + margin);
            infoControl.Width = networkViewControl.Width;
        }
    }
}
