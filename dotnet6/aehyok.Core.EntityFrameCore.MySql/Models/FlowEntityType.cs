using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    public partial class FlowEntityType
    {
        public string Id { get; set; } = null!;
        public string? FlowName { get; set; }
        public string? Description { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
