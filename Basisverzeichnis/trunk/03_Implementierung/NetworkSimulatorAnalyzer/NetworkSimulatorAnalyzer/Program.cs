using System;
using System.Windows.Forms;
using NSA.Controller;
using NSA.Controller.ViewControllers;
using NSA.View.Forms;

namespace NSA
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ProjectManager.Instance.CreateWindow());
            
        }
    }
}
