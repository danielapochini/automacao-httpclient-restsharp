using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helpers.Request;
using WebServiceAutomation.Helpers.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using Xunit;

namespace WebServiceAutomation.ParallelExecution
{
    public class TestParallelExecution
    {
        private string delayGetUrl = "http://localhost:8080/laptop-bag/webapi/delay/all";
        private string delayGetWithId = "http://localhost:8080/laptop-bag/webapi/delay/find/";
        private string delayPostUrl = "http://localhost:8080/laptop-bag/webapi/delay/add";
        private string delayPutUrl = "http://localhost:8080/laptop-bag/webapi/delay/update";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();
        private Helpers.Request.HttpAsyncClientHelper httpAsyncClientHelper = new Helpers.Request.HttpAsyncClientHelper();
        //Helpers.Request.HttpAsyncClientHelper

        private void SendGetRequest()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

             
            RestResponse restResponse = httpAsyncClientHelper.PerformGetRequest(delayGetUrl, httpHeader).GetAwaiter().GetResult();

            List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>
                (restResponse.responseContent); 
        }

        private void SendPostRequest()
        {
            int id = random.Next(1000);

            string xmlData =
                    "<Laptop>" +
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

            Dictionary<string, string> httpHeader = new Dictionary<string, string>()
            {
                { "Accept", "application/xml" }
            };


            RestResponse restResponse = httpAsyncClientHelper.PerformPostRequest(delayPostUrl, xmlData, xmlMediaType, httpHeader).GetAwaiter().GetResult();

            //HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType); 
            //httpAsyncClientHelper.PerformPostRequest(postUrl, httpContent, httpHeader);

            Assert.Equal(200, restResponse.StatusCode);

            Laptop xmlDatat = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.responseContent);
        }

        private void SendPutRequest()
        {
            int id = random.Next(1000);
            string xmlData =
                                           "<Laptop>" +
                                              "<BrandName>Alienware</BrandName>" +
                                                "<Features>" +
                                                   "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                                   "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                                   "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                                   "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                                 "</Features>" +
                                              "<Id>" + id + "</Id>" +
                                              "<LaptopName>Alienware M17</LaptopName>" +
                                           "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
            {"Accept","application/xml"}
            };

            RestResponse restResponse = httpAsyncClientHelper.PerformPostRequest(delayPostUrl, xmlData, xmlMediaType, headers).GetAwaiter().GetResult();
            Assert.Equal(200, restResponse.StatusCode);

            xmlData =
                               "<Laptop>" +
                                  "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>1 TB of SSD</Feature>" +
                                     "</Features>" +
                                  "<Id>" + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            restResponse = httpAsyncClientHelper.PerformPutRequest(delayPutUrl, xmlData, xmlMediaType, headers).GetAwaiter().GetResult();
            Assert.Equal(200, restResponse.StatusCode);

            restResponse = httpAsyncClientHelper.PerformGetRequest(delayGetWithId + id, headers).GetAwaiter().GetResult();
            Assert.Equal(200, restResponse.StatusCode);

            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.responseContent);
            Assert.Contains("1 TB of SSD", xmlObj.Features.Feature);
        }

        [Fact]
        public void TestTask()
        {
            Task get = new Task(() =>
            {
                SendGetRequest();
            });
            get.Start();

            Task post = new Task(() =>
            {
                SendPostRequest();
            });
            post.Start();

            Task put = new Task(() =>
            {
                SendPutRequest();
            });
            put.Start();

            get.Wait();
            put.Wait();
            post.Wait();
        }

        [Fact]
        public void TestTaskUsingTaskFactory()
        {
            var getTask = Task.Factory.StartNew(() =>
            {
                SendGetRequest();
            });

            var postTask = Task.Factory.StartNew(() =>
            {
                SendPostRequest();
            });

            var putTask = Task.Factory.StartNew(() =>
            {
                SendPutRequest();
            });

            getTask.Wait();
            postTask.Wait();
            putTask.Wait();

        }
    }
}
