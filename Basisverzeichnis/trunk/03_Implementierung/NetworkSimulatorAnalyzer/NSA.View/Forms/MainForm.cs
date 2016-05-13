using System.Windows.Forms;
using NSA.Logging;

namespace NSA.View.Forms
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      LogManager.WriteToLog("MainForm Constructor");
      InitializeComponent();
    }
  }
}
