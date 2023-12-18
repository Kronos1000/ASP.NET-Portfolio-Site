﻿using Microsoft.AspNetCore.Mvc;
using PatrickWare.Data;
using PatrickWare.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PatrickWare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve About data from the AboutController
            var aboutData = await _context.About.ToListAsync();

            // Pass the About data to the Index view
            return View(aboutData);
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
