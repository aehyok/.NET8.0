using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sun.Excel.Export
{
    public class ExcelExtensions
    {
        private const string REGEX_OUTPUT = @"\{\{(.*?)\}\}";

        public static string ReplaceParameter(Dictionary<string, object> data, string source)
        {
            foreach (Match m in Regex.Matches(source, REGEX_OUTPUT))
            {
                var propertyName = m.Groups[1].Value;

                if (propertyName.StartsWith("#"))
                {
                    var functionName = string.Empty;

                    // 匹配方法名称
                    var functionMatch = Regex.Match(propertyName, @"^#(.+?)\(");
                    if (functionMatch.Success)
                    {
                        functionName = functionMatch.Groups[1].Value;
                    }

                    // 匹配参数列表
                    var paramMatch = Regex.Match(propertyName, @"\((.+?)\)");
                    var parameters = paramMatch.Groups[1].Value.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(a => a.StartsWith("@") ? GetValue(data, a.TrimStart('@')) : a.Trim()).ToArray();

                    var value = RenderFunctionCell(functionName, parameters);
                    source = source.Replace(m.Value, value?.ToString());
                }
                else
                {
                    var value = GetValue(data, propertyName, "");
                    source = source.Replace(m.Value, value);
                }
            }

            return source;
        }

        public static object RenderFunctionCell(string functionName, object[] parameters)
        {
            var method = typeof(ReportHelper).GetMethod(functionName);

            if (functionName == "ExcelLogicalConjunction")
            {
                return method.Invoke(null, new[] { parameters.ToList() });
            }

            return method.Invoke(null, parameters);
        }

        public static object GetValue(Dictionary<string, object> data, string propertyPath)
        {
            if (string.IsNullOrWhiteSpace(propertyPath) || data is null)
            {
                return null;
            }

            var propertyPathList = propertyPath.Split(".", StringSplitOptions.RemoveEmptyEntries);

            if (!data.ContainsKey(propertyPathList[0]))
            {
                return null;
            }

            object currentData = data[propertyPathList[0]];
            if (currentData is null)
            {
                return null;
            }

            if (propertyPathList.Length <= 1)
            {
                return currentData;
            }

            var propertyPathIndex = 1;

            do
            {
                currentData = GetObjectValue(currentData, propertyPathList[propertyPathIndex]);
                if (currentData is null)
                {
                    return null;
                }

                propertyPathIndex++;
            } while (propertyPathIndex < propertyPathList.Length);

            return currentData;
        }

        public static T GetValue<T>(Dictionary<string, object> data, string propertyPath, T defaultValue = default)
        {
            var value = GetValue(data, propertyPath);
            if (value is System.Text.Json.JsonElement json)
            {
                value = json.GetString();
            }

            return value is null ? defaultValue : (T)Convert.ChangeType(value, typeof(T));
        }

        public static object GetObjectValue(object data, string propertyName)
        {
            if (data is IDictionary<string, object> obj)
            {
                if (obj.TryGetValue(propertyName, out var value))
                {
                    return value;
                }

                return null;
            }

            return data.GetType().GetProperty(propertyName)?.GetValue(data);
        }
    }
}
