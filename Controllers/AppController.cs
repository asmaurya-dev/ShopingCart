using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RP_task.AppCode.Interface;
using RP_task.AppCode.MiddleLayer;
using ShopingCart.Models;
using System;
using System.Collections.Generic;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
        [HttpPost]
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
        //[HttpPost("AddOrUpdateCategory/{CategoryName},{IsActive},{CategoryId}")]
        //public IActionResult AddOrUpdateCategory(string CategoryName, bool IsActive, int CategoryId)
        //{
        //    try
        //    {
        //        var category = _hr.AddOrUpdateCategory(CategoryId, CategoryName, IsActive);
        //        return Ok(category);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"An error occurred: {ex.Message}");
        //    }
        //}
        //http://localhost:5152/api/App/AddOrUpdateCategory
        // {
//    "CategoryId": 0,
//    "CategoryName": "Electron",
//    "IsActive": true
//}
//Body:Json
}

}
