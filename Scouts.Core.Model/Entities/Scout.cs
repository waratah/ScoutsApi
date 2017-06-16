using System;
using System.Collections.Generic;

namespace Scouts.Core.Model
{
    public class Scout
    {
        public Scout()
        {
            StartDate = DateTime.Today;
            Active = true;
        }

        public int ScoutId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberNumber { get; set; }
        public ScoutType Section { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Medical { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ScoutContact> Contacts { get; set; }
        public virtual ICollection<ActivityJoinScouts> Activities { get; set; }
    }
}
