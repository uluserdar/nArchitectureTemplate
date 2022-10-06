using EnvDTE;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.GenerationServices.TemplateFileTechnology.Constants;
using nArchitectureExtension.Services.ProjectServices;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace nArchitectureExtension.Services.GenerationServices.TemplateFileTechnology
{
    public class GenerationManager : IGenerationService
    {
        private readonly ProjectModel _applicationProjectModel;
        private readonly ProjectModel _persistenceProjectModel;

        private readonly IProjectService _projectService;
        public GenerationManager(IProjectService projectService)
        {
            _projectService = projectService;
            
            _applicationProjectModel = _projectService.GetProjectFromName("Application");
            _persistenceProjectModel= _projectService.GetProjectFromName("Persistence");
        }

        public void GenerateConstantCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();
            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string entityNamespace = classModel.Namespace;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Constants";
            string filePath = $"{directoryPath}\\{entityName}Messages.cs";

            string text = Resources.TemplateFiles.Constant
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateCreateCommandCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();
            StringBuilder properties = new StringBuilder();

            foreach (PropertyModel propertyModel in classModel.Properties)
            {
                properties.AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");
            }

            string applicationNamespace = _applicationProjectModel.Name;
            string entityNamespace = classModel.Namespace;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);
            string camelCaseEntityName = ProjectHelper.CamelCase(classModel.Name);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Commands\\Create{entityName}";
            string filePath = $"{directoryPath}\\Create{entityName}Command.cs";

            string text = Resources.TemplateFiles.CreateCommandTemplate
                 .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                 .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                 .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                 .Replace(PlaceHolderConstants.EntityName, entityName)
                 .Replace(PlaceHolderConstants.CamelCaseEntityName, camelCaseEntityName)
                 .Replace(PlaceHolderConstants.Properties, properties.ToString());

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateCreatedDtoCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            StringBuilder properties = new StringBuilder();

            foreach (PropertyModel propertyModel in classModel.Properties)
            {
                properties.AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");
            }

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Dtos";
            string filePath = $"{directoryPath}\\Created{entityName}Dto.cs";

            string text = Resources.TemplateFiles.CreatedDto
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.Properties,properties.ToString());

                FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateCreateValidatorCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Commands\\Create{entityName}";
            string filePath = $"{directoryPath}\\Create{entityName}CommandValidator.cs";

            string text = Resources.TemplateFiles.CreateCommandValidor
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateDeleteCommandCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            StringBuilder properties = new StringBuilder();

            foreach (PropertyModel propertyModel in classModel.Properties)
            {
                properties.AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");
            }

            string applicationNamespace = _applicationProjectModel.Name;
            string entityNamespace = classModel.Namespace;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);
            string camelCaseEntityName = ProjectHelper.CamelCase(classModel.Name);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Commands\\Delete{entityName}";
            string filePath = $"{directoryPath}\\Delete{entityName}Command.cs";

            string text = Resources.TemplateFiles.DeleteCommandTemplate
                 .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                 .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                 .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                 .Replace(PlaceHolderConstants.EntityName, entityName)
                 .Replace(PlaceHolderConstants.CamelCaseEntityName, camelCaseEntityName)
                 .Replace(PlaceHolderConstants.Properties, properties.ToString());

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateDeletedDtoCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            StringBuilder properties = new StringBuilder();

            foreach (PropertyModel propertyModel in classModel.Properties)
            {
                properties.AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");
            }

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Dtos";
            string filePath = $"{directoryPath}\\Deleted{entityName}Dto.cs";

            string text = Resources.TemplateFiles.DeletedDto
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.Properties, properties.ToString());

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateDeleteValidatorCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Commands\\Delete{entityName}";
            string filePath = $"{directoryPath}\\Delete{entityName}CommandValidator.cs";

            string text = Resources.TemplateFiles.DeleteCommandValidator
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateDtoCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            StringBuilder properties = new StringBuilder();

            foreach (PropertyModel propertyModel in classModel.Properties)
            {
                properties.AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");
            }

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Dtos";
            string filePath = $"{directoryPath}\\{entityName}Dto.cs";

            string text = Resources.TemplateFiles.Dto
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.Properties, properties.ToString());

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateGetByIdQueryCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityNamespace = classModel.Namespace;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Queries\\GetById{entityName}";
            string filePath = $"{directoryPath}\\GetById{entityName}Query.cs";

            string text = Resources.TemplateFiles.GetByIdQuery
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.CamelCaseEntityName, ProjectHelper.CamelCase(entityName));

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateGetListByDynamicQueryCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityNamespace = classModel.Namespace;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Queries\\GetListByDynamic{entityName}";
            string filePath = $"{directoryPath}\\GetListByDynamic{entityName}Query.cs";

            string text = Resources.TemplateFiles.GetListByDynamic
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.CamelCaseEntityName, ProjectHelper.CamelCase(entityName));

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateGetListQueryCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityNamespace = classModel.Namespace;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Queries\\GetList{entityName}";
            string filePath = $"{directoryPath}\\GetList{entityName}Query.cs";

            string text = Resources.TemplateFiles.GetListQuery
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.CamelCaseEntityName, ProjectHelper.CamelCase(entityName));

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateInterfaceRepositoryCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string entityNamespace = classModel.Namespace;

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Services\\Repositories";
            string filePath = $"{directoryPath}\\I{entityName}Repository.cs";

            string text = Resources.TemplateFiles.InterfaceRepository
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.EntityNamespace,entityNamespace);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateMappingProfileCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityNamespace = classModel.Namespace;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Profiles";
            string filePath = $"{directoryPath}\\MappingProfile.cs";

            string text = Resources.TemplateFiles.MappingProfile
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateModelCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Models";
            string filePath = $"{directoryPath}\\{entityName}ListModel.cs";

            string text = Resources.TemplateFiles.Model
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateRepositoryCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string persistenceNamespace = _persistenceProjectModel.Name;
            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string entityNamespace = classModel.Namespace;

            string directoryPath = $"{Path.GetDirectoryName(_persistenceProjectModel.FullPath)}\\Repositories";
            string filePath = $"{directoryPath}\\{entityName}Repository.cs";

            string text = Resources.TemplateFiles.Repository
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                .Replace(PlaceHolderConstants.PersistenceNamespace,persistenceNamespace);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateRuleCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Rules";
            string filePath = $"{directoryPath}\\{entityName}BusinessRule.cs";

            string text = Resources.TemplateFiles.Rule
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateUpdateCommandCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            StringBuilder properties = new StringBuilder();

            foreach (PropertyModel propertyModel in classModel.Properties)
            {
                properties.AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");
            }

            string applicationNamespace = _applicationProjectModel.Name;
            string entityNamespace = classModel.Namespace;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);
            string camelCaseEntityName = ProjectHelper.CamelCase(classModel.Name);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Commands\\Update{entityName}";
            string filePath = $"{directoryPath}\\Update{entityName}Command.cs";

            string text = Resources.TemplateFiles.UpdateCommandTemplate
                 .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                 .Replace(PlaceHolderConstants.EntityNamespace, entityNamespace)
                 .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                 .Replace(PlaceHolderConstants.EntityName, entityName)
                 .Replace(PlaceHolderConstants.CamelCaseEntityName, camelCaseEntityName)
                 .Replace(PlaceHolderConstants.Properties, properties.ToString());

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateUpdatedDtoCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            StringBuilder properties = new StringBuilder();

            foreach (PropertyModel propertyModel in classModel.Properties)
            {
                properties.AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");
            }

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Dtos";
            string filePath = $"{directoryPath}\\Updated{entityName}Dto.cs";

            string text = Resources.TemplateFiles.UpdatedDto
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName)
                .Replace(PlaceHolderConstants.Properties, properties.ToString());

            FileHelper.FileCreate(directoryPath, filePath, text);
        }

        public void GenerateUpdateValidatorCode()
        {
            ClassModel classModel = _projectService.GetSelectedEntity();

            string applicationNamespace = _applicationProjectModel.Name;
            string entityName = classModel.Name;
            string pluralEntityName = ProjectHelper.Pluralize(entityName);

            string directoryPath = $"{Path.GetDirectoryName(_applicationProjectModel.FullPath)}\\Features\\{pluralEntityName}\\Commands\\Update{entityName}";
            string filePath = $"{directoryPath}\\Update{entityName}CommandValidator.cs";

            string text = Resources.TemplateFiles.UpdateCommandValidator
                .Replace(PlaceHolderConstants.ApplicationNamespace, applicationNamespace)
                .Replace(PlaceHolderConstants.EntityName, entityName)
                .Replace(PlaceHolderConstants.PluralEntityName, pluralEntityName);

            FileHelper.FileCreate(directoryPath, filePath, text);
        }
    }
}
