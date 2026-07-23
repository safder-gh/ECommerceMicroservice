using AuthenticationApi.Application.DTOs;
using ecommerce.SharedLibrary.Responses;

namespace AuthenticationApi.Application.Interfaces;

public  interface IUserRepository
{
    Task<Response> Register(CreateUserDTO userDTO);
    Task<Response> Login(LoginDTO loginDTO);
    Task<AppUserDTO?> GetUser(Guid Id);
}
