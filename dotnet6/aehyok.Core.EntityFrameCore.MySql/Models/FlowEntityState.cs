using System;
using System.Collections.Generic;
using aehyok.Base;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    public partial class FlowEntityState: BaseEntity
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
