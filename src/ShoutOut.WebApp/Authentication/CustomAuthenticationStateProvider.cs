using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ShoutOut.WebApp.Authentication;

public class CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage) : AuthenticationStateProvider
{
    private const string UserSession = "UserSession";
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionStorageResult = await sessionStorage.GetAsync<UserSession>(UserSession);
            var session = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
            if (session is null)
                return new AuthenticationState(_anonymous);
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Email, userSessionStorageResult.Value!.Email),
                new(ClaimTypes.Role, userSessionStorageResult.Value!.Role)
            }, "CustomAuthentication"));
            return new AuthenticationState(claimsPrincipal);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task UpdateStateAsync(UserSession? userSession)
    {
        ClaimsPrincipal claimsPrincipal;
        if (userSession is not null)
        {
            await sessionStorage.SetAsync(UserSession, userSession);
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Email, userSession.Email),
                new(ClaimTypes.Role, userSession.Role)
            }, "CustomAuthentication"));
        }
        else
        {
            await sessionStorage.DeleteAsync(UserSession);
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}