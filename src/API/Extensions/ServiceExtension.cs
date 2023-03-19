using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace API.Extensions
{
    public static class ServiceExtension
    {
        /// <summary>
        ///     Add all services and repository with their interfaces automatically.
        /// </summary>
        /// <param name="services"></param>
        public static void AddServicesAndRepositoryWithInterface(this IServiceCollection services)
        {
            // Load repositories from Infrastructure assembly
            var folderRepositories = "Infrastructure.Repositories";
            var repositoryTypes = Assembly.Load(nameof(Infrastructure)).GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == folderRepositories && t.Name.EndsWith("Repository"));

            // Load services from Application assembly
            var folderServices = "Application.Services";
            var serviceTypes = Assembly.Load(nameof(Application)).GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == folderServices && t.Name.EndsWith("Service"));

            foreach (var repositoryType in repositoryTypes)
            {
                var interfaceType = repositoryType.GetInterfaces().FirstOrDefault(i => i.Name == $"I{repositoryType.Name}");
                if (interfaceType is null)
                {
                    continue;
                }

                services.AddScoped(interfaceType, repositoryType);
            }

            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterfaces().FirstOrDefault(i => i.Name == $"I{serviceType.Name}");
                if (interfaceType is null)
                {
                    continue;
                }

                services.AddScoped(interfaceType, serviceType);
            }
        }

        /// <summary>
        ///     Add all validators automatically from the Application.Validators folder.
        /// </summary>
        /// <param name="services"></param>
        public static void AddValidators(this IServiceCollection services)
        {
            // Load repositories from Application assembly
            var folderValidators = "Application.Validators";
            var validatorTypes = Assembly.Load(nameof(Application)).GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == folderValidators && t.Name.EndsWith("Validator"));

            foreach (var validator in validatorTypes) services.AddScoped(validator);
        }
    }
}
