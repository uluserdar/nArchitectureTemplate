using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.ConstantCodeGenerators
{
    public class ConstantCodeGeneratorManager : IConstantCodeGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public ConstantCodeGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateConstantCode()
        {
            TemplateResult result = GetPropertyAndTemplate(false, Resources.TemplateFiles.Constant);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Constants";
            string filePath = $"{directoryPath}\\{result.Placeholder.EntityName}Messages.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateResult GetPropertyAndTemplate(bool generateProperties,string template)
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
