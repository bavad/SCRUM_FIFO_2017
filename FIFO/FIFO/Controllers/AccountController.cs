using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using FIFO.Models;
using System;

namespace FIFO.Controllers
{
    public class AccountController : Controller, IDisposable
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void singInUser(User user)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (FIFODBContext db = new FIFODBContext())
                {
                    User user = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                    if (user == null)
                    {
                        ModelState.AddModelError("", "Неверный логин или пароль.");
                    }
                    else
                    {
                        singInUser(user);
                        return RedirectToAction("UserCabinet", "Home");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                using (FIFODBContext db = new FIFODBContext())
                {
                    User userEmail = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    User userLogin = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);

                    if (userEmail != null)
                    {
                        ModelState.AddModelError("", "Пользователь с таким имейлом уже существует");
                    }
                    else if (userLogin != null)
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                    else
                    {
                        db.Users.Add(new User { Email = model.Email, Login = model.Login, Password = model.Password });
                        db.SaveChanges();
                        User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                        singInUser(user);

                        return RedirectToAction("UserCabinet", "Home");
                    }

                }
            }
            return View(model);
        }
    }
}

