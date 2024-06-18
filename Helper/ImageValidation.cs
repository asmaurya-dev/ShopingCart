using ShopingCart.Models;
using Microsoft.AspNetCore.Http; // Import this namespace for IFormFile

namespace ShopingCart.Helper
{
    public static class ImageValidation
    {
        public static Response IsImageValid(IFormFile file)
        {
            var response = new Response();
            string[] allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant(); // Get file extension

            // Check if the extension is in the allowedExtensions array
            if (Array.Exists(allowedExtensions, ext => ext == extension))
            {
                response.Status = 1;
                response.Message = "File is valid";
            }
            else
            {
                response.Status = -1;
                response.Message = "Invalid File Format";
            }

            return response;
        }
    }
}
