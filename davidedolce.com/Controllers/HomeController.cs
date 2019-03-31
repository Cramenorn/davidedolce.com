using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebsite.Models;
using MyWebsite.Models.ViewModels;
using MyWebsite.BusinessLogic.IArticles;
using MyWebsite.BusinessLogic.Rss;
using MyWebsite.BusinessLogic.SendGrid;

namespace MyWebsite.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private IArticles repository;
        private ArticleListViewModel articleList;
        private readonly IOptions<SendGridModel> config;
        private PagingInfo pagingInfo;
        private SendGridEmail sendGridEmail;

        public int PageSizeIndex = 3;
        public int PageSizeBlog = 5;

        public HomeController(IArticles repo, IOptions<SendGridModel> grid, ArticleListViewModel alvm, PagingInfo pi, SendGridEmail sge)
        {
            repository = repo;
            config = grid;
            articleList = alvm;
            pagingInfo = pi;
            sendGridEmail = sge;
        }

        [HttpGet]
        public ViewResult Index(int articlePage = 1)
        {
            ViewBag.TitleHead = "Davide Dolce - Software Developer, writer, sportsman";
            ViewBag.TitleNav = "Davide Dolce";
            ViewBag.SubTitleNav = "Software Developer, writer, sportsman.";

            articleList.Articles = repository.Articles
                .OrderByDescending(a => a.ArticleID)
                .Skip((articlePage - 1) * PageSizeIndex)
                .Take(PageSizeIndex);
            pagingInfo.CurrentPage = articlePage;
            pagingInfo.ItemsPerPage = PageSizeIndex;
            pagingInfo.TotalItems = repository.Articles.Count();

            articleList.PagingInfo = pagingInfo;

            return View(articleList);
        }

        [HttpGet("blog")]
        [HttpGet("blog/page{articlePage}")]
        public ViewResult Blog(int articlePage = 1)
        {
            ViewBag.TitleHead = "Davide Dolce - Blog";
            ViewBag.TitleNav = "Blog";
            ViewBag.SubTitleNav = "When I have something of interesting to show, I write an article.";

            articleList.Articles = repository.Articles
                .OrderByDescending(a => a.ArticleID)
                .Skip((articlePage - 1) * PageSizeBlog)
                .Take(PageSizeBlog);
            pagingInfo.CurrentPage = articlePage;
            pagingInfo.ItemsPerPage = PageSizeBlog;
            pagingInfo.TotalItems = repository.Articles.Count();

            articleList.PagingInfo = pagingInfo;

            return View(articleList);
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            ViewBag.TitleHead = "Davide Dolce - About";
            ViewBag.TitleNav = "About me";
            ViewBag.SubTitleNav = "This is what I do.";
            return View();
        }

        [HttpGet("projects")]
        public IActionResult Projects()
        {
            ViewBag.TitleHead = "Davide Dolce - Projects";
            ViewBag.TitleNav = "Projects";
            ViewBag.SubTitleNav = "I made some projects in my career, here is the list.";
            return View();
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            articleList.Articles = repository.Articles
               .OrderByDescending(a => a.ArticleID);

            pagingInfo.CurrentPage = 2;
            pagingInfo.ItemsPerPage = PageSizeBlog;
            pagingInfo.TotalItems = repository.Articles.Count();

            articleList.PagingInfo = pagingInfo;

            var xmlOutput = new CreateRssFeed();

            return Content(xmlOutput.GenerateXml(articleList.Articles), "text/xml");
        }

        [HttpGet("privacy-policy")]
        public IActionResult PrivacyPolicy()
        {
            ViewBag.TitleHead = "Davide Dolce - Privacy Policy";
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.TitleHead = "Davide Dolce - Contact";
            ViewBag.TitleNav = "Contact";
            ViewBag.SubTitleNav = "I can answer to your questions, but sometime I can't :)";
            return View();
        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var v = cvm.GoogleReCaptchaResponse;
                    await sendGridEmail.SendContactFormData(cvm, config);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for contacting us.";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return View();
        }
    }
}
