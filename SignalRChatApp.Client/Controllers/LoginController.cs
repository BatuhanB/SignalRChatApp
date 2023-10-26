using Microsoft.AspNetCore.Mvc;

namespace SignalRChatApp.Client.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
