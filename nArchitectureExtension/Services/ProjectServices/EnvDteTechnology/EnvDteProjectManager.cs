using EnvDTE80;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Helpers;

namespace nArchitectureExtension.Services.ProjectServices.EnvDteTechnology
{
    public class EnvDteProjectManager : IProjectService
    {
        private static readonly DTE2 dte = nArchitectureExtensionPackage._dte;
        public ProjectModel GetProjectFromName(string name)
        {
            return dte.GetProjectModelFromName(name);
        }
        public ClassModel GetSelectedEntity()
        {
            return dte.GetSelectedClassModel();
        }

        public ProjectModel GetSelectedProject()
        {
            return dte.GetSelectedProjectModel();
        }
    }
}
