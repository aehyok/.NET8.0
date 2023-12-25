using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aehyok.Infrastructure
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// 字符串格式化，String.Format() 方法的语法糖
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string source, params object[] args)
        {
            return string.Format(source, args);
        }
    }
}
