using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace RestSharpAutomation.RestPostEndPoint
{
    public class FileUploadEndPoint
    {
        [Fact]
        public void Test_Upload_Of_File()
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = "https://jobportalkarate.herokuapp.com/normal/webapi/upload"
            };

            request.AddFile("file", @"C:\Cursos\WebServiceAutomation\RestSharpAutomation\tst.txt", "multipart/form-data");
            var response = client.Post(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
