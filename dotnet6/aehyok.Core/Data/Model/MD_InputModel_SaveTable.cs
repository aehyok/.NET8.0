using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
    public class MD_InputModel_SaveTable
    {
        /// <summary>
        /// 录入模型保存表Id
        /// </summary>
        
        public string Id { get; set; }

        /// <summary>
        /// 录入模型表名称
        /// </summary>
        
        public string TableName { get; set; }

        /// <summary>
        /// 录入模型表显示名称
        /// </summary>
        
        public string TableTitle { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        
        public bool IsLock { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 录入模型Id
        /// </summary>
        
        public string InputModelId { get; set; }
        
        public List<MD_InputModel_SaveTableColumn> Columns { get; set; }

        /// <summary>
        /// 保存模式
        /// </summary>
        
        public string SaveMode { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", TableTitle, TableName);
        }

        public MD_InputModel_SaveTable() { }
        public MD_InputModel_SaveTable(string id, string tableName, string tableTitle, bool isLock, string inputModelId, int displayOrder)
        {
            Id = id;
            TableName = tableName;
            TableTitle = tableTitle;
            IsLock = isLock;
            DisplayOrder = displayOrder;
            InputModelId = inputModelId;
        }
        public MD_InputModel_SaveTable(string id, string tableName, string tableTitle, bool isLock, string inputModelId, int displayOrder, string saveMode)
        {
            Id = id;
            TableName = tableName;
            TableTitle = tableTitle;
            IsLock = isLock;
            DisplayOrder = displayOrder;
            InputModelId = inputModelId;
            this.SaveMode = (saveMode == null || saveMode.Length == 0) ? "DEFAULT" : saveMode.ToUpper();
        }
    }
}
