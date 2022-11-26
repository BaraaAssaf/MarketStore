using MarketStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Controllers
{
    public class AdminViewController : Controller
    {

        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnviroment;


        public AdminViewController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        public async Task<IActionResult> Index()
        {
            var users = _context.User1s.ToList();
            var stores = _context.Stores.Include(c => c.Category);

            
            var order = _context.Order2s.Include(X => X.Store).Include(X => X.User).ToList();
            var product = _context.Products.ToList();
            var orderproduct = _context.Productorders.Include(o => o.Order).Include(o => o.Product).ToList();
            var orderproduct1 = _context.Productorders.ToList();

            DateTime d = DateTime.Now;
           
            ViewBag.countstore = _context.Stores.Count();
            ViewBag.countproduct = _context.Products.Count();
            ViewBag.salestoday = _context.Order2s.Where(x => x.OrderDate.Date == d.Date ).Sum(x => x.Totalprice);
            ViewBag.customer = users.Count();

            var model = Tuple.Create<IEnumerable<Store>, IEnumerable<User1>, IEnumerable<Order2>, IEnumerable<Productorder>>(stores, users , order , orderproduct);

            return View(model);
        }

  
        public async Task<IActionResult> profile()
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("AdminId");


            var user = await _context.User1s.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> profile(User1 user, string currentPassword, string newPassword, string renewPassword)
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("AdminId");


            if (id != user.Id)
            {
                return NotFound();
            }

            var userlog = _context.UserLogins.Where(u => u.Email == user.Email && u.RoleloginId == 1).SingleOrDefault();

            if (userlog != null)
            {
                if (currentPassword == userlog.Password)
                {
                    if (newPassword == renewPassword)
                    {
                        UserLogin userLogin = userlog;

                        userLogin.Email = user.Email;
                        userLogin.Password = newPassword;
                        userLogin.RoleloginId = 1; //static value
                        _context.Update(userLogin);
                        await _context.SaveChangesAsync();
                    }
                }

            }


            if (ModelState.IsValid)
            {



                try
                {
                    //Upload Image to server 
                    //1- path (~/Images/ImageName)
                    //~
                    if (user.ImageFile != null)
                    {
                        string rootPath = _webHostEnviroment.WebRootPath;
                        //ImageName
                        string ImageName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                        string fullPath = Path.Combine(rootPath + "/Images/" + ImageName);
                        //2- upload
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await user.ImageFile.CopyToAsync(fileStream);
                        }

                        //save image path
                        user.ImagePath = ImageName;
                    }
                    _context.Update(user);
                    await _context.SaveChangesAsync();





                }


                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);

        }


        public IActionResult Report()
        {
            var order1 = _context.Order2s.Include(c => c.User).Include(c => c.Store).ToList();
          

            var order = _context.Order2s.ToList();
            var proCus = _context.Productorders.ToList();
            var products = _context.Products.ToList();
            var stors = _context.Stores.ToList();

            var result = from o in order
                         join pc in proCus on o.Id equals pc.OrderId
                         join p in products on pc.ProductId equals p.Id
                         join sto in stors on p.StoreId equals sto.Id
                         select new jointable { product = p, store = sto, order = o, productorder = pc };
            ViewBag.TotalQuantity = _context.Productorders.Sum(x => x.Quntity);
            ViewBag.TotalPrice = _context.Order2s.Sum(x => x.Totalprice);

 
            var modelContext = _context.Productorders.Include(p => p.Order).Include(p => p.Product).ToList();
            ViewBag.Porfit = modelContext.Sum(s => s.Quntity * s.Product.SellingPrice) - modelContext.Sum(s => s.Quntity * s.Product.CostPrice);

            var model = Tuple.Create<IEnumerable<jointable>, IEnumerable<Productorder>>(result, modelContext);

            return View(model);
        }

        [HttpPost]
        public IActionResult Report(DateTime? startDate, DateTime? endDate)
        {
            var order = _context.Order2s.ToList();
            var proCus = _context.Productorders.ToList();
            var products = _context.Products.ToList();
            var stors = _context.Stores.ToList();

            var result = from o in order
                         join pc in proCus on o.Id equals pc.OrderId
                         join p in products on pc.ProductId equals p.Id
                         join sto in stors on p.StoreId equals sto.Id
                         select new jointable { product = p, store = sto, order = o, productorder = pc };
            ViewBag.TotalQuantity = _context.Productorders.Sum(x => x.Quntity);
            ViewBag.TotalPrice = _context.Order2s.Sum(x => x.Totalprice);
            var modelContext = _context.Productorders.Include(p => p.Order).Include(p => p.Product).ToList();
            ViewBag.Porfit = modelContext.Sum(s => s.Quntity * s.Product.SellingPrice) - modelContext.Sum(s => s.Quntity * s.Product.CostPrice);

            var order1 = _context.Order2s.Include(c => c.User).Include(c => c.Store).ToList();

            if (startDate == null && endDate == null)
            {
                var model = Tuple.Create<IEnumerable<jointable>, IEnumerable<Productorder>>(result, modelContext);
                ViewBag.TotalQuantity = _context.Productorders.Sum(x => x.Quntity);
                ViewBag.TotalPrice = _context.Order2s.Sum(x => x.Totalprice);
                ViewBag.Porfit = modelContext.Sum(s => s.Quntity * s.Product.SellingPrice) - modelContext.Sum(s => s.Quntity * s.Product.CostPrice);

                return View(model);
            }

            else if (startDate != null && endDate == null)
            {

                var result1 = modelContext.Where(x => x.Order.OrderDate.Date >= startDate);
                ViewBag.TotalQuantity = result1.Sum(x => x.Quntity);
                ViewBag.TotalPrice = result1.Sum(x => x.Quntity * x.Product.SellingPrice);
                ViewBag.Porfit = result1.Sum(s => s.Quntity * s.Product.SellingPrice) - result1.Sum(s => s.Quntity * s.Product.CostPrice);

                var model = Tuple.Create<IEnumerable<jointable>, IEnumerable<Productorder>>(result, result1);

                return View(model);
            }

            else if (startDate == null && endDate != null)
            {
                var result1 = modelContext.Where(x => x.Order.OrderDate.Date <= endDate);
                ViewBag.TotalQuantity = result1.Sum(x => x.Quntity);
                ViewBag.TotalPrice = result1.Sum(x => x.Quntity * x.Product.SellingPrice);
                ViewBag.Porfit = result1.Sum(s => s.Quntity * s.Product.SellingPrice) - result1.Sum(s => s.Quntity * s.Product.CostPrice);

                var model = Tuple.Create<IEnumerable<jointable>, IEnumerable<Productorder>>(result, result1);

                return View(model);
            }

            else
            {
                var result1 = modelContext.Where(x => x.Order.OrderDate.Date >= startDate && x.Order.OrderDate.Date <= endDate);

                ViewBag.TotalQuantity = result1.Sum(x => x.Quntity);
                ViewBag.TotalPrice = result1.Sum(x => x.Quntity * x.Product.SellingPrice);
                var model = Tuple.Create<IEnumerable<jointable>, IEnumerable<Productorder>>(result, result1);
                ViewBag.Porfit = result1.Sum(s => s.Quntity * s.Product.SellingPrice) - result1.Sum(s => s.Quntity * s.Product.CostPrice);
                return View(model);
            }
        }


    }
}
