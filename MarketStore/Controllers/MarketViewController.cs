using MarketStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Controllers
{
    public class MarketViewController : Controller
    {


        private readonly ModelContext _context;

      


        public MarketViewController(ModelContext context)
        {
            _context = context;

        }

        public IActionResult Order()
        {
            
            decimal id = (decimal)HttpContext.Session.GetInt32("Marketid");
            var order = _context.Order2s.Include(X => X.Store).Include(X => X.User).Where(x => x.StoreId == id && x.Status == "waiting").ToList();
            var orderproduct = _context.Productorders.Include(o => o.Order).Include(o => o.Product).ToList();

           
            var model = Tuple.Create<IEnumerable<Order2>, IEnumerable<Productorder>>(order, orderproduct);

            return View(model);
        }
      
  


          
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Order(decimal id)
             {


                 var order = await _context.Order2s.FindAsync(id);
            

                if (ModelState.IsValid)
                {
                try
                {
                    order.Status = "Delivery";
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {  
                   
                }
                return RedirectToAction(nameof(Order));
            }
            return RedirectToAction(nameof(Order));
        }  



        public IActionResult delivering()
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Marketid");
            var order = _context.Order2s.Include(X => X.Store).Include(X => X.User).Where(x => x.StoreId == id && x.Status == "Delivery").ToList();
            var orderproduct = _context.Productorders.Include(o => o.Order).Include(o => o.Product).ToList();

            var model = Tuple.Create<IEnumerable<Order2>, IEnumerable<Productorder>>(order, orderproduct);

            return View(model);      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> delivering(decimal id)
        {


            var order = await _context.Order2s.FindAsync(id);


            if (ModelState.IsValid)
            {
                try
                {
                    order.Status = "receive";
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(delivering));
            }
            return RedirectToAction(nameof(delivering));
        }
    }
}
