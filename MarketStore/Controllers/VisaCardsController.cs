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
    public class VisaCardsController : Controller
    {
        private readonly ModelContext _context;

        public VisaCardsController(ModelContext context)
        {
            _context = context;
        }

        // GET: VisaCards
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.VisaCards.Include(v => v.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: VisaCards/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaCard = await _context.VisaCards
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visaCard == null)
            {
                return NotFound();
            }

            return View(visaCard);
        }

        // GET: VisaCards/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User1s, "Id", "Email");
            return View();
        }

        // POST: VisaCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardNumber,SecurityCode,ExpiryDate,Balance,UserId")] VisaCard visaCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visaCard);
                await _context.SaveChangesAsync();
                return View();
            }
            ViewData["UserId"] = new SelectList(_context.User1s, "Id", "Email", visaCard.UserId);
            return View();

        }

        // GET: VisaCards/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaCard = await _context.VisaCards.FindAsync(id);
            if (visaCard == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User1s, "Id", "Address", visaCard.UserId);
            return View(visaCard);
        }

        // POST: VisaCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CardNumber,SecurityCode,ExpiryDate,Balance,UserId")] VisaCard visaCard)
        {
            if (id != visaCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visaCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaCardExists(visaCard.Id))
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
            ViewData["UserId"] = new SelectList(_context.User1s, "Id", "Address", visaCard.UserId);
            return View(visaCard);
        }

        // GET: VisaCards/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaCard = await _context.VisaCards
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visaCard == null)
            {
                return NotFound();
            }

            return View(visaCard);
        }

        // POST: VisaCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var visaCard = await _context.VisaCards.FindAsync(id);
            _context.VisaCards.Remove(visaCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisaCardExists(decimal id)
        {
            return _context.VisaCards.Any(e => e.Id == id);
        }
    }
}
