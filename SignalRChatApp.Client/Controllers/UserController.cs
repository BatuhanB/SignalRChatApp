using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Client.Models;
using System.Text;
using System.Text.Json;

namespace SignalRChatApp.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
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

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var http = _httpClientFactory.CreateClient("API");

            if (ModelState.IsValid)
            {
                var serializedData = JsonSerializer.Serialize(model);
                var stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                var result = http.PostAsync("api/User/Login", stringContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home", new { userName = model.UserName });
                }
            }
            return View("Index");
        }

        public IActionResult Logout()
        {
            var http = _httpClientFactory.CreateClient("API");
            var result = http.GetAsync("api/User/Logout").Result;
            var jsonString = result.Content.ReadAsStringAsync().Result;
            var res = JsonSerializer.Deserialize<LogoutModel>(jsonString);
            if (res!.statusCode is 200)
            {
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
