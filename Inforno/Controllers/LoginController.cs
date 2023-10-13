using Inforno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Inforno.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Auth u) 
        {
            if (ModelState.IsValid) 
            { 
                    ModelDBContext dbContext = new ModelDBContext();
                    var user = dbContext.Users.FirstOrDefault(e=>e.Email== u.Email);
                if (user.Email == u.Email && user.Password == u.Password) 
                {
                    dbContext.Dispose();
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    ModelState.AddModelError("", "Credenziali non valide");
                    return View(u);
                }
                   
            }
            else 
            {
                ModelState.AddModelError("", "Credenziali non valide");
                return View(u);
            
            }
                 
             
             
        }


        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            Session.Remove("Carrello");
            return RedirectToAction("Login", "Login");

        }

        public ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IDRuolo")] Users u)
        {
            if (ModelState.IsValid)
            {
                u.IDRuolo = 1;
                ModelDBContext dbContext = new ModelDBContext();
                dbContext.Users.Add(u);
                try
                {

                    dbContext.SaveChanges();


                    return RedirectToAction("Login", "Login");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("Si è verificato un errore durante la registrazione.",ex);
                    return View(u);

                }
            }

            return View(u);

        }
    }
}