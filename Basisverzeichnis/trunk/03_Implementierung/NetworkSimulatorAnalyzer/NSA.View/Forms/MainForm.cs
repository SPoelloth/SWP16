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

        public static MainForm Instance {
            get {
                lock (padlock) {
                    if (instance == null) {
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
        }

        public UserControl GetComponent(string name) {
            foreach (UserControl control in Controls)
                if (control.Tag.Equals(name)) return control;
            return null;
        }
    }
}
