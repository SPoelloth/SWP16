using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NSA.Model.BusinessLogic;
using NSA.View.Controls.InfoControl;
using NSA.View.Forms;

namespace NSA.Controller.ViewControllers
{
    public class InfoController
    {
        #region Singleton

        public static InfoController Instance = new InfoController();

        #endregion Singleton

        private readonly InfoControl infoControl;

        public void Initialize() {}

        /// <summary>
        /// Prevents a default instance of the <see cref="InfoController"/> class from being created.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">InfoControl was null/not found</exception>
        private InfoController()
        {
            infoControl = MainForm.Instance.GetComponent("InfoControl") as InfoControl;
            Debug.Assert(infoControl != null, "InfoControl was null/not found");

            var htc = infoControl.historyControl;
            var stc = infoControl.scenariosControl;
            var hstc = infoControl.hopsControl;

            Debug.Assert(htc != null, "HistoryTabControl was null/not found");
            Debug.Assert(stc != null, "ScenariosTabControl was null/not found");
            Debug.Assert(hstc != null, "HopsTabControl was null/not found");

            // History Control Eventhandler
            htc.HistoryRerunButtonClicked += historyTabPage_HistoryRerunButtonClicked;
            htc.HistoryClearButtonClicked += historyTabPage_HistoryClearButtonClicked;
            htc.HistoryDeleteButtonClicked += historyTabPage_HistoryDeleteButtonClicked;

            // Scenarios Control Eventhandler
            stc.StartScenarioButtonClicked += ScenarioTabPage_StartScenarioButtonClicked;

            // todo: HopControl
        }

        #region Event Handling

        /// <summary>
        /// Handles the HistoryRerunButtonClicked event of the HistoryTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void historyTabPage_HistoryRerunButtonClicked(object sender, EventArgs e)
        {
            DataRow row = sender as DataRow;
            if (row == null) return;
            string simID = row["Simulations ID"].ToString();
            SimulationManager.Instance.RunSimulationFromHistory(simID);
        }

        /// <summary>
        /// Handles the HistoryDeleteButtonClicked event of the HistoryTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void historyTabPage_HistoryDeleteButtonClicked(object sender, EventArgs e)
        {
            DataRow row = sender as DataRow;
            if (row == null) return;
            string simID = row["Simulations ID"].ToString();
            SimulationManager.Instance.DeleteSimulationFromHistory(simID);
            infoControl.historyControl.DeleteHistoryData(row);
        }

        /// <summary>
        /// Handles the HistoryClearButtonClicked event of the HistoryTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void historyTabPage_HistoryClearButtonClicked(object sender, EventArgs e)
        {
            ClearHistory();
        }

        /// <summary>
        /// Handles the StartScenarioButtonClicked event of the ScenarioTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void ScenarioTabPage_StartScenarioButtonClicked(object sender, EventArgs e)
        {
            DataRow row = sender as DataRow;
            if (row == null) return;

            var scenarioName = row["Testszenario"].ToString();
            Testscenario ts = ProjectManager.Instance.GetTestscenarioByName(scenarioName);
            if (ts == null) return;

            List<Simulation> failedSimulations = SimulationManager.Instance.StartTestscenario(ts);
            if (failedSimulations.Count == 0) addScenarioResultToResultsTab(scenarioName, "Erfolgreich");
            else
            {
                // {2} if a detailed result is available
                string baseResult = "Simulation von {0} nach {1} fehlgeschlagen {2}";

                foreach (Simulation s in failedSimulations)
                {
                    string simResult = string.Format(baseResult, s.Source, s.Destination, "");
                    addScenarioResultToResultsTab(scenarioName, simResult);
                }

                addScenarioResultToResultsTab(scenarioName, "Fehler aufgetreten");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the new simulation to history.
        /// </summary>
        /// <param name="Sim">The simulation.</param>
        public void AddSimulationToHistoryTab(Simulation Sim)
        {
            string expectedRes = Sim.ExpectedResult ? "Verbindung möglich" : "Verbindung nicht möglich";
            string simResult = SimulationManager.Instance.GetSimulationResult(Sim.Id) ? "Erfolgreich" : "Fehler aufgetreten";
           
            infoControl.historyControl.AddHistoryData(Sim.Id, expectedRes, simResult, Sim.Source, Sim.Destination);
        }

        /// <summary>
        /// Clears the history.
        /// </summary>
        public void ClearHistory()
        {
            SimulationManager.Instance.ClearHistory();
            infoControl.historyControl.Clear();
        }


        /// <summary>
        /// Adds the testscenario to scenario tab.
        /// </summary>
        /// <param name="T">The t.</param>
        public void AddTestscenarioToScenarioTab(Testscenario T)
        {
            infoControl.scenariosControl.AddTestScenario(T.FileName, T.SimulationCount);
        }

        /// <summary>
        /// Adds the scenario result to results tab.
        /// </summary>
        /// <param name="ScenarioName">Name of the scenario.</param>
        /// <param name="ScenarioResult">The scenario result.</param>
        private void addScenarioResultToResultsTab(string ScenarioName, string ScenarioResult)
        {
            infoControl.resultsControl.AddResultData(ScenarioName, ScenarioResult);
        }

        /// <summary>
        /// Updates the hops from last simulation.
        /// </summary>
        /// <param name="Sim">The sim.</param>
        public void UpdateHopsFromLastSimulation(Simulation Sim)
        {
            // ToDo: HopControl
            //var sendPackets = Sim.PacketsSend;
            //var receivedPackets = Sim.PacketsReceived;
            

            //foreach (var p in sendPackets)
            //{
            //    var hops = p.Hops;
            //    // isSendPacket, PacketIndex, NodeName start, NodeName dest
            //}
        }

        /// <summary>
        /// Clears the complete information control.
        /// </summary>
        public void ClearInfoControl()
        {
            infoControl.historyControl.Clear();
            infoControl.resultsControl.Clear();
            infoControl.hopsControl.Clear();
            infoControl.scenariosControl.Clear();
        }

        #endregion
    }
}