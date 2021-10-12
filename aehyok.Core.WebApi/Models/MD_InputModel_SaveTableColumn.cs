using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Models
{
    public class MD_InputModel_SaveTableColumn
    {
        
        public string Id { get; set; }
        
        public string SrcColumn { get; set; }
        
        public string DesColumn { get; set; }
        
        public string Method { get; set; }
        
        public string Descript { get; set; }

        public MD_InputModel_SaveTableColumn() { }
        public MD_InputModel_SaveTableColumn(string id, string srcColumn, string desColumn, string method, string descript)
        {
            Id = id;
            SrcColumn = srcColumn;
            DesColumn = desColumn;
            Method = method;
            Descript = descript;
        }
    }
}
