using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MarketStore.Controllers
{
    public class AboutusController : Controller
    {
     
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnviroment;


        public AboutusController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: Aboutus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aboutus.ToListAsync());
        }

        // GET: Aboutus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // GET: Aboutus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aboutu aboutu)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _webHostEnviroment.WebRootPath;
                //ImageName
                string ImageName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                string fullPath = Path.Combine(rootPath + "/Images/" + ImageName);
                //2- upload
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await aboutu.ImageFile.CopyToAsync(fileStream);
                }

                //save image path
                aboutu.Imagepath = ImageName;

                _context.Add(aboutu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutu);
        }

        // GET: Aboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu == null)
            {
                return NotFound();
            }
            return View(aboutu);
        }

        // POST: Aboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  Aboutu aboutu)
        {
            if (id != aboutu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutu.ImageFile != null)
                    {
                        string rootPath = _webHostEnviroment.WebRootPath;
                        //ImageName
                        string ImageName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                        string fullPath = Path.Combine(rootPath + "/Images/" + ImageName);
                        //2- upload
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await aboutu.ImageFile.CopyToAsync(fileStream);
                        }

                        //save image path
                        aboutu.Imagepath = ImageName;
                    }

                    _context.Update(aboutu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuExists(aboutu.Id))
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
            return View(aboutu);
        }

        // GET: Aboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // POST: Aboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var aboutu = await _context.Aboutus.FindAsync(id);
            _context.Aboutus.Remove(aboutu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuExists(decimal id)
        {
            return _context.Aboutus.Any(e => e.Id == id);
        }
    }
}
