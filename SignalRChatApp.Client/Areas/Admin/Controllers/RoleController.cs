using Microsoft.AspNetCore.Mvc;

namespace SignalRChatApp.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
