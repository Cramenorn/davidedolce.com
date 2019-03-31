using System.Linq;
using MyWebsite.Models;

namespace MyWebsite.BusinessLogic.IArticles
{
    public interface IArticles
    {
        IQueryable<Article> Articles { get; }
    }
}
