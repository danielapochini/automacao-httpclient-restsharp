using RestSharp;
using RestSharpAutomation.HelperClass.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestSharpAutomation.RestDeleteEndPoint
{
    public class TestDeleteEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add/"; 
        private string deleteEndPoint = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private Random random = new Random();

        [Fact]
        public void TestDeleteRequest()
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

            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = deleteEndPoint + id
            };

            request.AddHeader("Accept", "*/*");

            IRestResponse restResponse1 = client.Delete(request);
            Assert.Equal(200, (int)restResponse1.StatusCode);

            restResponse1 = client.Delete(request);
            Assert.Equal(404, (int)restResponse1.StatusCode);

        }

        [Fact]
        public void TestDeleteRequest_Helperclass()
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

            headers = new Dictionary<string, string>()
            {
                {"Accept", "*/*" }
            };

            IRestResponse restResponse1 = restClientHelper.PerformDeleteRequest(deleteEndPoint + id, headers);
            Assert.Equal(200, (int)restResponse1.StatusCode);

            restResponse1 = restClientHelper.PerformDeleteRequest(deleteEndPoint + id, headers);
            Assert.Equal(404, (int)restResponse1.StatusCode);

        }

    }
}
