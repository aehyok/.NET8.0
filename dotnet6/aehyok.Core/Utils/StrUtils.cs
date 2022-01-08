using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace aehyok.Core.Utils
{
    public static class StrUtils
    {
        /// <summary>
        /// 从XML字符串中取指定的名称段的内容
        /// </summary>
        /// <param name="_name">段名</param>
        /// <param name="_meta">XML字符串</param>
        /// <returns></returns>
        public static string GetMetaByName(this string meta, string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(string.Format("<XmlText>{0}</XmlText>", meta));

            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName(name);
            if (xmlNodeList.Count > 0)
            {
                return xmlNodeList[0].InnerXml;
            }
            else
            {
                return "";
            }
        }

        public static string GetMetaByName2(this string meta, string name)
        {
            RegexOptions regexOptions = RegexOptions.None;
            string regString = "<" + name + @">[^<]{1,}</" + name + ">";
            Regex regeMeta = new Regex(regString, regexOptions);
            int TagLength = name.Length;
            MatchCollection matchCollection = regeMeta.Matches(meta);
            foreach (Match match in matchCollection)
            {
                return match.Value.Substring(TagLength + 2, match.Length - (TagLength + 2) * 2 - 1);
            }
            return "";
        }

        /// <summary>
        /// 从XML字符串中取指定的名称段的内容列表
        /// </summary>
        /// <param name="_name">段名称</param>
        /// <param name="_meta">XML字符串</param>
        /// <returns></returns>
        public static ArrayList GetMetasByName(string _name, string _meta)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(string.Format("<XmlText>{0}</XmlText>", _meta));
            ArrayList _res = new ArrayList();
            XmlNodeList elemList = doc.GetElementsByTagName(_name);
            for (int i = 0; i < elemList.Count; i++)
            {
                string _s2 = elemList[i].InnerXml;
                _res.Add(_s2);
            }

            return _res;
        }

        public static ArrayList GetMetasByName2(string _name, string _meta)
        {
            ArrayList _res = new ArrayList();

            RegexOptions options = RegexOptions.None;
            string _regStr = "<" + _name + @">[^<]{1,}</" + _name + ">";
            Regex regeMeta = new Regex(_regStr, options);
            int _TagLength = _name.Length;
            MatchCollection _mc = regeMeta.Matches(_meta);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(_TagLength + 2, _m.Length - (_TagLength + 2) * 2 - 1);
                _res.Add(_s2);
            }

            return _res;
        }

        /// <summary>
        /// 把string转DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime String2Date(string date)
        {
            if (string.IsNullOrEmpty(date)) return DateTime.MinValue;
            string lsdate = date.Replace("年", "");
            lsdate = lsdate.Replace("月", "");
            lsdate = lsdate.Replace("日", "");
            lsdate = lsdate.Replace("/", "");
            lsdate = lsdate.Replace("-", "");
            lsdate = lsdate.Replace(" ", "");//替换空格
            lsdate = lsdate.Replace(":", "");
            try
            {
                switch (lsdate.Length)
                {
                    case 4:
                        int year4 = int.Parse(lsdate.Substring(0, 4));
                        return new DateTime(year4, 1, 1);
                    case 6:
                        int year6 = int.Parse(lsdate.Substring(0, 4));
                        int month6 = int.Parse(lsdate.Substring(4, 2));
                        return new DateTime(year6, month6, 1);
                    case 8:
                        int year8 = int.Parse(lsdate.Substring(0, 4));
                        int month8 = int.Parse(lsdate.Substring(4, 2));
                        int day8 = int.Parse(lsdate.Substring(6, 2));
                        return new DateTime(year8, month8, day8);
                    case 14:
                        int year14 = int.Parse(lsdate.Substring(0, 4));
                        int month14 = int.Parse(lsdate.Substring(4, 2));
                        int day14 = int.Parse(lsdate.Substring(6, 2));
                        int hour = int.Parse(lsdate.Substring(8, 2));
                        int min = int.Parse(lsdate.Substring(10, 2));
                        int sec = int.Parse(lsdate.Substring(12, 2));
                        return new DateTime(year14, month14, day14, hour, min, sec);
                    default:
                        return DateTime.MinValue;
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 全角转换成半角
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        static public string ConvertToNarrow(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }

                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }


        /// <summary>
        /// XML转为Dictionary<string,object>
        /// </summary>
        /// <param name="pstr"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ChangeXMLToDictionary(this string pstr)
        {
            int index, start, end, em_start;
            string mark, endmark;
            Dictionary<string, object> _ret = new Dictionary<string, object>();
            index = 0;
            if (pstr != null)
            {
                while (index < pstr.Length)
                {
                    start = pstr.IndexOf('<', index);
                    if (start >= 0)
                    {
                        end = pstr.IndexOf('>', start);
                        if (end >= 0)
                        {
                            mark = pstr.Substring(start + 1, end - start - 1);
                            endmark = string.Format("</{0}>", mark);
                            em_start = pstr.IndexOf(endmark, end + 1);
                            if (em_start >= 0)
                            {
                                string value = pstr.Substring(end + 1, em_start - end - 1);
                                _ret.Add(mark, value);
                                index = end + endmark.Length + 1;
                            }
                            else
                            {
                                index = end + 1;
                            }
                        }
                        else
                        {
                            index = start + 1;
                        }
                    }
                    else
                    {
                        index = pstr.Length + 1;
                    }
                }
            }
            return _ret;
        }

        /// <summary>
        /// 将指标字典类型参数序列化为XML字符串,例：<name>value</name>
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string DicToXMLString(Dictionary<string, object> paras)
        {
            string str = "";
            if (paras != null)
            {
                foreach (var p in paras)
                {
                    object _value = "";
                    if (p.Value != null && p.Value.GetType() == typeof(DateTime))
                    {
                        DateTime _dt = DateTime.MinValue;
                        DateTime.TryParse(p.Value.ToString(), out _dt);
                        _value = _dt.ToString("yyyyMMddHHmmss");
                    }
                    else
                        _value = p.Value;
                    str += "<" + p.Key + ">" + _value + "</" + p.Key + ">";
                }
            }
            return str;
        }

        /// <summary>
        /// 判断字符串是否有空格
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        static public bool IsSpace(this string tableName)//新添加
        {
            tableName = StrUtils.ConvertToNarrow(tableName);
            if (tableName.IndexOf(' ') > -1)
            {
                return true;
            }
            return false;
        }

        public static int IndexOfComma(this string data)
        {
            string s = @".";
            string s2 = @"" + data;
            Regex re = new Regex(Regex.Escape(s), RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = re.Matches(s2);
            return mc.Count;
        }

        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDigit(this string data)
        {
            Char[] chars = data.ToCharArray();
            foreach (char c in chars)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 确保字符串以指定的字符或字符串结尾(如果不是则自动添加)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string EnsureEndsWith(ref string str, char suffix)
        {
            string suffixStr = new string(suffix, 1);
            if (!str.EndsWith(suffixStr))
                str += suffixStr;
            return str;
        }

        private static char[] strChinese = new char[] {
                    '〇','一','二','三','四','五','六','七','八','九','十'
                    };

        /// <summary>
        /// 将日期转化成中文大写写法
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string ConvertToChineseDate(this string DateParam)
        {
            StringBuilder result = new StringBuilder();
            string strDate = DateParam.Replace("年", "/");
            strDate = strDate.Replace("月", "/");
            strDate = strDate.Replace("日", "");

            if (strDate.Length > 0)
            {
                // 将数字日期的年月日存到字符数组str中
                string[] str = null;
                if (strDate.Contains("-"))
                {
                    str = strDate.Split('-');
                }
                else if (strDate.Contains("/"))
                {
                    str = strDate.Split('/');
                }

                // str[0]中为年，将其各个字符转换为相应的汉字
                for (int i = 0; i < str[0].Length; i++)
                {
                    result.Append(strChinese[int.Parse(str[0][i].ToString())]);
                }
                result.Append("年");

                // 转换月
                int month = int.Parse(str[1]);
                int MN1 = month / 10;
                int MN2 = month % 10;

                if (MN1 > 1)
                {
                    result.Append(strChinese[MN1]);
                }
                if (MN1 > 0)
                {
                    result.Append(strChinese[10]);
                }
                if (MN2 != 0)
                {
                    result.Append(strChinese[MN2]);
                }
                result.Append("月");

                // 转换日
                int day = int.Parse(str[2]);
                int DN1 = day / 10;
                int DN2 = day % 10;

                if (DN1 > 1)
                {
                    result.Append(strChinese[DN1]);
                }
                if (DN1 > 0)
                {
                    result.Append(strChinese[10]);
                }
                if (DN2 != 0)
                {
                    result.Append(strChinese[DN2]);
                }
                result.Append("日");
                return result.ToString();
            }
            else
            {
                return "";
            }
        }

        public static NameValueCollection GetUrlStringQueryParameters(this string url)
        {
            int startIndex = url.IndexOf("?");
            NameValueCollection values = new NameValueCollection();

            if (startIndex <= 0)
                return values;

            string[] nameValues = url.Substring(startIndex + 1).Split('&');

            foreach (string s in nameValues)
            {
                string[] pair = s.Split('=');

                string name = pair[0];
                string value = string.Empty;

                if (pair.Length > 1)
                    value = pair[1];

                values.Add(name, value);
            }

            return values;
        }


        public static string GetXMLContent2(XmlDocument doc, string FNode, string CNodeName)
        {
            XmlNodeList elemList = doc.GetElementsByTagName(FNode);
            if (elemList.Count > 0)
            {
                XmlNode cnode = elemList[0];
                foreach (XmlNode _n in cnode.ChildNodes)
                {
                    if (_n.Name == CNodeName)
                    {
                        return _n.InnerXml;
                    }
                }
                return "";
            }
            else
            {
                return "";
            }
        }

        public static string GetXMLContent(XmlDocument doc, string NodeName)
        {
            XmlNodeList elemList = doc.GetElementsByTagName(NodeName);
            if (elemList.Count > 0)
            {
                return elemList[0].InnerXml;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取本机Ipv4列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalIpv4()
        {
            List<string> IPv4s = new List<string>();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    IPv4s.Add(ip.ToString());
            }
            return IPv4s;
        }
        /// <summary>
        /// 随机获取一个本机Ipv4
        /// </summary>
        /// <returns></returns>
        public static string GetOneLocalIpv4()
        {
            List<string> IPv4s = GetLocalIpv4();
            if (IPv4s.Count > 0)
                return IPv4s[0];
            else
                return "127.0.0.1";
        }
        /// <summary>
        /// 构建分页的sql语句
        /// </summary>
        /// <param name="sql">源sql语句</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortBy">排序字段</param>
        /// <param name="sortDirection">排序方向(ASC,DESC)</param>
        /// <returns></returns>
        public static string BuildPagingSQL(string sql, decimal pageIndex, decimal pageSize, string sortBy, string sortDirection)
        {
            StringBuilder _str = new StringBuilder();
            if (!string.IsNullOrEmpty(sortBy))
            {
                sql = " select * from (\n " + sql + " \n) order by " + sortBy + " " + sortDirection;
            }
            _str.Append(" select t_1.* from ");
            _str.Append(" ( select rownum r_0,t_0.* from ");
            _str.Append(" (\n " + sql + " \n) t_0 ");
            _str.Append(" where rownum <= " + (pageIndex * pageSize) + " ) t_1 ");
            _str.Append(" where r_0 > " + (pageIndex - 1) * pageSize);
            return _str.ToString();
        }

        /// <summary>
        /// 将金额转换成中文金额
        /// </summary>
        /// <param name="LowerMoney"></param>
        /// <returns></returns>
        public static string ConvertToChineseMoney(string LowerMoney)
        {
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数 
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数 
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4 
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                {
                    LowerMoney = LowerMoney + "0";
                }
            }
            else
            {
                LowerMoney = LowerMoney + ".00";
            }
            strLower = LowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹圆以下的金额的处理 
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }
        }
    }
}
