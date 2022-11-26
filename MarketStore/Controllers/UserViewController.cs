using MarketStore.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MarketStore.Controllers
{
    public class UserViewController : Controller
    {


        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnviroment;


        public UserViewController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }
        public IActionResult Index()
        {
            var store = _context.Stores.Include(c => c.Category).ToList();
            return View(store);
        }

        public async Task<IActionResult> Cart()
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");

            var modelContext = _context.Carts.Where(x => x.UserId == id).Include(c => c.Product).Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(decimal id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }
        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(string cardnumber, string SecurityCode ,string Address)
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");

            var visacard = _context.VisaCards.Where(x => x.UserId == id).SingleOrDefault();
            var Cart = _context.Carts.Where(x => x.UserId == id).Include(c => c.Product).Include(c => c.User);
            var totaleprice = Cart.Sum(s => s.Product.SellingPrice * s.Quntity);
         
            if (visacard != null)
            {
                if (visacard.CardNumber.ToString() == cardnumber && visacard.SecurityCode.ToString() == SecurityCode && visacard.Balance >= totaleprice)
                {
                    Order2 order = new Order2();
                   

                    order.OrderDate = DateTime.Now;
                    order.StoreId = Cart.First().Product.StoreId;
                    order.UserId = id;
                    order.LocationDelivery = Address;
                    order.Status = "waiting";
                    order.Totalprice = Cart.Sum(s => s.Product.SellingPrice * s.Quntity);

                    BodyBuilder bb = new BodyBuilder();
                    // bb.TextBody = body;
                    bb.HtmlBody = "<html>" + "<h1>" + "Date" + order.OrderDate + "</h1>" + "<table>" + "<thead>" +  "<tr>" 
                        + "<th>" +"Product NAME" + "</th>" + "<th>" + "Quntity" + "</th>" + "<th>" + "Price" + "</th>" + "</tr>"
                        + "</thead>"+"<tbody>";
                 

                    _context.Add(order);
                    await _context.SaveChangesAsync();

                    foreach (var item in Cart)
                    {
                        Productorder productorder = new Productorder();
                        productorder.OrderId = order.Id;
                        productorder.ProductId = item.ProductId;
                        productorder.Quntity = item.Quntity;
                        bb.HtmlBody = bb.HtmlBody  + "<tr>" + "<td>" + item.Product.ProductName + "</td>" + "<td>" + item.Quntity
                        + "</td>" + "<td>" + item.Product.SellingPrice + "</td>" + "</tr>";
                        _context.Add(productorder);
                        await _context.SaveChangesAsync();


                        _context.Carts.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                    bb.HtmlBody = bb.HtmlBody + "</tbody>" + "<tfoot>"+"<tr>"+"<td>"+"Total Price"+"</td>" + "<td>"
                        + totaleprice + "</td>" + "</tr>"+"</tfoot>"+"</table>"+"</html>";
                    visacard.Balance = visacard.Balance - totaleprice;
                    _context.Update(visacard);
                    await _context.SaveChangesAsync();


                    //MimeMessage obj = new MimeMessage();
                    //MailboxAddress emailfrom = new MailboxAddress("MarketStore", "ad.mstore1997@gmail.com");
                    //MailboxAddress emailto = new MailboxAddress("MarketStore", order.User.Email);
                    //obj.From.Add(emailfrom);
                    //obj.To.Add(emailto);
                    //obj.Subject = "Invoice";


                    //obj.Body = bb.ToMessageBody();
                    //SmtpClient emailclient = new SmtpClient();
                    //emailclient.Connect("smtp.gmail.com", 465, true);
                    //emailclient.Authenticate("ad.mstore1997@gmail.com", "Market@1234");
                    //emailclient.Send(obj);
                    //emailclient.Disconnect(true);
                    //emailclient.Dispose();

                    return RedirectToAction(nameof(Review));
                }
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> profile()
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");


            var user = await _context.User1s.FindAsync(id);
            var Visacard = _context.VisaCards.Where(x => x.UserId == id).SingleOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.visan = Visacard.CardNumber;
            ViewBag.visad = Visacard.ExpiryDate;
            ViewBag.visab = Visacard.Balance;

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> profile(User1 user, string currentPassword, string newPassword, string renewPassword)
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");


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
                
                    if (user.ImageFile != null)
                    {
                        string rootPath = _webHostEnviroment.WebRootPath;
              
                        string ImageName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                        string fullPath = Path.Combine(rootPath + "/Images/" + ImageName);
                   
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await user.ImageFile.CopyToAsync(fileStream);
                        }

               
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


        [HttpGet]
        public IActionResult Product(string did)
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");
            var product = _context.Products.Where(x => x.StoreId.ToString() == did).ToList();
            ViewBag.cart = _context.Carts.Where(x => x.UserId == id).Count();
            ViewBag.id = did;

            return View(product);
        }


        [HttpPost]
        public IActionResult Product(string name,string Search)
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");

            if (Search != null && name != null)
            {
                var item = _context.Products.Where(x => x.StoreId.ToString() == name && x.ProductName.ToLower().Contains(Search.ToLower())).ToList();
                ViewBag.id = name;
                ViewBag.cart = _context.Carts.Where(x => x.UserId == id).Count();
                return View(item);
            }
            else
            {
                var item = _context.Products.Where(x => x.StoreId.ToString() == name).ToList();
                ViewBag.id = name;
                ViewBag.cart = _context.Carts.Where(x => x.UserId == id).Count();
                return View(item);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(int Quntity , decimal productid)
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");
            var storeid = _context.Products.Find(productid);
            var product = _context.Products.Where(x => x.StoreId.ToString() == storeid.StoreId.ToString()).ToList();
            ViewBag.cart = _context.Carts.Where(x => x.UserId == id).Count();
            ViewBag.id = storeid.StoreId.ToString();
            if (Quntity != null && productid != null)
            {
                Cart cart = new Cart();
                cart.Quntity = (byte?)Quntity;
                cart.ProductId = productid;
                cart.UserId = id;

                _context.Add(cart);
                await _context.SaveChangesAsync();
                return View(product);


            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Invoice()
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");
            var order = _context.Order2s.Where(x => x.UserId == id).ToList();
            var productorder = _context.Productorders.Include(x => x.Order).Include(x => x.Product).ToList();
            var model = Tuple.Create<IEnumerable<Order2>, IEnumerable<Productorder>>(order, productorder);
            return View(model);
        }

        public IActionResult Review()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(Testimonial review)
        {
            decimal id = (decimal)HttpContext.Session.GetInt32("Userid");
            if (ModelState.IsValid)
            {
                review.UserId = id;
                review.StatusTestimonials ="none";
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Review));
            }
            return View(review);
        }


    }
}
