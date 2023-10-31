using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Application.Common.Models;
using SignalRChatApp.Domain.Entities.Dtos;

namespace SignalRChatApp.Persistence.Identity;

public class IdentityService : IIdentityService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
        if (user == null) { return false; }
        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);
        return result.Succeeded;
    }

    public async Task<(Response<object> result, string userId)> CreateUserAsync(object user)
    {
        var userCast = (UserRegistrationDto)user;
        var userObject = new ApplicationUser
        {
            UserName = userCast.UserName,
            FirstName = userCast.FirstName,
            LastName = userCast.LastName,
            Email = userCast.Email
        };
        var result = await _userManager.CreateAsync(userObject, userCast.Password);
        if (!result.Succeeded)
        {
            var errs = result.Errors.Select(x => x.Description).ToList();
            return (Response<object>.Fail(500, new Error(errs, false)), userObject.Id);
        }

        return (Response<object>.Success(userObject, 200),userObject.Id);
    }

    public async Task<Response<string>> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if(user != null)
        {
            await DeleteUserAsync(user);
        }

        return Response<string>.Success(userId,200);
    }

    public async Task<Response<object>> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return Response<object>.Success(result,200);
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<Response<object>> LoginAsync(object user)
    {
        var userCast = (UserLoginDto)user;
        if (userCast == null) return Response<object>.Fail(400,new Error());
        var result = await _signInManager.PasswordSignInAsync(userName: userCast.UserName, userCast.Password, isPersistent: userCast.RememberMe, false);
        if (!result.Succeeded)
        {
            return Response<object>.Fail(400,new Error());
        }
        return Response<object>.Success(userCast, 200);
    }
}