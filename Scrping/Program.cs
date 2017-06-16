using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading;
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
            string result = null;
            if (url.Equals("about:blank")) return null;
            if (!url.StartsWith("http://") && !url.StartsWith("https://")) { url = "http://" + url; }
            string filename = RandomKey(1111, 9999) + ".txt";
            DownloadOneFileByURLWithWebClient(filename, url, "C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\html\\");
            StreamReader sr = new StreamReader("C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\html\\" + filename, System.Text.Encoding.Default);
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
        private static void extarurl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            request.Accept = "*/*";
            request.KeepAlive = true;
            request.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responsestream = response.GetResponseStream())
                {
                    using (StreamReader stream = new StreamReader(responsestream, Encoding.UTF8))
                    {
                        string strResponse = stream.ReadToEnd();
                        long lgnResponse = response.ContentLength;
                        string pattern = @"(http|ftp|https):\/\/([\w+\.\w+])+([a-zA-Z0-9_\~\@\#\$\%\^\&\*\(\)\-\+\\\/\?\,\.\:\;\'\=]*)?";
                        Regex Filter = new Regex(pattern);
                        Console.WriteLine(strResponse);
                        Console.WriteLine("总长度：{0}", lgnResponse);
                        foreach (Match march in Filter.Matches(strResponse))
                        {
                            Console.WriteLine("URL : {0}", march.Value);
                        }
                    }
                }
            }
        }
        private static string GetWebContent(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
            Stream stream = respone.GetResponseStream();

            Encoding encoding = Encoding.GetEncoding("gb2312");
            StreamReader streamReader = new StreamReader(stream, encoding);
            return streamReader.ReadToEnd();
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
        static void Main(string[] args)
        {

            string tilte = "first Scrping";
            Console.Title  = tilte;
            Thread newThread = new Thread(new ThreadStart(GetWebBrowser));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
            //   string whatweget = GetPageByWebClient(url);
            //   Console.WriteLine(whatweget);
            while (flag) ;
            Console.WriteLine("--------END-------");

        }
    }
}
