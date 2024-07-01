using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OfficeOpenXml;
using RP_task.AppCode.BusinessLayer;
using RP_task.AppCode.Interface;
using ShopingCart.Models;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace RP_task.AppCode.MiddleLayer
{
    public class MHR : IHR
    {
        BLHR _blhr;
        public MHR(IConfiguration configuration)
        {
            _blhr = new BLHR(configuration);
        }
        #region
        public IEnumerable<Category> GetCategoryList()
        {
            DataTable dataTable = _blhr.ExecuteSelect("proc_selectCategory");
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
            DataTable dataTable = _blhr.ExecuteSelectWithParameters("proc_SelectCategoryById", new SqlParameter[]
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
            if (data is Response)
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
        #endregion
        #region
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
        public object AddOrUpdateProduct(Product product)
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
        #endregion
        #region
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
            DataTable dt = _blhr.ExecuteSelectWithParameters("[proc_GetProductNameForDropdown]", new SqlParameter[]
            {
        new SqlParameter("@CategoryId",categoryid)
            });
            List<Product> products = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(row["_Id"]);
                product.ProductName = row["_ProductName"].ToString();
                product.ProductPrice = Convert.ToDecimal(row["_ProductPrice"]);

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
        #endregion
        #region
        public object ChangePassword(User user)
        {

            object respons = _blhr.ExecuteScalarwithparamete("proc_ChangePassword", new SqlParameter[]
            {
         new SqlParameter("@Email",SqlDbType.VarChar,100){Value=user.Email},
         new SqlParameter("@Password",SqlDbType.VarChar,50){Value=user.Password}
            });

            return respons;
        }
        #endregion
        #region
        public List<User> MyProfile(string email)
        {
            DataTable dt = _blhr.ExecuteSelectWithParameters("proc_MyProfile", new SqlParameter[]
            {
                new SqlParameter("@Emailid",email)
            });
            List<User> userlist = new List<User>();
            foreach (DataRow row in dt.Rows)
            {
                User usermodel = new User();
                usermodel.Name = row["_Name"].ToString();
                usermodel.Address = row["_Address"].ToString();
                usermodel.Email = row["_Email"].ToString();
                usermodel.Phone = Convert.ToInt64(row["_Phone"]);
                usermodel.Password = row["_Password"].ToString();
                userlist.Add(usermodel);
            }
            return userlist;
        }
        public Response UpdateProfile(User users)
        {
            Response response = new Response();
            object data = _blhr.Execute("Proc_UpdateProfile", new SqlParameter[]
           {

        new SqlParameter("@Name",SqlDbType.VarChar,50){ Value=users.Name},
        new SqlParameter("@Phone",SqlDbType.BigInt){ Value=users.Phone},
        new SqlParameter("@Address",SqlDbType.VarChar,100){ Value=users.Address},
               new SqlParameter("@Email",SqlDbType.VarChar,100){ Value=users.Email},



           });
            if (data is Response)
            {
                response = (Response)data;
            }
            return response;
        }
        public IEnumerable<Category> GetCategoryListUseInProduct()
        {
            DataTable dataTable = _blhr.ExecuteSelect("[proc_CategoryListUseInProduct]");
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
        #endregion
        #region
        public object AddtblCart(Cartlist cart)
        {
            object result = null;

            foreach (var item in cart.Items)
            {
                result = _blhr.ExecuteScalarwithparamete("Proc_AddProductInCart", new SqlParameter[]
                {
            new SqlParameter("@ProductAmount", item.ProductAmount),
            new SqlParameter("@ProductId", item.ProductId),
            new SqlParameter("@Quantity", item.Quantity),
            new SqlParameter("@Email", SqlDbType.VarChar) { Value = item.Email }
                });
            }

            return result;
        }

        public Response FinalOrder()
        {

            Response response = new Response();
            object data = _blhr.Executeee("proc_InsertCartItemsIntoOrders");


            if (data is Response)
            {
                response = (Response)data;
            }
            return response;
        }
        public IEnumerable<OrderMaster> OrderMastersListForOrderReport(OrderMaster order)
        {
            DataTable dt = _blhr.ExecuteSelectWithParameters("proc_OrderReport", new SqlParameter[]
           {
        new SqlParameter("@Id",order.UserId),
         new SqlParameter("@From_Date",order.FromDate),
         new SqlParameter("@ToDate",order.ToDate)
           });
            List<OrderMaster> orderMasters = new List<OrderMaster>();
            foreach (DataRow row in dt.Rows)
            {
                OrderMaster order1 = new OrderMaster();
                order1.UserId = Convert.ToInt32(row["_Id"]);
                order1.UserNAme = row["_Name"].ToString();
                order1.OrderId = Convert.ToInt32(row["_OrderId"]);
                order1.TotalAmount = Convert.ToInt32(row["_TotalAmount"]);
                order1.EnteryDate = Convert.ToDateTime(row["_EntryDate"]);

                orderMasters.Add(order1);
            }
            return orderMasters;
        }
        public List<orderItem> ItemReport( int OrderId)
        {
            try
            {
                DataTable dt = _blhr.ExecuteSelectWithParameters("Proc_OrderItemReport", new SqlParameter[]
           {
         new SqlParameter("@OrderId",OrderId),

           });
                List<orderItem> itemList = new List<orderItem>();
                foreach (DataRow row in dt.Rows)
                {
                    orderItem item = new orderItem();
                    item.OrderaItemId = Convert.ToInt32(row["_OrderItemId"]);
                    item.Quantity = Convert.ToInt32(row["_Quantity"]);
                    item.ProductName = row["_ProductName"].ToString();
                    item.ProductPrice = Convert.ToDecimal(row["_ProductPrice"]);
                    item.EntryDate = Convert.ToDateTime(row["_EntryDate"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            catch (Exception ex)
            {
                // Properly handle or log the exception
                throw new Exception("An error occurred while adding order data.", ex);
            }
        }

        public List<PaymentReport> paymentReport()
        {
            try
            {
                DataTable dt = _blhr.ExecuteSelect("proc_PaymentReport");
                List<PaymentReport> paymentList = new List<PaymentReport>();
                foreach (DataRow row in dt.Rows)
                {
                    PaymentReport payment = new PaymentReport();
                    payment.PaymentId = Convert.ToInt32(row["_PaymentId"]);
                    payment.OrderId = Convert.ToInt32(row["_OrderId"]);
                    payment.Amount = Convert.ToDecimal(row["_Amount"]);
                    payment.EntryDate = Convert.ToDateTime(row["_EntryDate"]);
                    paymentList.Add(payment);
                }
                return paymentList;
            }
            catch (Exception ex)
            {
                // Properly handle or log the exception
                throw new Exception("An error occurred while adding order data.", ex);
            }
        }
        #endregion
        #region
        public object UploadCategories(IFormFile file)
        {
            try
            {
                List<Category> categories = ProcessExcel(file);

                DataTable table = new DataTable();
                table.Columns.Add("_CategoryName", typeof(string));
                table.Columns.Add("_IsActive", typeof(bool));

                foreach (var category in categories)
                {
                    table.Rows.Add(category.CategoryName, category.IsActive);
                }

                SqlParameter parameter = new SqlParameter("@myTable", SqlDbType.Structured)
                {
                    TypeName = "dbo.CategoryType",
                    Value = table
                };

                object obj = _blhr.ExecuteDML("proc_UploadCategory", new SqlParameter[] { parameter });
                return obj;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception($"Error occurred: {ex.Message}");
            }
        }
        private List<Category> ProcessExcel(IFormFile file)
        {
            List<Category> categories = new List<Category>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                        return categories;

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Assuming first row is header
                    {
                        string categoryName = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                        string isActiveString = worksheet.Cells[row, 2].Value?.ToString()?.Trim();

                        bool isActive = false; // Default to false if isActiveString is not recognized

                        if (!string.IsNullOrEmpty(isActiveString))
                        {
                            isActive = isActiveString.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                                       isActiveString.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                       isActiveString.Equals("1", StringComparison.OrdinalIgnoreCase);
                        }

                        if (!string.IsNullOrEmpty(categoryName))
                        {
                            categories.Add(new Category { CategoryName = categoryName, IsActive = isActive });
                        }
                    }
                }
            }

            return categories;
        }

        public object UploadProducts(IFormFile file)
        {
            try
            {
                List<Product> products = ProcessExcel2(file);

                DataTable table = new DataTable();
                table.Columns.Add("_CategoryId", typeof(int));
                table.Columns.Add("_ProductName", typeof(string));
                table.Columns.Add("_ProductPrice", typeof(decimal));
                table.Columns.Add("_IsActive", typeof(bool));

                foreach (var product in products)
                {
                    table.Rows.Add(product.CategoryId, product.ProductName, product.ProductPrice, product.IsActive);
                }

                SqlParameter parameter = new SqlParameter("@myTableProduct", SqlDbType.Structured)
                {
                    TypeName = "dbo.ProductType",
                    Value = table
                };

                object obj = _blhr.ExecuteDML("proc_UploadProduct", new SqlParameter[] { parameter });
                return obj;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        private List<Product> ProcessExcel2(IFormFile file)
        {
            List<Product> products = new List<Product>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream); // Copy file content to memory stream

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                        return products;

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Assuming first row is header
                    {
                        string productName = worksheet.Cells[row, 2].Value?.ToString()?.Trim(); // Corrected column index

                        decimal productPrice = 0;
                        if (decimal.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out decimal price))
                        {
                            productPrice = price;
                        }

                        int categoryId = 0;
                        if (int.TryParse(worksheet.Cells[row, 1].Value?.ToString(), out int id)) // Corrected column index
                        {
                            categoryId = id;
                        }

                        bool isActive = false;
                        string isActiveString = worksheet.Cells[row, 4].Value?.ToString()?.Trim();

                        if (!string.IsNullOrEmpty(isActiveString) &&
                            (isActiveString.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                             isActiveString.Equals("1", StringComparison.OrdinalIgnoreCase)))
                        {
                            isActive = true;
                        }

                        if (!string.IsNullOrEmpty(productName) && productPrice > 0 && categoryId > 0)
                        {
                            products.Add(new Product
                            {
                                CategoryId = categoryId,
                                ProductName = productName,
                                ProductPrice = productPrice,
                                IsActive = isActive
                            });
                        }
                    }
                }
            }

            return products;
        }
        #endregion
        #region
        public Response AddVendor(Vendor vendor)
        {
            Response response = new Response();
            object data = _blhr.Execute("[Proc_AddOrUpdateVendor]", new SqlParameter[]
           {
       new SqlParameter("@VendorId",SqlDbType.BigInt){ Value=vendor.Id},
        new SqlParameter("@VendorName",SqlDbType.VarChar){ Value=vendor.VendorName},
        new SqlParameter("@VendorEmail",SqlDbType.VarChar){ Value=vendor.VendorEmail},
        new SqlParameter("@VendorAddress",SqlDbType.VarChar){ Value=vendor.VendorAddress},
               new SqlParameter("@IsActive",SqlDbType.Bit){ Value=vendor.IsActive},



           });
            if (data is Response)
            {
                response = (Response)data;
            }
            return response;
        }
        public List<Vendor> vendorsList()
        {
            DataTable dataTable = _blhr.ExecuteSelect("[Proc_GetVendorList]");
            List<Vendor> categories = new List<Vendor>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Vendor  vendor = new Vendor();
                vendor.Id = Convert.ToInt32(dr["_Id"]);
                vendor.VendorEmail = dr["_VendorEmail"].ToString();
                vendor.VendorAddress = dr["_VendorAddress"].ToString();
                vendor.VendorName = dr["_VendorName"].ToString();
                vendor.IsActive = Convert.ToBoolean(dr["_IsActive"]);
                vendor.ModifyDate= Convert.ToDateTime(dr["_ModifyDate"]);
                vendor.EntryDate= Convert.ToDateTime(dr["_EntryDate"]);
                categories.Add(vendor);
            }
            return categories;
        }
        public Response DeleteVendor(int VendorID)
        {
            Response response = new Response();
            object data = _blhr.Execute("[Proc_DeleteVendor]", new SqlParameter[]
           {
       new SqlParameter("@Id",VendorID)
       

           });
            if (data is Response)
            {
                response = (Response)data;
            }
            return response;
        }
        public IEnumerable<Vendor> GetVendorListByID(int VendorID)
        {
            DataTable dataTable = _blhr.ExecuteSelectWithParameters("Proc_GetVendorListbyId", new SqlParameter[]
            {
        new SqlParameter("@ID", VendorID)
            });

            List<Vendor> vendors = new List<Vendor>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Vendor vendor = new Vendor();
                vendor.Id = Convert.ToInt32(dr["_Id"]);
                vendor.VendorEmail = dr["_VendorEmail"].ToString();
                vendor.VendorAddress = dr["_VendorAddress"].ToString();
                vendor.VendorName = dr["_VendorName"].ToString();
                vendor.IsActive = Convert.ToBoolean(dr["_IsActive"]);
             
                vendors.Add(vendor);
            }
            return vendors;
        }
        #endregion
    }
}