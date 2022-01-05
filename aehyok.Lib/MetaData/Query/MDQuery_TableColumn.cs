using aehyok.Lib.MetaData.EnumDefine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    public class MDQuery_TableColumn
    {
        public MDQuery_TableColumn()
        {
        }

        public MDQuery_TableColumn(MDModel_Table_Column _columnDefine)
        {
            this.ColumnAlgorithm = _columnDefine.ColumnAlgorithm;
            this.ColumnAlias = _columnDefine.ColumnAlias;
            this.ColumnDataType = _columnDefine.ColumnDataType;
            if (_columnDefine.ColumnDefine == null)
            {
                this.ColumnLength = 0;
                this.RefDMB = "";
                this.DisplayFormat = "";
                this.DisplayLength = 80;
            }
            else
            {
                if (_columnDefine.ColumnType == QueryColumnType.CalculationColumn)
                {
                    this.ColumnLength = 1;
                    this.DisplayFormat = "";
                    this.RefDMB = "";
                    this.DisplayLength = 80;
                }
                else
                {
                    this.ColumnLength = _columnDefine.ColumnDefine.TableColumn.Length;
                    this.DisplayFormat = _columnDefine.ColumnDefine.TableColumn.DisplayFormat;
                    this.RefDMB = _columnDefine.ColumnDefine.TableColumn.RefDMB;
                    this.DisplayLength = (_columnDefine.ColumnDefine.TableColumn.ColWidth > 10) ? _columnDefine.ColumnDefine.TableColumn.ColWidth : 80;
                }
            }
            this.ColumnName = _columnDefine.ColumnName;
            this.ColumnTitle = _columnDefine.ColumnTitle;
            this.ColumnType = _columnDefine.ColumnType;
            this.DisplayOrder = _columnDefine.DisplayOrder;
            this.TableName = _columnDefine.TableName;
            this.SecretLevel = _columnDefine.SecretLevel;
            this.Source = new MDQuery_ColumnSource(_columnDefine.QueryModelName, _columnDefine.TableName, _columnDefine.ColumnName);
        }


        #region 属性
        /// <summary>
        /// 字段类型
        /// </summary>
        /// 
        [DataMember]
        public QueryColumnType ColumnType { get; set; }

        /// <summary>
        /// 字段算法(指如果是计算字段或统计字段,其算法)
        /// </summary>
        /// 
        [DataMember]
        public string ColumnAlgorithm { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        /// 
        [DataMember]
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段别名
        /// </summary>
        /// 
        [DataMember]
        public string ColumnAlias { get; set; }

        /// <summary>
        /// 字段显示名称
        /// </summary>
        /// 
        [DataMember]
        public string ColumnTitle { get; set; }

        /// <summary>
        /// 所在表的表名
        /// </summary>
        /// 
        [DataMember]
        public string TableName { get; set; }

        /// <summary>
        /// 所在查询模型名称
        /// </summary>
        /// 
        [DataMember]
        public string QueryModelName { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        /// 
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 字段的数据类型
        /// </summary>
        /// 
        [DataMember]
        public string ColumnDataType { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        /// 
        [DataMember]
        public long ColumnLength { get; set; }

        /// <summary>
        /// 显示格式
        /// </summary>
        /// 
        [DataMember]
        public string DisplayFormat { get; set; }

        /// <summary>
        /// 列表显示的长度　(单位:px)
        /// </summary>
        /// 
        [DataMember]
        public int DisplayLength { get; set; }
        /// <summary>
        /// 代码表
        /// </summary>
        /// 
        [DataMember]
        public string RefDMB { get; set; }
        /// <summary>
        /// 引用表
        /// </summary>
        /// 
        [DataMember]
        public string RefWord { get; set; }

        /// <summary>
        /// 字段的安全级别
        /// </summary>
        [DataMember]
        public int SecretLevel { get; set; }

        /// <summary>
        /// 对应的查询模型字段来源
        /// </summary>
        [DataMember]
        public MDQuery_ColumnSource Source { get; set; }


        #endregion
    }
}
