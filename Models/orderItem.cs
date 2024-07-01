namespace ShopingCart.Models
{
    public class orderItem
    {
        
            public int? OrderaItemId { get; set; }
            public string? ProductName { get; set; }
            public int? OrderId { get; set; }
            public decimal? ProductPrice { get; set; }
            public DateTime? EntryDate { get; set; }

        public int? Quantity { get; set; }

    }
    
}
