using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.WebApiControllerGenerators
{
    public class WebApiControllerGeneratorManager : IWebApiControllerGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public WebApiControllerGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateControllerCode()
        {
            TemplateResult propertyAndTemplate = GetPropertyAndTemplate(Resources.TemplateFiles.WebApiController);
            string directoryPath = $"{PathHelper.GetWebApiDirectoryPath()}\\Controllers";
            string filePath = $"{directoryPath}\\{propertyAndTemplate.Placeholder.PluralEntityName}Controller.cs";
            FileHelper.FileCreate(directoryPath, filePath, propertyAndTemplate.Template);
        }

        private TemplateResult GetPropertyAndTemplate(string template)
        {
            PlaceholderModel replacePropertyModel = _replacePropertyModelGenerator.PlaceholderModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetCamelCaseEntityName = false,
                    GetEntityName = true,
                    GetEntityNamespace = false,
                    GetPluralEntityName = true,
                    GetProperties = false
                });

            string text = PlaceholderHelper.ReplacePlaceholders(replacePropertyModel, template);
            return new() { Placeholder = replacePropertyModel, Template = text };
        }
    }
}
