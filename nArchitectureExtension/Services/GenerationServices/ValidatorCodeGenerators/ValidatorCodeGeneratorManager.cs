using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.ValidatorCodeGenerators
{
    public class ValidatorCodeGeneratorManager : IValidatorCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public ValidatorCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateCreateValidatorCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.CreateCommandValidor);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Commands\\Create{result.Properties.EntityName}";
            string filePath = $"{directoryPath}\\Create{result.Properties.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateDeleteValidatorCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.DeleteCommandValidator);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Commands\\Delete{result.Properties.EntityName}";
            string filePath = $"{directoryPath}\\Delete{result.Properties.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateUpdateValidatorCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.UpdateCommandValidator);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Commands\\Update{result.Properties.EntityName}";
            string filePath = $"{directoryPath}\\Update{result.Properties.EntityName}CommandValidator.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool generateProperties = false)
        {
            ReplacePropertyModel replacePropertyModel = _replacePropertyModelGenerator.ReplacePropertyModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetPluralEntityName = true,
                },
                generateProperties: generateProperties);

            string text = ReplacePropertyHelper.ReplaceProperties(replacePropertyModel, template);
            return new() { Properties = replacePropertyModel, Template = text };
        }
    }
}
