using MarketStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        public HomeController(ILogger<HomeController> logger,ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var about = _context.Aboutus.ToList();
            var contact = _context.Contactus.ToList();
            var home = _context.Homes.ToList();
            var stores = _context.Stores.Include(c => c.Category).ToList();
            var review = _context.Testimonials.Where(r => r.StatusTestimonials == "true").Include(u => u.User).ToList();
            ViewBag.UserCount = _context.User1s.Count();
            ViewBag.StoreCount = _context.Stores.Count();
            ViewBag.ProductCount = _context.Products.Count();

            var model = Tuple.Create<IEnumerable<Aboutu>, IEnumerable<Store>, IEnumerable<Contactu>, IEnumerable<Home>, IEnumerable<Testimonial>>(about, stores, contact, home, review);

            return View(model);
       
        }

        [HttpPost]
       
        public async Task<IActionResult> PublicReview(string email , string message, string name)
        {

            PublicReview publicReview = new PublicReview();
            publicReview.Email = email;
            publicReview.Masseage = message;
            publicReview.Name = name;

            if (ModelState.IsValid)
            {
                _context.Add(publicReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
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
