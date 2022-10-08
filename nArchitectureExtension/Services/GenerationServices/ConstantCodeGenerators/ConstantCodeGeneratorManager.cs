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
            TemplateAndPropertyResult result = GetPropertyAndTemplate(false, Resources.TemplateFiles.Constant);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholders.PluralEntityName)}\\Constants";
            string filePath = $"{directoryPath}\\{result.Placeholders.EntityName}Messages.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(bool generateProperties,string template)
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
            return new() { Placeholders = replacePropertyModel, Template = text };
        }
    }
}
