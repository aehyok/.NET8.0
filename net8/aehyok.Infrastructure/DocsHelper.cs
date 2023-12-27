using LoxSmoke.DocXml;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure
{
    public class DocsHelper
    {
        private static List<string> Docs { get; set; } = [];

        private static void LoadDocs()
        {
            if (Docs.Count == 0)
            {
                // 获取程序执行目录下所有 xml 文档
                Docs = [.. Directory.GetFiles(AppContext.BaseDirectory, "*.xml")];
            }
        }

        public static string GetDocPath(string assemblyName)
        {
            LoadDocs();

            assemblyName = assemblyName.Replace(".dll", "").Replace(".xml", "");
            var docPath = Docs.FirstOrDefault(a => Path.GetFileNameWithoutExtension(a).ToLower() == assemblyName.ToLower());
            if (docPath.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return docPath;
        }

        public static string GetTypeComments(string assemblyName, Type type)
        {
            var docPath = GetDocPath(assemblyName);
            if (docPath.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var reader = new DocXmlReader(docPath);
            return reader.GetTypeComments(type).Summary;
        }

        public static string GetMemberComments(string assemblyName, MemberInfo member)
        {
            var docPath = GetDocPath(assemblyName);
            if (docPath.IsNullOrEmpty() || member is null)
            {
                return string.Empty;
            }

            var reader = new DocXmlReader(docPath);
            return reader.GetMemberComments(member).Summary;
        }

        public static string GetMethodComments(string assemblyName, MethodInfo method)
        {
            var docPath = GetDocPath(assemblyName);
            if (docPath.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var reader = new DocXmlReader(docPath);
            return reader.GetMethodComments(method).Summary;
        }
    }
}
