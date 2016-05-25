using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NSA.Model.BusinessLogic;

namespace NSA.Controller
{
    internal class ProjectManager
    {
        private Project _currentProject;
        private List<Testscenario> _testscenarios;

        // Default constructor:
        public ProjectManager()
        {
            _currentProject = null;
        }

        // Constructor:
        public ProjectManager(Project currentProject, List<Testscenario> testscenarios)
        {
            _currentProject = currentProject;
            _testscenarios = testscenarios;
        }

        public void CreateNewProject()
        {
            _currentProject = new Project();
        }

        public void CloseProject()
        {
            _currentProject = null;
        }

        public void SaveAs(string path)
        {
            var file = new StreamWriter(path);
            file.WriteLine(_currentProject);
            file.Close();
        }

        public void LoadTestscenarios()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            try
            {
                _testscenarios.Add(new Testscenario());
            }
            catch (IOException)
            {
            }
        }

        public Testscenario GetTestscenarioById(string id)
        {
            foreach (var testscenario in _testscenarios)
            {
                if (testscenario.Id.Equals(id))
                {
                    return testscenario;
                }
            }
            return null;
        }
    }
}