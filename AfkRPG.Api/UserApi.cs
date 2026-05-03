using AfkRPG.Api;
using AfkRPG.Application.Infrastructure;
using AfkRPG.Application.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;
public static class UserApi
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/users/login", async (LoginRequest req, AfkRPGContext db, IConfiguration config) =>
        {
            var user = await db.users
                .FirstOrDefaultAsync(u => u.username == req.username && u.password == req.password);

            if (user is null) return Results.Unauthorized();
            var token = CreateToken(user, config);
            return Results.Ok(new { token });
        });

        app.MapPost("/users/register", async (RegisterRequest req, AfkRPGContext db) =>
        {
            var exists = await db.users
                .AnyAsync(u => u.username == req.username || u.email == req.email);

            if (exists) return Results.BadRequest("Username or email already taken.");

            var user = new users(req.username, DateTime.Now, DateTime.Now, false, req.email, req.password);
            db.users.Add(user);
            db.units.Add(UnitGenerator.Generate(user,"common"));
            db.units.Add(UnitGenerator.Generate(user,"common"));
            db.units.Add(UnitGenerator.Generate(user,"common"));
            await db.SaveChangesAsync();

            return Results.Ok(new { user.Id });
        });
    }

    private static string CreateToken(users user, IConfiguration config)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
        new Claim("id", user.Id.ToString()),
        new Claim("username", user.username)
    };
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(7), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record LoginRequest(string username, string password);
public record RegisterRequest(string username, string email, string password);