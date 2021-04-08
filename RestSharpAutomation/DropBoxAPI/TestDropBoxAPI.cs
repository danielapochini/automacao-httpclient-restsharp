using RestSharp;
using RestSharpAutomation.DropBoxAPI.ListFolderModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestSharpAutomation.DropBoxAPI
{
    public class TestDropBoxAPI
    {
        private const string ListEndPointUrl = "https://api.dropboxapi.com/2/files/list_folder";
        private const string AccessToken = "sl.AuiwF3GMcYFZyiOSmW9JTBYEJjXWrSon03fP1v9oY3TcJJA3tV9e-QVLZp025iFiytb6KLU7vIxwfCV6ZE3rNlsnM2Co53lo9ArZiO4sSwlKL8GKdS1U30erZ6Mq46xjWD1jx5aaYz8";
       
        [Fact]
        public void TestListFolder()
        {

            string body = "{\"path\": \"\",\"recursive\": false,\"include_media_info\": false," +
                "\"include_deleted\": false,\"include_has_explicit_shared_members\": false,\"include_mounted_folders\": true," +
                "\"include_non_downloadable_files\": true}";

            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = ListEndPointUrl
            };

            request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            IRestResponse<RootObject> response = client.Post<RootObject>(request);

            Assert.Equal(200, (int)response.StatusCode);
        }
    }
}
