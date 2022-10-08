using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.MappingProfileCodeGenerators
{
    public class MappingProfileCodeGeneratorManager : IMappingProfileCodeGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public MappingProfileCodeGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateMappingProfileCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.MappingProfile);
            string directoryPath = $"{PathHelper.GetApplicationFeaturesDirectoryPath(result.Placeholder.PluralEntityName)}\\Profiles";
            string filePath = $"{directoryPath}\\MappingProfile.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateResult GetPropertyAndTemplate(string template, bool generateProperties = false)
        {
            PlaceholderModel replacePropertyModel = _replacePropertyModelGenerator.PlaceholderModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetPluralEntityName = true,
                    GetEntityNamespace = true,
                },
                generateProperties: generateProperties);

            string text = PlaceholderHelper.ReplacePlaceholders(replacePropertyModel, template);
            return new() { Placeholder = replacePropertyModel, Template = text };
        }
    }
}
