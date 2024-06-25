namespace ShopingCart.Models
{
    public class OrderMaster
    {
        public int? UserId { get; set; }
        public int? OrderId { get; set; }  
        public string? UserNAme  { get; set; }
        public int? TotalAmount { get; set; }
        public DateTime? EnteryDate { get; set; }
        public string ? ToDate { get; set; }
        public string ? FromDate { get; set; }

    }
}
