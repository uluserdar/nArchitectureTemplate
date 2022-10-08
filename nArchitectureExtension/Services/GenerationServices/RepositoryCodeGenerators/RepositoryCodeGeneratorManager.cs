using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.PlaceholderHelper;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.RepositoryCodeGenerators
{
    public class RepositoryCodeGeneratorManager : IRepositoryCodeGeneratorService
    {
        private readonly PlaceholderModelGenerator _replacePropertyModelGenerator;

        public RepositoryCodeGeneratorManager(PlaceholderModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateInterfaceRepositoryCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.InterfaceRepository, getPersistenceNameSpace: false);
            string directoryPath = $"{PathHelper.GetApplicationRepositoriesDirectoryPath()}";
            string filePath = $"{directoryPath}\\I{result.Placeholder.EntityName}Repository.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateRepositoryCode()
        {
            TemplateResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Repository, getPersistenceNameSpace: true, generateProperties: false);
            string directoryPath = $"{PathHelper.GetPersistenceRepositoriesDirectoryPath()}";
            string filePath = $"{directoryPath}\\{result.Placeholder.EntityName}Repository.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateResult GetPropertyAndTemplate(string template, bool getPersistenceNameSpace, bool generateProperties = false)
        {
            PlaceholderModel replacePropertyModel = _replacePropertyModelGenerator.PlaceholderModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetEntityNamespace = true,
                    GetPersistenceNamespace = getPersistenceNameSpace
                },
                generateProperties: generateProperties);

            string text = PlaceholderHelper.ReplacePlaceholders(replacePropertyModel, template);
            return new() { Placeholder = replacePropertyModel, Template = text };
        }
    }
}
