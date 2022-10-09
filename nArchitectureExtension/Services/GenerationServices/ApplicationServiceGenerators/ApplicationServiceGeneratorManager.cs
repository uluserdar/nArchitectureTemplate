using nArchitectureExtension.Constants;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices;

namespace nArchitectureExtension.Services.GenerationServices.ApplicationServiceGenerators
{
    public class ApplicationServiceGeneratorManager : IApplicationServiceGeneratorService
    {
        private readonly IProjectService _projectService;

        public ApplicationServiceGeneratorManager(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public void InsertApplicationServiceCode()
        {
            ProjectModel applicationProjectModel = _projectService.GetProjectModelFromName(SolutionVariables.ApplicationProjectName);
            ClassModel selectedEntity = _projectService.GetSelectedClassModel();

            string newNamespace = $"{applicationProjectModel.Name}.{SolutionVariables.FeaturesFolderName}.{ProjectHelper.Pluralize(selectedEntity.Name)}.{SolutionVariables.RulesFolderName}";

            string insertCode = $"services.AddScoped<{newNamespace}.{selectedEntity.Name}BusinessRule>();";
            _projectService.InsertCodeToFunction(SolutionVariables.ApplicationProjectName, SolutionVariables.ApplicationServiceRegistrationClassName,insertCode);
        }
    }
}
