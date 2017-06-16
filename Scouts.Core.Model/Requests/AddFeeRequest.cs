using System;

namespace Scouts.Core.Model.Requests
{
    public class AddFeeRequest
    {
        public int ScoutId { get; set; }
        public int FeeId { get; set; }
        public DateTime Date { get; set; }
    }
}
