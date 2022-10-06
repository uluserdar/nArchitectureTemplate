using EnvDTE;
using Microsoft.Extensions.DependencyInjection;
using nArchitectureExtension.Constants;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services;
using nArchitectureExtension.Services.ProjectServices;
using System.Linq;
using System.Text;

namespace nArchitectureExtension
{
    [Command(PackageIds.GenerateCodeFromEntityCommand)]
    internal sealed class GenerateCodeFromEntityCommand : BaseCommand<GenerateCodeFromEntityCommand>
    {
        private readonly IGenerationService _generationService;
        private readonly IProjectService _projectService;

        public GenerateCodeFromEntityCommand()
        {
            _generationService= nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IGenerationService>();
            _projectService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IProjectService>();

        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            if (_projectService.GetSelectedEntity().BaseClassList.Any(x=> x.Name=="Entity")) Generate();
            else await VS.MessageBox.ShowErrorAsync(Messages.InvalidSelectEntityMessage);
            
        }

        private void Generate()
        {
            _generationService.GenerateCreateCommandCode();
            _generationService.GenerateUpdateCommandCode();
            _generationService.GenerateDeleteCommandCode();
            _generationService.GenerateCreateValidatorCode();
            _generationService.GenerateUpdateValidatorCode();
            _generationService.GenerateDeleteValidatorCode();
            _generationService.GenerateCreatedDtoCode();
            _generationService.GenerateDeletedDtoCode();
            _generationService.GenerateUpdatedDtoCode();
            _generationService.GenerateInterfaceRepositoryCode();
            _generationService.GenerateRepositoryCode();
            _generationService.GenerateConstantCode();
            _generationService.GenerateDtoCode();
            _generationService.GenerateModelCode();
            _generationService.GenerateMappingProfileCode();
            _generationService.GenerateRuleCode();
            _generationService.GenerateGetByIdQueryCode();
            _generationService.GenerateGetListQueryCode();
            _generationService.GenerateGetListByDynamicQueryCode();
        }

    }
}
