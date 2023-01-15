using Microsoft.EntityFrameworkCore;
using RunnerApp.Data;
using RunnerApp.Helpers;
using RunnerApp.Interfaces;
using RunnerApp.Services;

namespace RunnerApp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IMapRepository, MapRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddControllers();
            services.AddCors();
            services.AddScoped<IMessageRepository, MessageRepository>();

            return services;
        }
    }
}
