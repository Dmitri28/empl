using Microsoft.AspNetCore.Mvc;
using MyProject;
using MyProject.Models;

namespace Controllers
{
	public class ProjectController : Controller
	{
       public IActionResult Index()
        {
            return View(new ProjectsViewModel(Database.Instance.Projects));
        }
        public IActionResult AddIn(string title,string content)
        {
            var login = Request.Cookies["login"];
            if (login == null)
                return View(nameof(Index));
            var project = new Project(login, title, content);
            Database.Instance.AddIn(project);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteIn(string title)
        {
            Database.Instance.RemoveTable(title);
            return RedirectToAction(nameof(Index));
        }

       
    }

}
