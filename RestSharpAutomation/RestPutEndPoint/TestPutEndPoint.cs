using RestSharp;
using RestSharpAutomation.HelperClass.Request;
using System;
using System.Collections.Generic;
using System.Text;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using Xunit;

namespace RestSharpAutomation.RestPutEndPoint
{
    public class TestPutEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add/";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update/";
        private Random random = new Random();

        [Fact]
        public void TestPutWithJsonData()
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
                {"Content-Type", "application/json" },
                {"Accept", "application/json" }
            };


            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(postUrl, headers, jsonData, RestSharp.DataFormat.Json);

            Assert.Equal(200, (int)restResponse.StatusCode);

            jsonData = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"," +
                                    "\"New Feature\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = putUrl
            };
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "application/json"); 
            restRequest.AddJsonBody(jsonData);

            IRestResponse<JsonRootObject> restResponse1 = restClient.Put<JsonRootObject>(restRequest);
            Assert.Equal(200, (int)restResponse1.StatusCode);
            Assert.True(restResponse1.Data.Features.Feature.Contains("New Feature"), "Feature did not got updated");

            headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" }
            };

            restClientHelper.PerformGetRequest<JsonRootObject>(getUrl, headers);
            Assert.Contains("New Feature", restResponse1.Data.Features.Feature);
        }

        [Fact]
        public void TestPutWithXmlData()
        {
            int id = random.Next(1000);

            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type", "application/xml" },
                {"Accept", "application/xml" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(postUrl, headers, xmlData, RestSharp.DataFormat.Xml);
            Assert.Equal(200, (int)restResponse.StatusCode);

            xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>Updated Feature</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = putUrl
            };

            restRequest.AddHeader("Content-Type", "application/xml");
            restRequest.AddHeader("Accept", "application/xml"); 
            restRequest.AddParameter("xmlBody", xmlData, ParameterType.RequestBody);

            IRestResponse restResponse1 = restClient.Put(restRequest);
            var deserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();

            var laptop = deserializer.Deserialize<Laptop>(restResponse1);
            Assert.True(laptop.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");

            headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            var restResponse2 = restClientHelper.PerformGetRequest<Laptop>(getUrl + id, headers);
            Assert.Equal(200, (int)restResponse2.StatusCode);
            Assert.True(restResponse2.Data.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");
        }

        [Fact]
        public void TestPutWithXmlData_HelperClass()
        {
            int id = random.Next(1000);

            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type", "application/xml" },
                {"Accept", "application/xml" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(postUrl, headers, xmlData, RestSharp.DataFormat.Xml);
            Assert.Equal(200, (int)restResponse.StatusCode);

            xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>Updated Feature</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            var restResponse3 = restClientHelper.PerformPutRequest<Laptop>(putUrl, headers, xmlData, DataFormat.Xml);
            Assert.True(restResponse3.Data.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");

            headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            var restResponse2 = restClientHelper.PerformGetRequest<Laptop>(getUrl + id, headers);
            Assert.Equal(200, (int)restResponse2.StatusCode);
            Assert.True(restResponse2.Data.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");
        }

    }
}
