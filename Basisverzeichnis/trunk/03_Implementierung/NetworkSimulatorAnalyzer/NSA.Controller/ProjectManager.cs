using System;
using System.Collections.Generic;
using NSA.Model.BusinessLogic;

namespace NSA.Controller
{
    internal class ProjectManager
    {
        public Project CurrentProject { get; }
        public List<Testscenario> Testscenarios { get; }

        // Default constructor:
        public ProjectManager()
        {

        }

        // Constructor:
        public ProjectManager(Project currentProject, List<Testscenario> testscenarios)
        {
            CurrentProject = currentProject;
            Testscenarios = testscenarios;
        }

        public void OnCreateNewProject()
        {

        }

        public void CreateNewProject()
        {

        }

        public void CloseProject()
        {

        }

        public void SaveAs(string path)
        {

        }

        public void LoadTestscenarios()
        {

        }
    }
}