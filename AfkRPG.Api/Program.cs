using AfkRPG.Api;
using AfkRPG.Application.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddDbContext<AfkRPGContext>(opt => opt.UseSqlite("Data Source=afkrpg.db"));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("ilikelemonicecreambutmangoisalsoreallygoodinmyopinion")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AfkRPGContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/units", async (ClaimsPrincipal user, AfkRPGContext db) => {
    var userId = int.Parse(user.FindFirst("id")!.Value);
    return await db.units
        .Where(u => u.player.Id == userId)
        .ToListAsync();
}).RequireAuthorization();
app.MapUserEndpoints();

for (int i = 0; i < 30; i++)
    Console.WriteLine(NameGenerator.Generate(noble: false));

Console.WriteLine("nobles");

for (int i = 0; i < 30; i++)
    Console.WriteLine(NameGenerator.Generate(noble: true));

app.Run();

