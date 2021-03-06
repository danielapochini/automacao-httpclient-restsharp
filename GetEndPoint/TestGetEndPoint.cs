using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Helpers.Authentication;
using WebServiceAutomation.Helpers.Request;
using WebServiceAutomation.Helpers.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using Xunit;
using Xunit.Abstractions;

namespace WebServiceAutomation.GetEndPoint
{

    public class TestGetEndPoint
    {
        private readonly ITestOutputHelper output;

        public TestGetEndPoint(ITestOutputHelper output)
        {
            this.output = output;
        }

        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private string secureGetUrl = "http://localhost:8080/laptop-bag/webapi/secure/all";
        private string delayGet = "http://localhost:8080/laptop-bag/webapi/delay/all";

        [Fact]
        public void TestGetAllEndPoint()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.GetAsync(getUrl);

            //Close the connection
            httpClient.Dispose();
        }

        [Fact]
        public void TestGetAllEndpointWithUri()
        {
            HttpClient httpClient = new HttpClient();
            Uri getUri = new Uri(getUrl);
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage HttpResponseMessage = httpResponse.Result;
            output.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            output.WriteLine("Status Code " + statuscode);
            output.WriteLine("Status Code " + (int)statuscode);
            httpClient.Dispose();

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            output.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [Fact]
        public void TestGetAllEndpointWitInvalidUrl()
        {
            HttpClient httpClient = new HttpClient();
            Uri getUri = new Uri(getUrl + "/random");
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage HttpResponseMessage = httpResponse.Result;
            output.WriteLine(HttpResponseMessage.ToString());

            //Status Code 
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            output.WriteLine("Status Code " + statuscode);
            output.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            output.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [Fact]
        public void TestGetAllEndpoinJsonFormat()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/json");


            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage HttpResponseMessage = httpResponse.Result;
            output.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            output.WriteLine("Status Code " + statuscode);
            output.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            output.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }


        [Fact]
        public void TestGetAllEndpointXmlFormat()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/xml");


            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage HttpResponseMessage = httpResponse.Result;
            output.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            output.WriteLine("Status Code " + statuscode);
            output.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            output.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }


        [Fact]
        public void TestGetAllEndpointUsingAcceptHeader()
        {
            MediaTypeWithQualityHeaderValue jsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Accept.Add(jsonHeader);


            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage HttpResponseMessage = httpResponse.Result;
            output.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            output.WriteLine("Status Code " + statuscode);
            output.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            output.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [Fact]
        public void TestGetEndPointUsingSendAsync()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(getUrl);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/json");

            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

            HttpResponseMessage HttpResponseMessage = httpResponse.Result;
            output.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            output.WriteLine("Status Code " + statuscode);
            output.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            output.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [Fact]
        public void TestGetEndPointUsingStatement()
        {
            //using statement automatically calls the dispose() to release the resources
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage HttpResponseMessage = httpResponse.Result)
                    {
                        output.WriteLine(HttpResponseMessage.ToString());

                        //Status Code
                        HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
                        output.WriteLine("Status Code " + statuscode);
                        output.WriteLine("Status Code " + (int)statuscode);

                        //Response Data
                        HttpContent responseContent = HttpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        output.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statuscode, responseData.Result);
                        output.WriteLine(restResponse.ToString());
                    }
                }
            }

        }

        [Fact]
        public void TestGetEndpointDeserilizationOfJsonResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage HttpResponseMessage = httpResponse.Result)
                    {
                        output.WriteLine(HttpResponseMessage.ToString());

                        //Status Code
                        HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
                        output.WriteLine("Status Code " + statuscode);
                        output.WriteLine("Status Code " + (int)statuscode);

                        //Response Data
                        HttpContent responseContent = HttpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        output.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statuscode, responseData.Result);
                        //output.WriteLine(restResponse.ToString());

                        //Deserialization of JSON Response
                        List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.responseContent);
                        output.WriteLine(jsonRootObject[0].ToString());

                    }
                }
            }
        }

        [Fact]
        public void TestGetEndpointDeserilizationOfXmlResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/xml");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage HttpResponseMessage = httpResponse.Result)
                    {
                        output.WriteLine(HttpResponseMessage.ToString());

                        //Status Code
                        HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
                        output.WriteLine("Status Code " + statuscode);
                        output.WriteLine("Status Code " + (int)statuscode);

                        //Response Data
                        HttpContent responseContent = HttpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        output.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statuscode, responseData.Result);
                        //output.WriteLine(restResponse.ToString());

                        //Deserialization of XML Response
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopDetailss));

                        TextReader textReader = new StringReader(restResponse.responseContent);

                        LaptopDetailss xmlData = (LaptopDetailss)xmlSerializer.Deserialize(textReader);
                        output.WriteLine(xmlData.ToString());


                        //Asserts (Status Code and Response)
                        Assert.Equal(200, restResponse.StatusCode);
                        Assert.NotNull(restResponse.responseContent);
                        Assert.Contains("Windows 10 Home 64-bit English", xmlData.Laptop[0].Features.Feature);
                        Assert.Equal("Alienware", xmlData.Laptop[0].BrandName);
                    }
                }
            }
        }

        [Fact]
        public void TestGetEndpointUsingHelperMethod()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);

            List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>
                (restResponse.responseContent);
            output.WriteLine(jsonRootObject[0].ToString());
        } 

        [Fact]
        public void GetUsingHelperClass()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>()
            {
                { "Accept", "application/json" }
            };

            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.responseContent);
             
        }

        [Fact]
        public void TestSecureGetEndPoint()
        { 
            string authHeader = Base64StringConverter.GetBase64String("admin", "welcome");
            authHeader = "Basic " + authHeader;

            Dictionary<string, string> httpHeader = new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Authorization", authHeader }
            };
            //{"Authorization", "Basic YWRtaW46d2VsY29tZQ==" } 


            RestResponse restResponse = HttpClientHelper.PerformGetRequest(secureGetUrl, httpHeader);

            Assert.Equal(200, restResponse.StatusCode); 

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.responseContent);
             
        }

        [Fact]
        public void TestGetEndPointSync()
        {
            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);

            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);

            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);

            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);
        }

        [Fact]
        public void TestGetEndPointAsync()
        {

            Task t1 = new Task(GetEndPoint());
            t1.Start();
            Task t2 = new Task(GetEndPoint());
            t2.Start();
            Task t3 = new Task(GetEndPoint());
            t3.Start();
            Task t4 = new Task(GetEndPointFailed());
            t4.Start();

            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait();
        }

        private Action GetEndPoint()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept","application/xml"},
            };
            return new Action(() =>
            {
                RestResponse restResponse = HttpClientHelper.PerformGetRequest(delayGet, headers);
                Assert.Equal(200, restResponse.StatusCode);
            });
        }

        private Action GetEndPointFailed()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept","application/xml"},
            };
            return new Action(() =>
            {
                RestResponse restResponse = HttpClientHelper.PerformGetRequest(delayGet, headers);
                Assert.Equal(201, restResponse.StatusCode);
            });
        }
    }
}
