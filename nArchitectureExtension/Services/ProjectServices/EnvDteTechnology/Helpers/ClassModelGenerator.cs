using EnvDTE;
using nArchitectureExtension.Helpers;
using nArchitectureExtension.Models;
using nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nArchitectureExtension.Services.ProjectServices.EnvDteTechnology.Helpers
{
    public static class ClassModelGenerator
    {
        static ProjectItem _projectItem;

        public static ClassModel ClassModelBuilder(CodeClass codeClass,ProjectItem projectItem, string classNamespcae)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            _projectItem = projectItem;
            ClassModel result = new ClassModel();
            result.Name = PathHelper.GetFileNameWithoutExtension(GetPropertyItem(DteProjectVaraibles.FullPath));
            result.CustomToolNamespace = GetPropertyItem(DteProjectVaraibles.CustomToolNamespace);
            result.LocalPath = GetPropertyItem(DteProjectVaraibles.LocalPath);
            result.FullPath = GetPropertyItem(DteProjectVaraibles.FullPath);
            result.FileName = GetPropertyItem(DteProjectVaraibles.FileName);
            result.FileNameAndExtension = GetPropertyItem(DteProjectVaraibles.FileNameAndExtension);
            result.Extension = GetPropertyItem(DteProjectVaraibles.Extension);
            result.Namespace = classNamespcae;

            SetBaseClassList(codeClass, result.BaseClassList);
            SetProperties(codeClass, result.Properties);

            return result;
        }

        private static string GetPropertyItem(string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return _projectItem.Properties.Item(name)?.Value.ToString();
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

    }
}
