using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error/{statusCode}")]

        public IActionResult Error(int statusCode)
        {
            return View(statusCode);

        }
    }
}
