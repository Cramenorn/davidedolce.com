using System.Collections.Generic;
using System.Linq;
using MyWebsite.BusinessLogic.IArticles;

namespace MyWebsite.Models
{
    public class FakeArticleRepository : IArticles
    {
        public IQueryable<Article> Articles => new List<Article> {
                new Article {ArticleID = "1", Title = "Fetch Api", SubTitle = "A good alternative to Ajax and Jquery", PubDate = "Febraury 25, 2019"},
                new Article {ArticleID = "2", Title = "Use Tor on Kali Linux", SubTitle = string.Empty, PubDate = "March 20, 2019"},
                new Article {ArticleID = "3", Title = "DotNet Not Forever", SubTitle = "This is a subtitle, change it later", PubDate = "March 9, 2019"},
                new Article {ArticleID = "4", Title = "DotNet Not Forever", SubTitle = "This is a subtitle, change it later", PubDate = "March 9, 2019"}
        }.AsQueryable();
    }
}
