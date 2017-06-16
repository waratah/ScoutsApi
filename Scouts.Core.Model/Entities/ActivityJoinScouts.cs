using System;
using System.Collections.Generic;
using System.Text;

namespace Scouts.Core.Model
{
    public class ActivityJoinScouts
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int ScoutId{ get; set; }
        public Scout Scout { get; set; }
    }
}
