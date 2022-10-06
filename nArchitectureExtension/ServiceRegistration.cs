using Microsoft.Extensions.DependencyInjection;
using nArchitectureExtension.Services;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology;
using nArchitectureExtension.Services.ProjectServices;
using nArchitectureExtension.Services.GenerationServices.TemplateFileTechnology;

namespace nArchitectureExtension
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, EnvDteProjectManager>();
            services.AddScoped<IGenerationService, GenerationManager>();
        }
    }
}
