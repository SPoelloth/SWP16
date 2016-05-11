using System;
using System.Windows.Forms;

namespace NSA.View.Controls.Toolbar
{
    public partial class ToolbarControl : UserControl
    {
        public ToolbarControl()
        {
            InitializeComponent();
            AddButtons();
        }

        private void AddButtons()
        {
            Button toAdd;

            toAdd = new Button { Height = Width = 40, Text = "Open Project" };
            toAdd.Click += OpenProject_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Save Project" };
            toAdd.Click += SaveProject_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Save Project As" };
            toAdd.Click += SaveProjectAs_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Add Computer" };
            toAdd.Click += AddComputer_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Add Router" };
            toAdd.Click += AddRouter_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Add Switch" };
            toAdd.Click += AddSwitch_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Create Connection" };
            toAdd.Click += CreateConnection_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Quick Simulation" };
            toAdd.Click += QuickSimulation_Click;
            flpContents.Controls.Add(toAdd);

            toAdd = new Button { Height = Width = 40, Text = "Advanced Simulation" };
            toAdd.Click += AdvancedSimulation_Click;
            flpContents.Controls.Add(toAdd);
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
            MessageBox.Show((sender as Button).Text + " clicked");

        }

        void SaveProjectAs_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");

        }

        void SaveProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");

        }

        void OpenProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Text + " clicked");

        }

    }
}
