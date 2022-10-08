using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.DtoCodeGenerators
{
    public class DtoCodeGeneratorManager : IDtoCodeGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public DtoCodeGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateCreatedDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.CreatedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Created{result.Placeholders.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateDeletedDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.DeletedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Deleted{result.Placeholders.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateModelDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Dto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\{result.Placeholders.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateUpdatedDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.UpdatedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Updated{result.Placeholders.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateListModelCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Model, generateProperties: false);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Models";
            string filePath = $"{directoryPath}\\{result.Placeholders.EntityName}ListModel.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool generateProperties = true)
        {
            PlaceholderModel replacePropertyModel = _replacePropertyModelGenerator.PlaceholderModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetPluralEntityName = true,
                    GetProperties = generateProperties
                },
                generateProperties: generateProperties);

            string text = PlaceholderHelper.ReplacePlaceholders(replacePropertyModel, template);
            return new() { Placeholders = replacePropertyModel, Template = text };
        }
    }
}
