using System;
using System.Windows.Forms;
using NSA.View.Controls.Toolbar;
using NSA.View.Forms;

namespace NSA.Controller.ViewControllers
{
    public class ToolbarController
    {
        #region Singleton

        public static ToolbarController Instance = new ToolbarController();

        #endregion Singleton

        ToolbarControl flp;

        private ToolbarController()
        {
            flp = MainForm.Instance.GetComponent("ToolbarControl") as ToolbarControl;
            if (flp == null) throw new NullReferenceException("ToolbarControl darf nicht null sein");

            var btn = new Button { Height = 40, Width = 80, Text = "Projekt öffnen" };
            btn.Click += OpenProject_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Projekt speichern" };
            btn.Click += SaveProject_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Speichern unter..." };
            btn.Click += SaveProjectAs_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Computer hinzufügen" };
            btn.Click += AddComputer_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Router hinzufügen" };
            btn.Click += AddRouter_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Switch hinzufügen" };
            btn.Click += AddSwitch_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Verbindung erstellen" };
            btn.Click += CreateConnection_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Schnelle Simulation" };
            btn.Click += QuickSimulation_Click;
            flp.AddButton(btn);

            btn = new Button { Height = 40, Width = 80, Text = "Erweiterte Simulation" };
            btn.Click += AdvancedSimulation_Click;
            flp.AddButton(btn);
        }
        
        public void Init()
        {
            
        }

        void AdvancedSimulation_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");
        }

        void QuickSimulation_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");

        }

        void CreateConnection_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");
            
        }

        void AddSwitch_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");

        }

        void AddRouter_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");

        }

        void AddComputer_Click(object sender, EventArgs e)
        {
            // MessageBox.Show((sender as Button).Text + " clicked");
            NetworkManager.Instance.CreateHardwareNode(NetworkManager.HardwarenodeType.Workstation);
        }

        void SaveProjectAs_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.SaveAs();
        }

        void SaveProject_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.Save();
        }

        void OpenProject_Click(object sender, EventArgs e)
        {
           // ProjectManager.Instance.OpenProject();
        }
    }
}
