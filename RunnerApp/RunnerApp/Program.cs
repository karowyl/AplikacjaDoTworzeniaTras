using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RunnerApp.Data;
using RunnerApp.Extensions;
using RunnerApp.Interfaces;
using RunnerApp.Middleware;
using RunnerApp.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddApplicationServices(builder.Configuration);
 
builder.Services.AddIdentityServices(builder.Configuration);

// builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    //var userManager = services.GetRequiredService<UserManager<AppUser>>();
    //var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
    //await Seed.SeedUsers(userManager, roleManager);
}
catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

await app.RunAsync();
