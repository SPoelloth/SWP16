using NetworkSimulatorAnalyzer.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkSimulatorAnalyzer
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
