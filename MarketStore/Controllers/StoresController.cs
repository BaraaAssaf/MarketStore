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
    public class StoresController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnviroment;
        public StoresController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }
        // GET: Stores
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Stores.Include(s => s.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Store store , string password)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _webHostEnviroment.WebRootPath;
                //ImageName
                string ImageName = Guid.NewGuid().ToString() + "_" + store.ImageFile.FileName;
                string fullPath = Path.Combine(rootPath + "/Images/" + ImageName);
                //2- upload
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await store.ImageFile.CopyToAsync(fileStream);
                }

                //save image path
                store.Imagepath = ImageName;

                _context.Add(store);
                await _context.SaveChangesAsync();

                //add userlogin
                UserLogin userLogin = new UserLogin();
                userLogin.Email = store.StoreName;
                userLogin.Password = password;
                userLogin.RoleloginId = 3; //static value
                _context.Add(userLogin);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", store.CategoryId);
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", store.CategoryId);
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, Store store)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (store.ImageFile != null)
                    {
                        string rootPath = _webHostEnviroment.WebRootPath;
                        //ImageName
                        string ImageName = Guid.NewGuid().ToString() + "_" + store.ImageFile.FileName;
                        string fullPath = Path.Combine(rootPath + "/Images/" + ImageName);
                        //2- upload
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await store.ImageFile.CopyToAsync(fileStream);
                        }

                        //save image path
                        store.Imagepath = ImageName;
                    }

                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", store.CategoryId);
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(decimal id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
