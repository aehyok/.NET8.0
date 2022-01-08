using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aehyok.Lib.MetaData.Common
{
    public static class QueryModelCommon
    {
        public static MDModel_Table_Column GetColumnByName(this MDModel_Table Table, string ColumnName)
        {
            var _find = from _c in Table.Columns
                        where _c.ColumnName == ColumnName
                        select _c;
            if (_find == null || _find.Count() < 1) return null;
            return _find.First();
        }

        public static MDQuery_ConditionItem GetConditionItemByIndex(this MDQuery_Request QueryRequest, string ItemIndex)
        {
            var _find = from _item in QueryRequest.ConditionItems
                        where _item.ColumnIndex == ItemIndex
                        select _item;
            if (_find == null || _find.Count() < 1) return null;
            return _find.First();
        }



        public static List<MDModel_Table_Column> GetMDModelColumns(this MD_ViewTable ViewTable)
        {
            List<MDModel_Table_Column> _ret = new List<MDModel_Table_Column>();
            foreach (MD_ViewTableColumn _col in ViewTable.Columns)
            {
                MDModel_Table_Column _newcol = new MDModel_Table_Column(_col);
                _ret.Add(_newcol);
            }
            return _ret;
        }
    }
}
