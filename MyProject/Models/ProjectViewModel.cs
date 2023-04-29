

using MyProject;

namespace Controllers
{
	public class ProjectViewModel
	{
		public Project Project { get; private set; }


		public ProjectViewModel(Project project)
		{
			Project = project;
		}

	}
}
