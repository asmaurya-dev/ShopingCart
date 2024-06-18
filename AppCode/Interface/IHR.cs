using Microsoft.AspNetCore.Mvc;
using ShopingCart.Models;
using System.Data;


namespace RP_task.AppCode.Interface
{
    public interface IHR
    {
        public IEnumerable<Category> GetCategoryList();
        public IEnumerable<Product> GetProductList();
        public IEnumerable<Category> GetCategoryById(int CategoryId);
        public Response AddOrUpdateCategory(Category category);
        public object DeleteCategory(int CategoryId); 
        public object updateUserStatus(int userId);
        public IEnumerable<Category> CategoryListForDropdown();
        public object AddOrUpdateProduct(Product product);
        public object DeleteProduct(int ProductId);
        public IEnumerable<Product> GetProductListByID(int ProductId);
        public Response AddUser(User user);
        public IEnumerable<User> GetUserList();
        public Response UserLogin(Login login);
        public Response EmailVeryfy(string email);
        public List<Product> GetProductForDropdown(int categoryid);
        public object ChangePassword(User user);
    }

}
 