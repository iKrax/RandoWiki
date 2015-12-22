using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

    /*
        RandoWiki - iKrax (Jordan McCall)

        Random console application that finds a random wikipedia article, then copies the first paragraph to the
        clipboard. Useful for annoying people I guess....
    */

namespace RandoWiki
{
    class Program
    {

        //ADD STAThread attribute so console can use OLE functions
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                //Clear console of previous wiki
                Console.Clear();

                //Set url (incase it changes any time soon"
                string url = "https://en.wikipedia.org/wiki/Special:Random";
                
                //Get HTML code for random wiki
                string htmlCode = new System.Net.WebClient().DownloadString(url);

                //Find the first paragraph
                int begin = htmlCode.IndexOf("<p>");
                int end = htmlCode.IndexOf("</p>");
                htmlCode = htmlCode.Substring(begin, end - begin + 4);

                //Strip HTML tags from code
                htmlCode = StripTags(htmlCode);

                //Copy remaining text
                Clipboard.SetText(htmlCode);

                //Write to console for visual output
                Console.WriteLine(htmlCode);

                //Loop the loop
                Console.ReadKey(true);
            }


        }

        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
        public static string StripTags(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }
    }
}
