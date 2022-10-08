using nArchitectureExtension.Helpers;
using nArchitectureExtension.Helpers.ReplaceProperty;
using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.GenerationServices.RepositoryCodeGenerators
{
    public class RepositoryCodeGeneratorManager : IRepositoryCodeGeneratorService
    {
        private readonly ReplacePropertyModelGenerator _replacePropertyModelGenerator;

        public RepositoryCodeGeneratorManager(ReplacePropertyModelGenerator replacePropertyModelGenerator)
        {
            _replacePropertyModelGenerator = replacePropertyModelGenerator;
        }

        public void GenerateInterfaceRepositoryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.InterfaceRepository, getPersistenceNameSpace: false);
            string directoryPath = $"{PathHelper.GetApplicationRepositoriesDirectoryPath()}";
            string filePath = $"{directoryPath}\\I{result.Properties.EntityName}Repository.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        public void GenerateRepositoryCode()
        {
            TemplateAndPropertyResult result = GetPropertyAndTemplate(Resources.TemplateFiles.Repository, getPersistenceNameSpace: true, generateProperties: false);
            string directoryPath = $"{PathHelper.GetPersistenceRepositoriesDirectoryPath()}";
            string filePath = $"{directoryPath}\\{result.Properties.EntityName}Repository.cs";
            FileHelper.FileCreate(directoryPath, filePath, result.Template);
        }

        private TemplateAndPropertyResult GetPropertyAndTemplate(string template, bool getPersistenceNameSpace, bool generateProperties = false)
        {
            ReplacePropertyModel replacePropertyModel = _replacePropertyModelGenerator.ReplacePropertyModelBuilder(
                new()
                {
                    GetApplicationNamespace = true,
                    GetEntityName = true,
                    GetEntityNamespace = true,
                    GetPersistenceNamespace = getPersistenceNameSpace
                },
                generateProperties: generateProperties);

            string text = ReplacePropertyHelper.ReplaceProperties(replacePropertyModel, template);
            return new() { Properties = replacePropertyModel, Template = text };
        }
    }
}
