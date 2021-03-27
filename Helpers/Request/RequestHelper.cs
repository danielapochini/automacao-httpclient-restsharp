using System;
using System.Collections.Generic;
using System.Text;
using WebServiceAutomation.Model;
using Xunit;

namespace WebServiceAutomation.Helpers.Request
{
    public class RequestHelper
    {  
        public static void AddRecord(string postUrl, int id)
        {
            string xmlMediaType = "application/xml";

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

            var restResponse = HttpClientHelper.PerformPostRequest(postUrl, xmlData, xmlMediaType, headers);
            Assert.Equal(200, restResponse.StatusCode);
        }
    }
}
