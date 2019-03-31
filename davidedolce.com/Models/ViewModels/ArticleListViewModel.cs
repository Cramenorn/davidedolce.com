using System.Collections.Generic;
using System.Linq;
using MyWebsite.Models;

namespace MyWebsite.Models.ViewModels
{
    public class ArticleListViewModel
    {
        private readonly PagingInfo pag;

        public ArticleListViewModel(PagingInfo pi)
        {
            pag = pi;
        }

        public IEnumerable<Article> Articles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}