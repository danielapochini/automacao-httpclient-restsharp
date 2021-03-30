using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
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
            output.WriteLine($"{restResponse.IsSuccessful}");
            output.WriteLine($"{restResponse.StatusCode}");
            output.WriteLine($"{restResponse.ErrorMessage}");
            output.WriteLine($"{restResponse.ErrorException}");
        }
    }
}
