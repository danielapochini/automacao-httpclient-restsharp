using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helpers.Authentication;
using WebServiceAutomation.Helpers.Request;
using WebServiceAutomation.Helpers.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.XmlModel;
using Xunit;

namespace WebServiceAutomation.DeleteEndPoint
{
    public class TestDeleteEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add/"; 
        private string deleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private string securePostUrl = "http://localhost:8080/laptop-bag/webapi/secure/add/";
        private string secureDeleteUrl = "http://localhost:8080/laptop-bag/webapi/secure/delete/";
        private Random random = new Random();

        [Fact]
        public void TestDelete()
        {
           int id = random.Next(1000);
           RequestHelper.AddRecord(postUrl, id);

            using(HttpClient httpClient = new HttpClient())
            {
                Task<HttpResponseMessage> httpResponseMessage = httpClient.DeleteAsync(deleteUrl + id);
                HttpStatusCode httpStatusCode = httpResponseMessage.Result.StatusCode;
                
                Assert.Equal(200, (int)httpStatusCode);

                httpResponseMessage = httpClient.DeleteAsync(deleteUrl + id);
                httpStatusCode = httpResponseMessage.Result.StatusCode;
                
                Assert.Equal(404, (int)httpStatusCode);
            }
        }
         
        [Fact]
        public void TestDeleteUsingHelperClass()
        {
            int id = random.Next(1000);
            RequestHelper.AddRecord(postUrl, id);

            var restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id);

            Assert.Equal(200, (int)restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id);

            Assert.Equal(404, (int)restResponse.StatusCode);
        }

        [Fact]
        public void TestSecureDeleteEndPoint()
        {
            int id = random.Next(1000);
            RequestHelper.AddRecord(postUrl, id);

            string authHeader = Base64StringConverter.GetBase64String("admin", "welcome");
            authHeader = "Basic " + authHeader;

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"Authorization", authHeader }
            };

            var restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id, headers);

            Assert.Equal(200, (int)restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id, headers);

            Assert.Equal(404, (int)restResponse.StatusCode);
        }
       
    }
}
