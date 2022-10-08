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
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetByIdQuery);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Queries\\GetById{result.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\GetById{result.Placeholder.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateGetListByDynamicQueryCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetListByDynamic);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Queries\\GetListByDynamic{result.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\GetListByDynamic{result.Placeholder.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateGetListQueryCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetListQuery);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Queries\\GetList{result.Placeholder.EntityName}";
            string filePath = $"{directoryPath}\\GetList{result.Placeholder.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateResult GetPropertyAndTemplate(string template, bool generateProperties = false)
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
            return new() { Placeholder = replacePropertyModel, Template = text };
        }
    }
}
