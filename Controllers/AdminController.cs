using Microsoft.AspNetCore.Mvc;
using System.IO;
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

namespace ShopingCart.Controllers
{
    public class AdminController : Controller
    {
        IHR _hr;
        public static string apiBaseUrl = "http://localhost:5152/";
        public static string apiBaseUrl1 = "https://localhost:7099/";
        private readonly IHostingEnvironment hostingEnvironment;
        public AdminController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)

        {
            _hr = new MHR(configuration);
            this.hostingEnvironment = hostingEnvironment;
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
                var jsonResponse = JsonConvert.DeserializeObject(response);
                return Json(jsonResponse);
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
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }
       
        [HttpPost]
        //     public IActionResult product([FromForm] Product product, IFormFile file)
        //     {
        //         string apiUrl = apiBaseUrl + "api/App/Product";

        //         try
        //         {
        //             using (var client = new HttpClient())
        //             {
        //                 var formData = new MultipartFormDataContent();

        //                 // Add product properties as string content with null checks
        //                 if (product.Id != null)
        //                     formData.Add(new StringContent(product.Id.ToString()), "id");
        //                 if (!string.IsNullOrEmpty(product.ProductName))
        //                     formData.Add(new StringContent(product.ProductName), "productName");
        //                 if (product.ProductPrice != null)
        //                     formData.Add(new StringContent(product.ProductPrice.ToString()), "productPrice");
        //                 if (!string.IsNullOrEmpty(product.productDescription))
        //                     formData.Add(new StringContent(product.productDescription), "productDescription");
        //                 if (product.CategoryId != null)
        //                     formData.Add(new StringContent(product.CategoryId.ToString()), "categoryId");
        //                 if (!string.IsNullOrEmpty(product.CategoryName))
        //                     formData.Add(new StringContent(product.CategoryName), "categoryName");
        //                 if (product.IsActive != null)
        //                     formData.Add(new StringContent(product.IsActive.ToString()), "isActive");

        //                 if (file != null)
        //                 {
        //                     var fileStreamContent = new StreamContent(file.OpenReadStream());
        //                     fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //                     {
        //                         Name = "file",
        //                         FileName = file.FileName
        //                     };
        //                     fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        //                     formData.Add(fileStreamContent);
        //                 }

        //                 // Execute HTTP request using ApiService
        //string responseContent = ApiService.ExecuteHttpRequestss(HttpMethod.Post, apiUrl, formData: formData);

        //                 // Deserialize response JSON
        //                 var jsonResponse = JsonConvert.DeserializeObject<Response>(responseContent);

        //                 return Json(jsonResponse);
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine("An error occurred: " + ex.Message);
        //             return StatusCode(500, new { error = "An error occurred while processing your request." });
        //         }
        //     }
        public IActionResult Product([FromForm] Product product, IFormFile file)
        {
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

                var jsonResponse = JsonConvert.DeserializeObject<Response>(ApiService.ExecuteHttpRequestss(HttpMethod.Post, apiBaseUrl + "api/App/Product", formData: formData));

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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
