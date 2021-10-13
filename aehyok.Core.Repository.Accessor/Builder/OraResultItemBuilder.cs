using aehyok.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Repository.Accessor.Builder
{
    public class OraResultItemBuilder
    {
        public static string BuildItem(MDQuery_TableColumn rColumn, MDModel_QueryModel qv, SinoRequestUser requestUser)
        {
            switch (rColumn.ColumnType)
            {
                case QueryColumnType.TableColumn:
                    return BuildTableColumnResult(rColumn, qv, requestUser);

                case QueryColumnType.CalculationColumn:
                    return BuildCalculationColumnResult(rColumn, qv, requestUser);

                case QueryColumnType.StatisticsColumn:
                    return BuildStatisticsColumnResult(rColumn, qv, requestUser); ;
            }
            return "";
        }

        private static string BuildStatisticsColumnResult(MDQuery_TableColumn rColumn, MDModel_QueryModel qv, SinoRequestUser requestUser)
        {
            string _itemString = "";
            if (rColumn.ColumnDataType == "NUMBER")
            {
                _itemString = string.Format("Round( ({0}),20) {1}", rColumn.ColumnAlgorithm, rColumn.ColumnAlias);
            }
            else
            {
                _itemString = string.Format("({0}) {1}", rColumn.ColumnAlgorithm, rColumn.ColumnAlias);
            }

            return "," + _itemString;
        }

        private static string BuildCalculationColumnResult(MDQuery_TableColumn rColumn, MDModel_QueryModel qv, SinoRequestUser requestUser)
        {
            string _itemString = "";
            if (rColumn.ColumnDataType == "NUMBER")
            {
                _itemString = string.Format("Round({0},20) {1}", rColumn.ColumnAlgorithm, rColumn.ColumnAlias);
            }
            else
            {
                _itemString = string.Format("{0} {1}", rColumn.ColumnAlgorithm, rColumn.ColumnAlias);
            }

            return "," + _itemString;
        }

        public static MDModel_Table_Column GetColumnDefineByName(MDModel_QueryModel qv, string tName, string cName)
        {
            MDModel_Table_Column _ret = null;
            if (qv.MainTable.TableName == tName)
            {
                _ret = (from _c in qv.MainTable.Columns
                        where _c.ColumnName == cName
                        select _c).First();
            }
            else
            {
                MDModel_Table _tb = (from _t in qv.ChildTables
                                     where _t.TableName == tName
                                     select _t).First();
                if (_tb == null) return null;

                _ret = (from _c in _tb.Columns
                        where _c.ColumnName == cName
                        select _c).First();

            }
            return _ret;

        }

        private static string BuildTableColumnResult(MDQuery_TableColumn rColumn, MDModel_QueryModel qv, SinoRequestUser requestUser)
        {
            MDModel_Table_Column _mcolumn = GetColumnDefineByName(qv, rColumn.TableName, rColumn.ColumnName);
            if (_mcolumn == null) return "";
            return "," + BuildItem(_mcolumn, requestUser);
        }

        public static string BuildItem(MDModel_Table_Column mcolumn, SinoRequestUser requestUser)
        {
            if ((mcolumn.SecretLevel > requestUser.BaseInfo.SecretLevel) || (!mcolumn.CanDisplay))
            {
                return string.Format("'＊＊＊' {0}", mcolumn.ColumnAlias);
            }


            if (mcolumn.ColumnRefDMB == string.Empty)
            {
                if (mcolumn.ColumnDataType == "NUMBER")
                {
                    return string.Format("Round({0}.{1},20) {2}", mcolumn.TableName, mcolumn.ColumnName, mcolumn.ColumnAlias);
                }
                else if (mcolumn.ColumnDataType == "单位名称")
                {
                    return string.Format("ZHTJ_ZZJG2.GETDWMC(to_number({0}.{1})) {2}", mcolumn.TableName, mcolumn.ColumnName,
                        mcolumn.ColumnAlias);
                }
                else if (mcolumn.ColumnDataType == "用户名称")
                {
                    return string.Format("ZHTJ_ZZJG2.Get_YHXM(to_number({0}.{1})) {2}", mcolumn.TableName, mcolumn.ColumnName,
                        mcolumn.ColumnAlias);
                }
                else
                {
                    return string.Format("{0}.{1} {2}", mcolumn.TableName, mcolumn.ColumnName, mcolumn.ColumnAlias);
                }

            }
            else
            {
                if (mcolumn.DMBLevelFormat == "多选")
                {
                    return string.Format("ZHCX.GetCTMuti_FGF(TO_CHAR({0}.{1}),'{3}','{4}') {2}", mcolumn.TableName, mcolumn.ColumnName,
    mcolumn.ColumnAlias, mcolumn.ColumnRefDMB, '；');
                }
                else
                {
                    return string.Format("ZHCX.GetCT(TO_CHAR({0}.{1}),'{3}') {2}", mcolumn.TableName, mcolumn.ColumnName,
mcolumn.ColumnAlias, mcolumn.ColumnRefDMB);
                }
            }
        }


    }
}
