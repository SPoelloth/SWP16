using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSA.View.Controls.InfoControl.ControlContents
{
    /// <summary>
    /// Visualizes the layerstack of a hardwarenode involved in a hop
    /// </summary>
    public partial class LayerStackVisualizationControl : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LayerStackVisualizationControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the hop data
        /// </summary>
        /// <param name="NodeName">Name of the node</param>
        /// <param name="Layers">Layers of the node</param>
        /// <param name="ErrorIndex">Index of the layer where an error occured</param>
        /// <param name="ReceiveError">Flag indicating whether a receive error occured</param>
        public void LoadHopData(string NodeName, List<string> Layers, int ErrorIndex = -1, bool ReceiveError = false)
        {
            flpLayers.Controls.Clear();
            labelHardwarenode.Text = ReceiveError ? "Nicht erreichbar" : NodeName;
            for(int i = 0; i < Layers.Count; i++)
            {
                Label newLabel = new Label() { AutoSize = false, Text = Layers[i], BorderStyle = BorderStyle.FixedSingle, TextAlign = ContentAlignment.MiddleCenter};
                if (i == ErrorIndex)
                {
                    newLabel.BackColor = Color.Red;
                }
                else if (ReceiveError || ErrorIndex > -1 && i > ErrorIndex)
                {
                    newLabel.BackColor = Color.LightGray;
                }
                else
                {
                    newLabel.BackColor = Color.LightGreen;
                }
                flpLayers.Controls.Add(newLabel);
            }
            flpLayers.AutoSize = false;
            foreach (Label label in flpLayers.Controls)
            {
                label.Width = this.Width - 10;
            }
            flpLayers.AutoSize = true;
        }

        /// <summary>
        /// Resets the control to its default state
        /// </summary>
        public void Reset()
        {
            flpLayers.Controls.Clear();
            labelHardwarenode.Text = "Hardwarenode";
        }
    }
}
