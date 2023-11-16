using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Client.Models;
using System.Diagnostics;

namespace SignalRChatApp.Client.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        //[Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Chats()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}