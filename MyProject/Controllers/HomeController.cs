using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using MyProject.Controllers;
using Controllers;
using MyProject;

[Route("[controller]")]
[ApiController]
public class HomeController : Controller
    {

	private readonly ILogger<HomeController> _logger;
	private readonly ILogger _specificLogger;

	public HomeController(ILogger<HomeController> logger, ILoggerFactory loggerFactory)
	{

		_logger = logger;
	}

	public IActionResult Index()
        {
            HttpRequest request = HttpContext.Request;
            string login = Request.Cookies["login"];
            string password = Request.Cookies["password"];
            if (login != null
                && password != null
                && Database.Instance.DoesExistLogin(login)
                && Database.Instance.IsCorrectUserData(login, password))
            {
                return RedirectToAction(nameof(ProjectController.Index), "Project");
            }
            else
            {
                return RedirectToAction(nameof(Registration.Index), "Registration");
            }
        }
        public IActionResult OnAdd(string username, string password)
        {

            var cookie = new Cookie("username", "pasword");
            if (cookie != null)
            {



                cookie.Name = username;
                cookie.Value = password;
                Response.WriteAsync("Cookie has alredy written" + cookie.Name + " " + cookie.Value);


            }
            return View(cookie);
        }
        public IActionResult Home(string login, string password)
        {
            Response.Cookies.Append("login", login);
            Response.Cookies.Append("password", password);

            return RedirectToAction("Index1", "Home");

        }
        public IActionResult Register(string login,string password)
        {
            var doesExist = Database.Instance.DoesExistLogin(login);
            if (doesExist)
                return RedirectToAction(nameof(Index));
            var user = new User(login, password);
            Database.Instance.AddUsers(user);
            var cookies = Response.Cookies;
            cookies.Append(nameof(login), login);
            cookies.Append(nameof(password), password);
            return RedirectToAction(nameof(Index), "Autorization");
        }


    }
    public class UserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Passowrd { get; private set; }
    }
    namespace Projects
    {
        class HttpCookie
        {
            private string v1;
            private string v2;

            public HttpCookie(string v1, string v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }

        }
    }

