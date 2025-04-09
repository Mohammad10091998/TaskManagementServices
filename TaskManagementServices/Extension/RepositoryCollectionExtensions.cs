using TaskManagementServices.Repositories.Interface;
using TaskManagementServices.Repositories;

namespace TaskManagementServices.Extension
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
