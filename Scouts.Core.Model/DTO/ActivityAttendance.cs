using System;
using System.Collections.Generic;

namespace Scouts.Core.Model
{
    public class ActivityAttendance
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public virtual ICollection<Attendance> Attending { get; set; }
    }
}
