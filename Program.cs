using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Scraping
{
    class TagElement
    {
        public int id { get; set; }
        public string title { get; set; }
        public string tag { get; set; }
        public string attribute { get; set; }

        public static List<TagElement> List()
        {
            return new List<TagElement>
            {
                new TagElement { id = 1, title = "hyperlink", tag = "a", attribute = "href" },
                new TagElement { id = 2, title = "emphasized text", tag = "em", attribute = "" },
            };
        }

        public static TagElement Get(int id)
        {
            return List().FirstOrDefault(t => t.id == id);
        }

        public static string ToFormat()
        {
            var result = "";
            foreach (var t in List())
            {
                result += $"{Environment.NewLine}{t.id}: {t.title}";
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string scrapURL;
                int tagIndex;
                string searchText;

                //PARAMS
                Console.WriteLine("1 - Set scrap URL");
                scrapURL = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine($"2 - Set search tag: {TagElement.ToFormat()}");
                tagIndex = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("3- Set search text");
                searchText = Console.ReadLine();
                Console.WriteLine();

                //PROCESS
                Console.WriteLine("START SCRAPING...");
                Console.WriteLine();

                TagElement tagElement = TagElement.Get(tagIndex);
                Scrap(scrapURL, tagElement, searchText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        static void Scrap(string url, TagElement tag, string text)
        {
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(url);            
            
            var nodes = doc.DocumentNode.SelectNodes("//" + tag.tag);
            if (nodes != null)
            {
                var content = nodes.FirstOrDefault(a => a.InnerText.Contains(text));
                Console.WriteLine("VALUE OF TAG IS : " + (content != null ? content.InnerText : "CONTENT IS NULL"));
                
                if (content != null && !string.IsNullOrEmpty(tag.attribute))
                    Console.WriteLine("VALUE OF ATTRIBUTE IS : " + content.Attributes[tag.attribute].Value);

                Console.ReadLine();
            }
        }
    }
}
