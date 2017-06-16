using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouts.Core.Model
{
    public class ScoutContact
    {
        public int ScoutId { get; set; }
        public int ContactId { get; set; }
        public string Relationship { get; set; }

        public virtual Scout Scout { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
