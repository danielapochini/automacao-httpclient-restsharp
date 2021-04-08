using RestSharp;
using RestSharpAutomation.JwtToken.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace RestSharpAutomation.JWTAuthentication
{
    public class TestJwtToken : IDisposable
    {
        /***
         1. Register the user with the enpoint https://jobapplicationjwt.herokuapp.com/users/sign-up
         2. Autheticate the user and generate the token https://jobapplicationjwt.herokuapp.com/users/authenticate
         3. Extract the token from the response
         4. Pass the token in the header for the Get Request https://jobapplicationjwt.herokuapp.com/auth/webapi/all
         
         */ 
        private string RegisterUrl = "https://jobapplicationjwt.herokuapp.com/users/sign-up";
        private string AuthenticateUrl = "https://jobapplicationjwt.herokuapp.com/users/authenticate";
        private string GetAllUrl = "https://jobapplicationjwt.herokuapp.com/auth/webapi/all";
        private IRestClient client;
        private IRestRequest request;
        private string token;
        private string user = "{ \"password\": \"Guns and Bikes\",  \"username\": \"John Wick\"}";
         
        public TestJwtToken()
        {
            SetUp();
        }

        public void SetUp()
        {
            client = new RestClient();
            // Registration
            request = new RestRequest()
            {
                Resource = RegisterUrl
            };
            request.AddJsonBody(user);
            var response = client.Post(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Generate the token

            request = new RestRequest()
            {
                Resource = AuthenticateUrl
            };
            request.AddJsonBody(user);
            IRestResponse<ModelJwtToken> responseToken = client.Post<ModelJwtToken>(request);
            Assert.Equal(HttpStatusCode.OK, responseToken.StatusCode);
            token = responseToken.Data.Token; // JWT token
        }

        [Fact]
        public void TestGetWithJwt()
        {
            request = new RestRequest()
            {
                Resource = GetAllUrl
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Get(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            
        }
    }
}
