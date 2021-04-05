using RestSharp;
using RestSharpAutomation.HelperClass.Request;
using System;
using System.Collections.Generic;
using System.Text;
using WebServiceAutomation.Model.XmlModel;
using Xunit;

namespace RestSharpAutomation.RestPostEndPoint
{
    public class TestPostEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add/";
        private Random random = new Random();

        [Fact]
        public void TestPostWithJsonData()
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

            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = postUrl
            };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "application/xml");
            restRequest.AddJsonBody(jsonData);

            IRestResponse restResponse = restClient.Post(restRequest);

            Assert.Equal(200, (int)restResponse.StatusCode);

        }

        private Laptop GetLaptopObject()
        {
            Laptop laptop = new Laptop();
            laptop.BrandName = "Sample Brand Name";
            laptop.LaptopName = "Sample Laptop Name";

            Features features = new Features();
            List<string> featureList = new List<string>()
            {
                ("sample feaure")
            };

            features.Feature = featureList;
            laptop.Id = "" + random.Next(1000);
            laptop.Features = features;

            return laptop;
        }

        [Fact]
        public void TestPostWithModelObject()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = postUrl
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/xml"); 
            request.AddJsonBody(GetLaptopObject());

            IRestResponse response = restClient.Post(request);
            Assert.Equal(200, (int)response.StatusCode);
            Console.WriteLine(response.Content);
        }

        [Fact]
        public void TestPostWithModelObjectHelperClass()
        { 
            RestClientHelper restClientHelper = new RestClientHelper();   
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }
            };

            IRestResponse<Laptop> restResponse = restClientHelper.PerformPostRequest<Laptop>(postUrl, headers, GetLaptopObject());

            Assert.Equal(200, (int)restResponse.StatusCode);
            Assert.NotNull(restResponse.Data);
        }
    } 
}
