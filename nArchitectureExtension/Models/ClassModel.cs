using System.Collections.Generic;

namespace nArchitectureExtension.Models
{
    public class ClassModel
    {
        public ClassModel()
        {
            Properties = new List<PropertyModel>();
            BaseClassList = new List<ClassModel>();
        }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string LocalPath { get; set; }
        public string FullPath { get; set; }
        public string FileNameAndExtension { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public string CustomToolNamespace { get; set; }
        public List<PropertyModel> Properties { get; set; }
        public List<ClassModel> BaseClassList { get; set; }
    }
}
