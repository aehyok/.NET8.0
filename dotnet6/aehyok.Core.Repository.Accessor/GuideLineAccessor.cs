using aehyok.Core.Data.Entity.GuideLine;
using aehyok.Core.Data.Model;
using aehyok.Core.MySql;
using aehyok.Core.MySqlDataAccessor;
using aehyok.Core.Repository.Accessor.Builder;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD_GuideLine = aehyok.Core.Data.Model.MD_GuideLine;
using MD_GuideLineParameter = aehyok.Core.Data.Model.MD_GuideLineParameter;

namespace aehyok.Core.Repository.Accessor
{
    public class GuideLineAccessor
    {
        #region 通用指标定义
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

        private const string SqlGetGuidelineDefine = @"select ID,ZBMC,ZBZT,ZBMETA,FID,JSMX_ZBMETA,XSXH,ZBSM from TJ_ZDYZBDYB where ID=:Id ";
        /// <summary>
        /// 取指标定义
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <returns></returns>
        public static MD_GuideLine GetGuidelineDefine(string guideLineId)
        {
            MD_GuideLine define = null;
            if (!string.IsNullOrEmpty(guideLineId))
            {
                if (GuidelineDefine.ContainsKey(guideLineId))
                    define = GuidelineDefine[guideLineId];
                else
                {
                    using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(SqlGetGuidelineDefine, cn);
                            cmd.Parameters.Add(":Id", (MySqlDbType)decimal.Parse(guideLineId));
                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    string id = dr.IsDBNull(0) ? "" : dr.GetMySqlDecimal(0).Value.ToString();
                                    string name = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                    string groupname = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                    string zbmeta1 = dr.IsDBNull(3) ? "" : dr.GetString(3);
                                    string fatherid = dr.IsDBNull(4) ? "0" : dr.GetMySqlDecimal(4).Value.ToString();
                                    string zbmeta2 = dr.IsDBNull(5) ? "" : dr.GetString(5);
                                    int displayorder = dr.IsDBNull(6) ? 0 : Convert.ToInt32(dr.GetMySqlDecimal(6).Value);
                                    string descript = dr.IsDBNull(7) ? "" : dr.GetString(7);
                                    string fullMeta = zbmeta1 + zbmeta2;

                                    define = new MD_GuideLine(id, name, groupname, fatherid, displayorder, descript);
                                    //define.Parameters = MD_GuideLine.GetParametersFromMeta(fullMeta);
                                    //define.ResultGroups = MC_GuideLine.GetFieldGroupsFromMeta(fullMeta);
                                    //define.DetailDefines = MC_GuideLine.GetDetaiDefinelFromMeta(fullMeta);
                                    define.Children = GetChildGuidelineDefine(define.Id, cn);
                                    GuidelineDefine.Add(guideLineId, define);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            string errorMessage = string.Format("取指标[{0}]的定义出错,错误信息为{1}", guideLineId, exception.Message);
                            //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                        }
                    }
                }
            }
            return define;
        }

        private const string SqlGetChildGuidelineDefine = @"select ID,ZBMC,ZBZT,ZBMETA,FID,JSMX_ZBMETA,XSXH,ZBSM from TJ_ZDYZBDYB where FID=:Id ";

        public static List<MD_GuideLine> GetChildGuidelineDefine(string guideLineId, MySqlConnection cn)
        {
            List<MD_GuideLine> _ret = new List<MD_GuideLine>();
            MD_GuideLine define = null;
            if (!string.IsNullOrEmpty(guideLineId))
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SqlGetChildGuidelineDefine, cn);
                    //_cmd.Parameters.Add(":Id", decimal.Parse(guideLineId));
                    MySqlDataReader dr = _cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string id = dr.IsDBNull(0) ? "" : dr.GetMySqlDecimal(0).Value.ToString();
                        string name = dr.IsDBNull(1) ? "" : dr.GetString(1);
                        string groupname = dr.IsDBNull(2) ? "" : dr.GetString(2);
                        string zbmeta1 = dr.IsDBNull(3) ? "" : dr.GetString(3);
                        string fatherid = dr.IsDBNull(4) ? "0" : dr.GetMySqlDecimal(4).Value.ToString();
                        string zbmeta2 = dr.IsDBNull(5) ? "" : dr.GetString(5);
                        int displayorder = dr.IsDBNull(6) ? 0 : Convert.ToInt32(dr.GetMySqlDecimal(6).Value);
                        string descript = dr.IsDBNull(7) ? "" : dr.GetString(7);
                        string fullMeta = zbmeta1 + zbmeta2;

                        define = new MD_GuideLine(id, name, groupname, fatherid, displayorder, descript);
                        //define.Parameters = MC_GuideLine.GetParametersFromMeta(fullMeta);
                        //define.ResultGroups = MC_GuideLine.GetFieldGroupsFromMeta(fullMeta);
                        //define.DetailDefines = MC_GuideLine.GetDetaiDefinelFromMeta(fullMeta);
                        define.Children = GetChildGuidelineDefine(define.Id, cn);
                        _ret.Add(define);
                    }
                    dr.Close();
                }
                catch (Exception exception)
                {
                    string errorMessage = string.Format("取GetChildGuidelineDefine子指标[{0}]的定义出错,错误信息为{1}", guideLineId, exception.Message);
                    //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                }

            }
            return _ret;
        }


        /// <summary>
        /// 取结果记录数
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="filterWord"></param>
        /// <param name="requestUser"></param>
        /// <returns></returns>
        public static int GetQueryResultCount(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser)
        {
            int ret = 0;
            string queryStr = GetGuidelineMethod(guideLineId);
            MD_GuideLine define = GetGuidelineDefine(guideLineId);
            if (define != null)
            {
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
                    queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }
                if (requestUser != null)
                {
                    queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
                }
                if (!string.IsNullOrEmpty(filterWord))
                {
                    queryStr = string.Format("select * from (\n {0} \n) where {1}", queryStr, filterWord);
                }
                try
                {
                    ret = Convert.ToInt32(MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, string.Format("select count(*) from (\n {0} \n) ", queryStr)));
                }
                catch (Exception exception)
                {
                    string errorMessage = string.Format("取指标[{0}]的结果记录数出错,错误信息为{1}", guideLineId, exception.Message);
                    //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                }
            }
            return ret;
        }

        public static int GetQueryResultCount(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser, MySqlConnection cn)
        {
            int ret = 0;
            string queryStr = GetGuidelineMethod(guideLineId);
            MD_GuideLine define = GetGuidelineDefine(guideLineId);
            if (define != null)
            {
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
                    queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }
                if (requestUser != null)
                {
                    queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
                }
                if (!string.IsNullOrEmpty(filterWord))
                {
                    queryStr = string.Format("select * from (\n {0} \n) where {1}", queryStr, filterWord);
                }
                try
                {
                    ret = Convert.ToInt32(MysqlDBHelper.ExecuteScalar(cn, CommandType.Text, string.Format("select count(*) from (\n {0} \n) ", queryStr)));
                }
                catch (Exception exception)
                {
                    string errorMessage = string.Format("取指标[{0}]的结果记录数出错,错误信息为{1}", guideLineId, exception.Message);
                    //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                }
            }
            return ret;
        }

        #region  added by lqm 2014.12.26
        /// <summary>
        /// added by lqm 2014.03.27 指标查询三个参数
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="requestUser"></param>
        /// <returns></returns>
        public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, SinoRequestUser requestUser)
        {
            return QueryGuideline(guideLineId, param, "", requestUser);
        }
        /// <summary>
        /// added by lqm 2014.12.26 
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="requestUser"></param>
        /// <returns></returns>
        public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, SinoRequestUser requestUser, MySqlConnection MySqlConnection)
        {
            return QueryGuideline(guideLineId, param, "", requestUser, MySqlConnection);
        }
        /// <summary>
        /// 取指标结果集
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="filterWord"></param>
        /// <param name="requestUser"></param>
        /// <returns></returns>
        public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser, MySqlConnection MySqlConnection)
        {
            int getQueryStartTime = Environment.TickCount;
            int count = 0;
            DataTable tb = new DataTable("ResultTable");
            MD_GuideLine define = GetGuidelineDefine(guideLineId);
            if (define != null)
            {
                string queryStr = GetGuidelineMethod(guideLineId);

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
                    queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }
                if (requestUser != null)
                {
                    queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
                }
                if (!string.IsNullOrEmpty(filterWord))
                {
                    queryStr = string.Format("select * from (\n {0} \n) where {1}", queryStr, filterWord);
                }
                tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count, MySqlConnection);
                if (requestUser != null && requestUser.BaseInfo != null)
                {
                    //OracleLogWriter.WriteQueryLog(BuildQueryLogStr(guideLineId, param, requestUser), Environment.TickCount - getQueryStartTime, count.ToString(), requestUser.BaseInfo.UserId, "2");
                }
            }
            return tb;
        }
        #endregion

        /// <summary>
        /// 取指标结果集
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="filterWord"></param>
        /// <param name="requestUser"></param>
        /// <returns></returns>
        public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser)
        {
            int getQueryStartTime = Environment.TickCount;
            int count = 0;
            DataTable tb = new DataTable("ResultTable");
            MD_GuideLine define = GetGuidelineDefine(guideLineId);
            if (define != null)
            {
                string queryStr = GetGuidelineMethod(guideLineId);

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
                    queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }
                if (requestUser != null)
                {
                    queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
                }
                if (!string.IsNullOrEmpty(filterWord))
                {
                    queryStr = string.Format("select * from (\n {0} \n) where {1}", queryStr, filterWord);
                }
                tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count);
                if (requestUser != null && requestUser.BaseInfo != null)
                {
                    //OracleLogWriter.WriteQueryLog(BuildQueryLogStr(guideLineId, param, requestUser), Environment.TickCount - getQueryStartTime, count.ToString(), requestUser.BaseInfo.UserId, "2");
                }
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
        /// <param name="requestUser"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, decimal pageIndex, decimal pageSize, string sortBy, string sortDirection, SinoRequestUser requestUser, ref int recordCount)
        {
            int getQueryStartTime = Environment.TickCount;
            int count = 0;
            DataTable tb = new DataTable("ResultTable");
            MD_GuideLine define = GetGuidelineDefine(guideLineId);
            if (define != null)
            {
                string queryStr = GetGuidelineMethod(guideLineId);

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
                    queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }
                if (requestUser != null)
                {
                    queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
                }
                if (!string.IsNullOrEmpty(filterWord))
                {
                    queryStr = string.Format("select * from (\n {0} \n) where {1}", queryStr, filterWord);
                }

                try
                {
                    recordCount = Convert.ToInt32(MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, string.Format("select count(*) from (\n {0} \n) ", queryStr)));
                }
                catch (Exception e)
                {
                    //OracleLogWriter.WriteSystemLog("Exception :QueryGuideline310行异常，异常信息为" + e.Message, "ERROR");
                }

                queryStr = OraQueryBuilder.BuildPagingSQL(queryStr, pageIndex, pageSize, sortBy, sortDirection);
                tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count);
                if (requestUser != null && requestUser.BaseInfo != null)
                {
                    //OracleLogWriter.WriteQueryLog(BuildQueryLogStr(guideLineId, param, requestUser), Environment.TickCount - getQueryStartTime, count.ToString(), requestUser.BaseInfo.UserId, "2");
                }
            }
            return tb;
        }
        #endregion

        /// <summary>
        /// 取所有主键的值
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <param name="param"></param>
        /// <param name="keyField"></param>
        /// <param name="requestUser"></param>
        /// <returns></returns>
        public static string GetAllKeyField(string guideLineId, Dictionary<string, object> param, string keyField, SinoRequestUser requestUser)
        {
            int count = 0;
            DataTable tb = new DataTable("ResultTable");
            MD_GuideLine define = GetGuidelineDefine(guideLineId);
            if (define != null)
            {
                string queryStr = GetGuidelineMethod(guideLineId);

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
                    queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
                }
                if (requestUser != null)
                {
                    queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
                }
                tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count);

                try
                {
                    tb.PrimaryKey = new DataColumn[] { tb.Columns[keyField] };
                }
                catch
                {
                    string errMsg = string.Format("取指标[zbid={0}]的结果集中所有主键[KeyField={1}]的值时设置主键出错，请检查主键是否唯一！", guideLineId, keyField);
                    //OracleLogWriter.WriteSystemLog(errMsg, "ERROR");
                    return "";
                }
            }

            List<string> listKeyField = new List<string>();
            foreach (DataRow row in tb.Rows)
            {
                listKeyField.Add(row[keyField].ToString());
            }
            return string.Join(",", listKeyField);
        }

        private const string SqlGetGuideLineMethod = @"select ZBSF  from TJ_ZDYZBDYB where ID=:Id";
        /// <summary>
        /// 取指标的sql语句
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <returns></returns>
        public static string GetGuidelineMethod(string guideLineId)
        {
            MySqlParameter[] param = { new MySqlParameter(":Id", MySqlDbType.Decimal) };
            try
            {
                param[0].Value = decimal.Parse(guideLineId);

                object sfobj = MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, SqlGetGuideLineMethod, param);
                if (sfobj == null || sfobj == DBNull.Value) return "";
                return sfobj.ToString();
            }
            catch (Exception e)
            {
                string errorMessage = string.Format("取指标[{0}]的sql语句出错,错误信息为{1}", guideLineId, e.Message);
                //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                return "";
            }
        }

        /// <summary>
        /// 组织指标查询日志语句
        /// </summary>
        /// <param name="zbid"></param>
        /// <param name="param"></param>
        /// <param name="requestUser"></param>
        /// <returns></returns>
        private static string BuildQueryLogStr(string zbid, Dictionary<string, object> param, SinoRequestUser requestUser)
        {
            string paraStr = "";
            if (param != null)
            {
                foreach (var p in param)
                {
                    paraStr += p.Key + ":" + p.Value + ";";
                }
            }
            return string.Format("用户{0}查看了 {1}（指标id={2}，参数={3}）", requestUser.BaseInfo.UserName, GetGuidelineDefine(zbid).GuideLineName, zbid, paraStr);
        }
    }
}
