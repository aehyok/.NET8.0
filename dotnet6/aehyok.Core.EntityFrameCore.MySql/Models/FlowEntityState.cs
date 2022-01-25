using System;
using System.Collections.Generic;
using aehyok.Base;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public partial class FlowEntityState: EntityBase
    {
        //public string Id { get; set; } = null!;
        public string? FlowId { get; set; }
        public string? StateName { get; set; }
        public string? StateDisplayName { get; set; }
        public string? StateDescript { get; set; }
        public string? StateType { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
