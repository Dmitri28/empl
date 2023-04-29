

using MyProject;

namespace Controllers
{
	public class ProjectsViewModel
	{
		public readonly List<Project> _projects = new List<Project>();
		public ProjectsViewModel(List<Project> projects)
		{
			_projects = projects;
		}
	}
}
