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

namespace ShopingCart.Controllers
{
    public class AdminController : Controller
    {
        IHR _hr;
        public static string apiBaseUrl = "http://localhost:5152/";
        private readonly IHostingEnvironment hostingEnvironment;
        public AdminController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)

        {
            _hr = new MHR(configuration);
            this.hostingEnvironment = hostingEnvironment;
        }
        //public IActionResult PrtCategoryList()
        //{
        //    try
        //    {
        //        IEnumerable<Category> masterCategories = _hr.GetCategoryList();
        //        return PartialView(masterCategories);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("An error occurred while fetching product categories.");
        //    }
        //}
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


        //public IActionResult AddOrUpdateCategory(Category category)
        //{
        //    try
        //    {
        //        var categoryres = _hr.AddOrUpdateCategory(category);
        //        return Json(categoryres);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        // }
        [HttpPost]
        public ActionResult AddOrUpdateCategory(Category masterCategory)
        {
            string apiUrl = apiBaseUrl + "api/App/AddOrUpdateCategory";

            try
            {
                // Assuming masterCategory is the data object you want to send
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
        //public IActionResult GetCategoryById(int CategoryID)
        //{
        //    try
        //    {
        //        var category = _hr.GetCategoryById(CategoryID);
        //        return Json(category);
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}
        public ActionResult GetCategoryById(int categoryId)
        {
            string apiUrl = apiBaseUrl + $"api/App/GetCategoryById?CategoryID={categoryId}";

            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(response);

                // If you expect only one category, you can select the first item from the list
               

                // Return the first category or handle the case when there are no categories
                return Json(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return Json(new { error = "An error occurred while processing your request." });
            }
        }


        //public IActionResult DeleteCategory(int CategoryID)
        //{
        //    try
        //    {
        //        var category = _hr.DeleteCategory(CategoryID);
        //        return Json(category);
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}
      
        public ActionResult DeleteCategory(int categoryId)
        {
            string apiUrl = apiBaseUrl + $"api/App/DeleteCategory?CategoryID={categoryId}";

            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Delete, apiUrl);
                var jsonResponse = JsonConvert.DeserializeObject<Response>(response);
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
            try
            {
                IEnumerable<Product> product = _hr.GetProductList();
                return Json(product);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Product(Product product, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string filename = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filepath = Path.Combine(hostingEnvironment.WebRootPath, "Images", filename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                   file.CopyTo(stream);
                }
                product.ProductImage=filename;
            }
            var Product = _hr.AddOrUpdateProduct(product);
            return View(Product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
//using Microsoft.AspNetCore.Mvc;
//using System.IO;
//using RP_task.AppCode.Interface;
//using RP_task.AppCode.MiddleLayer;
//using ShopingCart.Models;
//using System.Diagnostics;
//using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
//using Newtonsoft.Json;
//using System.Net;
//using System.Text;

//namespace ShopingCart.Controllers
//{
//    public class AdminController : Controller
//    {

//        IHR _hr;
//        public static string apiBaseUrl = "http://localhost:5152/";
//        private readonly IHostingEnvironment hostingEnvironment;
//        public AdminController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)

//        {
//            _hr = new MHR(configuration);
//            this.hostingEnvironment = hostingEnvironment;
//        }

//        public IActionResult PrtCategoryList()
//        {
//            try
//            {
//                string apiUrl = apiBaseUrl + "api/App/CategoryList";

//                // Create an HttpClient instance to make the API call
//                using (HttpClient client = new HttpClient())
//                {
//                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
//                    if (response.IsSuccessStatusCode)
//                    {
//                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
//                        List<Category> masterCategories = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);
//                        return PartialView(masterCategories);
//                    }
//                    else
//                    {
//                        return BadRequest("Failed to fetch product categories. Status code: " + response.StatusCode);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                return BadRequest("An error occurred while fetching product categories: " + ex.Message);
//            }
//        }




//        public IActionResult Category()
//        {
//            return View();
//        }
//        public IActionResult Product()
//        {

//            return View();
//        }




//        public IActionResult AddOrUpdateCategory(Category category)
//        {
//            try
//            {
//                string apiUrl = $"{apiBaseUrl}api/App/AddOrUpdateCategory";

//                using (HttpClient client = new HttpClient())
//                {
//                    // Convert Category object to JSON string
//                    string jsonContent = JsonConvert.SerializeObject(category);
//                    // Create StringContent with JSON
//                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

//                    // Send POST request to API
//                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

//                    // Check if request is successful
//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Read response content as JSON
//                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
//                        // Deserialize JSON to get response data
//                        var categoryres = JsonConvert.DeserializeObject<Response>(jsonResponse);
//                        // Return JSON response
//                        return Json(categoryres);
//                    }
//                    else
//                    {
//                        // Return error message
//                        return Json($"Failed to add or update category. Status code: {response.StatusCode}");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle exceptions
//                return Json(ex.Message);
//            }
//        }
//        public IActionResult GetCategoryById(int CategoryID)
//        {
//            try
//            {
//                string apiUrl = $"{apiBaseUrl}api/App/GetCategoryById?CategoryID={CategoryID}";

//                using (HttpClient client = new HttpClient())
//                {
//                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
//                    if (response.IsSuccessStatusCode)
//                    {
//                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
//                        List<Category> masterCategories = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);// Corrected from SerializeObject to DeserializeObject
//                        return Json(masterCategories);
//                    }
//                    else
//                    {
//                        return BadRequest($"Failed to fetch product category with ID {CategoryID}. Status code: {response.StatusCode}");
//                    }
//                }
//            }
//            catch (HttpRequestException ex)
//            {
//                // Handle HTTP request errors
//                return BadRequest($"Failed to fetch category with ID {CategoryID}. Error: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                // Handle other exceptions
//                return BadRequest($"An error occurred: {ex.Message}");
//            }
//        }


//        public IActionResult DeleteCategory(int CategoryID)
//        {
//            try
//            {
//                var category = _hr.DeleteCategory(CategoryID);
//                return Json(category);
//            }
//            catch (Exception ex)
//            {
//                return Json(ex.Message);
//            }
//        }


//        public IActionResult CategoryListForDropdown()
//        {
//            try
//            {
//                IEnumerable<Category> category = _hr.CategoryListForDropdown();
//                return Json(category);
//            }
//            catch (Exception ex)
//            {
//                return Json(ex.Message);
//            }
//        }
//        public IActionResult GetProductList()
//        {
//            try
//            {
//                IEnumerable<Product> product = _hr.GetProductList();
//                return Json(product);
//            }
//            catch (Exception ex)
//            {
//                return Json(ex.Message);
//            }
//        }
//        [HttpPost]
//        public IActionResult Product(Product product, IFormFile file)
//        {
//            if (file != null && file.Length > 0)
//            {
//                string filename = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
//                string filepath = Path.Combine(hostingEnvironment.WebRootPath, "Images", filename);
//                using (var stream = new FileStream(filepath, FileMode.Create))
//                {
//                    file.CopyTo(stream);
//                }
//                product.ProductImage = filename;
//            }
//            var Product = _hr.AddOrUpdateProduct(product);
//            return View(Product);
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}