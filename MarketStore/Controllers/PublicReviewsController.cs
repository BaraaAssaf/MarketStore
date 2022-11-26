using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketStore.Models;

namespace MarketStore.Controllers
{
    public class PublicReviewsController : Controller
    {
        private readonly ModelContext _context;

        public PublicReviewsController(ModelContext context)
        {
            _context = context;
        }

        // GET: PublicReviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.PublicReviews.ToListAsync());
        }

        // GET: PublicReviews/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicReview = await _context.PublicReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicReview == null)
            {
                return NotFound();
            }

            return View(publicReview);
        }

        // GET: PublicReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublicReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Masseage,Email,Name")] PublicReview publicReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicReview);
        }

        // GET: PublicReviews/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicReview = await _context.PublicReviews.FindAsync(id);
            if (publicReview == null)
            {
                return NotFound();
            }
            return View(publicReview);
        }

        // POST: PublicReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Masseage,Email,Name")] PublicReview publicReview)
        {
            if (id != publicReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicReviewExists(publicReview.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publicReview);
        }

        // GET: PublicReviews/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicReview = await _context.PublicReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicReview == null)
            {
                return NotFound();
            }

            return View(publicReview);
        }

        // POST: PublicReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var publicReview = await _context.PublicReviews.FindAsync(id);
            _context.PublicReviews.Remove(publicReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicReviewExists(decimal id)
        {
            return _context.PublicReviews.Any(e => e.Id == id);
        }
    }
}
