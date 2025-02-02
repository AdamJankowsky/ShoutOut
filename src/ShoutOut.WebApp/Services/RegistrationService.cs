using ShoutOut.Database;
using ShoutOut.Database.Entities;
using ShoutOut.WebApp.Authentication;
using ShoutOut.WebApp.Cryptography;
using ShoutOut.WebApp.Models;

namespace ShoutOut.WebApp.Services;

public class RegistrationService(
    ShoutOutDbContext dbContext,
    IHashingService hashingService,
    CustomAuthenticationStateProvider customAuthenticationStateProvider)
{
    public async Task CreateAccount(CreateAccount createAccount)
    {
        var (password, salt) = hashingService.CreateNewPassword(createAccount.Password);

        dbContext.Users.Add(new User
        {
            NickName = createAccount.UserName,
            Email = createAccount.Email,
            Password = password,
            Salt = salt,
            AvatarId = Guid.Empty
        });
        await dbContext.SaveChangesAsync();


        await customAuthenticationStateProvider.UpdateStateAsync(new UserSession
            { Email = createAccount.Email, Role = "admin" });
    }
}