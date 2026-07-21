using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Interfaces;
using ecommerce.SharedLibrary.Responses;

namespace AuthenticationApi.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    public Task<AppUserDTO> GetUser(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<Response> Login(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public Task<Response> Register(AppUserDTO userDTO)
    {
        throw new NotImplementedException();
    }
}
