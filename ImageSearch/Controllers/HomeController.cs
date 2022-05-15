using ImageSearch.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImageSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Image> list = new List<Image>
            {
                new Image { Id = 1, Name = "Persian Cat", Keywords = new List<string>{ "cat", "animal" }, Source = "https://images.unsplash.com/photo-1591429939960-b7d5add10b5c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=686&q=80"  },
                new Image { Id = 2, Name = "Husky", Keywords = new List<string>{ "dog", "animal" },  Source = "https://images.unsplash.com/photo-1563889362352-b0492c224f62?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80" },
                new Image { Id = 3, Name = "Sun", Keywords = new List<string>{"sun", "sky", "nature" }, Source = "https://images.unsplash.com/photo-1563630381190-77c336ea545a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8c3VufGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60" }
            };
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}