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

}
