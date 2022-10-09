using EnvDTE;
using EnvDTE80;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Helpers;
using System.Collections.Generic;
using System.Linq;
using Project = EnvDTE.Project;

namespace nArchitectureExtension.Services.ProjectServices.EnvDteTechnology
{
    public class EnvDteProjectManager : IProjectService
    {
        private static readonly DTE2 dte = nArchitectureExtensionPackage._dte;
        public ProjectModel GetProjectModelFromName(string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IEnumerable<Project> projects = dte.Solution.Projects.Cast<Project>();
            Project project = EnvDteHelper.GetProjectFromName(projects, name);
            CodeModel codeModel = project.CodeModel as CodeModel;
            CodeNamespace codeNamespace = EnvDteHelper.GetCodeProjectNamespace(codeModel);

            return ProjectModelGenerator.ProjectModelBuilder(project, codeNamespace.Name);
        }
        public ClassModel GetSelectedClassModel()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ProjectItem projectItem = EnvDteHelper.GetSelectedProjectItem(dte);
            if (!projectItem.Saved) projectItem.Save();
            FileCodeModel fileCodeModel = EnvDteHelper.GetFileCodeModel(projectItem);
            CodeNamespace codeNamespace = EnvDteHelper.GetCodeNamespace(fileCodeModel);
            CodeClass codeClass = EnvDteHelper.GetCodeClass(codeNamespace);

            return ClassModelGenerator.ClassModelBuilder(codeClass, projectItem, codeNamespace.Name);
        }

        public ProjectModel GetSelectedProjectModel()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Project project = EnvDteHelper.GetSelectedProject(dte);
            CodeModel codeModel = project.CodeModel as CodeModel;
            CodeNamespace codeNamespace = EnvDteHelper.GetCodeProjectNamespace(codeModel);

            return ProjectModelGenerator.ProjectModelBuilder(project, codeNamespace.Name);
        }

        public void InsertCodeToFunction(string projectName,string className,string insertCode)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IEnumerable<Project> projects = dte.Solution.Projects.Cast<Project>();
            Project project = EnvDteHelper.GetProjectFromName(projects, projectName);
            FileCodeModel fileCodeModel=EnvDteHelper.GetCodeClassFromName(project, className);
            CodeNamespace codeNamespace = EnvDteHelper.GetCodeNamespace(fileCodeModel);
            CodeClass codeClass = EnvDteHelper.GetCodeClass(codeNamespace);
           
            foreach (var item in codeClass.Members.OfType<CodeFunction>())
            {
                TextPoint startPoint = item.GetStartPoint(vsCMPart.vsCMPartHeader);
                TextPoint endPoint = item.GetEndPoint();
           
                var str = startPoint.CreateEditPoint().GetText(endPoint);

                if (!str.Contains(insertCode))
                {
                    TextPoint textPoint = item.GetStartPoint(vsCMPart.vsCMPartBody);
                    textPoint.CreateEditPoint().Insert("\t\t\t"+insertCode+System.Environment.NewLine);
                }
            }
        }
    }
}
