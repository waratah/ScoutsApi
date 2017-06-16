
namespace Scouts.Core.Model.Requests
{
    public class FeeUpdate
    {
        public int FeeId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
