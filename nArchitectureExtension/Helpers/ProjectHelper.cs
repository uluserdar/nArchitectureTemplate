using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace nArchitectureExtension.Helpers
{
    public static class ProjectHelper
    {
        public static string Pluralize(string name)
        {
            return PluralizationService.CreateService(new CultureInfo("en-US")).Pluralize(name);
        }

        public static string CamelCase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return name;
            }
            return name[0].ToString(CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture) + name.Substring(1);
        }

        public static string GetAccessModifier(string value)
        {
            return CamelCase(value.Replace("vsCMAccess", ""));
        }
    }
}
