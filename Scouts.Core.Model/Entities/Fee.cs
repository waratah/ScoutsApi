using System;
using System.Collections.Generic;

namespace Scouts.Core.Model
{
    public class Fee
    {
        public int FeeId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal CurrentCost { get; set; }

        //public virtual ICollection<ActivityJoinScouts> Scouts { get; set; }
    }
}
