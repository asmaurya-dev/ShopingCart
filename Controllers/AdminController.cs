using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using RP_task.AppCode.Interface;
using RP_task.AppCode.MiddleLayer;
using ShopingCart.Models;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Newtonsoft.Json;
using ShopingCart.AppCode.BusinessLayer;
using System.Text;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ShopingCart.Controllers
{
    public class AdminController : Controller
    {
        IHR _hr;
        public static string apiBaseUrl = "http://localhost:5152/";
        public static string apiBaseUrl1 = "https://localhost:7099/";
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ExcelService _excelService;
        public AdminController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ExcelService excelService)

        {
            _hr = new MHR(configuration);
            this.hostingEnvironment = hostingEnvironment;
            _excelService = excelService;
        }


        public IActionResult PrtCategoryList()
        {
            // Example usage
            string apiUrl = apiBaseUrl + "api/App/CategoryList";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(response);

                return PartialView(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }




        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Product()
        {

            return View();
        }



        [HttpPost]
        public ActionResult AddOrUpdateCategory(Category masterCategory)
        {
            string apiUrl = apiBaseUrl + "api/App/AddOrUpdateCategory";

            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl, masterCategory);
                Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);
                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }

        public ActionResult GetCategoryById(int categoryId)
        {
            string apiUrl = apiBaseUrl + $"api/App/GetCategoryById?CategoryID={categoryId}";

            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(response);


                return Json(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }



        public ActionResult DeleteCategory(int categoryId)
        {
            string apiUrl = apiBaseUrl + $"api/App/DeleteCategory?CategoryID={categoryId}";

            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Delete, apiUrl);
                
                return Json(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }


        public IActionResult CategoryListForDropdown()
        {
            try
            {
                IEnumerable<Category> category = _hr.CategoryListForDropdown();
                return Json(category);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public IActionResult GetProductList()
        {
            string apiUrl = apiBaseUrl + "api/App/GetProductList";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Product> product = JsonConvert.DeserializeObject<List<Product>>(response);

                return Json(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }
        public ActionResult DeleteProduct(int ProductId)
        {
            string apiUrl = apiBaseUrl + $"api/App/DeleteProduct?ProductId={ProductId}";

            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Delete, apiUrl);
                var jsonResponse = JsonConvert.DeserializeObject(response);
                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occrured: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }
       
        [HttpPost]
       
        public IActionResult AddOrUpdateProduct([FromForm] Product product, IFormFile? file)
        {
            string apiUrl = apiBaseUrl + "api/App/AddOrUpdateProducts";
            try
            {
                var formData = new MultipartFormDataContent();

                AddIfNotNull(formData, "id", product.Id);
                AddIfNotNull(formData, "productName", product.ProductName);
                AddIfNotNull(formData, "productPrice", product.ProductPrice);
                AddIfNotNull(formData, "productDescription", product.productDescription);
                AddIfNotNull(formData, "categoryId", product.CategoryId);
                AddIfNotNull(formData, "categoryName", product.CategoryName);
                AddIfNotNull(formData, "isActive", product.IsActive);

                if (file != null)
                {
                    formData.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
                }

                var jsonResponse = JsonConvert.DeserializeObject<Response>(ApiService.ExecuteHttpRequestss(HttpMethod.Post,apiUrl, formData: formData));

                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        private void AddIfNotNull(MultipartFormDataContent formData, string key, object value)
        {
            if (value != null)
            {
                formData.Add(new StringContent(value.ToString()), key);
            }
        }

        public IActionResult GetProductListById(int ProductId)
        {
            string apiUrl = apiBaseUrl + $"api/App/GetProductListById?ProductId={ProductId}";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Product> product = JsonConvert.DeserializeObject<List<Product>>(response);

                return Json(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }
        public IActionResult PrtProductList()
        {
            // Example usage
            string apiUrl = apiBaseUrl + "api/App/GetProductList";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(response);

                return PartialView(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }

       
      public IActionResult PrtUserList()
      {
            string apiUrl = apiBaseUrl + "api/App/GetUserList";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<User> Users = JsonConvert.DeserializeObject<List<User>>(response);

                return PartialView(Users);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }
           
      
      public IActionResult GetUserList()
        {
            return View("GetUserList", "AdminLayout");
        }
        public IActionResult updateUserStatus(int userID)
        {
            string apiUrl = apiBaseUrl + $"api/App/Updatestatus?userId={userID}";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);

                return Json(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }

        public IActionResult AdminDashbord()
        {
            return View();
        }
        public IActionResult AdminProfile()

        {
            string username = HttpContext.Session.GetString("Username");
            string apiUrl = apiBaseUrl + $"api/App/myProfile?email={username}";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<User> Users = JsonConvert.DeserializeObject<List<User>>(response);

                return View(Users);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });


            }

        }
        public IActionResult updateProfile(User users)
        {
            string apiUrl = apiBaseUrl + "api/App/UpdateProfile";
            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl, users);

                Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);

                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult OrderReport()
        {
            return View();
        }

        public IActionResult PrtOrderReport(OrderMaster orderMaster)
        {
            string apiUrl = apiBaseUrl + "api/App/OrderMastersListForOrderReport";
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl, orderMaster);
                List<OrderMaster> Users = JsonConvert.DeserializeObject<List<OrderMaster>>(response);

                return PartialView(Users);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
            return View();
        }
        public IActionResult OrderItemReport(int OrderId)
        {
            string apiUrl = apiBaseUrl + "api/App/getItemReport?OrderId="+ OrderId;
            try
            {
                // ExecuteHttpRequest is now synchronous
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl, OrderId);
                List<orderItem> Users = JsonConvert.DeserializeObject<List<orderItem>>(response);

                return PartialView(Users);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
            return View();
        }
        public IActionResult paymentReport()
        {
            return View();
        }
        public IActionResult prtPaymentReport()
        {
            try
            {
                string apiUrl = apiBaseUrl + "api/App/getPaymentReport";
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<PaymentReport> paymentlist = JsonConvert.DeserializeObject<List<PaymentReport>>(response);

                return PartialView(paymentlist);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while fetching product categories.");
            }
            

        }
        public IActionResult Upload(IFormFile file)
        {
            string apiUrl = apiBaseUrl + "api/App/UploadCategoery";
            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    formData.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
                }

                var jsonResponse = ApiService.ExecuteHttpRequestss(HttpMethod.Post, apiUrl, formData: formData);

                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        public IActionResult ProductUpload(IFormFile file)
        {
            string apiUrl = apiBaseUrl + "api/App/UploadProducts";
            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    formData.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
                }

                var jsonResponse = ApiService.ExecuteHttpRequestss(HttpMethod.Post, apiUrl, formData: formData);

                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            } // Redirect to appropriate action after successful upload
        }
        public IActionResult Wendors()
        {
            return View();
        }
        public IActionResult AddVendor( Vendor vendor)
        {
            try
            {
                string apiUrl = apiBaseUrl + "api/App/AddVendor";

                try
                {
                    string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl, vendor);
                    Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);
                    return Json(jsonResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return Json(new { error = "An error occurred while processing your request." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while fetching product categories.");
            }
        }

      public IActionResult  prtVendor()
        {
            try
            {
                string apiUrl = apiBaseUrl + "api/App/vendorsList";
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Vendor> paymentlist = JsonConvert.DeserializeObject<List<Vendor>>(response);

                return PartialView(paymentlist);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while fetching product categories.");
            }

        }
        public IActionResult DeleteVendor(int VendorID)
        {
            try
            {
                string apiUrl = apiBaseUrl + "api/App/Deletevendors?VendorID="+ VendorID;

                string response = ApiService.ExecuteHttpRequest(HttpMethod.Delete, apiUrl, VendorID);
                Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);
                return Json(jsonResponse);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while fetching product categories.");
            }

        }

        public IActionResult GetVendorById( int VendorID)
        {   
            try
            {

                string apiUrl = apiBaseUrl + "api/App/vendorsListById?VendorID=" + VendorID;
             string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Vendor> vendors = JsonConvert.DeserializeObject<List<Vendor>>(response);


               
                return Json(vendors);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while fetching product categories.");
            }

        }
    }
}


