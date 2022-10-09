using Community.VisualStudio.Toolkit;
using EnvDTE;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Constants;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project = EnvDTE.Project;

namespace nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Helpers
{
    public static class ProjectModelGenerator
    {
        static Project _project;
        public static ProjectModel ProjectModelBuilder(Project project,string projectNamespace)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            _project = project;
            var projectModel = new ProjectModel
            {
                Name = project.Name,
                Namespace = projectNamespace,
                DirectoryPath = Path.GetDirectoryName(GetPropertyItem(DteProjectVaraibles.FullPath)),
                FullPath = GetPropertyItem(DteProjectVaraibles.FullPath),
                LocalPath = GetPropertyItem(DteProjectVaraibles.LocalPath),
                LocalNamespace = GetPropertyItem(DteProjectVaraibles.LocalNamespace),
                RootNamespace = GetPropertyItem(DteProjectVaraibles.RootNamespace)
            };

            return projectModel;
        }

        private static string GetPropertyItem(string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return _project.Properties.Item(name)?.Value.ToString();
        }
    }
}
