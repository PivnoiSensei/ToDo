using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Authentication;
using ToDoAPI.Data;
using ToDoAPI.Data.Repositories;
using ToDoAPI.Infrastructure.Authentication;
using ToDoAPI.Infrastructure.Behaviours;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;
using ToDoAPI.Services;

namespace ToDoAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            //Data Access
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            //Security
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddJwtAuthentication(configuration);

            return services;
        }

    }
}
