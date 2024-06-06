using Microsoft.AspNetCore.Mvc;
using System.IO;
using RP_task.AppCode.Interface;
using RP_task.AppCode.MiddleLayer;
using ShopingCart.Models;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ShopingCart.Controllers
{
    public class AdminController : Controller
    {
        IHR _hr;
        private readonly IHostingEnvironment hostingEnvironment;
        public AdminController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)

        {
            _hr = new MHR(configuration);
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult PrtCategoryList()
        {
            try
            {
                IEnumerable<Category> masterCategories = _hr.GetCategoryList();
                return PartialView(masterCategories);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while fetching product categories.");
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
       
       
        public IActionResult AddOrUpdateCategory(Category category)
        {
            try
            {
                var categoryres = _hr.AddOrUpdateCategory(category);
                return Json(categoryres);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
         }
        public IActionResult GetCategoryById(int CategoryID)
        {
            try
            {
                var category = _hr.GetCategoryById(CategoryID);
                return Json(category);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public IActionResult DeleteCategory(int CategoryID)
        {
            try
            {
                var category = _hr.DeleteCategory(CategoryID);
                return Json(category);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
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
