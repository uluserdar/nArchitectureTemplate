using EnvDTE;
using EnvDTE80;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Constants;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Project = EnvDTE.Project;

namespace nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Extensions
{
    public static class DteExtension
    {

        public static ClassModel GetSelectedClassModel(this DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ProjectItem projectItem = GetSelectedProjectItem(dte);
            if (!projectItem.Saved) projectItem.Save();
            FileCodeModel fileCodeModel = GetFileCodeModel(projectItem);
            CodeNamespace codeNamespace = GetCodeNamespace(fileCodeModel);
            CodeClass codeClass = GetCodeClass(codeNamespace);



            ClassModel result = new ClassModel();
            result.Name = FileNameWithoutExtension(projectItem.Properties.Item(DteProjectVaraibles.FullPath).Value.ToString());
            result.CustomToolNamespace = projectItem.Properties.Item(DteProjectVaraibles.CustomToolNamespace).Value.ToString();
            result.LocalPath = projectItem.Properties.Item(DteProjectVaraibles.LocalPath).Value.ToString();
            result.FullPath = projectItem.Properties.Item(DteProjectVaraibles.FullPath).Value.ToString();
            result.FileName = projectItem.Properties.Item(DteProjectVaraibles.FileName).Value.ToString();
            result.FileNameAndExtension = projectItem.Properties.Item(DteProjectVaraibles.FileNameAndExtension).Value.ToString();
            result.Extension = projectItem.Properties.Item(DteProjectVaraibles.Extension).Value.ToString();
            result.Namespace = codeNamespace.Name;


            SetBaseClassList(codeClass, result.BaseClassList);
            SetProperties(codeClass, result.Properties);


            return result;
        }

        public static ProjectModel GetSelectedProjectModel(this DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Project project = GetSelectedProject(dte);
            CodeModel codeModel = project.CodeModel as CodeModel;
            CodeNamespace codeNamespace = GetCodeProjectNamespace(codeModel);

            List<string> directoryList = project.FullName.Split('\\').ToList();
            directoryList.RemoveAt(directoryList.Count - 1);
            string directoryPath = string.Join("\\", directoryList);

            ProjectItem projectItem = GetSelectedProjectItem(dte);

            ProjectModel result = new ProjectModel
            {
                Name = project.Name,
                Namespace = codeNamespace.Name,
                DirectoryPath = directoryPath,
                FullPath = projectItem.Properties.Item(DteProjectVaraibles.FullPath).Value.ToString(),
                LocalPath = projectItem.Properties.Item(DteProjectVaraibles.LocalPath).Value.ToString(),
                LocalNamespace = projectItem.Properties.Item(DteProjectVaraibles.LocalNamespace).Value.ToString(),
                RootNamespace = projectItem.Properties.Item(DteProjectVaraibles.RootNamespace).Value.ToString()
            };

            return result;
        }

        public static ProjectModel GetProjectModelFromName(this DTE2 dte, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IEnumerable<Project> projects = dte.Solution.Projects.Cast<Project>();
            Project project = GetProjectFromName(projects, name);
            CodeModel codeModel = project.CodeModel as CodeModel;
            CodeNamespace codeNamespace = GetCodeProjectNamespace(codeModel);

            List<string> directoryList = project.FullName.Split('\\').ToList();
            directoryList.RemoveAt(directoryList.Count - 1);
            string directoryPath = string.Join("\\", directoryList);


            ProjectModel result = new ProjectModel
            {
                Name = project.Name,
                Namespace = codeNamespace.Name,
                DirectoryPath = directoryPath,
                FullPath = project.Properties.Item(DteProjectVaraibles.FullPath).Value.ToString(),
                LocalPath = project.Properties.Item(DteProjectVaraibles.LocalPath).Value.ToString(),
                LocalNamespace = project.Properties.Item(DteProjectVaraibles.LocalNamespace).Value.ToString(),
                RootNamespace = project.Properties.Item(DteProjectVaraibles.RootNamespace).Value.ToString()
            };

            return result;
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

        private static void SetBaseClassList(CodeClass codeClass, List<ClassModel> list)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (codeClass == null) return;

            foreach (var item in codeClass.Bases)
            {
                list.Add(new ClassModel { Name = (item as CodeClass).Name });
            }
        }

        private static void SetProperties(CodeClass codeClass, List<PropertyModel> list)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (codeClass == null) return;

            foreach (var item in codeClass.Members)
            {
                if (item is CodeProperty)
                {
                    CodeProperty codeProperty = (CodeProperty)item;
                    list.Add(new PropertyModel
                    {
                        Name = codeProperty.Name,
                        Type = codeProperty.Type.AsString,
                        AccessModifier = codeProperty.Access.ToString()
                    });

                }
            }

            foreach (var item in codeClass.Bases)
            {
                SetProperties(item as CodeClass, list);
            }
        }

        private static string FileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

    }
}
