using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using WebServiceAutomation.Model.JsonModel;
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
                output.WriteLine("Status code: " + restResponse.StatusCode); 
                output.WriteLine("Size of the list: " + restResponse.Data.Count);
                //  restResponse.Data retorna o objeto depois da deserialização 
            } else
            {
                output.WriteLine("Error msg: " + restResponse.ErrorMessage);
                output.WriteLine("Stack Trace: " + restResponse.ErrorException);
            }
        }
    }
}
