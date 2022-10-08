using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.QueryCodeGenerators
{
    public class QueryCodeGeneratorManager : IQueryCodeGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public QueryCodeGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateGetByIdQueryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetByIdQuery);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Queries\\GetById{result.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\GetById{result.Placeholders.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateGetListByDynamicQueryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetListByDynamic);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Queries\\GetListByDynamic{result.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\GetListByDynamic{result.Placeholders.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateGetListQueryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetListQuery);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Queries\\GetList{result.Placeholders.EntityName}";
            string filePath = $"{directoryPath}\\GetList{result.Placeholders.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool generateProperties = false)
        {
            PlaceholderModel replacePropertyModel = _replacePropertyModelGenerator.PlaceholderModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetEntityNamespace = true,
                    GetPluralEntityName = true,
                    GetCamelCaseEntityName = true,
                },
                generateProperties: generateProperties);

            string text = PlaceholderHelper.ReplacePlaceholders(replacePropertyModel, template);
            return new() { Placeholders = replacePropertyModel, Template = text };
        }
    }
}
