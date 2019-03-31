using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Models
{
    public class Article
    {
        public string ArticleID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string FileName { get; set; }
    }
}
