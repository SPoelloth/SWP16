using System.Windows.Forms;

namespace NSA.View.Controls.Forms
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      LogManager.LogManager.WriteToLog("MainForm Constructor");
      InitializeComponent();
    }
  }
}
