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
        /// <param name="NodeSrcName">Name of the source hardwarenode</param>
        /// <param name="NodeSrcLayers">Layers of the source hardwarenode</param>
        /// <param name="ErrorSrcIndex">Index of the source error</param>
        /// <param name="SendErrorSrc">Flag indicating whether an error occured at the source</param>
        /// <param name="NodeDestName">Name of the target hardwarenode</param>
        /// <param name="NodeDestLayers">Layers of the target hardwarenode</param>
        /// <param name="ErrorDestIndex">Flag indicating whether an error occured at the target</param>
        /// <param name="ReceiveError">Index of the target error</param>
        public void LoadHopInfo(
            string NodeSrcName, List<string> NodeSrcLayers, int ErrorSrcIndex, bool SendErrorSrc,
            string NodeDestName, List<string> NodeDestLayers, int ErrorDestIndex, bool ReceiveError)
        {
            ClearHopInfo();
            layerStackVisualizationControlSrc.LoadHopData(NodeSrcName, NodeSrcLayers, ErrorSrcIndex);
            layerStackVisualizationControlDest.LoadHopData(NodeDestName, NodeDestLayers, ErrorDestIndex, ReceiveError);
        }

        /// <summary>
        /// Clears the controls contents
        /// </summary>
        public void ClearHopInfo()
        {
            layerStackVisualizationControlSrc.Reset();
            layerStackVisualizationControlDest.Reset();
        }
    }
}
