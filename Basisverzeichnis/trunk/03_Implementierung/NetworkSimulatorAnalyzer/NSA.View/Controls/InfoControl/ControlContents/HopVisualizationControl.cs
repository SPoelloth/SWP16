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
        /// Clears LayerstackVisualizationControls and forwards information to them
        /// </summary>
        /// <param name="NodeSrcName"></param>
        /// <param name="NodeSrcLayers"></param>
        /// <param name="ErrorSrcIndex"></param>
        /// <param name="SendErrorSrc"></param>
        /// <param name="NodeDestName"></param>
        /// <param name="NodeDestLayers"></param>
        /// <param name="ErrorDestIndex"></param>
        /// <param name="ReceiveError"></param>
        public void LoadHopInfo(
            string NodeSrcName, List<string> NodeSrcLayers, int ErrorSrcIndex, bool SendErrorSrc,
            string NodeDestName, List<string> NodeDestLayers, int ErrorDestIndex, bool ReceiveError)
        {
            ClearHopInfo();
            layerStackVisualizationControlSrc.LoadHopData(NodeSrcName, NodeSrcLayers, ErrorSrcIndex);
            layerStackVisualizationControlDest.LoadHopData(NodeDestName, NodeDestLayers, ErrorDestIndex, ReceiveError);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearHopInfo()
        {
            layerStackVisualizationControlSrc.Reset();
            layerStackVisualizationControlDest.Reset();
        }
    }
}
