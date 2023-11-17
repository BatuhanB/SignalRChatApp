using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Client.Extensions;
using SignalRChatApp.Client.ViewModels;
using SignalRChatApp.Domain.Entities.Dtos;
using System.Text;
using System.Text.Json;

namespace SignalRChatApp.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserRegistrationDto()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserName = model.UserName
                };
                var result = await _userService.CreateUserAsync(user);
                if (result.isSuccess)
                {
                    TempData["SuccessMessage"] = "Registration has successfully completed!";
                    return RedirectToAction("Index", "User");
                }
                ModelState.AddModelErrorList(result.Error?.Errors!);
                return View();
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Please check your information!");
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = new UserLoginDto()
                {
                    Email = model.Email,
                    Password = model.Password,
                    RememberMe = model.RememberMe
                };
                var result = await _userService.LoginAsync(user);
                if (result.isSuccess)
                {
                    return Redirect(returnUrl!);
                }
                ModelState.AddModelErrorList(result.Error?.Errors!);
                return View();
            }
            return View();
        }

        public async Task Logout()
        {
            await _userService.LogoutAsync();
        }
    }
}
