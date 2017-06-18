using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Scrping;
namespace Scrping
{
    class Program
    {
        static bool flag = true;
        public static void DownloadOneFileByURLWithWebClient(string fileName, string url, string localPath)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            if (File.Exists(localPath + fileName)) { File.Delete(localPath + fileName); }
            if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
            wc.DownloadFile(url + fileName, localPath + fileName);
        }
        private static string GetPageByWebClient(string url)
        {
            if (url.StartsWith("https://"))
            {
                url = url.Replace("https://", "http://");
            }
            string result = null;
            if (url.Equals("about:blank")) return null;
            if (!url.StartsWith("http://") && !url.StartsWith("https://")) { url = "http://" + url; }
            string filename = RandomKey(1111, 9999) + ".txt";
            DownloadOneFileByURLWithWebClient(filename, url, "C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\html\\");
            StreamReader sr = new StreamReader("C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\html\\" + filename, Encoding.GetEncoding("gb2312"));
            try { result = sr.ReadToEnd(); return result; }
            catch { return null; }
            finally
            {
                if (sr != null) { sr.Close(); }
            }
        }
        private static string RandomKey(int b, int e)
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss-fff-") + getRandomID(b, e);
        }
        private static int getRandomID(int minValue, int maxValue)
        {
            Random ri = new Random(unchecked((int)DateTime.Now.Ticks));
            int k = ri.Next(minValue, maxValue);
            return k;
        }
        private static string GuidString
        {
            get { return Guid.NewGuid().ToString(); }
        }
        private static string extarurl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            Console.WriteLine("reading1");
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2) AppleWebKit/525.13 (KHTML, like Gecko) Chrome/0.2.149.27 Safari/525.13";            request.Credentials = CredentialCache.DefaultCredentials;            request.Method = "GET";            request.KeepAlive = false;
            //request.ContentType="text/plain";
            request.ProtocolVersion = HttpVersion.Version10;
            var response = (HttpWebResponse)request.GetResponse();
            
                Console.WriteLine("reading2");
                Stream responsestream = response.GetResponseStream();
                
                    Console.WriteLine("reading3");
                    StreamReader stream = new StreamReader(responsestream, Encoding.UTF8);
                        Console.WriteLine("reading4");
                        string strResponse;
                        strResponse = "";
                        strResponse = stream.ReadToEnd();
                        response.Close();
                       stream.Close();
            //           long lgnResponse = response.ContentLength;
            Console.WriteLine(strResponse);
            return strResponse;
        }


        static string GetPage(string url)        {            var request = WebRequest.CreateHttp(url);            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2) AppleWebKit/525.13 (KHTML, like Gecko) Chrome/0.2.149.27 Safari/525.13";            request.Credentials = CredentialCache.DefaultCredentials;            request.Method = "GET";            request.KeepAlive = false;
            //request.ContentType="text/plain";
            request.ProtocolVersion = HttpVersion.Version10;            var rep = request.GetResponse() as HttpWebResponse;            var stream = rep.GetResponseStream();            var reader = new StreamReader(stream, Encoding.UTF8);            string str = reader.ReadToEnd();            reader.Close();            rep.Close();            return str;        }
        private static void GetWebContent(string url)
        {
            if (url.StartsWith("https://"))
            {
                url = url.Replace("https://", "http://");
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
            Stream stream = respone.GetResponseStream();

            Encoding encoding = Encoding.GetEncoding("gb2312");
            StreamReader streamReader = new StreamReader(stream, encoding);
            Console.WriteLine("Begin");
            Console.WriteLine(streamReader.ReadToEnd());
        }
        private static void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser web = (WebBrowser)sender;
            HtmlElementCollection ElementCollection = web.Document.GetElementsByTagName("Table");
            foreach (HtmlElement item in ElementCollection)
            {
                File.AppendAllText("Kaijiang_xj.txt", item.InnerText);
            }
        }
        private static void GetWebBrowser()
        {
            WebBrowser web = new WebBrowser();
            web.Navigate("http://www.bilibili.com");
            web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
            Console.WriteLine(web.DocumentText);
             flag = false;
       } 
        private static void Getallpage(string url,string behind,int t)
        {
            string nowurl;
            string result;
            string path;
            int i = 1;
            try
            {
                for (i = t; i <= 1319; i++)
                {
                    path = "C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\student\\";
                    path += behind;
                    Console.WriteLine("reading---{0}", i);
                    nowurl = url;
                    nowurl += "startPage=";
                    nowurl += i.ToString();
                    result = GetPage(nowurl);
                    path += i.ToString();
                    path += ".txt";
                    WirtePageFile(result, path);
                    Console.WriteLine("OVER--{0}", i);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Getallpage(url, behind, i);
            }
            
        }
        private static void WirtePageFile(string xx,string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(xx);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        static void Main(string[] args)
        {

            string tilte = "first Scrping";
            Console.Title  = tilte;
            //    string url = Console.ReadLine();
            //    Thread newThread = new Thread(new ThreadStart(GetWebBrowser));
            //    newThread.SetApartmentState(ApartmentState.STA);
            //    newThread.Start();
            //   string whatweget = GetPageByWebClient(url);
            //   Console.WriteLine(whatweget);
            Getallpage("https://oa.jlu.edu.cn/defaultroot/PortalInformation!jldxList.action?1=1&channelId=179581&", "student", 1);
            // ReadFile.Reafile("C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\news\\");
            //ReadFile.Reafilenews("C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\news\\");
            Console.WriteLine("--------END-------");

        }
    }
}
