using nArchitectureExtension.Models;

namespace nArchitectureExtension.Services.ProjectServices
{
    public interface IProjectService
    {
        public ClassModel GetSelectedEntity();
        public ProjectModel GetSelectedProject();
        public ProjectModel GetProjectFromName(string name);
    }
}
