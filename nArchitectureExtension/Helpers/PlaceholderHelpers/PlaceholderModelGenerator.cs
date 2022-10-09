using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices;
using System.Text;

namespace nArchitectureExtension.Helpers.PlaceholderHelper
{
    public class PlaceholderModelGenerator
    {
        protected readonly ProjectModel _applicationProjectModel;
        private readonly ProjectModel _persistenceProjectModel;
        private readonly IProjectService _projectService;

        public PlaceholderModelGenerator(IProjectService projectService)
        {
            _projectService = projectService;
            _applicationProjectModel = _projectService.GetProjectModelFromName("Application");
            _persistenceProjectModel = _projectService.GetProjectModelFromName("Persistence");
        }

        public PlaceholderModel PlaceholderModelBuilder(PlaceholderSelector placeholderModelSelector, bool generateProperties = true)
        {
            var propertyBuilder = PropertyBuilder(generateProperties);
            var placeholderModel = new PlaceholderModel
            {
                ApplicationNamespace = placeholderModelSelector.GetApplicationNamespace ? _applicationProjectModel.Name : null,
                PersistenceNamespace = placeholderModelSelector.GetPersistenceNamespace ? _persistenceProjectModel.Name : null,
                EntityNamespace = placeholderModelSelector.GetEntityNamespace ? propertyBuilder.ClassModel.Namespace : null,
                EntityName = placeholderModelSelector.GetEntityName ? propertyBuilder.ClassModel.Name : null,
                PluralEntityName = placeholderModelSelector.GetPluralEntityName ? ProjectHelper.Pluralize(propertyBuilder.ClassModel.Name) : null,
                CamelCaseEntityName = placeholderModelSelector.GetCamelCaseEntityName ? ProjectHelper.CamelCase(propertyBuilder.ClassModel.Name) : null,
                Properties = placeholderModelSelector.GetProperties ? propertyBuilder.Properties.ToString() : null,
            };

            return placeholderModel;
        }

        public (string Properties, ClassModel ClassModel) PropertyBuilder(bool generateProperties = true)
        {
            ClassModel classModel = _projectService.GetSelectedClassModel();
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
