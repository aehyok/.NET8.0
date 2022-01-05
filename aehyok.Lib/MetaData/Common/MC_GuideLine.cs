using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace aehyok.Lib.MetaData.Common
{
    /// <summary>
    /// 指标管理通用方法
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MC_GuideLine
    {

        public static List<MD_GuideLineFieldGroup> GetFieldGroupsFromMeta(string _metaStr)
        {
            List<MD_GuideLineFieldGroup> _ret = new List<MD_GuideLineFieldGroup>();
            Dictionary<string, MD_GuideLineFieldGroup> _dict = new Dictionary<string, MD_GuideLineFieldGroup>();

            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<FG>[^<]{1,}</FG>", options);

            MatchCollection _mc = regeMeta.Matches(_metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 1)
                {
                    string _name = _s3[0].ToUpper();
                    string _title = _s3[1];
                    int _order = (_s3.Length > 2) ? int.Parse((_s3[2] == "") ? "0" : _s3[2]) : 0;
                    string _align = (_s3.Length > 3) ? _s3[3] : "LEFT";
                    bool _hide = (_s3.Length > 4) ? (int.Parse(_s3[4]) > 0) : false;
                    string _status = (_s3.Length > 5) ? _s3[5] : "SHOW";
                    MD_GuideLineFieldGroup _glg = new MD_GuideLineFieldGroup(_name, _title, _align, _order, _hide, _status);
                    _glg.Fields = new List<MD_GuideLineFieldName>();
                    _dict.Add(_name, _glg);
                    _ret.Add(_glg);
                }
            }
            GetFieldNamesFromMeta(_metaStr, _dict, _ret);
            if (_ret.Count < 1)
            {
                MD_GuideLineFieldGroup _glg = new MD_GuideLineFieldGroup("DEFAULT", "(默认组)", "CENTER", 1, false, "SHOW");
                _glg.Fields = new List<MD_GuideLineFieldName>();
                _ret.Add(_glg);
                _dict.Add("DEFAULT", _glg);
            }
            foreach (MD_GuideLineFieldGroup _group in _ret)
            {
                _group.Fields.Sort((g1, g2) => { return g1.DisplayOrder.CompareTo(g2.DisplayOrder); });
            }
            _ret.Sort((g1, g2) => { return g1.DisplayOrder.CompareTo(g2.DisplayOrder); });
            return _ret;
        }

        private static void GetFieldNamesFromMeta(string _metaStr, Dictionary<string, MD_GuideLineFieldGroup> _dict, List<MD_GuideLineFieldGroup> _ret)
        {
            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<FN>[^<]{1,}</FN>", options);

            MatchCollection _mc = regeMeta.Matches(_metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 1)
                {
                    string _fname = _s3[0].ToUpper();
                    string _title = _s3[1];
                    int _order = (_s3.Length > 2) ? int.Parse((_s3[2] == "") ? "0" : _s3[2]) : 0;
                    int _width = (_s3.Length > 3) ? int.Parse((_s3[3] == "") ? "100" : _s3[3]) : 100;
                    string _center = (_s3.Length > 4) ? _s3[4] : "LEFT";
                    if (_center == "1") _center = "CENTER";
                    if (_center == "0") _center = "LEFT";
                    string _display = (_s3.Length > 5) ? _s3[5] : "";
                    string _group = (_s3.Length > 6) ? _s3[6] : "DEFAULT";
                    bool _canHide = (_s3.Length > 7) ? ((_s3[7] == "0") ? false : true) : false;
                    MD_GuideLineFieldName _gfn = new MD_GuideLineFieldName(_fname, _title, _order, _width, _center, _display, _canHide);
                    if (_dict.ContainsKey(_group))
                    {
                        MD_GuideLineFieldGroup _glg = _dict[_group];
                        if (_glg.Fields == null) _glg.Fields = new List<MD_GuideLineFieldName>();
                        _glg.Fields.Add(_gfn);
                    }
                    else
                    {
                        MD_GuideLineFieldGroup _glg = new MD_GuideLineFieldGroup(_group, "(默认组)", "CENTER", 1, false, "SHOW");
                        _ret.Add(_glg);
                        _dict.Add(_group, _glg);
                        _glg.Fields = new List<MD_GuideLineFieldName>();
                        _glg.Fields.Add(_gfn);
                    }
                }
            }
        }

        public static List<MD_GuideLineFieldName> GetFieldNamesFromMeta(string _metaStr)
        {
            List<MD_GuideLineFieldName> _ret = new List<MD_GuideLineFieldName>();

            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<FN>[^<]{1,}</FN>", options);

            MatchCollection _mc = regeMeta.Matches(_metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 1)
                {
                    string _fname = _s3[0].ToUpper();
                    string _title = _s3[1];
                    int _order = (_s3.Length > 2) ? int.Parse((_s3[2] == "") ? "0" : _s3[2]) : 0;
                    int _width = (_s3.Length > 3) ? int.Parse((_s3[3] == "") ? "100" : _s3[3]) : 100;
                    string _center = (_s3.Length > 4) ? _s3[4] : "LEFT";
                    string _display = (_s3.Length > 5) ? _s3[5] : "";
                    string _group = (_s3.Length > 6) ? _s3[6] : "DEFAULT";
                    bool _canHide = (_s3.Length > 7) ? ((_s3[7] == "0") ? false : true) : false;
                    MD_GuideLineFieldName _gfn = new MD_GuideLineFieldName(_fname, _title, _order, _width, _center, _display, _canHide);
                    _ret.Add(_gfn);
                }

            }
            return _ret;
        }

        public static List<MD_GuideLineParameter> GetParametersFromMeta(string _metaStr)
        {
            List<MD_GuideLineParameter> _ret = new List<MD_GuideLineParameter>();
            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<CS>[^<]{1,}</CS>", options);

            MatchCollection _mc = regeMeta.Matches(_metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 2)
                {
                    string _pname = _s3[0];
                    string _title = _s3[1];
                    string _retTable = "";
                    string _selectAllCode = "";
                    bool _incldeChildren = false;
                    string _type = ParseParamType(_s3[2], ref _retTable, ref _incldeChildren, ref _selectAllCode);
                    int _order = (_s3.Length > 3) ? int.Parse((_s3[3] == "") ? "0" : _s3[3]) : 0;
                    int _inputWidth = (_s3.Length > 4) ? int.Parse((_s3[4] == "") ? "200" : _s3[4]) : 200;
                    MD_GuideLineParameter _mgp = new MD_GuideLineParameter(_pname, _title, _type, _order, _inputWidth, _retTable, _incldeChildren, _selectAllCode);
                    _ret.Add(_mgp);
                }

            }
            _ret.Sort((p1, p2) => { return p1.DisplayOrder.CompareTo(p2.DisplayOrder); });
            return _ret;
        }

        private static string ParseParamType(string _typeStr, ref string _retTable, ref bool _incldeChildren, ref string _selectAllCode)
        {
            if (_typeStr.Contains("代码表"))
            {
                int _index = _typeStr.IndexOf('[');
                string[] _retTableStrings = _typeStr.Substring(_index + 1, _typeStr.Length - _index - 2).Split(',');
                _retTable = _retTableStrings[0].ToUpper();
                _incldeChildren = (_retTableStrings.Length > 1) ? (_retTableStrings[1] == "1") : false;
                _selectAllCode = (_retTableStrings.Length > 2) ? _retTableStrings[2] : "";
                return "代码表";
            }
            else
            {
                return _typeStr;
            }
        }



        public static List<MD_GuideLineDetailDefine> GetDetaiDefinelFromMeta(string _metaStr)
        {
            List<MD_GuideLineDetailDefine> _ret = new List<MD_GuideLineDetailDefine>();
            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<MX>[^<]{1,}</MX>", options);

            MatchCollection _mc = regeMeta.Matches(_metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 3)
                {
                    string _fname = _s3[0].ToUpper();
                    string _type = _s3[1];
                    string _qid = _s3[2];
                    string _qcs = _s3[3];
                    string _links = (_s3.Length > 4) ? _s3[4] : "";
                    MD_GuideLineDetailDefine _gdd = new MD_GuideLineDetailDefine(_fname, _type, _qid, _qcs, _links);

                    _ret.Add(_gdd);
                }
            }
            return _ret;
        }
        public static Dictionary<string, MD_GuideLineDetailDefine> GetDetailDefineDict(string _metaStr)
        {
            Dictionary<string, MD_GuideLineDetailDefine> _ret = new Dictionary<string, MD_GuideLineDetailDefine>();
            List<MD_GuideLineDetailDefine> _dList = GetDetaiDefinelFromMeta(_metaStr);
            foreach (MD_GuideLineDetailDefine _item in _dList)
            {
                _ret.Add(_item.TargetFieldName, _item);
            }
            return _ret;
        }

        public static string CreateMeta(IList<MD_GuideLineFieldGroup> _fieldGroupList, IList<MD_GuideLineParameter> _parameterList, IList<MD_GuideLineDetailDefine> _detailList)
        {
            StringBuilder _metastr = new StringBuilder();
            foreach (MD_GuideLineFieldGroup _glf in _fieldGroupList)
            {
                _metastr.Append(string.Format("<FG>{0}:{1}:{2}:{3}:{4}:{5}</FG>\n", _glf.GroupName, _glf.DisplayTitle, _glf.DisplayOrder,
                        _glf.TextAlign, (_glf.CanHide ? 1 : 0), _glf.DefaultStatus));
                foreach (MD_GuideLineFieldName _fieldName in _glf.Fields)
                {
                    _metastr.Append(string.Format("<FN>{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}</FN>\n", _fieldName.FieldName, _fieldName.DisplayTitle,
                            _fieldName.DisplayOrder.ToString(), _fieldName.DisplayWidth.ToString(),
                            _fieldName.TextAlign, _fieldName.DisplayFormat,
                            _glf.GroupName, (_fieldName.CanHide ? 1 : 0)));
                }

            }

            foreach (MD_GuideLineParameter _param in _parameterList)
            {
                _metastr.Append(string.Format("<CS>{0}:{1}:{2}:{3}:{4}</CS>\n",
                        _param.ParameterName.Trim(), _param.DisplayTitle, CreateReTableType(_param), _param.DisplayOrder, _param.InputWidth));


            }

            foreach (MD_GuideLineDetailDefine _detail in _detailList)
            {
                _metastr.Append(string.Format("<MX>{0}:{1}:{2}:{3}:{4}</MX>\n",
                        _detail.TargetFieldName, _detail.QueryDetailType, _detail.DetailMethodID, _detail.DetailParameterMeta, _detail.DetailLinkMeta));
            }

            return _metastr.ToString();
        }

        private static string CreateReTableType(MD_GuideLineParameter _param)
        {
            if (_param.ParameterType == "代码表")
            {
                return string.Format("代码表[{0},{1},{2}]", _param.RefTableName, (_param.IncludeChildren) ? 1 : 0, _param.SelectAllCode);
            }
            else
            {
                return _param.ParameterType;
            }
        }

        public static MD_GuideLine GetGuideLineDefineData(MD_GuideLine _guideLine)
        {
            MD_GuideLine _ret = new MD_GuideLine();
            _ret.ID = _guideLine.ID;
            _ret.GroupName = _guideLine.GroupName;
            _ret.GuideLineMeta = _guideLine.GuideLineMeta;
            _ret.GuideLineMethod = _guideLine.GuideLineMethod;
            _ret.GuideLineName = _guideLine.GuideLineName;
            _ret.GuideLineQueryMethod = _guideLine.GuideLineQueryMethod;
            _ret.DetailMeta = _guideLine.DetailMeta;
            _ret.DisplayOrder = _guideLine.DisplayOrder;
            _ret.FatherID = _guideLine.FatherID;
            _ret.Description = _guideLine.Description;
            return _ret;
        }




        public static List<MD_GuideLineDetailParameter> GetGuideLineDetailParam(string _metaStr)
        {
            List<MD_GuideLineDetailParameter> _ret = new List<MD_GuideLineDetailParameter>();

            string[] _paramStr = _metaStr.Split(',');
            foreach (string _s in _paramStr)
            {
                string[] _cs = _s.Split('=');
                if (_cs.Length > 3)
                {
                    MD_GuideLineDetailParameter _p = new MD_GuideLineDetailParameter(_cs[0], _cs[1], _cs[2], _cs[3]);
                    _ret.Add(_p);
                }

                if (_cs.Length == 2)
                {
                    MD_GuideLineDetailParameter _p = new MD_GuideLineDetailParameter(_cs[0], "", "", _cs[1]);
                    _ret.Add(_p);
                }
            }
            return _ret;
        }



        public static List<MDQuery_GuideLineParameter> CreateQueryParameter(MD_GuideLine GuideLineDefine, string _params)
        {
            if (GuideLineDefine == null) return new List<MDQuery_GuideLineParameter>();
            //建立表
            List<MD_GuideLineDetailParameter> _detailParamList = MC_GuideLine.GetGuideLineDetailParam(_params);
            Dictionary<string, string> _detailParamDict = new Dictionary<string, string>();
            foreach (MD_GuideLineDetailParameter _dparam in _detailParamList)
            {
                _detailParamDict.Add(_dparam.Name, _dparam.DataValue);
            }


            //建立查询参数
            List<MDQuery_GuideLineParameter> _ret = new List<MDQuery_GuideLineParameter>();

            //建立指标参数字典
            List<MD_GuideLineParameter> _gParam = MC_GuideLine.GetParametersFromMeta(GuideLineDefine.GuideLineMeta);
            foreach (MD_GuideLineParameter _p in _gParam)
            {
                if (_detailParamDict.ContainsKey(_p.ParameterName))
                {
                    string _dataDefine = _detailParamDict[_p.ParameterName];
                    MDQuery_GuideLineParameter _param = new MDQuery_GuideLineParameter(_p, _dataDefine);
                    _ret.Add(_param);
                }
            }

            return _ret;
        }
    }
}
