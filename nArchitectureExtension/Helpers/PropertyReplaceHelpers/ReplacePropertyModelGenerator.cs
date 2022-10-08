using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices;
using System.Text;

namespace nArchitectureExtension.Helpers.ReplaceProperty
{
    public class ReplacePropertyModelGenerator
    {
        protected readonly ProjectModel _applicationProjectModel;
        private readonly ProjectModel _persistenceProjectModel;
        private readonly IProjectService _projectService;

        public ReplacePropertyModelGenerator(IProjectService projectService)
        {
            _projectService = projectService;
            _applicationProjectModel = _projectService.GetProjectFromName("Application");
            _persistenceProjectModel = _projectService.GetProjectFromName("Persistence");
        }

        public ReplacePropertyModel ReplacePropertyModelBuilder(ReplacePropertyModelSelector replacePropertyModelSelector, bool generateProperties = true)
        {
            var propertyBuilder = PropertyBuilder(generateProperties);
            var replacePropertyModel = new ReplacePropertyModel
            {
                ApplicationNamespace = replacePropertyModelSelector.GetApplicationNamespace ? _applicationProjectModel.Name : null,
                PersistenceNamespace = replacePropertyModelSelector.GetPersistenceNamespace ? _persistenceProjectModel.Name : null,
                EntityNamespace = replacePropertyModelSelector.GetEntityNamespace ? propertyBuilder.ClassModel.Namespace : null,
                EntityName = replacePropertyModelSelector.GetEntityName ? propertyBuilder.ClassModel.Name : null,
                PluralEntityName = replacePropertyModelSelector.GetPluralEntityName ? ProjectHelper.Pluralize(propertyBuilder.ClassModel.Name) : null,
                CamelCaseEntityName = replacePropertyModelSelector.GetCamelCaseEntityName ? ProjectHelper.CamelCase(propertyBuilder.ClassModel.Name) : null,
                Properties = replacePropertyModelSelector.GetProperties ? propertyBuilder.Properties.ToString() : null,
            };

            return replacePropertyModel;
        }

        public (string Properties, ClassModel ClassModel) PropertyBuilder(bool generateProperties = true)
        {
            ClassModel classModel = _projectService.GetSelectedEntity();
            if (generateProperties)
            {
                StringBuilder properties = new();

                foreach (PropertyModel propertyModel in classModel.Properties)
                    properties
                        .AppendLine($"{ProjectHelper.GetAccessModifier(propertyModel.AccessModifier)} {propertyModel.Type} {propertyModel.Name} " + "{get;set;}");

                return (properties.ToString(), classModel);
            }

            return (null, classModel);
        }
    }
}
