using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using WebServiceAutomation.Model.XmlModel;
using Xunit;

namespace RestSharpAutomation.QueryParameter
{
    public class QueryParameter
    {
        private string searchUrl = "http://localhost:8080/laptop-bag/webapi/api/query";

        [Fact]
        public void TestQueryParameter()
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = searchUrl
            };

            request.AddHeader("Accept", "application/xml");
            // 1st apporach 
            //request.AddParameter("id", "1", ParameterType.QueryString);
            request.AddQueryParameter("id", "1");
            request.AddQueryParameter("laptopName", "Alienware M17");

            var response = client.Get<Laptop>(request);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Alienware", response.Data.BrandName);

        }
    }
}