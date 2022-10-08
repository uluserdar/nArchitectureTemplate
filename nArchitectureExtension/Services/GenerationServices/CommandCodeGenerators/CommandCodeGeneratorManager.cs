using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.CommandCodeGenerators
{
    public class CommandCodeGeneratorManager : ICommandCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public CommandCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateCreateCommandCode()
        {
            TemplateAndPropertyResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.CreateCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Properties.PluralEntityName)}\\Commands\\Create{propertyAndTemplate.Properties.EntityName}";
            string filePath = $"{directoryPath}\\Create{propertyAndTemplate.Properties.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        public void GenerateDeleteCommandCode()
        {
            TemplateAndPropertyResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.DeleteCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Properties.PluralEntityName)}\\Commands\\Delete{propertyAndTemplate.Properties.EntityName}";
            string filePath = $"{directoryPath}\\Delete{propertyAndTemplate.Properties.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        public void GenerateUpdateCommandCode()
        {
            TemplateAndPropertyResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.UpdateCommandTemplate);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(propertyAndTemplate.Properties.PluralEntityName)}\\Commands\\Update{propertyAndTemplate.Properties.EntityName}";
            string filePath = $"{directoryPath}\\Update{propertyAndTemplate.Properties.EntityName}Command.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template)
        {
            ReplacePropertyModel replacePropertyModel = _replacePropertyModelGenerator.ReplacePropertyModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetCamelCaseEntityName = true,
                    GetEntityName = true,
                    GetEntityNamespace = true,
                    GetPluralEntityName = true,
                    GetProperties = true
                });

            string text = ReplacePropertyHelper.ReplaceProperties(replacePropertyModel, template);
            return new() { Properties = replacePropertyModel, Template = text };
        }
    }
}
