namespace ShopingCart.Models
{
    public class PaymentReport
    {
        public int? PaymentId { get; set; }
        public int? OrderId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}
