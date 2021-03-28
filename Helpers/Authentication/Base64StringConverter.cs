using System;
using System.Collections.Generic;
using System.Text;

namespace WebServiceAutomation.Helpers.Authentication
{
    public class Base64StringConverter
    {
        public static string GetBase64String(string username, string password)
        {
            string auth = username + ":" + password;
            var array = UTF8Encoding.UTF8.GetBytes(auth);
            return Convert.ToBase64String(array); 
        }
    }
}
