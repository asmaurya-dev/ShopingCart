namespace ShopingCart.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public DateTime EntryDate { get; set; }
     }
   
    public class Response
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        
        public Response()
        {
            Status = -1;
            Message = "some error occured";
        }
    }
}

