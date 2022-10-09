using Microsoft.Extensions.DependencyInjection;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Services.GenerationServices.ApplicationServiceGenerators;
using nArchitectureExtension.Services.GenerationServices.CommandCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.ConstantCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.DtoCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.MappingProfileCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.QueryCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.RepositoryCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.RuleCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.ValidatorCodeGenerators;
using nArchitectureExtension.Services.ProjectServices;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology;

namespace nArchitectureExtension
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, EnvDteProjectManager>();
            services.AddScoped<ICommandCodeGeneratorService, CommandCodeGeneratorManager>();
            services.AddScoped<IQueryCodeGeneratorService, QueryCodeGeneratorManager>();
            services.AddScoped<IConstantCodeGeneratorService, ConstantCodeGeneratorManager>();
            services.AddScoped<IDtoCodeGeneratorService, DtoCodeGeneratorManager>();
            services.AddScoped<IMappingProfileCodeGeneratorService, MappingProfileCodeGeneratorManager>();
            services.AddScoped<IRepositoryCodeGeneratorService, RepositoryCodeGeneratorManager>();
            services.AddScoped<IRuleCodeGeneratorService, RuleCodeGeneratorManager>();
            services.AddScoped<IValidatorCodeGeneratorService, ValidatorCodeGeneratorManager>();
            services.AddScoped<IApplicationServiceGeneratorService, ApplicationServiceGeneratorManager>();
            services.AddScoped<PlaceholderModelGenerator>();
        }
    }
}
