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
    class Creathtml
    {
        public static void creathtml()
        {
            string path = "C:\\Users\\乌骓\\Desktop\\NET\\Scrping\\URL\\url.txt";
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string line;
            while((line = sr.ReadLine())!=null)
            {

            }
        }
    }
}
