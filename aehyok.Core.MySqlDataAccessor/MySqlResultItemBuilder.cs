using aehyok.Lib.MetaData.Common;
using aehyok.Lib.MetaData.EnumDefine;
using aehyok.Lib.MetaData.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.MysqlDataAccess
{
    public class MySqlResultItemBuilder
    {
        public static string BuildItem(MDQuery_TableColumn _rColumn, MDModel_QueryModel _qv)
        {
            switch (_rColumn.ColumnType)
            {
                case QueryColumnType.TableColumn:
                    return BuildTableColumnResult(_rColumn, _qv);

                case QueryColumnType.CalculationColumn:
                    return BuildCalculationColumnResult(_rColumn, _qv);

                case QueryColumnType.StatisticsColumn:
                    return BuildStatisticsColumnResult(_rColumn, _qv); ;
            }
            return "";
        }

        private static string BuildTableColumnResult(MDQuery_TableColumn _rColumn, MDModel_QueryModel _qv)
        {
            MDModel_Table_Column _mcolumn = MC_QueryModel.GetColumnDefineByName(_qv, _rColumn.TableName, _rColumn.ColumnName);
            if (_mcolumn == null) return "";
            return "," + BuildItem(_mcolumn);
        }

        private static string BuildCalculationColumnResult(MDQuery_TableColumn _rColumn, MDModel_QueryModel _qv)
        {
            string _itemString = "";
            if (_rColumn.ColumnDataType == "NUMBER")
            {
                _itemString = string.Format("Round({0},20) {1}", _rColumn.ColumnAlgorithm, _rColumn.ColumnAlias);
            }
            else
            {
                _itemString = string.Format("{0} {1}", _rColumn.ColumnAlgorithm, _rColumn.ColumnAlias);
            }

            return "," + _itemString;
        }

        private static string BuildStatisticsColumnResult(MDQuery_TableColumn _rColumn, MDModel_QueryModel _qv)
        {
            string _itemString = "";
            if (_rColumn.ColumnDataType == "NUMBER")
            {
                _itemString = string.Format("Round( ({0}),20) {1}", _rColumn.ColumnAlgorithm, _rColumn.ColumnAlias);
            }
            else
            {
                _itemString = string.Format("({0}) {1}", _rColumn.ColumnAlgorithm, _rColumn.ColumnAlias);
            }

            return "," + _itemString;
        }


        public static string BuildItem(MDModel_Table_Column _mcolumn)
        {

            //if ((_mcolumn.ColumnDefine.TableColumn.SecretLevel > SinoUserCtx.CurUser.SecretLevel) || (!_mcolumn.ColumnDefine.TableColumn.CanDisplay))
            if (!_mcolumn.ColumnDefine.TableColumn.CanDisplay)
            {
                return string.Format("'＊＊＊' {0}", _mcolumn.ColumnAlias);
            }


            if (_mcolumn.ColumnRefDMB == string.Empty)
            {
                switch(_mcolumn.ColumnDataType)
                {
                    case "date":
                        return string.Format("DATE_FORMAT({0}.{1},'%Y-%m-%d') {2}", _mcolumn.TableName, _mcolumn.ColumnName, _mcolumn.ColumnAlias);
                    case "datetime":
                    case "timestamp":
                        return string.Format("DATE_FORMAT({0}.{1},'%Y-%m-%d %H:%i:%s') {2}", _mcolumn.TableName, _mcolumn.ColumnName, _mcolumn.ColumnAlias);
                    case "NUMBER":
                        return string.Format("Round({0}.{1},20) {2}", _mcolumn.TableName, _mcolumn.ColumnName, _mcolumn.ColumnAlias);
                    default:
                        return string.Format("{0}.{1} {2}", _mcolumn.TableName, _mcolumn.ColumnName, _mcolumn.ColumnAlias);
                }
            
            }
            else
            {
                return string.Format("mdfun_getct({0}.{1},'{3}') {2}", _mcolumn.TableName, _mcolumn.ColumnName,
                        _mcolumn.ColumnAlias, _mcolumn.ColumnRefDMB);
            }
        }


    }
}
