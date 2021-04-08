using RestSharp;
using RestSharpAutomation.DropBoxAPI.ListFolderModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace RestSharpAutomation.DropBoxAPI
{
    public class TestDropBoxAPI
    {
        private const string ListEndPointUrl = "https://api.dropboxapi.com/2/files/list_folder";
        private const string CreateEndPointUrl = "https://api.dropboxapi.com/2/files/create_folder_v2";
        private const string DownloadEndPointUrl = "https://api.dropboxapi.com/2/files/download";
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

        [Fact]
        public void TestCreateFolder()
        {
            string body = "{\"path\": \"/TestFolder\",\"autorename\": true}";
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = CreateEndPointUrl
            };

            request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            var response = client.Post(request);
            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public void TestDownloadFile()
        {
            string location = "{\"path\": \"/Book.xlsx\"}";
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = DownloadEndPointUrl
            };

            request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddHeader("Dropbox-API-Arg", location); 

            var dataInByte = client.DownloadData(request);
            File.WriteAllBytes("Test.xlsx", dataInByte);

        }
    }
}
