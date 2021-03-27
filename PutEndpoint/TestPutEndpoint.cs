using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helpers.Request;
using WebServiceAutomation.Helpers.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using Xunit;

namespace WebServiceAutomation.PutEndpoint
{
    public class TestPutEndpoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add/";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update/";
        private RestResponse restResponse;
        private RestResponse restResponseForGet;
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();

        [Fact]
        public void TestPutUsingXmlData()
        {
            int id = random.Next(1000);
            string xmlData =
                                           "<Laptop>" +
                                              "<BrandName>Alienware</BrandName>" +
                                                "<Features>" +
                                                   "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                                   "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                                   "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                                   "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                                 "</Features>" +
                                              "<Id>" + id + "</Id>" +
                                              "<LaptopName>Alienware M17</LaptopName>" +
                                           "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
            {"Accept","application/xml"}
            };

            restResponse = HttpClientHelper.PerformPostRequest(postUrl, xmlData, xmlMediaType, headers);
            Assert.Equal(200, restResponse.StatusCode);

            xmlData =
                               "<Laptop>" +
                                  "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>1 TB of SSD</Feature>" +
                                     "</Features>" +
                                  "<Id>" + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            using(HttpClient httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType);

                Task<HttpResponseMessage> httpResponseMessage = httpClient.PutAsync(putUrl, httpContent);

                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode,
                    httpResponseMessage.Result.Content.ReadAsStringAsync().Result);

                Assert.Equal(200, restResponse.StatusCode);
            }

            restResponse = HttpClientHelper.PerformGetRequest(getUrl + id, headers);
            Assert.Equal(200, restResponse.StatusCode);

            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.responseContent);
            Assert.Contains("1 TB of SSD", xmlObj.Features.Feature);
        }

        [Fact]
        public void TestPutUsingJsonData()
        {
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
                        "\"Id\": " + id + "," +
                        "\"LaptopName\": \"Alienware M17\"" +
                    "}";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
            {"Accept","application/json"}
            };

            restResponse = HttpClientHelper.PerformPostRequest(postUrl, jsonData, jsonMediaType, headers);
            Assert.Equal(200, restResponse.StatusCode);

            jsonData = "{" +
                        "\"BrandName\": \"Alienware\"," +
                        "\"Features\": {" +
                        "\"Feature\": [" +
                        "\"8th Generation Intel® Core™ i5-8300H\"," +
                        "\"Windows 10 Home 64-bit English\"," +
                        "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                        "\"1 TB of SSD\"," +
                        "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                        "]" +
                        "}," +
                        "\"Id\": " + id + "," +
                        "\"LaptopName\": \"Alienware M17\"" +
                    "}";

            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);

                Task<HttpResponseMessage> httpResponseMessage = httpClient.PutAsync(putUrl, httpContent);

                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode,
                    httpResponseMessage.Result.Content.ReadAsStringAsync().Result);

                Assert.Equal(200, restResponse.StatusCode);
            }

            restResponse = HttpClientHelper.PerformGetRequest(getUrl + id, headers);
            Assert.Equal(200, restResponse.StatusCode);

            JsonRootObject jsonObject = ResponseDataHelper.DeserializeJsonResponse<JsonRootObject>
                (restResponse.responseContent);

            Assert.Contains("1 TB of SSD", jsonObject.Features.Feature);
        }

        [Fact]
        public void TestPutWithJsonAndHelperClass()
        {
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
                        "\"Id\": " + id + "," +
                        "\"LaptopName\": \"Alienware M17\"" +
                    "}";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
            {"Accept","application/json"}
            };

            restResponse = HttpClientHelper.PerformPostRequest(postUrl, jsonData, jsonMediaType, headers);
            Assert.Equal(200, restResponse.StatusCode);

            jsonData = "{" +
                        "\"BrandName\": \"Alienware\"," +
                        "\"Features\": {" +
                        "\"Feature\": [" +
                        "\"8th Generation Intel® Core™ i5-8300H\"," +
                        "\"Windows 10 Home 64-bit English\"," +
                        "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                        "\"1 TB of SSD\"," +
                        "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                        "]" +
                        "}," +
                        "\"Id\": " + id + "," +
                        "\"LaptopName\": \"Alienware M17\"" +
                    "}";

            restResponse = HttpClientHelper.PerformPutRequest(putUrl, jsonData, jsonMediaType, headers);
            Assert.Equal(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformGetRequest(getUrl + id, headers);
            Assert.Equal(200, restResponse.StatusCode);

            JsonRootObject jsonObject = ResponseDataHelper.DeserializeJsonResponse<JsonRootObject>
                (restResponse.responseContent);

            Assert.Contains("1 TB of SSD", jsonObject.Features.Feature);
        }


        [Fact]
        public void TestPutWithXmlAndHelperClass()
        {
            int id = random.Next(1000);
            string xmlData =
                                           "<Laptop>" +
                                              "<BrandName>Alienware</BrandName>" +
                                                "<Features>" +
                                                   "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                                   "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                                   "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                                   "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                                 "</Features>" +
                                              "<Id>" + id + "</Id>" +
                                              "<LaptopName>Alienware M17</LaptopName>" +
                                           "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
            {"Accept","application/xml"}
            };

            restResponse = HttpClientHelper.PerformPostRequest(postUrl, xmlData, xmlMediaType, headers);
            Assert.Equal(200, restResponse.StatusCode);

            xmlData =
                               "<Laptop>" +
                                  "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>1 TB of SSD</Feature>" +
                                     "</Features>" +
                                  "<Id>" + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            restResponse = HttpClientHelper.PerformPutRequest(putUrl, xmlData, xmlMediaType, headers);
            Assert.Equal(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformGetRequest(getUrl + id, headers);
            Assert.Equal(200, restResponse.StatusCode);
  
            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.responseContent);
            Assert.Contains("1 TB of SSD", xmlObj.Features.Feature);
        }
    }
}
