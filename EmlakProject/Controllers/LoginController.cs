using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using EmlakProject.Models;

namespace EmlakProject.Controllers
{
    public class LoginController : Controller
    {
        Emlak e = new Emlak();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UyeTablosu u)
        {
            UyeTablosu uye = e.UyeTablosus.FirstOrDefault(x => x.uyeMail == u.uyeMail && x.uyeSifre == u.uyeSifre);
            if (uye!=null)
            {
                FormsAuthentication.SetAuthCookie(uye.uyeAdSoyad, false);

                return RedirectToAction("Index","Home");
                
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı mail veya şifre hatalı";
                return View();
            }
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UyeTablosu u)
        {
            e.UyeTablosus.Add(u);
            e.SaveChanges();
            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult Profil()
        {
            UyeTablosu uye = e.UyeTablosus.FirstOrDefault(x => x.uyeAdSoyad == HttpContext.User.Identity.Name);
            return View(uye);
        }
        [HttpPost]
        public ActionResult Profil(UyeTablosu uye)
        {
            e.UyeTablosus.AddOrUpdate(uye);
            e.SaveChanges();

            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(uye.uyeAdSoyad, false);

            return RedirectToAction("Index", "Home");
        }
    }
}
