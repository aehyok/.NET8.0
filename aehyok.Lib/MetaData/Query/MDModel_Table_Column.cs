using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.EnumDefine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    /// <summary>
    /// 查询模型数据列
    /// </summary>
    public class MDModel_Table_Column
    {
        public MDModel_Table_Column()
        { }

        public MDModel_Table_Column(MD_ViewTableColumn _tc)
        {
            this.ColumnDefine = _tc;
            this.ColumnType = QueryColumnType.TableColumn;

            this.TCID = _tc.TableColumn.ColumnID;
            this.TID = _tc.TableColumn.TID;
            this.TableName = _tc.TableName;   ///modified by lqm
            this.ColumnName = _tc.TableColumn.ColumnName;
            this.IsNullable = _tc.TableColumn.IsNullable;
            this.Type = _tc.TableColumn.ColumnType;
            this.Precision = _tc.TableColumn.Precision;
            this.Scale = _tc.TableColumn.Scale;
            this.Length = _tc.TableColumn.Length;
            this.ColumnRefDMB = _tc.TableColumn.RefDMB;
            this.DMBLevelFormat = _tc.TableColumn.DMBLevelFormat;
            this.SecretLevel = _tc.TableColumn.SecretLevel;
            this.ColumnTitle = _tc.TableColumn.DisplayTitle;
            this.DisplayTitle = _tc.TableColumn.DisplayTitle;
            this.DisplayFormat = _tc.TableColumn.DisplayFormat;
            this.DisplayLength = _tc.TableColumn.DisplayLength;
            this.DisplayHeight = _tc.TableColumn.DisplayHeight;
            this.DisplayOrder = _tc.DisplayOrder;
            this.CanDisplay = _tc.TableColumn.CanDisplay;
            this.ColWidth = _tc.TableColumn.ColWidth;
            this.DWDM = _tc.TableColumn.DWDM;
            this.CTAG = _tc.TableColumn.CTag;
            this.RefWordTB = _tc.TableColumn.RefWordTableName;
            this.VTCID = _tc.ViewTableColumnID;
            this.CanConditionShow = _tc.CanShowAsCondition;
            this.CanResultShow = _tc.CanShowAsResult;
            this.DefaultShow = _tc.DefaultResult;
            this.FixQueryItem = _tc.IsFixQueryItem;
            this.CanModify = _tc.CanModify;
            this.Priority = _tc.Priority;
            this.ColumnDataType = _tc.TableColumn.ColumnType;
        }

        public MDModel_Table_Column(string tcid, string tid, string columnname, bool isnullable,
                                    string type, int precision, int scale, int length, string refdmb,
                                    string dmblevelformat, int secretlevel, string displaytitle,
                                    string displayformat, int displaylength, int displayheight,
                                    int displayorder, bool candisplay, int colwidth, string dwdm,
                                    string ctag, string refwordtb, string vtcid, bool canconditionshow,
                                    bool canresultshow, bool defaultshow, bool fixqueryitem,
                                    bool canmodify, int priority, string dataType)
        {
            // TODO: Complete member initialization
            this.TCID = tcid;
            this.TID = tid;
            this.ColumnName = columnname;
            this.IsNullable = isnullable;
            this.Type = type;
            this.Precision = precision;
            this.Scale = scale;
            this.Length = length;
            this.ColumnRefDMB = refdmb;
            this.DMBLevelFormat = dmblevelformat;
            this.SecretLevel = secretlevel;
            this.DisplayTitle = displaytitle;
            this.DisplayFormat = displayformat;
            this.DisplayLength = displaylength;
            this.DisplayHeight = displayheight;
            this.DisplayOrder = displayorder;
            this.CanDisplay = candisplay;
            this.ColWidth = colwidth;
            this.DWDM = dwdm;
            this.CTAG = ctag;
            this.RefWordTB = refwordtb;
            this.VTCID = vtcid;
            this.CanConditionShow = canconditionshow;
            this.CanResultShow = canresultshow;
            this.DefaultShow = defaultshow;
            this.FixQueryItem = fixqueryitem;
            this.CanModify = canmodify;
            this.Priority = priority;
            this.ColumnDataType = dataType;
        }

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
        /// 字段标题
        /// </summary>
        /// 
        [DataMember]
        public string ColumnTitle { get; set; }


        /// <summary>
        /// 字段类型
        /// </summary>
        /// 
        [DataMember]
        public QueryColumnType ColumnType { get; set; }

        [DataMember]
        public MD_ViewTableColumn ColumnDefine { get; set; }

        /// <summary>
        /// 字段算法(指如果是计算字段或统计字段,其算法)
        /// </summary>
        /// 
        [DataMember]
        public string ColumnAlgorithm { get; set; }



        /// <summary>
        /// 所在表的表名
        /// </summary>
        /// 
        [DataMember]
        public string TableName { get; set; }


        /// <summary>
        /// 字段所在的查询模型名称
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
        /// 字段的代码表
        /// </summary>
        /// 
        [DataMember]
        public string ColumnRefDMB { get; set; }

        /// <summary>
        /// 表的COLUMN ID
        /// </summary>
        [DataMember]
        public string TCID { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        [DataMember]
        public string Type { get; set; }
        /// <summary>
        /// 是否可以为空
        /// </summary>
        [DataMember]
        public bool IsNullable { get; set; }
        /// <summary>
        /// 小数位数
        /// </summary>
        [DataMember]
        public int Precision { get; set; }
        /// <summary>
        /// 显示长度
        /// </summary>
        [DataMember]
        public long Length { get; set; }

        /// <summary>
        /// 代码表分级模式
        /// </summary>
        [DataMember]
        public string DMBLevelFormat { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>
        [DataMember]
        public int SecretLevel { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [DataMember]
        public string DisplayTitle { get; set; }
        /// <summary>
        /// 显示格式
        /// </summary>
        [DataMember]
        public string DisplayFormat { get; set; }
        /// <summary>
        /// 显示长度
        /// </summary>
        [DataMember]
        public int DisplayLength { get; set; }
        /// <summary>
        /// 显示高度
        /// </summary>
        [DataMember]
        public int DisplayHeight { get; set; }
        /// <summary>
        /// 是否可以显示 
        /// </summary>
        [DataMember]
        public bool CanDisplay { get; set; }
        /// <summary>
        /// 列宽度
        /// </summary>
        [DataMember]
        public int ColWidth { get; set; }
        /// <summary>
        /// 概念标签
        /// </summary>
        [DataMember]
        public string CTAG { get; set; }
        /// <summary>
        /// 引用词
        /// </summary>
        [DataMember]
        public string RefWordTB { get; set; }
        /// <summary>
        /// 模型表字段ID
        /// </summary>
        [DataMember]
        public string VTCID { get; set; }
        /// <summary>
        /// 是否可做条件
        /// </summary>
        [DataMember]
        public bool CanConditionShow { get; set; }
        /// <summary>
        /// 是否可做结果
        /// </summary>
        [DataMember]
        public bool CanResultShow { get; set; }
        /// <summary>
        /// 是否默认结果
        /// </summary>
        [DataMember]
        public bool DefaultShow { get; set; }
        /// <summary>
        /// 是否固定查询项
        /// </summary>
        [DataMember]
        public bool FixQueryItem { get; set; }
        /// <summary>
        /// 是否允许修正
        /// </summary>
        [DataMember]
        public bool CanModify { get; set; }
        /// <summary>
        /// 优先级别
        /// </summary>
        [DataMember]
        public int Priority { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        [DataMember]
        public string TID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Scale { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        [DataMember]
        public string DWDM { get; set; }


    }
}
