using RestSharp;
using RestSharpAutomation.HelperClass.Request;
using System;
using System.Collections.Generic;
using System.Text;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using Xunit;
using Xunit.Abstractions;

namespace RestSharpAutomation.RestGetEndPoint
{
    public class TestGetEndPoint
    {
        private readonly ITestOutputHelper output;

        public TestGetEndPoint(ITestOutputHelper output)
        {
            this.output = output;
        }
         
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
         
        [Fact]
        public void TestGetUsingRestSharp()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            IRestResponse restResponse = restClient.Get(restRequest);

            /*output.WriteLine($"{restResponse.IsSuccessful}");
            output.WriteLine($"{restResponse.StatusCode}");
            output.WriteLine($"{restResponse.ErrorMessage}");
            output.WriteLine($"{restResponse.ErrorException}");*/

            if (restResponse.IsSuccessful)
            {
                output.WriteLine("Status code: " + restResponse.StatusCode);
                output.WriteLine("Status code: " + restResponse.Content);
            }
        }

        [Fact]
        public void TestGetInXmlFormat()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                output.WriteLine("Status code: " + restResponse.StatusCode);
                output.WriteLine("Status code: " + restResponse.Content);
            }
        }

        [Fact]
        public void TestGetInJsonFormat()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                output.WriteLine("Status code: " + restResponse.StatusCode);
                output.WriteLine("Status code: " + restResponse.Content);
            }
        }

        [Fact]
        public void TestGetWithJsonDeserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");

            IRestResponse <List<JsonRootObject>> restResponse = restClient.Get<List<JsonRootObject>>(restRequest); 

            if (restResponse.IsSuccessful)
            { 
                Assert.Equal(200, (int)restResponse.StatusCode);

                //  restResponse.Data retorna o objeto depois da deserialização 
                List<JsonRootObject> data = restResponse.Data;
                
                //expressao lambda para filtrar a lista e pegar o objeto com o id igual a 1
                JsonRootObject jsonRootObject = data.Find((x) =>
                {
                    return x.Id == 1;
                });

                Assert.Equal("Alienware M17", jsonRootObject.LaptopName);
                Assert.Contains("8th Generation Intel® Core™ i5-8300H", jsonRootObject.Features.Feature);
            } else
            {
                output.WriteLine("Error msg: " + restResponse.ErrorMessage);
                output.WriteLine("Stack Trace: " + restResponse.ErrorException);
            }
        }

        [Fact]
        public void TestGetWithXmlDeserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");

            var dotNetXmlDeserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();

            //IRestResponse<LaptopDetailss> restResponse = restClient.Get<LaptopDetailss>(restRequest);
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Assert.Equal(200, (int)restResponse.StatusCode);

                //  restResponse.Data retorna o objeto depois da deserialização 
                LaptopDetailss data = dotNetXmlDeserializer.Deserialize<LaptopDetailss>(restResponse); 

                Laptop laptop = data.Laptop.Find((x) =>
                {
                    return x.Id.Equals("1", StringComparison.OrdinalIgnoreCase);
                });

                Assert.Equal("Alienware M17", laptop.LaptopName);
                Assert.Contains("8th Generation Intel® Core™ i5-8300H", laptop.Features.Feature);
            }
            else
            {
                output.WriteLine("Error msg: " + restResponse.ErrorMessage);
                output.WriteLine("Stack Trace: " + restResponse.ErrorException);
            }
        }

        [Fact]  
        public void TestGetWithExecute()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Method = Method.GET,
                Resource = getUrl
            };

            restRequest.AddHeader("Accept", "application/json");

            //Execute() passando o tipo irá deserializa a response após enviar a request
            IRestResponse<List<Laptop>> restResponse = restClient.Execute<List<Laptop>>(restRequest);

            Assert.Equal(200, (int)restResponse.StatusCode);
            Assert.NotNull(restResponse.Data);
        }

        [Fact]
        public void TestGetWithXmlUsingHelperClass()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformGetRequest(getUrl, headers);
            
            Assert.Equal(200, (int)restResponse.StatusCode);
            Assert.NotNull(restResponse.Content);

        }

        [Fact]
        public void TestGetWithXmlUsingHelperClassAndTyped()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse<LaptopDetailss> restResponse = restClientHelper.PerformGetRequest<LaptopDetailss>(getUrl, headers);
            
            Assert.Equal(200, (int)restResponse.StatusCode);
            Assert.NotNull(restResponse.Data);
        }

        [Fact]
        public void TestGetWithJsonUsingHelperClass()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformGetRequest(getUrl, headers);
            
            Assert.Equal(200, (int)restResponse.StatusCode);
            Assert.NotNull(restResponse.Content);
        }

        [Fact]
        public void TestGetWithJsonUsingHelperClassAndTyped()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse<List<Laptop>> restResponse = restClientHelper.PerformGetRequest<List<Laptop>>(getUrl, headers);

            Assert.Equal(200, (int)restResponse.StatusCode);
            Assert.NotNull(restResponse.Data);
        }
    }
}
