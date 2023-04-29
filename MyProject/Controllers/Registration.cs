using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace MyProject.Controllers
{
    public class Registration : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(string login, string password)
        {
            var doesExist = Database.Instance.DoesExistUser(login);
            if (doesExist)
                return RedirectToAction(nameof(Index));

            var user = new User(login, password);
            Database.Instance.AddUsers(user);

            var cookies = Response.Cookies;
            cookies.Delete(nameof(login));
            cookies.Delete(nameof(password));

            cookies.Append(nameof(login),login);
            cookies.Append(nameof(password), password);

            return RedirectToAction(nameof(Index), "Project");
        }
        public IActionResult Examination(string login,string password)
        {
            var cookies = new Cookie();
            if (cookies == null)
            {
                return RedirectToAction(nameof(Registration.Index),"Registration");
            }
            return View();
        }
    }
}
