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
        //public object UpdateCategory(int CategoryId, string CategoryName, bool IsActive);
        public IEnumerable<Category> CategoryListForDropdown();
        public object AddOrUpdateProduct(Product product);


    }

}
 