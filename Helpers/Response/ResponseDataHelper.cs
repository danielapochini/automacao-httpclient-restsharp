using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WebServiceAutomation.Helpers.Response
{
    public class ResponseDataHelper
    {
         
        public static T DeserializeJsonResponse<T>(string responseData) where T : class
        {
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        public static T DeserializeXmlResponse<T>(string responseData) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(responseData);
            return (T)xmlSerializer.Deserialize(textReader);
        }
    }
}
