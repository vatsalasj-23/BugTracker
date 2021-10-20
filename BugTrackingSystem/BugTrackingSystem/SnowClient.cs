using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace BugTrackingSystem
{
    public class SnowClient
    {
        public string short_description { get; set; }
        public string description { get; set; }
        public int impact { get; set; }
        public string sys_created_by { get; set; }
        public string priority { get; set; }


        public string POSTData(object json, string url, byte[] byteArray)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                using (var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = client.PostAsync(url, content).Result;
                    string returnValue = result.Content.ReadAsStringAsync().Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                        return returnValue;

                    throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                }
            }
        }


    }


}