namespace ShopingCart.Models
{
    public class Product
    {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }

            public int CategoryId { get; set; }
            public bool IsActive { get; set; }
            public DateTime EntryDate { get; set; }
            public string  ProductImage { get; set; }
        public string  ProductImag { get; set; }
            public string CategoryName { get; set; }
           


        

    }
}
