﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using RP_task.AppCode.Interface;
using RP_task.AppCode.MiddleLayer;
using ShopingCart.Models;
using System.Drawing; 
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using ShopingCart.Helper;using ShopingCart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RP_task.AppCode.BusinessLayer;
using System.Data.SqlClient;
using System.Data;

namespace ShopingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppController : ControllerBase
    {
        private readonly IHR _hr;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AppController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _hr = new MHR(configuration);
            _hostingEnvironment = hostingEnvironment;
        }
        #region CATEGORY 


        //http://localhost:5152/api/App/CategoryList
        [HttpGet]
        public IActionResult CategoryList()
        {
            try
            {
                IEnumerable<Category> categoriesList = _hr.GetCategoryList();
                return Ok(categoriesList);
            }
            catch (Exception ex)
            {
                // Log the exception (Console.WriteLine can be replaced with proper logging)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        //http://localhost:5152/api/App/GetCategoryById?CategoryID=1042
        //http://localhost:5152/api/App/GetCategoryById?CategoryID=1052
        [HttpGet]
        public IActionResult GetCategoryById(int CategoryID)
        {
            try
            {
                var category = _hr.GetCategoryById(CategoryID);
                if (category == null)
                {
                    return NotFound($"Category with ID {CategoryID} not found.");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult AddOrUpdateCategory([FromBody] Category category)
        {
            try
            {
                var categoryres = _hr.AddOrUpdateCategory(category);
                return Ok(categoryres);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete]
        //http://localhost:5152/api/App/DeleteCategory?CategoryID=1042
        public IActionResult DeleteCategory(int CategoryID)
        {
            try
            {
                var category = _hr.DeleteCategory(CategoryID);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        ////http://localhost:5152/api/App/AddOrUpdateCategory
      
        #endregion
        #region PRODUCT
        [HttpGet]
        //  //http://localhost:5152/api/App/GetProductList
        public IActionResult GetProductList()
        {
            try
            {
                IEnumerable<Product> product = _hr.GetProductList();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpDelete]
        //http://localhost:5152/api/App/DeleteProduct?ProductId=1042
        public IActionResult DeleteProduct(int ProductId)
        {
            try
            {
                var category = _hr.DeleteProduct(ProductId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        public IActionResult GetProductListById(int ProductId)
        {
            try
            {
                IEnumerable<Product> product = _hr.GetProductListByID(ProductId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddOrUpdateProducts([FromForm] Product product, IFormFile? file)
        {
            try
            {
                // Check if a file was provided
                if (file != null)
                {
                    var response = ImageValidation.IsImageValid(file);
                    if (response.Status == -1)
                    {
                        return Ok(response);
                    }
                    if (response.Status == 1)
                    {
                        // Save the file to the server
                        string filename = Path.GetFileName(file.FileName);
                        string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", filename);
                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        // Update product with the filename
                        product.ProductImage = filename;
                    }
                }

                // Add or update product in the database
                try
                {
                    var updatedProduct = _hr.AddOrUpdateProduct(product);
                    return Ok(updatedProduct);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error during product update: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        #endregion
        #region
        [HttpPost]
        public IActionResult AddUser(User users)
        {
            try
            {
               var response= PasswordValidation.IsvalidPassword(users);
                if(response.Status==-1)
                {
                    return Ok(response);
                }
                 
                var userData = _hr.AddUser(users);
       
                

                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
           
        }
        [HttpGet]
        public IActionResult GetUserList()
        {
            try
            {
                IEnumerable<User> users = _hr.GetUserList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception (Console.WriteLine can be replaced with proper logging)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        public IActionResult Updatestatus(int userId)
        { 
            var res = _hr.updateUserStatus(userId);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult  UserLogin(Login login)
        {
            try { 

          
            var userData = _hr.UserLogin(login);
            return Ok(userData);
        }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
    }
            return Ok();
        }
        [HttpGet]
        public IActionResult productListForBind(int categoryid)
        {
            try
            {
                List<Product> products = _hr.GetProductForDropdown(categoryid);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        [HttpGet]
        public IActionResult VeryfiEmail(string email)
        {     
            try
            {
                var Response = _hr.EmailVeryfy(email);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                // Log the exception (Console.WriteLine can be replaced with proper logging)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult changePassword(User user)
        {
            try
            {
                var response = PasswordValidation.IsvalidPassword(user);
                if (response.Status == -1)
                {
                    return Ok(response);
                }
                var data = _hr.ChangePassword(user);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        public IActionResult myProfile(string email)
        {
            try
            {
                List<User> userlist= _hr.MyProfile(email);
                return Ok(userlist);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        [HttpPost]
        public IActionResult UpdateProfile(User users)
        {
            try
            {
                
                var userData = _hr.UpdateProfile(users);    

                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }

        }
        [HttpPost]
        public IActionResult AddCartItem([FromBody] Cartlist cart)
        {
            try
            {
                var response = _hr.AddtblCart(cart);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult FinalOrder()
        {
            try
            {
                var response = _hr.FinalOrder
                    ();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult OrderMastersListForOrderReport(OrderMaster order)
        {
            try
            {
                IEnumerable<OrderMaster> users = _hr.OrderMastersListForOrderReport(order);
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception (Console.WriteLine can be replaced with proper logging)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet]
        public IActionResult getItemReport(int OrderId)
        {
            try
            {
                List<orderItem> itemreport = _hr.ItemReport(OrderId);
                return Ok(itemreport);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        [HttpGet]
        public IActionResult getPaymentReport()
        {
            try
            {
                List<PaymentReport> paymentreport = _hr.paymentReport();
                return Ok(paymentreport);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        [HttpPost]
        public IActionResult UploadCategoery( [FromForm]IFormFile? file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Call MHR's UploadExcelData method to process the uploaded file
                object result = _hr.UploadCategories(file);
                // Return Ok with result (you can customize this response based on your needs)
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Return 500 status code and error message on exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult UploadProducts([FromForm] IFormFile? file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Call MHR's UploadExcelData method to process the uploaded file
                object result = _hr.UploadProducts(file);
                // Return Ok with result (you can customize this response based on your needs)
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Return 500 status code and error message on exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        #endregion
        #region
        [HttpPost]
        public IActionResult AddVendor(Vendor vendor)
        {
           Response response =_hr.AddVendor(vendor);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult vendorsList()
        {
            try
            {
                List<Vendor> vendors = _hr.vendorsList();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        [HttpDelete]
        public IActionResult Deletevendors(int VendorID)
        {
            try
            {
                Response response= _hr.DeleteVendor(VendorID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        [HttpGet]
        public IActionResult vendorsListById(int VendorID)
        {
            try
            {
                var vendors = _hr.GetVendorListByID(VendorID);
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        #endregion
    }

}
