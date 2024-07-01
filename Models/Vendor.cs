namespace ShopingCart.Models
{
    public class Vendor
    {
       
            public int Id { get; set; }
            public string VendorName { get; set; }
            public string VendorAddress { get; set; }
            public string VendorEmail { get; set; }
            public bool IsActive { get; set; }
            public DateTime EntryDate { get; set; }
            public DateTime ModifyDate { get; set; }
        
    }
}
