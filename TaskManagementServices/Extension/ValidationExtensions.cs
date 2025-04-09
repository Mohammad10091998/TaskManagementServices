using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;

namespace TaskManagementServices.Extension
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(); 
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 

            return services;
        }
    }
}
