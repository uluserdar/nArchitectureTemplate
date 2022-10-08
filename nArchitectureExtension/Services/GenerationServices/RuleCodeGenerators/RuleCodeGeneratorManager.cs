using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.RuleCodeGenerators
{
    public class RuleCodeGeneratorManager : IRuleCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public RuleCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateRuleCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Rule);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Rules";
            string filePath = $"{directoryPath}\\{result.Properties.EntityName}BusinessRule.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool generateProperties = false)
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
