using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;

namespace Scrping
{
    class Program
    {
        private static void extarurl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responsestream = response.GetResponseStream())
                {
                    using (StreamReader stream = new StreamReader(responsestream, Encoding.UTF8))
                    {
                        string strResponse = stream.ReadToEnd();
                        long lgnResponse = response.ContentLength;
                        string pattern = @"(http|ftp|https):\/\/([\w+\.\w+])+([a-zA-Z0-9\~\@\#\$\%\^\&\*\(\)\-\+\\\/\?\,\.\:\;\'\_\=]*)?";

                    }
                }
            }
        }
        static void Main(string[] args)
        {

        }
    }
}
