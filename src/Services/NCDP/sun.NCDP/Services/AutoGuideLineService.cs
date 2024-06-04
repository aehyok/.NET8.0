using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using sun.Core.Domains;
using sun.Core.Dtos.MD_GuideLine;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Services
{
    public  class AutoGuideLineService(DbContext dbContext, IMapper mapper) : ServiceBase<AutoGuideLineDefine>(dbContext,mapper), IAutoGuideLineService, IScopedDependency
    {
        public async Task<DataTable> QueryCustomFormGuideline(string guideLineId, Dictionary<string, object> Parameters, string keyword, long areaid)
        {
            //string _zbid = guideLineId;
            string _zbid = guideLineId.Replace("#", "");
            var guideLine = await this.GetByIdAsync(_zbid);

            DataTable tb = new DataTable("ResultTable");
            var guideLine_ex = BuildCustomFormGuideline(guideLine, Parameters, areaid);

            if (guideLine_ex != null && guideLine_ex.QueryString != null)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    //对于keyword，暂不处理
                }
                try
                {
                    tb = await FillResultData(guideLine_ex.QueryString, guideLine_ex.Parameters, "ResultTable"); ;

                }
                catch (Exception e)
                {
                    string _errormsg = $"Exception :QueryGuideline执行异常，异常信息为{e.Message}";
                    throw new ErrorCodeException(-1, _errormsg);
                }

                //处理代码表字段,暂不处理


                //处理加密的结果字段,暂不处理

            }
            return tb;
        }

        private async Task<DataTable> FillResultData(string queryString, Dictionary<string, object> parameters, string tableName)
        {
            DataTable _dt = new DataTable(tableName);
            var cn = this.DbContext.Database.GetDbConnection();
            var dr = await cn.ExecuteReaderAsync(queryString, parameters);
            _dt.Load(dr);
            return _dt;
        }

        private GuideLine_Ex BuildCustomFormGuideline(AutoGuideLineDefine guideLine, Dictionary<string, object> Parameters, long areaid)
        {
            GuideLine_Ex guideLine_Ex = new GuideLine_Ex();

            if (guideLine != null)
            {
                //重新生成查询语句
                RebuildGuidelineMethod(guideLine_Ex, guideLine, Parameters, areaid.ToString());

            }
            return guideLine_Ex;
        }

        /// <summary>
        /// 重新生成指标方法
        /// </summary>
        /// <param name="guideLine_Ex"></param>
        /// <param name="guideLine"></param>
        /// <param name="parameters"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private void RebuildGuidelineMethod(GuideLine_Ex guideLine_Ex, AutoGuideLineDefine guideLine, Dictionary<string, object> parameters, string areaId)
        {
            string _sql = guideLine.zbsf;
            if (_sql.Contains(" from "))
            {
                //带from的是get指标
                RebuildGuidelineMethodOfGet(guideLine_Ex, _sql, parameters, areaId);
            }
            else
            {
                //不带from的是取数据指标
                RebuildGuidelineMethodOfInit(guideLine_Ex, _sql, parameters, areaId);
            }
        }

        /// <summary>
        /// 重新处理Init指标的SQL
        /// </summary>
        /// <param name="guideLine_Ex"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        private void RebuildGuidelineMethodOfInit(GuideLine_Ex guideLine_Ex, string sql, Dictionary<string, object> parameters, string areaid)
        {
            Dictionary<string, object> _newparam = new Dictionary<string, object>();
            List<string> _resutlFieldList = new List<string>();
            string _fieldStr = sql.Replace("select", "").Trim();
            string[] _fieldItems = _fieldStr.Split(',');
            foreach (var item in _fieldItems)
            {
                string _itemstr = item.Trim();
                //如果是id，则不处理
                if (_itemstr.Contains(" id"))
                {
                    _resutlFieldList.Add(_itemstr);
                    continue;
                }

                //如果不是id，由根据参数加上对应的参数值
                foreach (string key in parameters.Keys)
                {
                    //参数名可能带@，也可能不带，统一去掉@处理
                    string _fieldName = key.Replace("@", "");
                    if (_itemstr.Contains($" {_fieldName}"))
                    {
                        _itemstr = $"@{_fieldName} {_fieldName}";
                        _newparam.Add(_fieldName, parameters[key].ToString());
                        continue;
                    }
                }
                _resutlFieldList.Add(_itemstr);
            }

            string _ret = "select " + string.Join(",", _resutlFieldList);

            foreach (string _key in _newparam.Keys)
            {
                if (_key == "regionid")
                {
                    string _cs = _newparam[_key].ToString();
                    switch (_cs.ToLower())
                    {
                        case "#currentregionid":
                            _newparam[_key] = areaid;
                            break;
                        default:

                            break;
                    }
                }
            }

            guideLine_Ex.QueryString = _ret;
            guideLine_Ex.Parameters = _newparam;
        }


        /// <summary>
        /// 重新处理Get指标的SQL
        /// </summary>
        /// <param name="guideLine_Ex"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        private void RebuildGuidelineMethodOfGet(GuideLine_Ex guideLine_Ex, string sql, Dictionary<string, object> param, string areaid)
        {
            string _ret = sql;
            string _joinStr = "";
            Dictionary<string, object> _newparam = new Dictionary<string, object>();
            if (param != null)
            {
                foreach (string key in param.Keys)
                {
                    //参数名可能带@，也可能不带，分别处理
                    string _fieldName = key.Replace("@", "");

                    _ret += $" {_joinStr} {_fieldName}=@{_fieldName} ";
                    _joinStr = " and ";
                    if (_fieldName == "regionid")
                    {
                        string _cs = param[key].ToString();
                        switch (_cs.ToLower())
                        {
                            case "#currentregionid":
                                _newparam.Add(_fieldName, areaid);
                                break;
                            default:
                                _newparam.Add(_fieldName, param[key].ToString());
                                break;
                        }

                    }
                    else
                    {
                        _newparam.Add(_fieldName, param[key].ToString());
                    }
                }
            }
            guideLine_Ex.QueryString = _ret;
            guideLine_Ex.Parameters = _newparam;

        }
    }
}
