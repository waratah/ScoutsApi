
namespace Scouts.Core.Model
{
    public class ScoutSummary
    {
        public int ScoutId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string MemberNumber { get; set; }
        public ScoutType Section { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }
    }
}
