using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Client.Models;

namespace SignalRChatApp.Client.Controllers
{
    public class LoginController : Controller
    {
        // Index Page
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ViewData["FirstName"] = model.FirstName;
                ViewData["LastName"] = model.LastName;
                ViewData["UserName"] = model.UserName;
                ViewData["Email"] = model.Email;
                ViewData["Password"] = model.Password;
                return View("Index");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
