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
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.CreateCommandValidor);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Commands\\Create{result.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\Create{result.Placeholder.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateDeleteValidatorCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.DeleteCommandValidator);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Commands\\Delete{result.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\Delete{result.Placeholder.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateUpdateValidatorCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.UpdateCommandValidator);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Commands\\Update{result.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\Update{result.Placeholder.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateResult GetPropertyAndTemplate(string template, bool generateProperties = false)
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
            return new() { Placeholder = replacePropertyModel, Template = text };
        }
    }
}
