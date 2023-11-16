using Microsoft.AspNetCore.Identity;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Application.Common.Models;
using SignalRChatApp.Domain.Entities.Dtos;
using SignalRChatApp.Persistence.Identity;

namespace SignalRChatApp.Application.User
{
    public class UserService:IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Response<UserRegistrationDto>> CreateUserAsync(UserRegistrationDto user)
        {
            var userObject = new ApplicationUser
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(userObject, user.Password);
            if (!result.Succeeded)
            {
                var errs = result.Errors.Select(x => x.Description).ToList();
                return Response<UserRegistrationDto>.Fail(500, new Error(errs, false));
            }

            return Response<UserRegistrationDto>.Success(user, 200);
        }

        public async Task<Response<UserLoginDto>> LoginAsync(UserLoginDto user)
        {
            if(user == null)
            {
                Response<UserLoginDto>.Fail(400, new Error($"There is no user!", true));
            }
            var userModel = await _userManager.FindByEmailAsync(user!.Email);

            if (userModel == null) return Response<UserLoginDto>.Fail(400, new Error($"No user has found with {user.Email}", true));

            var result = await _signInManager.PasswordSignInAsync(user: userModel, user.Password, isPersistent: user.RememberMe, false);

            if (!result.Succeeded)
            {
                return Response<UserLoginDto>.Fail(400, new Error("Incorrect email or password!", true));
            }
            return Response<UserLoginDto>.Success(user, 200);
        }

        public async Task<Response<object>> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Response<object>.Success(200);
        }
    }
}
