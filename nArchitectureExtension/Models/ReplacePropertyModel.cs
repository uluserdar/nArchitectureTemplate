namespace nArchitectureExtension.Models
{
    public class ReplacePropertyModel
    {
        public string ApplicationNamespace { get; set; }
        public string PersistenceNamespace { get; set; }
        public string EntityNamespace { get; set; }
        public string PluralEntityName { get; set; }
        public string EntityName { get; set; }
        public string CamelCaseEntityName { get; set; }
        public string Properties { get; set; }
    }

    public struct ReplacePropertyModelSelector
    {
        public bool GetApplicationNamespace { get; set; }
        public bool GetEntityNamespace { get; set; }
        public bool GetPluralEntityName { get; set; }
        public bool GetEntityName { get; set; }
        public bool GetCamelCaseEntityName { get; set; }
        public bool GetProperties { get; set; }
        public bool GetPersistenceNamespace { get; set; }
    }
}
