using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.CommandCodeGenerators
{
    public class CommandCodeGeneratorManager : ICommandCodeGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public CommandCodeGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateCreateCommandCode()
        {
            TemplateResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.CreateCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Placeholder.PluralEntityName)}\\Commands\\Create{propertyAndTemplate.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\Create{propertyAndTemplate.Placeholder.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        public void GenerateDeleteCommandCode()
        {
            TemplateResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.DeleteCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Placeholder.PluralEntityName)}\\Commands\\Delete{propertyAndTemplate.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\Delete{propertyAndTemplate.Placeholder.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        public void GenerateUpdateCommandCode()
        {
            TemplateResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.UpdateCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Placeholder.PluralEntityName)}\\Commands\\Update{propertyAndTemplate.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\Update{propertyAndTemplate.Placeholder.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        private TemplateResult GetPropertyAndTemplate(string template)
        {
            PlaceholderModel replacePropertyModel = _replacePropertyModelGenerator.PlaceholderModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetCamelCaseEntityName = true,
                    GetEntityName = true,
                    GetEntityNamespace = true,
                    GetPluralEntityName = true,
                    GetProperties = true
                });

            string text = PlaceholderHelper.ReplacePlaceholders(replacePropertyModel, template);
            return new() { Placeholder = replacePropertyModel, Template = text };
        }
    }
}
