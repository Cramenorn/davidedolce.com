using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        [HttpGet("fetch-api")]
        public IActionResult FetchApi()
        {
            ViewBag.PostTitle = "Fetch Api";
            ViewBag.PostSubTitle = "A good alternative to Ajax and Jquery";
            ViewBag.PostDate = "Febraury 25, 2019";
            return View();
        }

        [HttpGet("use-tor-on-kali-linux")]
        public IActionResult UseTorOnKaliLinux()
        {
            ViewBag.PostTitle = "Use Tor on Kali Linux";
            ViewBag.PostDate = "March 20, 2019";
            return View();
        }

        [HttpGet("dotnet-not-forever")]
        public IActionResult DotnetNotForever()
        {
            ViewData["Title"] = "Dotnet Not Forever";
            return View();
        }
    }
}
