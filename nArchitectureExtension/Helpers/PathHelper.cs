using Microsoft.Extensions.DependencyInjection;
using nArchitectureExtension.Services.ProjectServices;
using System.IO;

namespace nArchitectureExtension.Helpers
{
    public static class PathHelper
    {
        public static string GetApplicationFeaturesDirectoryPath(string pluralEntityName)
        {
            var projectService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IProjectService>();
            var applicationProjectModel = projectService.GetProjectModelFromName("Application");
            var result = $"{Path.GetDirectoryName(applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}";
            return result;
        }

        public static string GetPersistenceRepositoriesDirectoryPath()
        {
            var projectService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IProjectService>();
            var persistenceProjectModel = projectService.GetProjectModelFromName("Persistence");
            var result = $"{Path.GetDirectoryName(persistenceProjectModel.FullPath)}\\Repositories";
            return result;
        }

        public static string GetApplicationRepositoriesDirectoryPath()
        {
            var projectService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IProjectService>();
            var applicationProjectModel = projectService.GetProjectModelFromName("Application");
            var result = $"{Path.GetDirectoryName(applicationProjectModel.FullPath)}\\Services\\Repositories";
            return result;
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}
