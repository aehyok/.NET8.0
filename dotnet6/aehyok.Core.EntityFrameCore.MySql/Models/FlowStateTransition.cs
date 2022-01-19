using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    public partial class FlowStateTransition
    {
        public string Id { get; set; } = null!;
        public string? StateId { get; set; }
        public string? ActionName { get; set; }
        public string? ActionTitle { get; set; }
        public string? TargetStateId { get; set; }
        public int? ActionType { get; set; }
        public int? UserType { get; set; }
        public int? DisplayOrder { get; set; }
        public string? ActionParameter { get; set; }
    }
}
