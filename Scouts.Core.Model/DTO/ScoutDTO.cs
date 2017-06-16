using System;

namespace Scouts.Core.Model
{
    public class ScoutDTO
    {
        public int ScoutId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string MemberNumber { get; set; }
        public ScoutType Section { get; set; }
        public DateTime? DOB { get; set; }
        public string Medical { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }
    }
}
