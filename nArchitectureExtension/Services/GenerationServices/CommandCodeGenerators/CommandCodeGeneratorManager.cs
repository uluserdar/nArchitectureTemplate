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
            TemplateAndPropertyResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.CreateCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Placeholders.PluralEntityName)}\\Commands\\Create{propertyAndTemplate.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\Create{propertyAndTemplate.Placeholders.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        public void GenerateDeleteCommandCode()
        {
            TemplateAndPropertyResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.DeleteCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Placeholders.PluralEntityName)}\\Commands\\Delete{propertyAndTemplate.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\Delete{propertyAndTemplate.Placeholders.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        public void GenerateUpdateCommandCode()
        {
            TemplateAndPropertyResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.UpdateCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Placeholders.PluralEntityName)}\\Commands\\Update{propertyAndTemplate.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\Update{propertyAndTemplate.Placeholders.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template)
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
            return new() { Placeholders = replacePropertyModel, Template = text };
        }
    }
}
