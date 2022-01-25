using System;
using System.Collections.Generic;
using aehyok.Base;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public partial class FlowEntityType: EntityBase
    {
        //public string Id { get; set; } = null!;
        public string? FlowName { get; set; }
        public string? Description { get; set; }
        public int? DisplayOrder { get; set; }

        public int? Status { get; set; }
    }
}
