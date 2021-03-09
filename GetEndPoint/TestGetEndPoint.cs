using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using Xunit;

namespace WebServiceAutomation.GetEndPoint
{

    public class TestGetEndPoint
    {
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";

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
            Task<HttpResponseMessage> httpResponse  = httpClient.GetAsync(getUri);
            HttpResponseMessage HttpResponseMessage = httpResponse.Result;
            Console.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            Console.WriteLine("Status Code " + statuscode);
            Console.WriteLine("Status Code " + (int)statuscode);
            httpClient.Dispose();

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

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
            Console.WriteLine(HttpResponseMessage.ToString());

            //Status Code 
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            Console.WriteLine("Status Code " + statuscode);
            Console.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

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
            Console.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            Console.WriteLine("Status Code " + statuscode);
            Console.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

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
            Console.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            Console.WriteLine("Status Code " + statuscode);
            Console.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

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
            Console.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            Console.WriteLine("Status Code " + statuscode);
            Console.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

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
            Console.WriteLine(HttpResponseMessage.ToString());

            //Status Code
            HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
            Console.WriteLine("Status Code " + statuscode);
            Console.WriteLine("Status Code " + (int)statuscode);

            //Response Data
            HttpContent responseContent = HttpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [Fact]
        public void TestUsingStatement()
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
                        Console.WriteLine(HttpResponseMessage.ToString());

                        //Status Code
                        HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
                        Console.WriteLine("Status Code " + statuscode);
                        Console.WriteLine("Status Code " + (int)statuscode);

                        //Response Data
                        HttpContent responseContent = HttpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statuscode, responseData.Result);
                        Console.WriteLine(restResponse.ToString());
                    }
                }
            }
             
        }

        [Fact]
        public void TestDeserilizationOfJsonObject()
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
                        Console.WriteLine(HttpResponseMessage.ToString());

                        //Status Code
                        HttpStatusCode statuscode = HttpResponseMessage.StatusCode;
                        Console.WriteLine("Status Code " + statuscode);
                        Console.WriteLine("Status Code " + (int)statuscode);

                        //Response Data
                        HttpContent responseContent = HttpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statuscode, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        //Deserialization of JSON Response
                        List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.responseContent);
                        Console.WriteLine(jsonRootObject[0].ToString());

                    }
                }
            }
        }
    }
}
