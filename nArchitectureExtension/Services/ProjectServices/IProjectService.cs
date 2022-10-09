using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.ProjectServices
{
    public interface IProjectService
    {
        public ClassModel GetSelectedClassModel();
        public ProjectModel GetSelectedProjectModel();
        public ProjectModel GetProjectModelFromName(string name);
        public void InsertCodeToFunction(string projectName, string className,string insertCode);
    }
}
