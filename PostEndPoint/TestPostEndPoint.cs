using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using Xunit;

namespace WebServiceAutomation.PostEndPoint
{
    public class TestPostEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add/";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private RestResponse restResponse;
        private RestResponse restResponseForGet;
        private string jsonMediaType = "application/json";
        private Random random = new Random();

        [Fact]
        public void TestEndPointWithJson()
        {
            // Method POST -- PostAsync
            // Body along with request - HttpContent Class
            // Header - info about data format

            int id = random.Next(1000);
            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": "+id+ "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", jsonMediaType);
                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
                Task<HttpResponseMessage> postResponse = httpClient.PostAsync(postUrl, httpContent);
                HttpStatusCode statusCode = postResponse.Result.StatusCode;
                HttpContent responseContent = postResponse.Result.Content;
                string responseData = responseContent.ReadAsStringAsync().Result;

                restResponse = new RestResponse((int)statusCode, responseData);

                Assert.Equal(200, restResponse.StatusCode); 
                Assert.NotNull(restResponse.responseContent);

                Task<HttpResponseMessage> getResponse = httpClient.GetAsync(getUrl + id);
                restResponseForGet = new RestResponse((int)getResponse.Result.StatusCode, getResponse.Result.Content.ReadAsStringAsync().Result);

                JsonRootObject jsonObject = JsonConvert.DeserializeObject<JsonRootObject>(restResponseForGet.responseContent);

                Assert.Equal(id, jsonObject.Id);
                Assert.Equal("Alienware", jsonObject.BrandName);

            }
        }
    }
}
