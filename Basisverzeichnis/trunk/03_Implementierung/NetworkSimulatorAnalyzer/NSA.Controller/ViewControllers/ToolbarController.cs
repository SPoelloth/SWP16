using System;
using System.Drawing;
using System.Linq;
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

            int btnWidth = 52;
            int btnHeight = 52;
            var btnColor = SystemColors.Control;

            // Create the ToolTip and associate with the Form container.
            ToolTip t = new ToolTip();
            // Set up the delays for the ToolTip.
            t.AutoPopDelay = 5000;
            t.InitialDelay = 500;
            t.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            t.ShowAlways = true;

            var btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.CreateNew, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += newProject_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Neues Projekt anlegen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Load, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += OpenProject_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Projekt öffnen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Save, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += SaveProject_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Projekt speichern");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.SaveAs, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += SaveProjectAs_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Projekt speichern unter...");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Workstation, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += AddComputer_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Computer hinzufügen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Router, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += AddRouter_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Router hinzufügen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Switch, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += AddSwitch_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Switch hinzufügen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.NetworkCable, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += CreateConnection_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Verbindung erstellen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Simulation, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += QuickSimulation_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Schnelle Simulation ausführen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Erweiterte_Simulation, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += AdvancedSimulation_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Erweiterte Simulation ausführen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Broadcasting_90, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += Broadcast_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Broadcast ausführen");

            btn = new Button { Height = btnHeight, Width = btnWidth, BackColor = btnColor, BackgroundImage = View.Properties.Resources.Screenshot_90, BackgroundImageLayout = ImageLayout.Stretch };
            btn.Click += Screenshot_Click;
            flp.AddButton(btn);
            t.SetToolTip(btn, "Netzwerk als Bild exportieren");
        }

        private void Screenshot_Click(object sender, EventArgs e)
        {
            NetworkViewController.Instance.SaveToBitmap();
        }

        private void newProject_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.CreateNewProject();
        }

        public void Init()
        {

        }

        void Broadcast_Click(object sender, EventArgs e)
        {
            BroadcastSimulationForm form = new BroadcastSimulationForm();
            form.SetWorkstations(NetworkManager.Instance.GetAllWorkstations().Select(w => w.Name).ToList());
            var dlgresult = form.ShowDialog();
            if (dlgresult != DialogResult.OK) return;

            SimulationManager.Instance.CreateAndExecuteBroadcast(form.SourceName, form.TargetSubnet, form.ExpectedResult);
        }

        void AdvancedSimulation_Click(object sender, EventArgs e)
        {
            AdvancedSimulationForm form = new AdvancedSimulationForm();
            form.SetWorkstations(NetworkManager.Instance.GetAllWorkstations().Select(w => w.Name).ToList());
            var dlgresult = form.ShowDialog();
            if (dlgresult != DialogResult.OK) return;

            SimulationManager.Instance.CreateAndExecuteSimulation(form.SourceName, form.TargetName, form.MaxHopCount,
                form.ExpectedResult);
        }

        void QuickSimulation_Click(object sender, EventArgs e)
        {
            NetworkViewController.Instance.QuickSimulationRequest();
        }

        void CreateConnection_Click(object sender, EventArgs e)
        {
            NetworkViewController.Instance.CreateHardwarenodeRequest();
        }

        void AddSwitch_Click(object sender, EventArgs e)
        {
            NetworkManager.Instance.CreateHardwareNode(NetworkManager.HardwarenodeType.Switch);
        }

        void AddRouter_Click(object sender, EventArgs e)
        {
            NetworkManager.Instance.CreateHardwareNode(NetworkManager.HardwarenodeType.Router);
        }

        void AddComputer_Click(object sender, EventArgs e)
        {
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
