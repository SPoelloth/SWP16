using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;

namespace NSA.View.Controls.InfoControl.ControlContents
{
    /// <summary>
    /// Class for the HopVisualization tab of the InfoControl. 
    /// It displays two hardwarenodes' layerstacks
    /// </summary>
    public partial class HopVisualizationControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HopVisualizationControl"/> class.
        /// </summary>
        public HopVisualizationControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Node1Name"></param>
        /// <param name="Node1Layers"></param>
        /// <param name="error1Index"></param>
        /// <param name="Node2Name"></param>
        /// <param name="Node2Layers"></param>
        /// <param name="error2Index"></param>
        public void LoadHopInfo(string Node1Name, List<string> Node1Layers, int error1Index, string Node2Name, List<string> Node2Layers, int error2Index)
        {
            layerStackVisualizationControl1.LoadHopData(Node1Name, Node1Layers, error1Index);
            layerStackVisualizationControl2.LoadHopData(Node2Name, Node2Layers, error2Index);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearHopInfo()
        {
            layerStackVisualizationControl1.Reset();
            layerStackVisualizationControl2.Reset();
        }
    }
}
