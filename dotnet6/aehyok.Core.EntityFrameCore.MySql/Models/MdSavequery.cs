using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    public partial class MdSavequery
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public byte[]? Tjsf { get; set; }
        public int? Yhid { get; set; }
        public string? Type { get; set; }
        public DateOnly? Sj { get; set; }
        public long? Viewid { get; set; }
        public long? Sydwid { get; set; }
        public int? Ispublic { get; set; }
    }
}
