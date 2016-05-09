using System;
using System.Collections.Generic;
using NSA.Model.BusinessLogic;
using NSA.Model.NetworkComponents;

namespace NSA.Controller
{
	class ProjectManager
	{
		private Project currentProject;
		private List<Testscenario> testscenarios;

		// Default constructor:
		public ProjectManager()
		{
			
		}

		// Constructor:
		public ProjectManager(Project currentProject, List<Testscenario> testscenarios)
		{
			this.currentProject = currentProject;
			this.testscenarios = testscenarios;
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

		public void SaveAs(String path)
		{

		}

		public void LoadTestscenarios()
		{

		}
	}
}