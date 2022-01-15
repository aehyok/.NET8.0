using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    public partial class MdViewgroupitem
    {
        public long Vgiid { get; set; }
        public long? Vgid { get; set; }
        public long? Viewid { get; set; }
        public int? Displayorder { get; set; }
        public string? Dwdm { get; set; }
    }
}
