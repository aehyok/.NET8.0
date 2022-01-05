using MySql.Data.MySqlClient;
using aehyok.Lib.MetaData.Define;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using aehyok.Core.MySql;

namespace aehyok.MysqlDataAccess
{
    public class MD_DefineFactory
    {

        /// <summary>
        /// 更新列定义
        /// </summary>
        /// <param name="_table"></param>
        /// <param name="_tc"></param>
        public static void UpdateColumnDefine(MD_Table _table, MD_TableColumn _tc)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" update md_tablecolumn  set TID=@TID,COLUMNNAME=@COLUMNNAME,");
            _sb.Append(" ISNULLABLE=@ISNULLABLE,TYPE=@TYPE,`PRECISION`=@PRECISION,SCALE=@SCALE,");
            _sb.Append(" LENGTH=@LENGTH,REFDMB=@REFDMB,DMBLEVELFORMAT=@DMBLEVELFORMAT,SECRETLEVEL=@SECRETLEVEL,");
            _sb.Append(" DISPLAYTITLE=@DISPLAYTITLE,DISPLAYFORMAT=@DISPLAYFORMAT,DISPLAYLENGTH=@DISPLAYLENGTH,DISPLAYHEIGHT=@DISPLAYHEIGHT,");
            _sb.Append(" DISPLAYORDER=@DISPLAYORDER,CANDISPLAY=@CANDISPLAY,COLWIDTH=@COLWIDTH,DWDM=@DWDM,");
            _sb.Append(" CTAG=@CTAG,REFWORDTB=@REFWORD ");
            _sb.Append(" WHERE TCID=@TCID");

            MySqlParameter[] _param3 = {
                new MySqlParameter("@TID", MySqlDbType.Int64),
                new MySqlParameter("@COLUMNNAME", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ISNULLABLE", MySqlDbType.VarChar, 100),
                new MySqlParameter("@TYPE", MySqlDbType.VarChar, 20),
                new MySqlParameter("@PRECISION", MySqlDbType.Int32),
                new MySqlParameter("@SCALE",MySqlDbType.Int32),
                new MySqlParameter("@LENGTH",MySqlDbType.Int32),
                new MySqlParameter("@REFDMB",MySqlDbType.VarChar,50),
                new MySqlParameter("@DMBLEVELFORMAT",MySqlDbType.VarChar,20),
                new MySqlParameter("@SECRETLEVEL", MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DISPLAYFORMAT", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DISPLAYLENGTH", MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYHEIGHT", MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYORDER", MySqlDbType.Int32),
                new MySqlParameter("@CANDISPLAY",MySqlDbType.Int32),
                new MySqlParameter("@COLWIDTH",MySqlDbType.Int32),
                new MySqlParameter("@DWDM",MySqlDbType.VarChar,20),
                new MySqlParameter("@CTAG",MySqlDbType.VarChar,500),
                new MySqlParameter("@REFWORD",MySqlDbType.VarChar,50),
                new MySqlParameter("@TCID", MySqlDbType.Int64)
            };

            _param3[0].Value = Convert.ToInt64(_table.TID);
            _param3[1].Value = _tc.ColumnName;
            _param3[2].Value = _tc.IsNullable ? "Y" : "N";
            _param3[3].Value = _tc.ColumnType;
            _param3[4].Value = Convert.ToInt32(_tc.Precision);
            _param3[5].Value = Convert.ToInt32(_tc.Scale);
            _param3[6].Value = Convert.ToInt32(_tc.Length);
            _param3[7].Value = _tc.RefDMB;
            _param3[8].Value = _tc.DMBLevelFormat;

            _param3[9].Value = Convert.ToInt32(_tc.SecretLevel);
            _param3[10].Value = _tc.DisplayTitle;
            _param3[11].Value = _tc.DisplayFormat;
            _param3[12].Value = Convert.ToInt32(_tc.DisplayLength);
            _param3[13].Value = Convert.ToInt32(_tc.DisplayHeight);
            _param3[14].Value = Convert.ToInt32(_tc.DisplayOrder);
            _param3[15].Value = _tc.CanDisplay ? 1 : 0;
            _param3[16].Value = Convert.ToInt32(_tc.ColWidth);
            _param3[17].Value = _tc.DWDM;
            _param3[18].Value = _tc.CTag;
            _param3[19].Value = _tc.RefWordTableName;
            _param3[20].Value = Convert.ToInt64(_tc.ColumnID);

            MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param3);
            MyDA_MetaDataQuery.ModelLib.Clear();
        }

        /// <summary>
        /// 插入列定义
        /// </summary>
        /// <param name="_table"></param>
        /// <param name="_tc"></param>
        public static void InsertColumnDefine(MD_Table _table, MD_TableColumn _tc)
        {
            try
            {
                StringBuilder _sb_insert = new StringBuilder();
                _sb_insert.Append(" insert into md_tablecolumn (TCID,TID,COLUMNNAME,");
                _sb_insert.Append(" ISNULLABLE,TYPE,`PRECISION`,SCALE,");
                _sb_insert.Append(" LENGTH,REFDMB,DMBLEVELFORMAT,SECRETLEVEL,");
                _sb_insert.Append(" DISPLAYTITLE,DISPLAYFORMAT,DISPLAYLENGTH,DISPLAYHEIGHT,");
                _sb_insert.Append(" DISPLAYORDER,CANDISPLAY,COLWIDTH,DWDM,");
                _sb_insert.Append(" CTAG,REFWORDTB ) values ");
                _sb_insert.Append(" (@TCID,@TID,@COLUMNNAME,");
                _sb_insert.Append(" @ISNULLABLE,@TYPE,@PRECISION,@SCALE,");
                _sb_insert.Append(" @LENGTH,@REFDMB,@DMBLEVELFORMAT,@SECRETLEVEL,");
                _sb_insert.Append(" @DISPLAYTITLE,@DISPLAYFORMAT,@DISPLAYLENGTH,@DISPLAYHEIGHT,");
                _sb_insert.Append(" @DISPLAYORDER,@CANDISPLAY,@COLWIDTH,@DWDM,");
                _sb_insert.Append(" @CTAG,@REFWORDTB)  ");

                MySqlParameter[] _param3 = {
                new MySqlParameter("@TCID", MySqlDbType.Int64),
                new MySqlParameter("@TID", MySqlDbType.Int64),
                new MySqlParameter("@COLUMNNAME", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ISNULLABLE", MySqlDbType.VarChar, 100),
                new MySqlParameter("@TYPE", MySqlDbType.VarChar, 20),
                new MySqlParameter("@PRECISION", MySqlDbType.Int32),
                new MySqlParameter("@SCALE",MySqlDbType.Int32),
                new MySqlParameter("@LENGTH",MySqlDbType.Int32),
                new MySqlParameter("@REFDMB",MySqlDbType.VarChar,50),
                new MySqlParameter("@DMBLEVELFORMAT",MySqlDbType.VarChar,20),
                new MySqlParameter("@SECRETLEVEL", MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DISPLAYFORMAT", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DISPLAYLENGTH", MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYHEIGHT", MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYORDER", MySqlDbType.Int32),
                new MySqlParameter("@CANDISPLAY",MySqlDbType.Int32),
                new MySqlParameter("@COLWIDTH",MySqlDbType.Int32),
                new MySqlParameter("@DWDM",MySqlDbType.VarChar,20),
                new MySqlParameter("@CTAG",MySqlDbType.VarChar,500),
                new MySqlParameter("@REFWORDTB",MySqlDbType.VarChar,50)
            };
                _param3[0].Value = Convert.ToInt64(_tc.ColumnID);
                _param3[1].Value = Convert.ToInt64(_table.TID);
                _param3[2].Value = _tc.ColumnName;
                _param3[3].Value = _tc.IsNullable ? "Y" : "N";
                _param3[4].Value = _tc.ColumnType;
                _param3[5].Value = Convert.ToInt32(_tc.Precision);
                _param3[6].Value = Convert.ToInt32(_tc.Scale);
                _param3[7].Value = Convert.ToInt32(_tc.Length);
                _param3[8].Value = _tc.RefDMB;
                _param3[9].Value = _tc.DMBLevelFormat;

                _param3[10].Value = Convert.ToInt32(_tc.SecretLevel);
                _param3[11].Value = _tc.DisplayTitle;
                _param3[12].Value = _tc.DisplayFormat;
                _param3[13].Value = Convert.ToInt32(_tc.DisplayLength);
                _param3[14].Value = Convert.ToInt32(_tc.DisplayHeight);
                _param3[15].Value = Convert.ToInt32(_tc.DisplayOrder);
                _param3[16].Value = _tc.CanDisplay ? 1 : 0;
                _param3[17].Value = Convert.ToInt32(_tc.ColWidth);
                _param3[18].Value = _tc.DWDM;
                _param3[19].Value = _tc.CTag;
                _param3[20].Value = _tc.RefWordTableName;


                MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb_insert.ToString(), _param3);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            MyDA_MetaDataQuery.ModelLib.Clear();
        }





    }
}
