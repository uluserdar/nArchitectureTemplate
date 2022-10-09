using nArchitectureExtension.Constants;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nArchitectureExtension.Services.GenerationServices.PersistenceServiceGeneretors
{
    public class PersistenceServiceGeneretorManager : IPersistenceServiceGeneratorService
    {
        private readonly IProjectService _projectService;

        public PersistenceServiceGeneretorManager(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public void InsertPersistenceServiceCode()
        {
            ClassModel selectedEntity = _projectService.GetSelectedClassModel();

            string insertCode = $"services.AddScoped<I{selectedEntity.Name}Repository, {selectedEntity.Name}Repository>();";
            _projectService.InsertCodeToFunction(SolutionVariables.PersistenceProjectName, SolutionVariables.PersistenceServiceRegistrationClassName, insertCode);
        }
    }
}
