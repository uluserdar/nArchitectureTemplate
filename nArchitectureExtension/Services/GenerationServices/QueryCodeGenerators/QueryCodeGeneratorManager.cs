using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.QueryCodeGenerators
{
    public class QueryCodeGeneratorManager : IQueryCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public QueryCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateGetByIdQueryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetByIdQuery);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Queries\\GetById{result.Properties.EntityName}";
            string filePath = $"{directoryPath}\\GetById{result.Properties.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateGetListByDynamicQueryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetListByDynamic);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Queries\\GetListByDynamic{result.Properties.EntityName}";
            string filePath = $"{directoryPath}\\GetListByDynamic{result.Properties.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateGetListQueryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.GetListQuery);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Queries\\GetList{result.Properties.EntityName}";
            string filePath = $"{directoryPath}\\GetList{result.Properties.EntityName}Query.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool generateProperties = false)
        {
            ReplacePropertyModel replacePropertyModel = _replacePropertyModelGenerator.ReplacePropertyModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetEntityNamespace = true,
                    GetPluralEntityName = true,
                    GetCamelCaseEntityName = true,
                },
                generateProperties: generateProperties);

            string text = ReplacePropertyHelper.ReplaceProperties(replacePropertyModel, template);
            return new() { Properties = replacePropertyModel, Template = text };
        }
    }
}
