using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
            object data = _blhr.ExecuteScalarwithparamete("proc_DeleteCategoryById", new SqlParameter[]
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
                product.productDescription = dr["_ProductDesc"].ToString();
                product.ProductPrice = Convert.ToInt32(dr["_ProductPrice"]);
                product.CategoryId = Convert.ToInt32(dr["_CategoryId"]);
                product.ProductName = dr["_ProductName"].ToString();
                product.ProductImage = dr["_ProductImage"].ToString();
      
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
          
            
            Response response = new Response();
            object data = _blhr.Execute("proc_AddOrUpdateProduct", new SqlParameter[]
           {

                  new SqlParameter("@ProductName", product.ProductName),
        new SqlParameter("@CategoryId", product.CategoryId),
        new SqlParameter("@IsActive", product.IsActive),
        new SqlParameter("@ProductPrice", product.ProductPrice),
        new SqlParameter("@ProductDesc", product.productDescription),
        new SqlParameter("@ProductImage", product.ProductImage),
        new SqlParameter("@productId", product.Id)
           });
            if (data is Response)
            {
                response = (Response)data;
            }
            return response;


            
        }

        public object DeleteProduct(int ProductId)
        {
            object data = _blhr.ExecuteScalar("Proc_DeleteProduct", new SqlParameter[]
             {
                new SqlParameter("@ProductId",ProductId),

             });
            return data;
        }
        public IEnumerable<Product> GetProductListByID(int ProductId)
        {
            DataTable dataTable = _blhr.ExecuteSelectWithParameters("proc_SelectListByID", new SqlParameter[]
            {
                new SqlParameter("@product",ProductId)
            });
            List<Product> products = new List<Product>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dr["_Id"]);
                product.productDescription = dr["_ProductDesc"].ToString();
                product.ProductPrice = Convert.ToInt32(dr["_ProductPrice"]);
                product.CategoryId = Convert.ToInt32(dr["_CategoryId"]);
                product.ProductName = dr["_ProductName"].ToString();
                product.ProductImage = dr["_ProductImage"].ToString();
                product.IsActive = Convert.ToBoolean(dr["_IsActive"]);
                products.Add(product);
                     }
            return products;

        }
        public Response AddUser(User users)
        {
            Response response = new Response();
            object data = _blhr.Execute("[proc_AddOrUpdateUser]", new SqlParameter[]
           {
                
        new SqlParameter("@Name",SqlDbType.VarChar,50){ Value=users.Name},
        new SqlParameter("@Email",SqlDbType.VarChar,100){ Value=users.Email},
        new SqlParameter("@Phone",SqlDbType.BigInt){ Value=users.Phone},
        new SqlParameter("@IsActive",SqlDbType.Bit){ Value=users.IsActive},
        new SqlParameter("@Address",SqlDbType.VarChar,100){ Value=users.Address},
        new SqlParameter("@Password",SqlDbType.VarChar,20){ Value=users.Password}


           });
            if (data is Response)
            {
                response = (Response)data;
            }
            return response;
        }
        public IEnumerable<User> GetUserList()
        {
            DataTable dataTable = _blhr.ExecuteSelect("Proc_GetUserList");
            List<User> users = new List<User>();
            foreach (DataRow dr in dataTable.Rows)
            {
                User user = new User();

                user.Address = dr["_Address"].ToString();
                user.Password = dr["_Password"].ToString();
                user.Email = dr["_Email"].ToString();
                user.Id = Convert.ToInt32(dr["_Id"]);
              
                user.Name = dr["_Name"].ToString();
                user.IsActive = Convert.ToBoolean(dr["_IsActive"]);
                user.EntryDate = Convert.ToDateTime(dr["_EntryDate"]);
                user.Phone = Convert.ToInt64(dr["_Phone"]);
                users.Add(user);

            }
            return users;

        }
        public object updateUserStatus(int UserId)
        {
            object updateemployeeStatus = _blhr.ExecuteScalar("proc_ManipulateUser", new SqlParameter[] {
                new SqlParameter("@Id",UserId)
           });
            return updateemployeeStatus;
        }
        public Response UserLogin(Login login)
        {
            Response response = new Response();
            object data = _blhr.Executee("proc_UserLogin", new SqlParameter[]
           {

        new SqlParameter("@UserId",SqlDbType.VarChar,50){ Value=login.username},
        new SqlParameter("@Password",SqlDbType.VarChar,100){ Value=login.password},
      
           });
            if (data is Response)
            {
                response = (Response)data;
            }
            return response;
        }
        public List<Product> GetProductForDropdown(int categoryid)
        {
            DataTable dt = _blhr.ExecuteSelectWithParameters("proc_GetProductName", new SqlParameter[]
            {
        new SqlParameter("@CategoryId",categoryid)
            });
            List<Product> products = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(row["_Id"]);
                product.ProductName = row["_ProductName"].ToString();
                products.Add(product);
            }
            return products;
        }
        public Response EmailVeryfy(string email)
        {
            Response response = new Response();
            object data = _blhr.Execute("Proc_EmailVeryfy", new SqlParameter[]
            {
                new SqlParameter("@Email",SqlDbType.VarChar,50){ Value=email},


           });
            if (data is Response)
            {
                response = (Response)data;
            }
            return response;

        }
        public object  ChangePassword(User user)
        {

            object respons = _blhr.ExecuteScalarwithparamete("proc_ChangePassword", new SqlParameter[]
            {
         new SqlParameter("@Email",SqlDbType.VarChar,100){Value=user.Email},
         new SqlParameter("@Password",SqlDbType.VarChar,50){Value=user.Password}
            });

            return respons;
        }
    }       

}