using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
         
       
    }
}
