using Controllers;

namespace MyProject.Models
{
	public class UserViewModel
	{
		public readonly List<ProjectViewModel> user = new List<ProjectViewModel>();
		public UserViewModel(List<ProjectViewModel> users)
		{
			users = user;
		}
	}
}
