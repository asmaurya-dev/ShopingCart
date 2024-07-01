using Microsoft.AspNetCore.Mvc;
using ShopingCart.Models;
using System.Data;


namespace RP_task.AppCode.Interface
{
    public interface IHR
    {
        #region
        public IEnumerable<Category> GetCategoryList();
        public IEnumerable<Product> GetProductList();
        public IEnumerable<Category> GetCategoryById(int CategoryId);
        public Response AddOrUpdateCategory(Category category);
        public object DeleteCategory(int CategoryId);
        #endregion
        #region
        public object updateUserStatus(int userId);
        public IEnumerable<Category> CategoryListForDropdown();
        public object AddOrUpdateProduct(Product product);
        public object DeleteProduct(int ProductId);
        public IEnumerable<Product> GetProductListByID(int ProductId);
        #endregion
        public Response AddUser(User user);
        public IEnumerable<User> GetUserList();
        public Response UserLogin(Login login);
        public Response EmailVeryfy(string email);
        public List<Product> GetProductForDropdown(int categoryid);
        public object ChangePassword(User user);
        public List<User> MyProfile(string email);
        public Response UpdateProfile( User users);
        public IEnumerable<Category> GetCategoryListUseInProduct();
        public object AddtblCart(Cartlist cart);
        #region
        public Response FinalOrder();
        public IEnumerable<OrderMaster> OrderMastersListForOrderReport(OrderMaster order);
        public List<orderItem> ItemReport(int OrderId);
        public List<PaymentReport> paymentReport();
        #endregion
        #region
        public object UploadCategories(IFormFile file);
        public object UploadProducts(IFormFile file);
        #endregion
        #region
        public Response AddVendor(Vendor vendor);
        public List<Vendor> vendorsList();
        public Response DeleteVendor(int VendorID);
        public IEnumerable<Vendor> GetVendorListByID(int VendorID);
        #endregion
    }

}
 