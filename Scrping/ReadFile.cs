using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Web;
namespace Scrping
{
    class ReadFile
    {
        public static void Reafile(string path)
        {
            string nowpath;
            string result;
            string patten;
            string loadpath = "C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\URL\\url.txt";
            FileStream fs = new FileStream(loadpath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            int j = 0;
            for (int i =1;i<=1312;i++)
            {
                nowpath = path;
                nowpath += i.ToString();
                nowpath += ".txt";
                StreamReader sr = new StreamReader(nowpath, Encoding.UTF8);
                result = sr.ReadToEnd();
                //   patten = @"PortalInformation\!getInformation\.action\?title\=[\W]*[0-9]*[\W]*\&id\=[0-9]+\&categoryName\=";
                patten = @"PortalInformation\!getInformation\.action\?title\=[\W|\d\u4E00-\u9FFF]+id=[\d]+\&categoryName\=校内通知";
                foreach (Match match in Regex.Matches(result, patten))
                {
                   Console.WriteLine(match.Value);
                   j++;
                  sw.WriteLine(match.Value);
                }
                sr.Close();
            }
            Console.WriteLine("J={0}", j);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public static void Reafilenews(string path)
        {
            string nowpath;
            string result;
            string patten;
            string loadpath = "C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\URL\\url-news.txt";
            FileStream fs = new FileStream(loadpath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            int j = 0;
            for (int i = 1; i <= 1182; i++)
            {
                nowpath = path;
                nowpath += "news";
                nowpath += i.ToString();
                nowpath += ".txt";
                StreamReader sr = new StreamReader(nowpath, Encoding.UTF8);
                result = sr.ReadToEnd();
                //   patten = @"PortalInformation\!getInformation\.action\?title\=[\W]*[0-9]*[\W]*\&id\=[0-9]+\&categoryName\=";
                patten = @"PortalInformation\!getInformation\.action\?title\=[\W|\d\u4E00-\u9FFF]+id=[\d]+\&categoryName\=校园快讯";
                foreach (Match match in Regex.Matches(result, patten))
                {
                    Console.WriteLine(match.Value);
                    j++;
                    sw.WriteLine(match.Value);
                }
                sr.Close();
            }
            Console.WriteLine("J={0}", j);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
