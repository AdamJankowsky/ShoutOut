using System.Security.Claims;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using ShoutOut.Database;
using ShoutOut.Database.Entities;
using ShoutOut.WebApp.Authentication;

namespace ShoutOut.WebApp.Services;

public class PostsService(
    CustomAuthenticationStateProvider customAuthenticationStateProvider,
    ShoutOutDbContext dbContext)
{
    public async Task<BlogPost[]> GetAsync()
    {
        return await dbContext.Posts.AsNoTracking().OrderByDescending(x => x.DateAdded).ToArrayAsync();
    }

    public async Task<Result> AddPostAsync(string? postText)
    {
        if (postText is null) return Result.Fail("Post text is null");

        var user = await customAuthenticationStateProvider.GetAuthenticationStateAsync();
        var userEmail = user.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
        var dbUser =
            await dbContext.Users.AsNoTracking()
                .SingleAsync(x => x.Email == userEmail);

        await dbContext.Posts.AddAsync(new BlogPost
        {
            Author = dbUser.NickName,
            Content = postText,
            UserId = dbUser.Id,
            DateAdded = DateTime.UtcNow
        });
        await dbContext.SaveChangesAsync();
        return Result.Ok();
    }
}