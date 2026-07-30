using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Domain.Entities;
using AuthenticationApi.Infrastructure.Data;
using BCrypt.Net;
using ecommerce.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationApi.Infrastructure.Repository;

public class UserRepository(AuthenticationDbContext context,IConfiguration configuration) : IUserRepository
{
    private async Task<AppUserDTO?> GetUserByEmail(string Email)
    {
        var entity = await context.AppUsers.FirstOrDefaultAsync(u=>u.Email == Email);
        return entity is null ? null : new AppUserDTO(entity.Id,
            entity.DisplayName,
            entity.CellNumber ?? string.Empty,
            entity.Address ?? string.Empty,
            entity.Email,
            entity.DisplayName,
            entity.Password ?? string.Empty,
            entity.Role ?? string.Empty
            );
    }
    public async Task<AppUserDTO?> GetUser(Guid Id)
    {
        var entity = await context.AppUsers.FindAsync(Id);
        return entity is null ? null: new AppUserDTO(entity.Id,
            entity.DisplayName,
            entity.CellNumber ?? string.Empty,
            entity.Address?? string.Empty,
            entity.Email,
            entity.DisplayName,
            entity.Password ?? string.Empty,
            entity.Role ?? string.Empty
            );
    }

    public async Task<Response> Login(LoginDTO loginDTO)
    {
        var existing = await GetUserByEmail(loginDTO.Email);
        if (existing is null) return new Response(false, "Invalid email or password");
        var verify = BCrypt.Net.BCrypt.Verify(loginDTO.Password, existing!.Password);
        if (!verify) return new Response(false, "Invalid email or password");
        return new Response(true, GenerateToken(existing));
    }

    private string GenerateToken(AppUserDTO existing)
    {
        var Key = Encoding.UTF8.GetBytes(configuration["Authentication:Key"]!);
        var SecurityKey = new SymmetricSecurityKey(Key);
        var credentials = new SigningCredentials(SecurityKey,SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,existing.Name),
            new Claim(ClaimTypes.Email,existing.Email),
        };
            
        if(!string.IsNullOrEmpty(existing.Role) || !Equals("string", existing.Role)) claims.Add(new Claim(ClaimTypes.Role, existing.Role));

        var token = new JwtSecurityToken(
            issuer: configuration["Authentication:Issuer"],
            audience: configuration["Authentication:Audience"],
            claims:claims,
            expires:null,
            signingCredentials:credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Response> Register(CreateUserDTO userDTO)
    {
        var existing = await GetUserByEmail(userDTO.Email);
        if (existing is not null) return new Response(false, "Email already exists.");
        await context.AppUsers.AddAsync(new AppUser
        {
            Email = userDTO.Email,
            Name = userDTO.Name,
            DisplayName = userDTO.DisplayName,
            Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
            Address = userDTO.Address,
            Role = userDTO.Role,
            CellNumber = userDTO.CellNumber,
        });
        await context.SaveChangesAsync();
        return new Response(true, $"User {userDTO.Email} registered successfully.");
    }
}
