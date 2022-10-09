using EnvDTE;
using Microsoft.Extensions.DependencyInjection;
using nArchitectureExtension.Constants;
using nArchitectureExtension.Services.GenerationServices.ApplicationServiceGenerators;
using nArchitectureExtension.Services.GenerationServices.BaseDbContextGenerators;
using nArchitectureExtension.Services.GenerationServices.CommandCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.ConstantCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.DtoCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.MappingProfileCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.PersistenceServiceGeneretors;
using nArchitectureExtension.Services.GenerationServices.QueryCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.RepositoryCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.RuleCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.ValidatorCodeGenerators;
using nArchitectureExtension.Services.GenerationServices.WebApiControllerGenerators;
using nArchitectureExtension.Services.ProjectServices;
using System.Linq;

namespace nArchitectureExtension
{
    [Command(PackageIds.GenerateCodeFromEntityCommand)]
    internal sealed class GenerateCodeFromEntityCommand : BaseCommand<GenerateCodeFromEntityCommand>
    {
        private readonly ICommandCodeGeneratorService _commandCodeGeneratorService;
        private readonly IQueryCodeGeneratorService _queryCodeGeneratorService;
        private readonly IConstantCodeGeneratorService _constantCodeGeneratorService;
        private readonly IDtoCodeGeneratorService _dtoCodeGeneratorService;
        private readonly IMappingProfileCodeGeneratorService _mappingProfileCodeGeneratorService;
        private readonly IRepositoryCodeGeneratorService _repositoryCodeGeneratorService;
        private readonly IRuleCodeGeneratorService _ruleCodeGeneratorService;
        private readonly IValidatorCodeGeneratorService _validatorCodeGeneratorService;
        private readonly IApplicationServiceGeneratorService _applicationGeneratorService;
        private readonly IPersistenceServiceGeneratorService _persistenceServiceGeneratorService;
        private readonly IBaseDbContextService _baseDbContextService;
        private readonly IWebApiControllerGeneratorService _webApiControllerGeneratorService;
        private readonly IProjectService _projectService;

        public GenerateCodeFromEntityCommand()
        {
            _commandCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<ICommandCodeGeneratorService>();
            _queryCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IQueryCodeGeneratorService>();
            _constantCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IConstantCodeGeneratorService>();
            _dtoCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IDtoCodeGeneratorService>();
            _mappingProfileCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IMappingProfileCodeGeneratorService>();
            _repositoryCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IRepositoryCodeGeneratorService>();
            _ruleCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IRuleCodeGeneratorService>();
            _validatorCodeGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IValidatorCodeGeneratorService>();
            _applicationGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IApplicationServiceGeneratorService>(); 
            _persistenceServiceGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IPersistenceServiceGeneratorService>();
            _baseDbContextService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IBaseDbContextService>();
            _webApiControllerGeneratorService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IWebApiControllerGeneratorService>();
            _baseDbContextService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IBaseDbContextService>();
            _projectService = nArchitectureExtensionPackage.Services.BuildServiceProvider().GetService<IProjectService>();
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            if (_projectService.GetSelectedClassModel().BaseClassList.Any(x => x.Name == "Entity"))
                Generate();
            else
                await VS.MessageBox.ShowErrorAsync(Messages.InvalidSelectEntityMessage);
        }

        private void Generate()
        {
            _commandCodeGeneratorService.GenerateCreateCommandCode();
            _commandCodeGeneratorService.GenerateUpdateCommandCode();
            _commandCodeGeneratorService.GenerateDeleteCommandCode();
            _validatorCodeGeneratorService.GenerateCreateValidatorCode();
            _validatorCodeGeneratorService.GenerateUpdateValidatorCode();
            _validatorCodeGeneratorService.GenerateDeleteValidatorCode();
            _dtoCodeGeneratorService.GenerateCreatedDtoCode();
            _dtoCodeGeneratorService.GenerateDeletedDtoCode();
            _dtoCodeGeneratorService.GenerateUpdatedDtoCode();
            _repositoryCodeGeneratorService.GenerateInterfaceRepositoryCode();
            _repositoryCodeGeneratorService.GenerateRepositoryCode();
            _constantCodeGeneratorService.GenerateConstantCode();
            _dtoCodeGeneratorService.GenerateModelDtoCode();
            _dtoCodeGeneratorService.GenerateListModelCode();
            _mappingProfileCodeGeneratorService.GenerateMappingProfileCode();
            _ruleCodeGeneratorService.GenerateRuleCode();
            _queryCodeGeneratorService.GenerateGetByIdQueryCode();
            _queryCodeGeneratorService.GenerateGetListQueryCode();
            _queryCodeGeneratorService.GenerateGetListByDynamicQueryCode();
            _applicationGeneratorService.InsertApplicationServiceCode();
            _persistenceServiceGeneratorService.InsertPersistenceServiceCode();
            _baseDbContextService.InsertBaseDbContextCode();
            _webApiControllerGeneratorService.GenerateControllerCode();

        }

    }
}
