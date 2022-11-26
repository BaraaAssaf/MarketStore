using MarketStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Controllers
{
    public class LoginandRegisterationController : Controller
    {

        private readonly ModelContext _context;

        public LoginandRegisterationController(ModelContext context)
        {
            _context = context;

        }

        public IActionResult Login()
        {
            return View();
        }




        //http method
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {        
            var auth = _context.UserLogins.Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password).SingleOrDefault();
            var userid = _context.User1s.Where(x => x.Email == userLogin.Email).SingleOrDefault();
            var storeid = _context.Stores.Where(x => x.StoreName == userLogin.Email).SingleOrDefault();
            if (auth != null)
            {
                // login user
                // Role Id = 1 >> Admin
                // Role Id = 2 >> User      
                switch (auth.RoleloginId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AdminId", (int)userid.Id);
                        return RedirectToAction("Index", "AdminView");
                    case 2:
                        HttpContext.Session.SetInt32("Userid", (int)userid.Id);
                        return RedirectToAction("Index", "UserView");
                    case 3:
                        HttpContext.Session.SetInt32("Marketid", (int)storeid.Id);
                        return RedirectToAction("Order", "MarketView");
                }
            }
            return View();
        }


        public IActionResult Register()
        {
            return View();

        }
 
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User1 user, string password)
        {
            if (ModelState.IsValid)
            {         

                user.ImagePath = "";
                //add customer
                _context.Add(user);
                await _context.SaveChangesAsync();

                //add userlogin
                UserLogin userLogin = new UserLogin();
                userLogin.Email = user.Email;
                userLogin.Password = password;
                userLogin.RoleloginId = 2; //static value
                _context.Add(userLogin);
                await _context.SaveChangesAsync();

               
                return RedirectToAction(nameof(Login));

            }
            return View();
        }
    }
}
