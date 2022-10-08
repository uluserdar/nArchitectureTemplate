using nArchitectureExtension.Models;
using nArchitectureExtension.Services.GenerationServices.Constants;

namespace nArchitectureExtension.Helpers.PlaceholderHelper
{
    public static class PlaceholderHelper
    {
        public static string ReplacePlaceholders(PlaceholderModel placeholderModel, string template)
        {
            if (!string.IsNullOrWhiteSpace(placeholderModel.ApplicationNamespace))
                template = template.Replace(PlaceholderConstants.ApplicationNamespace, placeholderModel.ApplicationNamespace);

            if (!string.IsNullOrWhiteSpace(placeholderModel.PersistenceNamespace))
                template = template.Replace(PlaceholderConstants.PersistenceNamespace, placeholderModel.PersistenceNamespace);

            if (!string.IsNullOrWhiteSpace(placeholderModel.PluralEntityName))
                template = template.Replace(PlaceholderConstants.PluralEntityName, placeholderModel.PluralEntityName);

            if (!string.IsNullOrWhiteSpace(placeholderModel.EntityName))
                template = template.Replace(PlaceholderConstants.EntityName, placeholderModel.EntityName);

            if (!string.IsNullOrWhiteSpace(placeholderModel.EntityNamespace))
                template = template.Replace(PlaceholderConstants.EntityNamespace, placeholderModel.EntityNamespace);

            if (!string.IsNullOrWhiteSpace(placeholderModel.CamelCaseEntityName))
                template = template.Replace(PlaceholderConstants.CamelCaseEntityName, placeholderModel.CamelCaseEntityName);

            if (!string.IsNullOrWhiteSpace(placeholderModel.Properties))
                template = template.Replace(PlaceholderConstants.Properties, placeholderModel.Properties);

            return template;
        }
    }
}
