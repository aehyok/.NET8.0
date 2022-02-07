using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using aehyok.Lib.MetaData.Common;
using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using aehyok.Core.MySql;

namespace aehyok.Core.MySqlDataAccessor
{
    public class GuidelineAccessor
    {
        /// <summary>
        /// 缓存指标定义
        /// </summary>
        protected static Dictionary<string, MD_GuideLine> GuidelineDefine = new Dictionary<string, MD_GuideLine>();

        /// <summary>
        /// 清空指标定义
        /// </summary>
        public static void ClearGuideLineDefine()
        {
            GuidelineDefine.Clear();
        }


        private const string SqlGetGuidelineDefine = @"select ID,ZBMC,ZBZT,ZBMETA,FID,JSMX_ZBMETA,XSXH,ZBSM,ZBSF from tj_zdyzbdyb where ID=@Id ";
        /// <summary>
        /// 取指标定义
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <returns></returns>
        public async static Task<MD_GuideLine> GetGuidelineDefine(string guideLineId)
        {
            MD_GuideLine define = null;
            if (!string.IsNullOrEmpty(guideLineId))
            {
                //if (GuidelineDefine.ContainsKey(guideLineId))
                //    define = GuidelineDefine[guideLineId];
                //else
                {
                    using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(SqlGetGuidelineDefine, cn);
                            cmd.Parameters.Add(new MySqlParameter("@Id", Convert.ToInt64(guideLineId)));

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (await dr.ReadAsync())
                                {
                                    string id = dr.IsDBNull(0) ? "" : dr.GetInt64(0).ToString();
                                    string name = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                    string groupname = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                    string zbmeta1 = dr.IsDBNull(3) ? "" : dr.GetString(3);
                                    string fatherid = dr.IsDBNull(4) ? "0" : dr.GetInt64(4).ToString();
                                    string zbmeta2 = dr.IsDBNull(5) ? "" : dr.GetString(5);
                                    int displayorder = dr.IsDBNull(6) ? 0 : Convert.ToInt32(dr.GetInt32(6));
                                    string descript = dr.IsDBNull(7) ? "" : dr.GetString(7);
                                    string fullMeta = zbmeta1 + zbmeta2;
                                    string zbsf = dr.IsDBNull(8) ? "" : dr.GetString(8);

                                    define = new MD_GuideLine(id, name, groupname, fatherid, displayorder, descript);
                                    define.Parameters = JsonConvert.DeserializeObject<List<MD_GuideLineParameter>>(zbmeta1); //MC_GuideLine.GetParametersFromMeta(fullMeta);
                                    define.ResultGroups = JsonConvert.DeserializeObject<List<MD_GuideLineFieldName>>(zbmeta2);  //MC_GuideLine.GetFieldGroupsFromMeta(fullMeta);
                                    define.DetailDefines = MC_GuideLine.GetDetaiDefinelFromMeta(fullMeta);
                                    define.GuideLineMethod = zbsf;

                                    GuidelineDefine.Add(guideLineId, define);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            string errorMessage = string.Format("取指标[{0}]的定义出错,错误信息为{1}", guideLineId, exception.Message);
                            MysqlLogWriter.WriteSystemLog(errorMessage, "ERROR");
                        }
                    }
                }
            }
            return define;
        }


        public async static Task<DataTable> QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord)
        {
            int getQueryStartTime = Environment.TickCount;           
            DataTable tb = new DataTable("ResultTable");
            MD_GuideLine define = await GetGuidelineDefine(guideLineId);
            if (define != null)
            {
                string queryStr = await GetGuidelineMethod(guideLineId);

                List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
                if (param != null && define.Parameters != null)
                {
                    foreach (var p in param)
                    {
                        MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
                        if (md_pa != null)
                        {
                            glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
                        }
                    }
                }
                foreach (MDQuery_GuideLineParameter gp in glPara)
                {
                    queryStr = MySqlQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }

                if (!string.IsNullOrEmpty(filterWord))
                {
                    queryStr = string.Format("select * from (\n {0} \n) where {1}", queryStr, filterWord);
                }
                tb = await FillResultData(queryStr, "ResultTable");
                MysqlLogWriter.WriteQueryLog(queryStr, Environment.TickCount - getQueryStartTime, "2");

            }
            return tb;
        }

        /// <summary>
        /// 分页取指标结果集
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="filterWord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public async static Task<IPagedList<DataTable>> QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, int pageIndex, int pageSize, string sortBy, string sortDirection)
        {
            int getQueryStartTime = Environment.TickCount;
            int recordCount = 0;
            DataTable tb = new DataTable("ResultTable");
            MD_GuideLine define = await GetGuidelineDefine(guideLineId);
            if (define != null)
            {
                string queryStr = await GetGuidelineMethod(guideLineId);

                List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
                if (param != null && define.Parameters != null)
                {
                    foreach (var p in param)
                    {
                        MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
                        if (md_pa != null)
                        {
                            glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
                        }
                    }
                }
                foreach (MDQuery_GuideLineParameter gp in glPara)
                {
                    queryStr = MySqlQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }

                if (!string.IsNullOrEmpty(filterWord))
                {
                    queryStr = string.Format("select * from (\n {0} \n) where {1}", queryStr, filterWord);
                }

                try
                {
                    var cot = await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.queryString, CommandType.Text, string.Format("select count(*) from (\n {0} \n) as t ", queryStr));
                    recordCount = Convert.ToInt32(cot);
                }
                catch (Exception e)
                {
                    MysqlLogWriter.WriteSystemLog("Exception :QueryGuideline310行异常，异常信息为" + e.Message, "ERROR");
                }

                queryStr = StrUtils.BuildPagingSQL22(queryStr, pageIndex, pageSize, sortBy, sortDirection);
                tb = await FillResultData(queryStr, "ResultTable");
                MysqlLogWriter.WriteQueryLog(queryStr, Environment.TickCount - getQueryStartTime, "2");
            }          
            var list = new List<DataTable>();
            list.Add(tb);         
            var ret = new StaticPagedList<DataTable>(list, pageIndex, pageSize, recordCount);
        
            return ret;
        }




        private const string SqlGetGuideLineMethod = @"select ZBSF  from tj_zdyzbdyb where ID=@Id";
        /// <summary>
        /// 取指标的sql语句
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <returns></returns>
        public async static Task<string> GetGuidelineMethod(string guideLineId)
        {
            try
            {
                MySqlParameter[] param = { new MySqlParameter("@Id", MySqlDbType.Int64) };
                param[0].Value = Convert.ToInt64(guideLineId);

                object sfobj = await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, SqlGetGuideLineMethod, param);
                if (sfobj == null || sfobj == DBNull.Value) return "";
                return sfobj.ToString();
            }
            catch (Exception e)
            {
                string errorMessage = string.Format("取指标[{0}]的sql语句出错,错误信息为{1}", guideLineId, e.Message);
                MysqlLogWriter.WriteSystemLog(errorMessage, "ERROR");
                return "";
            }
        }


        public async static Task<DataTable> FillResultData(string _selectStr, string _tableName)
        {
            DataTable _dt = new DataTable(_tableName);

            try
            {
                _dt = await MysqlDBHelper.FillDataTable(MysqlDBHelper.queryString, CommandType.Text, _selectStr);
            }
            catch (Exception e)
            {
                string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
                                e.Message, _selectStr);
                MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
            }
            return _dt;
        }




    }
}
