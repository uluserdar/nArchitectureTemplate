using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nArchitectureExtension.Services.GenerationServices.TemplateFileTechnology.Constants
{
    public static class PlaceHolderConstants
    {
        public static string ApplicationNamespace=> "{ApplicationNamespace}";
        public static string EntityName => "{EntityName}";
        public static string CamelCaseEntityName => "{entityName}";
        public static string PluralEntityName => "{PluralEntityName}";
        public static string EntityNamespace => "{EntityNamespace}";
        public static string Properties => "{Properties}";
        public static string PersistenceNamespace => "{PersistenceNamespace}";
    }
}
