using System.Collections.Generic;
using Scouts.Core.Model.Enums;

namespace Scouts.Core.Model
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WorkEmail { get; set; }
        public virtual MembershipStatus Status { get; set; }
        public virtual ContactValueType ContactValue { get; set; }

        public virtual ICollection<ScoutContact> Scouts { get; set; }

    }
}
