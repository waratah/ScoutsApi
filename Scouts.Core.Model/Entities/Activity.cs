using System;
using System.Collections.Generic;

namespace Scouts.Core.Model
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string ActivitySummary { get; set; }
        public decimal Cost { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<ActivityJoinScouts> Scouts { get; set; }
    }
}
