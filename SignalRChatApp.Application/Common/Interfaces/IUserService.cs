using SignalRChatApp.Application.Common.Models;
using SignalRChatApp.Domain.Entities.Dtos;

namespace SignalRChatApp.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserRegistrationDto>> CreateUserAsync(UserRegistrationDto user);

        Task<Response<UserLoginDto>> LoginAsync(UserLoginDto user);

        Task<Response<object>> LogoutAsync();
    }
}
