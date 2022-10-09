using nArchitectureExtension.Constants;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nArchitectureExtension.Services.GenerationServices.BaseDbContextGenerators
{
    public class BaseDbContextManager : IBaseDbContextService
    {
        private readonly IProjectService _projectService;

        public BaseDbContextManager(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public void InsertBaseDbContextCode()
        {
            ProjectModel domainProjectModel = _projectService.GetProjectModelFromName(SolutionVariables.DomainProjectName);
            ClassModel selectedEntity = _projectService.GetSelectedClassModel();
            string newNamespace = $"{domainProjectModel.Name}.{SolutionVariables.EntitiesFolderName}";

            string insertCode = $"public DbSet<{newNamespace}.{selectedEntity.Name}> {ProjectHelper.Pluralize(selectedEntity.Name)} {{ get; set; }}";
            _projectService.InserPropertyToClass(SolutionVariables.PersistenceProjectName, SolutionVariables.BaseDbContextClassName, insertCode);
        }
    }
}
