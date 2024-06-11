using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopingCart.Models;

namespace ShopingCart
{
    public class Utility
    {

        //public static async Task<string> ExecuteHttpRequests(string apiendpoint, Product product = null, IFormFile fileData = null)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var multipartContent = new MultipartFormDataContent();

        //        // Add product data (if not null)
        //        if (product != null)
        //        {
        //            string jsonData = JsonConvert.SerializeObject(product);
        //            multipartContent.Add(new StringContent(jsonData, Encoding.UTF8, "application/json"), "product");
        //        }

        //        // Add file data (if not null)
        //        if (fileData != null && fileData.Length > 0)
        //        {
        //            var fileContent = new StreamContent(fileData.OpenReadStream());
        //            fileContent.Headers.ContentType = new MediaTypeHeaderValue(fileData.ContentType);
        //            multipartContent.Add(fileContent, "file", fileData.FileName);
        //        }

        //        var response = await httpClient.PostAsync(apiendpoint, multipartContent);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseContent = await response.Content.ReadAsStringAsync();
        //            return responseContent;
        //        }
        //        return "";
        //    }
        //}

        public static string ExecuteHttpRequests(string apiendpoint, Product product = null, IFormFile fileData = null)
        {
            using (var httpClient = new HttpClient())
            {
                // MultipartFormDataContent object initialize karen
                var multipartContent = new MultipartFormDataContent();

                // Product data ko JSON format mein serialize karen
                string jsonData = JsonConvert.SerializeObject(product);

                // StringContent object banayein jsonData ke saath
                var jsonContent = new StringContent(jsonData, Encoding.UTF8, "multipart/form-data");

                // JSON content ko MultipartFormDataContent mein add karen, ek alag field ke roop mein
                multipartContent.Add(jsonContent, "product");

                // Ab file data ko bhi add karen, agar file hai
                if (fileData != null && fileData.Length > 0)
                {
                    var fileContent = new StreamContent(fileData.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(fileData.ContentType);
                    multipartContent.Add(fileContent, "file", fileData.FileName);
                }

                // Ab HttpClient ke saath multipartContent ko POST request ke roop mein bhejein
                var response = httpClient.PostAsync(apiendpoint, multipartContent).Result;




                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    return responseContent;
                }
                return "";
            }
        }


        public static byte[] GetFileArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
