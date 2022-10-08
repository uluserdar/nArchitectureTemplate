using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.ConstantCodeGenerators
{
    public class ConstantCodeGeneratorManager : IConstantCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public ConstantCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateConstantCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(false, Resources.TemplateFiles.Constant);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Constants";
            string filePath = $"{directoryPath}\\{result.Properties.EntityName}Messages.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(bool generateProperties,string template)
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
