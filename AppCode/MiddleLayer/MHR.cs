using Microsoft.Extensions.Hosting.Internal;
using RP_task.AppCode.BusinessLayer;
using RP_task.AppCode.Interface;
using ShopingCart.Models;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
namespace RP_task.AppCode.MiddleLayer
{  
    public class MHR : IHR
    {
        BLHR _blhr;
        public MHR(IConfiguration configuration)
        {
            _blhr = new BLHR(configuration);
        }
        public IEnumerable<Category> GetCategoryList()
        {
           DataTable dataTable= _blhr.ExecuteSelect("proc_selectCategory");
            List<Category> catagoaries = new List<Category>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Category catagoary = new Category();
                catagoary.CategoryId = Convert.ToInt32(dr["_ID"]);
                catagoary.CategoryName = dr["_CategoryName"].ToString();
                catagoary.IsActive = Convert.ToBoolean(dr["_IsActive"]);
                catagoary.EntryDate = Convert.ToDateTime(dr["_EntryDate"]);
                catagoaries.Add(catagoary);
            }
            return catagoaries;

        }
        public IEnumerable<Category> GetCategoryById(int CategoryId)
        {
           DataTable dataTable= _blhr.ExecuteSelectWithParameters("proc_SelectCategoryById", new SqlParameter[]
           {
               new SqlParameter("@_id",CategoryId)
           });
            List<Category> catagoaries = new List<Category>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Category catagoary = new Category();
                catagoary.CategoryId = Convert.ToInt32(dr["_ID"]);
                catagoary.CategoryName = dr["_CategoryName"].ToString();
                catagoary.IsActive = Convert.ToBoolean(dr["_IsActive"]);
               
                catagoaries.Add(catagoary);
            }
            return catagoaries;

        }

        public Response AddOrUpdateCategory(Category category)
        {
            Response response = new Response();
            object data = _blhr.Execute("proc_AddOrUpdateCategory", new SqlParameter[]
           {

                new SqlParameter("@CategoryName",category.CategoryName),
                new SqlParameter("@IsActive",category.IsActive ),
                 new SqlParameter("@CategoryId",category.CategoryId)
           });
            if(data is Response)
            {
                response = (Response)data;
            }
            return response;
            
        }
       
        public object DeleteCategory(int CategoryId)
        {
            object data = _blhr.ExecuteScalar("proc_DeleteCategoryById", new SqlParameter[]
              {
                new SqlParameter("@CategoryID",CategoryId),
               
              });
            return data;
        }
        public IEnumerable<Product> GetProductList()
        {
            DataTable dataTable = _blhr.ExecuteSelect("Proc_GetProductList");
            List<Product> products = new List<Product>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dr["_ID"]);
                product.ProductDescription = dr["_ProductDesc"].ToString();
                product.ProductPrice = Convert.ToInt32(dr["_ProductPrice"]);
                product.ProductName = dr["_ProductName"].ToString();
      
                product.CategoryName = dr["_CategoryName"].ToString();
                product.IsActive = Convert.ToBoolean(dr["_IsActive"]);
                product.EntryDate = Convert.ToDateTime(dr["_EntryDate"]);
                products.Add(product);

            }
            return products;

        }

        public IEnumerable<Category> CategoryListForDropdown()
        {
            DataTable dataTable = _blhr.ExecuteSelect("proc_CategoryListForDropdown");
            List<Category> categories = new List<Category>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Category category = new Category();
                category.CategoryId = Convert.ToInt32(dr["_ID"]);
                category.CategoryName = dr["_CategoryName"].ToString();
                categories.Add(category);
            }
            return categories;
        }
        public object AddOrUpdateProduct(Product product )
        {
            object data = _blhr.ExecuteScalarwithparamete("proc_AddOrUpdateProduct", new SqlParameter[]
            {
        new SqlParameter("@ProductName", product.ProductName),
        new SqlParameter("@CategoryId", product.CategoryId),
        new SqlParameter("@IsActive", product.IsActive),
        new SqlParameter("@ProductPrice", product.ProductPrice),
        new SqlParameter("@ProductDesc", product.ProductDescription),
        new SqlParameter("@ProductImage", product.ProductImage),
        new SqlParameter("@productId", product.Id)
            });

           

            return data;
        }



    }

}