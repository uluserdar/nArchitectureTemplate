using EnvDTE;
using EnvDTE80;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Constants;
using System.Collections.Generic;
using System.Linq;
using Project = EnvDTE.Project;

namespace nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Helpers
{
    public static class EnvDteHelper
    {
        public static ClassModel GetSelectedClassModel(this DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ProjectItem projectItem = GetSelectedProjectItem(dte);
            if (!projectItem.Saved) projectItem.Save();
            FileCodeModel fileCodeModel = GetFileCodeModel(projectItem);
            CodeNamespace codeNamespace = GetCodeNamespace(fileCodeModel);
            CodeClass codeClass = GetCodeClass(codeNamespace);

            return ClassModelGenerator.ClassModelBuilder(codeClass, projectItem, codeNamespace.Name);
        }

        public static ProjectModel GetSelectedProjectModel(this DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Project project = GetSelectedProject(dte);
            CodeModel codeModel = project.CodeModel as CodeModel;
            CodeNamespace codeNamespace = GetCodeProjectNamespace(codeModel);

            return ProjectModelGenerator.ProjectModelBuilder(project,codeNamespace.Name);
        }

        public static ProjectModel GetProjectModelFromName(this DTE2 dte, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IEnumerable<Project> projects = dte.Solution.Projects.Cast<Project>();
            Project project = GetProjectFromName(projects, name);
            CodeModel codeModel = project.CodeModel as CodeModel;
            CodeNamespace codeNamespace = GetCodeProjectNamespace(codeModel);

            return ProjectModelGenerator.ProjectModelBuilder(project,codeNamespace.Name);
        }

        private static Project GetSelectedProject(DTE2 dte)
        {
            return GetSelectedProjectItem(dte)?.ContainingProject;
        }

        private static Project GetProjectFromName(IEnumerable<Project> projects, string name)
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

        private static ProjectItem GetSelectedProjectItem(DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ProjectItem result = null;
            foreach (SelectedItem item in dte.SelectedItems)
            {
                if (item.ProjectItem != null) result = item.ProjectItem;
            }

            return result;
        }

        private static FileCodeModel GetFileCodeModel(ProjectItem projectItem)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (projectItem == null) return null;

            return projectItem.FileCodeModel as FileCodeModel;
        }

        private static CodeClass GetCodeClass(CodeNamespace codeNamespace)
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

        private static CodeNamespace GetCodeNamespace(FileCodeModel fileCodeModel)
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

        private static CodeNamespace GetCodeProjectNamespace(CodeModel codeModel)
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

    }
}
