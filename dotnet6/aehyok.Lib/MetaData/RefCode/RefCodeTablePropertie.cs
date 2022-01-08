using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.RefCode
{
    public class RefCodeTablePropertie
    {
        private string _refTableName = "";
        private string _displayTitle = "";
        private RefCodeType _codeType = RefCodeType.Number;

        private bool _supportPyzt = true;               //是否支持拼音字头
        private bool _supportLevel = false;
        private int _levelDownloadMode = 1;
        private int _refTableMode = 1;	                //代码表模式：1：正常模式 2参数比较下载模式
        private bool _hideCode = false;
        private string _LevelFormat = "";
        protected string _codeFieldName = "DM";
        protected string _pyztFieldName = "PYZT";
        protected string _valueFieldName = "MC";
        //代码类型
        [DataMember]
        public RefCodeType CodeType { get { return _codeType; } set { _codeType = value; } }
        //是否隐藏代码  
        [DataMember]
        public bool HideCode { get { return _hideCode; } set { _hideCode = value; } }
        //是否支持拼音字头
        [DataMember]
        public bool SupportPyzt { get { return _supportPyzt; } set { _supportPyzt = value; } }
        //是否分级代码
        [DataMember]
        public bool SupportLevel { get { return _supportLevel; } set { _supportLevel = value; } }
        //数据下载模式 1：一次性全部下载 2：分级下载
        [DataMember]
        public int LevelDownloadMode { get { return _levelDownloadMode; } set { _levelDownloadMode = value; } }
        //代码字段名
        [DataMember]
        public string CodeFieldName { get { return _codeFieldName; } set { _codeFieldName = value; } }
        //拼音字头字段名
        [DataMember]
        public string PyztFieldName { get { return _pyztFieldName; } set { _pyztFieldName = value; } }
        //值字段名
        [DataMember]
        public string ValueFieldName { get { return _valueFieldName; } set { _valueFieldName = value; } }
        //分级格式
        [DataMember]
        public string LevelFormat { get { return _LevelFormat; } set { _LevelFormat = value; } }
        //代码表模式：1：正常模式 2参数比较下载模式
        [DataMember]
        public int RefTableMode { get { return _refTableMode; } set { _refTableMode = value; } }
        //代码表名
        [DataMember]
        public string Name { get { return _refTableName; } set { _refTableName = value; } }
        //显示名称    
        [DataMember]
        public string DisplayTitle { get { return _displayTitle; } set { _displayTitle = value; } }

        public RefCodeTablePropertie()
        {
        }

        public RefCodeTablePropertie(string _name, string _title, RefCodeType _type, bool _pyzt, bool _level, int _downLoadMode, int _refMode, string _format, bool _hide)
        {
            _refTableName = _name;
            _displayTitle = _title;
            _codeType = _type;
            _supportPyzt = _pyzt;
            _supportLevel = _level;
            _levelDownloadMode = _downLoadMode;
            _refTableMode = _refMode;
            _LevelFormat = _format;
            _hideCode = _hide;

        }

    }
}
