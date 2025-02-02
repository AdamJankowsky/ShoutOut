using FluentResults;
using Microsoft.EntityFrameworkCore;
using ShoutOut.Database;
using ShoutOut.WebApp.Authentication;
using ShoutOut.WebApp.Cryptography;

namespace ShoutOut.WebApp.Services;

public class LoginService(
    ShoutOutDbContext dbContext,
    CustomAuthenticationStateProvider customAuthenticationStateProvider,
    IHashingService hashingService)
{
    public async Task<Result> LoginAsync(string email, string password)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return Result.Fail($"Could not find user with email {email}");

        var userEnteredPassword = hashingService.HashPassword(password, user.Salt);
        if (userEnteredPassword == user.Password)
        {
            await customAuthenticationStateProvider.UpdateStateAsync(new UserSession
            {
                Email = email,
                Role = "admin"
            });
            return Result.Ok();
        }

        return Result.Fail("Password did not match");
    }

    public async Task LogoutAsync()
    {
        await customAuthenticationStateProvider.UpdateStateAsync(null);
    }
}