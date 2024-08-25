using Azure.Core;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinAPIMusicProject.Data;
using MinAPIMusicProject.DTOs;
using MinAPIMusicProject.Models;

namespace MinAPIMusicProject.Endpoints;

public static class UserEnpoints
{
    public static void AddUserEnpoints(this IEndpointRouteBuilder app)
    {
        var endpoint = app.MapGroup("/api/user");

        app.MapPost("/register", async (MusicContext context, [FromBody]UserDTO userDto) =>
        {
            if (await context.Users.AnyAsync(u => u.Login == userDto.Login))
            {
                return Results.BadRequest("User already exists.");
            }

            var user = new User
            {
                Name = userDto.Name,
                Login = userDto.Login,
                Password = userDto.Password // hashed???
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Results.Created($"/users/{user.Id}", user);
        });

        app.MapPost("/login", async (MusicContext context, [FromQuery] string login, [FromQuery] string password) =>
        {
            var user = await context.Users.Where(x => x.Login.Equals(login) && x.Password.Equals(password)).FirstOrDefaultAsync();

            if (user == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(user);
        });

        app.MapGet("/users/{userId}/liked-tracks", async (MusicContext context, [FromRoute]int userId) =>
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null)
                return Results.NotFound();

            var likedTracks = user.LikedTracksIds.ToList();

            return Results.Ok(likedTracks);
        });
    }
}
