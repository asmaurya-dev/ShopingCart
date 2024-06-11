using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ShopingCart.AppCode.BusinessLayer
{
    public static class ApiService
    {

        public static string ExecuteHttpRequest(HttpMethod method, string apiUrl, object data = null)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(method, apiUrl);

                    if (data != null)
                    {
                        string jsonData = JsonConvert.SerializeObject(data);
                        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = client.Send(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        throw new Exception($"Failed to perform HTTP request. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
       
       
            public static string ExecuteHttpRequestss(HttpMethod method, string apiUrl, object data = null, MultipartFormDataContent formData = null)
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (HttpClient client = new HttpClient(handler))
                    {
                        try
                        {
                            HttpRequestMessage request = new HttpRequestMessage(method, apiUrl);

                            if (formData != null)
                            {
                                request.Content = formData;
                            }
                            else if (data != null)
                            {
                                string jsonData = JsonConvert.SerializeObject(data);
                                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                            }

                            HttpResponseMessage response = client.SendAsync(request).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                return response.Content.ReadAsStringAsync().Result;
                            }
                            else
                            {
                                throw new Exception($"Failed to perform HTTP request. Status code: {response.StatusCode}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                    }
                }
            }
        }
    }




