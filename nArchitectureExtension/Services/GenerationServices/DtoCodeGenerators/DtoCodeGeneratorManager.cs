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
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.CreatedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Created{result.Placeholder.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateDeletedDtoCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.DeletedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Deleted{result.Placeholder.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateModelDtoCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Dto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\{result.Placeholder.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateUpdatedDtoCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.UpdatedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Updated{result.Placeholder.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateListModelCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Model, generateProperties: false);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Models";
            string filePath = $"{directoryPath}\\{result.Placeholder.EntityName}ListModel.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateResult GetPropertyAndTemplate(string template, bool generateProperties = true)
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
            return new() { Placeholder = replacePropertyModel, Template = text };
        }
    }
}
