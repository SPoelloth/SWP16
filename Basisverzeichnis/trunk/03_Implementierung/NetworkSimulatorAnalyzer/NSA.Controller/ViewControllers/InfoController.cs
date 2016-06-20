using System;
using System.Data;
using NSA.Model.BusinessLogic;
using NSA.View.Controls.InfoControl;
using NSA.View.Controls.InfoControl.TabPages;
using NSA.View.Forms;

namespace NSA.Controller.ViewControllers
{
    public class InfoController
    {
        #region Singleton

        public static InfoController Instance = new InfoController();

        #endregion Singleton

        private InfoControl infoControl;

        public void Initialize() {}

        /// <summary>
        /// Prevents a default instance of the <see cref="InfoController"/> class from being created.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">InfoControl was null/not found</exception>
        private InfoController()
        {
            infoControl = MainForm.Instance.GetComponent("InfoControl") as InfoControl;
            if (infoControl == null)
            {
                throw new InvalidOperationException("InfoControl was null/not found");
            }
            var htp = infoControl.GetTabControl().TabPages["HistoryTabPage"] as HistoryTabPage;
            if (htp != null) htp.HistoryRerunButtonClicked += HistoryTabPage_HistoryRerunButtonClicked;
        }

        #region Event Handling

        /// <summary>
        /// Handles the HistoryRerunButtonClicked event of the HistoryTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HistoryTabPage_HistoryRerunButtonClicked(object sender, EventArgs e)
        {
            DataRow row = sender as DataRow;
            if (row != null)
            {
                var values = row["Simulationen"].ToString().Split(' ');
                SimulationManager.Instance.RunSimulationFromHistory(""); //int.Parse(values[1])); todo FIX THIS
            }
        }

        #endregion


        /// <summary>
        /// Adds the new simulation to history.
        /// </summary>
        /// <param name="Sim">The simulation.</param>
        public void AddNewSimulationToHistory(Simulation Sim)
        {
            string simResult = SimulationManager.Instance.GetSimulationResult(Sim.Id) ? "Erfolgreich" : "Fehlgeschlagen";
           
            infoControl.AddNewSimulationToHistory("Simulation " + Sim.Id, simResult, Sim.Source, Sim.Destination);
        }
    }
}