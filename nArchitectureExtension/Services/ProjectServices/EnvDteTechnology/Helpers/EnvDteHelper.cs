using EnvDTE;
using EnvDTE80;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Constants;
using System.Collections.Generic;
using System.Linq;
using VSLangProj80;
using Project = EnvDTE.Project;

namespace nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Helpers
{
    public static class EnvDteHelper
    {
        public static Project GetSelectedProject(DTE2 dte)
        {
            return GetSelectedProjectItem(dte)?.ContainingProject;
        }

        public static Project GetProjectFromName(IEnumerable<Project> projects, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (Project project in projects)
            {
                var projectName = project.Name;
                if (projectName.EndsWith(name) && !projectName.StartsWith("Core"))
                {
                    return project;
                }
                else if (project.Kind is ProjectKinds.vsProjectKindSolutionFolder)
                {
                    var subProjects = project
                            .ProjectItems
                            .OfType<ProjectItem>()
                            .Where(item => item.SubProject != null)
                            .Select(item => item.SubProject);

                    var projectInFolder = GetProjectFromName(subProjects, name);

                    if (projectInFolder != null)
                    {
                        return projectInFolder;
                    }
                }
            }

            return null;
        }

        public static ProjectItem GetSelectedProjectItem(DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ProjectItem result = null;
            foreach (SelectedItem item in dte.SelectedItems)
            {
                if (item.ProjectItem != null) result = item.ProjectItem;
            }

            return result;
        }

        public static FileCodeModel GetFileCodeModel(ProjectItem projectItem)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (projectItem == null) return null;

            return projectItem.FileCodeModel as FileCodeModel;
        }

        public static CodeClass GetCodeClass(CodeNamespace codeNamespace)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (codeNamespace == null) return null;

            CodeClass result = null;

            foreach (CodeClass codeClass in codeNamespace.Members)
            {
                if (codeClass.Kind == vsCMElement.vsCMElementClass) result = (CodeClass)codeClass;
            }
            return result;
        }

        public static CodeNamespace GetCodeNamespace(FileCodeModel fileCodeModel)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (fileCodeModel == null) return null;

            CodeNamespace result = null;

            foreach (CodeElement item in fileCodeModel.CodeElements)
            {
                if (item.Kind == vsCMElement.vsCMElementNamespace)
                {
                    result = (CodeNamespace)item;
                }
            }
            return result;
        }

        public static CodeNamespace GetCodeProjectNamespace(CodeModel codeModel)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (codeModel == null) return null;

            CodeNamespace result = null;

            foreach (CodeElement element in codeModel.CodeElements)
            {
                if (element.Kind == vsCMElement.vsCMElementNamespace) result = (CodeNamespace)element;
            }
            return result;
        }

        public static FileCodeModel GetCodeClassFromName(Project project,string className)
        {
            ProjectItem projectItem = null;
            foreach (var item in project.ProjectItems.OfType<ProjectItem>())
            {
                if (item?.Name == className)
                {
                    projectItem = item;
                    break;
                }
            }
            
            return GetFileCodeModel(projectItem);
        }
        public static FileCodeModel GetCodeClassFromName(Project project, string className,string folderName)
        {
            ProjectItem projectItem = null;
            foreach (var item in project.ProjectItems.OfType<ProjectItem>())
            {
                
                if (item?.Name == folderName)
                {
                    foreach (var x in item.ProjectItems.OfType<ProjectItem>())
                    {
                        if (x?.Name == className)
                        {
                            projectItem = x;
                            break;
                        }
                    }
                }
            }

            return GetFileCodeModel(projectItem);
        }
    }
}
