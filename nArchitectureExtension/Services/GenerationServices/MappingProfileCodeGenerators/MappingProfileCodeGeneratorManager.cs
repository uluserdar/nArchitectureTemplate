using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.MappingProfileCodeGenerators
{
    public class MappingProfileCodeGeneratorManager : IMappingProfileCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public MappingProfileCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateMappingProfileCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.MappingProfile);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Properties.PluralEntityName)}\\Profiles";
            string filePath = $"{directoryPath}\\MappingProfile.cs";
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
                    GetEntityNamespace = true,
                },
                generateProperties: generateProperties);

            string text = ReplacePropertyHelper.ReplaceProperties(replacePropertyModel, template);
            return new() { Properties = replacePropertyModel, Template = text };
        }
    }
}
