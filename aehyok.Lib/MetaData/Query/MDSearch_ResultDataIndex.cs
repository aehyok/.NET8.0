using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MDSearch_ResultDataIndex
    {

        public MDSearch_ResultDataIndex() { }

        public MDSearch_ResultDataIndex(string _content, int _position, MDSearch_Column _location, string _source, string _key)
        {
            Content = _content;
            MatchPosition = _position;
            SourceColumn = _location;
            DataLocation = string.Format("{0}.{1}", _location.TableTitle, _location.ColumnTitle);
            DataSource = _source;
            MainKey = _key;
        }

        [DataMember]
        public MDSearch_Column SourceColumn { get; set; }


        /// <summary>
        /// 结果的数据主键(指表的主键)
        /// </summary>
        [DataMember]
        public string MainKey { get; set; }

        /// <summary>
        /// 数据来源,(指查询模型名称)
        /// </summary>
        [DataMember]
        public string DataSource { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public string Content { get; set; }
        /// <summary>
        /// 匹配位置
        /// </summary>
        [DataMember]
        public int MatchPosition { get; set; }

        /// <summary>
        /// 数据位置
        /// </summary>
        [DataMember]
        public string DataLocation { get; set; }
    }
}
