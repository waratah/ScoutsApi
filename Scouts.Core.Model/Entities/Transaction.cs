using System;

namespace Scouts.Core.Model.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int? DiscountId { get; set; }
        public int? ActivityId { get; set; }
        public int ScoutId { get; set; }
        public int? FeeId { get; set; }

        public DateTime TransactionDate { get; set; }
        public string ReceiptId { get; set; }
        public decimal Amount { get; set; }
        public int? BookNo { get; set; }

        public Scout Scout { get; set; }
    }
}
