using MySql.Data.MySqlClient;
using aehyok.Lib.MetaData.Define;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.MySqlDataAccessor
{
    public class GuideLineParamSetting
    {
        public static MD_GuideLine_ParamSetting GetCurrentPostRecord(string _guideLineID, MySqlConnection cn)
        {
            MD_GuideLine_ParamSetting _ret = null;
            string _sql = "select CSID,ZBID,DWID,CS from tj_zdyzbdyb_cs where zbid =@ZBID  ";
            MySqlCommand _cmd = new MySqlCommand(_sql, cn);
            _cmd.Parameters.Add(new MySqlParameter("@ZBID", Convert.ToInt64(_guideLineID)));
            // _cmd.Parameters.Add(new MySqlParameter("@DWID", Convert.ToInt64(000)));
            using (MySqlDataReader _dr = _cmd.ExecuteReader())
            {
                while (_dr.Read())
                {
                    string _csid = _dr.IsDBNull(0) ? "" : _dr.GetDecimal(0).ToString();
                    string _zbid = _dr.IsDBNull(1) ? "" : _dr.GetDecimal(1).ToString();
                    string _dwid = _dr.IsDBNull(2) ? "" : _dr.GetDecimal(2).ToString();
                    string _cs = _dr.IsDBNull(3) ? "" : _dr.GetString(3);
                    _ret = new MD_GuideLine_ParamSetting(_csid, _zbid, _dwid, _cs);

                }
            }
            return _ret;
        }

        public static MD_GuideLine_ParamSetting GetDefaultRecord(string _guideLineID, MySqlConnection cn)
        {
            MD_GuideLine_ParamSetting _ret = null;            
            long _csid = Lib.Snowflake.Instance.NextId();
            string _zbid = _guideLineID;
            string _dwid = "000";
            _ret = new MD_GuideLine_ParamSetting(_csid.ToString(), _zbid, _dwid, "");

            return _ret;
        }

        public const string ChangeSetting = @"update tj_zdyzbdyb_cs set cs=@CS,querysql =@QUERYSQL 
                                        where zbid=@ZBID   ";
        public static void SaveCurrentPostRecord(MD_GuideLine_ParamSetting SavedSetting, string _queryStr, MySqlConnection cn)
        {
            MySqlCommand _cmd = new MySqlCommand(ChangeSetting, cn);
            _cmd.Parameters.Add(new MySqlParameter("@CS", SavedSetting.ParamMeta));
            _cmd.Parameters.Add(new MySqlParameter("@QUERYSQL", _queryStr));
            _cmd.Parameters.Add(new MySqlParameter("@ZBID", Convert.ToInt64(SavedSetting.GuideLineID)));
            //  _cmd.Parameters.Add(new MySqlParameter("@DWID", Convert.ToInt64(000)));
            _cmd.ExecuteNonQuery();

        }
        public const string InsertSetting = @"insert into tj_zdyzbdyb_cs (csid,zbid,dwid,cs,querysql)
                                        values  (@CSID,@ZBID,@DWID,@CS,@QUERYSQL) ";
        internal static void InsertCurrentPostRecord(MD_GuideLine_ParamSetting SavedSetting, string _queryStr, MySqlConnection cn)
        {
            MySqlCommand _cmd = new MySqlCommand(InsertSetting, cn);
            _cmd.Parameters.Add(new MySqlParameter("@CS", Convert.ToInt64(SavedSetting.CSID)));
            _cmd.Parameters.Add(new MySqlParameter("@ZBID", Convert.ToInt64(SavedSetting.GuideLineID)));
            _cmd.Parameters.Add(new MySqlParameter("@DWID", Convert.ToInt64(000)));
            _cmd.Parameters.Add(new MySqlParameter("@CS", SavedSetting.ParamMeta));
            _cmd.Parameters.Add(new MySqlParameter("@QUERYSQL", _queryStr));
            _cmd.ExecuteNonQuery();
        }

    }
}
