using System;
using System.Collections.Generic;
using NSA.Model.BusinessLogic;

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
		public ProjectManager(Project currentProject)
		{
			this.currentProject = currentProject;
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