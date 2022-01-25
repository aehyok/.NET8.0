using System;
using System.Collections.Generic;
using aehyok.Base;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public partial class FlowStateTransition: EntityBase
    {
        //public string Id { get; set; } = null!;
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
