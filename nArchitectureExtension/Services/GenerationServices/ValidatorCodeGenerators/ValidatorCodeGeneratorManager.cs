using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.ValidatorCodeGenerators
{
    public class ValidatorCodeGeneratorManager : IValidatorCodeGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public ValidatorCodeGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateCreateValidatorCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.CreateCommandValidor);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Commands\\Create{result.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\Create{result.Placeholders.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateDeleteValidatorCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.DeleteCommandValidator);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Commands\\Delete{result.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\Delete{result.Placeholders.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateUpdateValidatorCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.UpdateCommandValidator);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Commands\\Update{result.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\Update{result.Placeholders.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool generateProperties = false)
        {
            PlaceholderModel replacePropertyModel = _replacePropertyModelGenerator.PlaceholderModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetPluralEntityName = true,
                },
                generateProperties: generateProperties);

            string text = PlaceholderHelper.ReplacePlaceholders(replacePropertyModel, template);
            return new() { Placeholders = replacePropertyModel, Template = text };
        }
    }
}
