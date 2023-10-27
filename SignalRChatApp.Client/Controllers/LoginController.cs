using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Client.Models;
using System.Text;
using System.Text.Json;

namespace SignalRChatApp.Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


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
            var http = _httpClientFactory.CreateClient("API");

            if (ModelState.IsValid)
            {
                var serializedData = JsonSerializer.Serialize(model);
                var stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                var result = http.PostAsync("api/User/Register", stringContent).Result;

                if (result.IsSuccessStatusCode)
                {
                    return View("Index");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
