using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Application.Common.Models;
using SignalRChatApp.Domain.Entities.Dtos;
using System.Data;
using System.Security.Claims;

namespace SignalRChatApp.Persistence.Identity;

public class IdentityService : IIdentityService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<UserRoles> _roleManager;

    public IdentityService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<UserRoles> roleManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<Response<object>> AssignRoles(string id, string roleName)
    {
        var user = await _userManager.FindByIdAsync(id);
        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            return Response<object>.Success(new { Role = roleName, UserId = user.Id }, 200);
        }
        var err = result.Errors.Select(x => x.Description).ToList();
        var errors = new Error(err, false);
        return Response<object>.Fail(400, errors);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
        if (user == null) { return false; }
        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);
        return result.Succeeded;
    }

    public async Task<Response<object>> CreateRoleAsync(object model)
    {
        var modelCast = (CreateRoleDto)model;
        var role = new UserRoles() { Id = Guid.NewGuid().ToString(), Name = modelCast.Name };
        var result = new IdentityResult();
        if (!string.IsNullOrEmpty(role.Name))
        {
            if (!(await _roleManager.RoleExistsAsync(role.Name)))
            {
                result = await _roleManager.CreateAsync(role);
            }
        }

        if (result.Succeeded)
        {
            return Response<object>.Success(role, 200);
        }
        var errors = result.Errors.Select(x => x.Description).ToList();
        var err = new Error(errors, false);
        return Response<object>.Fail(400, err);
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

        return (Response<object>.Success(userObject, 200), userObject.Id);
    }

    public async Task<Response<string>> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user != null)
        {
            await DeleteUserAsync(user);
        }

        return Response<string>.Success(userId, 200);
    }

    public async Task<Response<object>> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return Response<object>.Success(result, 200);
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
        if (userCast == null) return Response<object>.Fail(400, new Error());
        //Gonna fix it later 

        var result = await _signInManager.PasswordSignInAsync(userName: userCast.UserName, userCast.Password, isPersistent: userCast.RememberMe, false);

        if (!result.Succeeded)
        {
            return Response<object>.Fail(400, new Error());
        }
        return Response<object>.Success(userCast, 200);
    }

    public async Task<Response<object>> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return Response<object>.Success(200);
    }

}