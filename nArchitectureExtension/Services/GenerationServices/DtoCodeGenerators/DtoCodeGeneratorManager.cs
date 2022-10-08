using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.DtoCodeGenerators
{
    public class DtoCodeGeneratorManager : IDtoCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public DtoCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateCreatedDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.CreatedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Created{result.Properties.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateDeletedDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.DeletedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Deleted{result.Properties.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateModelDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Dto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\{result.Properties.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateUpdatedDtoCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.UpdatedDto);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Dtos";
            string filePath = $"{directoryPath}\\Updated{result.Properties.EntityName}Dto.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateListModelCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Model, generateProperties: false);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Models";
            string filePath = $"{directoryPath}\\{result.Properties.EntityName}ListModel.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool generateProperties = true)
        {
            ReplacePropertyModel replacePropertyModel = _replacePropertyModelGenerator.ReplacePropertyModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetPluralEntityName = true,
                    GetProperties = generateProperties
                },
                generateProperties: generateProperties);

            string text = ReplacePropertyHelper.ReplaceProperties(replacePropertyModel, template);
            return new() { Properties = replacePropertyModel, Template = text };
        }
    }
}
