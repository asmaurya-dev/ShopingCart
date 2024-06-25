using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopingCart.AppCode.BusinessLayer;
using ShopingCart.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using RP_task.AppCode.Interface;
using RP_task.AppCode.MiddleLayer;

using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

using Microsoft.AspNetCore.Http; 
using System.Text;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;


namespace ShopingCart.Controllers
{
    public class UserController : Controller
    {
        IHR _hr;
        public static string apiBaseUrl = "http://localhost:5152/";
        public static string apiBaseUrl1 = "https://localhost:7099/";
        private readonly IHostingEnvironment hostingEnvironment;

        public UserController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)

        {
            _hr = new MHR(configuration);
            this.hostingEnvironment = hostingEnvironment;
        }


        public IActionResult User()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addUser(User users)
        {
            string apiUrl = "http://localhost:5152/api/App/AddUser";
            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl, users);
                Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)

        {
            string apiUrl = apiBaseUrl + "api/App/userLogin";
            try
            {
                HttpContext.Session.SetString("Username", login.username);

                string username = HttpContext.Session.GetString("Username");
                if (username == null)
                {
                    return RedirectToAction("Login", "User");
                }
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl, login);

                // Deserialize JSON response
                Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);



                // Store responseData in ViewBag to pass it to the view
                HttpContext.Session.SetString("Name", jsonResponse.Name);


                // Return JSON response
                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred");
            }
        }

        public IActionResult UserDashbord()
        {
            string username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;

            return View();
        }


        public IActionResult CategoryListForDropdown()
        {
            try
            {
                IEnumerable<Category> category = _hr.GetCategoryListUseInProduct();
                return Json(category);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);


            }
        }
        public IActionResult productListForBinds(int categoryid)
        {
            try
            {
                string apiUrl = apiBaseUrl + "api/App/productListForBind?categoryid=" + categoryid;
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl);
                List<Product> productlist = JsonConvert.DeserializeObject<List<Product>>(response);
                return Json(productlist);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult VeryfiEmail(string email)
        {

            string apiUrl = apiBaseUrl + $"api/App/VeryfiEmail?email={email}";
            try
            {

                string response = ApiService.ExecuteHttpRequest(HttpMethod.Get, apiUrl, email);
                Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);
                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
        [HttpPost]
        public IActionResult changePasswords(User user)
        {
            string apiUrl = apiBaseUrl + "api/App/changePassword";
            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl, user);


                return Json(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }

        }
        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                HttpContext.Session.Remove("Username");
               
            }
            return RedirectToAction("Login");
        }
        public IActionResult myProfile()
        
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
        [HttpPost]
        public IActionResult AddCart( [FromBody]Cartlist cart)
        {
            string apiUrl = apiBaseUrl + "api/App/AddCartItem";
            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl, cart);

              

                return Json(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult FinalOrder()
        {
            string apiUrl = apiBaseUrl + "api/App/FinalOrder";
            try
            {
                string response = ApiService.ExecuteHttpRequest(HttpMethod.Post, apiUrl);

                Response jsonResponse = JsonConvert.DeserializeObject<Response>(response);

                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
      
    }
}