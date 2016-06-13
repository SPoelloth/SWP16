using System;
using System.Drawing;
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

        private ToolbarController()
        {
            var flp = MainForm.Instance.GetComponent("ToolbarControl") as ToolbarControl;
            if (flp == null) throw new NullReferenceException("ToolbarControl darf nicht null sein");

            int btnWidth = 80;
            int btnHeight = 40;
            var btnColor = SystemColors.Control;

            var btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Neues Projekt" };
            btn.Click += newProject_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Projekt öffnen" };
            btn.Click += OpenProject_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Projekt speichern" };
            btn.Click += SaveProject_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Speichern unter..." };
            btn.Click += SaveProjectAs_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Computer hinzufügen" };
            btn.Click += AddComputer_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Router hinzufügen" };
            btn.Click += AddRouter_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Switch hinzufügen" };
            btn.Click += AddSwitch_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Verbindung erstellen" };
            btn.Click += CreateConnection_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Schnelle Simulation" };
            btn.Click += QuickSimulation_Click;
            flp.AddButton(btn);

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, Text = "Erweiterte Simulation" };
            btn.Click += AdvancedSimulation_Click;
            flp.AddButton(btn);
        }

        private void newProject_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.CreateNewProject();
        }

        public void Init()
        {
            
        }

        void AdvancedSimulation_Click(object sender, EventArgs e)
        {
            AdvancedSimulationForm form = new AdvancedSimulationForm();
            var dlgresult = form.ShowDialog();
            if (dlgresult != DialogResult.OK) return;

            //SimulationManager.Instance.CreateSimulation();
            //todo 
        }

        void QuickSimulation_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");
        }

        void CreateConnection_Click(object sender, EventArgs e)
        {
            NetworkViewController.Instance.CreateHardwarenodeRequest();
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
            ProjectManager.Instance.LoadProject();
        }
    }
}
