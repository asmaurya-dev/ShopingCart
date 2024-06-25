namespace ShopingCart.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Cpassword { get; set; }
        public long? Phone { get; set; }
        public bool? IsActive { get; set; }
        public string? Address { get; set; }
        public DateTime? EntryDate { get; set; }
    }
    public class Login
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }
     public class Cart
    {
        public int ?Id { get; set; }
        public decimal? ProductAmount { get; set; }
        public int? ProductId { get; set; }
        public int ?UserId { get; set; }
        public int? Quantity { get; set; }
        public string? ProductName { get; set; }
        public string ? Email { get; set; }
     
    }
    public class Cartlist
    {
        public List<CartItem> Items { get; set; }
    }



    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductAmount { get; set; }
        public string Email { get; set; }

    }


}
