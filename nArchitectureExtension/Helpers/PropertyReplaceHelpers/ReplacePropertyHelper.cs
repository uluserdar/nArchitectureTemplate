using nArchitectureExtension.Models;
using nArchitectureExtension.Services.GenerationServices.Constants;

namespace nArchitectureExtension.Helpers.ReplaceProperty
{
    public static class ReplacePropertyHelper
    {
        public static string ReplaceProperties(ReplacePropertyModel replacePropertyModel, string template)
        {
            if (!string.IsNullOrWhiteSpace(replacePropertyModel.ApplicationNamespace))
                template = template.Replace(PlaceHolderConstants.ApplicationNamespace, replacePropertyModel.ApplicationNamespace);

            if (!string.IsNullOrWhiteSpace(replacePropertyModel.PersistenceNamespace))
                template = template.Replace(PlaceHolderConstants.PersistenceNamespace, replacePropertyModel.PersistenceNamespace);

            if (!string.IsNullOrWhiteSpace(replacePropertyModel.PluralEntityName))
                template = template.Replace(PlaceHolderConstants.PluralEntityName, replacePropertyModel.PluralEntityName);

            if (!string.IsNullOrWhiteSpace(replacePropertyModel.EntityName))
                template = template.Replace(PlaceHolderConstants.EntityName, replacePropertyModel.EntityName);

            if (!string.IsNullOrWhiteSpace(replacePropertyModel.EntityNamespace))
                template = template.Replace(PlaceHolderConstants.EntityNamespace, replacePropertyModel.EntityNamespace);

            if (!string.IsNullOrWhiteSpace(replacePropertyModel.CamelCaseEntityName))
                template = template.Replace(PlaceHolderConstants.CamelCaseEntityName, replacePropertyModel.CamelCaseEntityName);

            if (!string.IsNullOrWhiteSpace(replacePropertyModel.Properties))
                template = template.Replace(PlaceHolderConstants.Properties, replacePropertyModel.Properties);

            return template;
        }
    }
}
