using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Build_Completed_E_Commerce.Models;
using Build_Completed_E_Commerce.Security;
using Microsoft.AspNetCore.Mvc;

namespace Build_Completed_E_Commerce.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/login")]
    public class LoginController : Controller
    {
        private CompanyContext db = new CompanyContext();
        private SecurityManagers securityManager = new SecurityManagers();
        public LoginController(CompanyContext _db)
        {
            db = _db;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(string username,string password)
        {
            var account = ProcessLogin(username, password);  
            if (account != null)
            {
                securityManager.SignIn(this.HttpContext, account);
                return RedirectToAction("index", "dashboard", new {area="admin"});

            }
            else
            {
                ViewBag.error = "Tài khoản không hợp lệ";
                return View("Index");
            }
           
        }
        private Account ProcessLogin(string username,string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username)&&a.Status==true);
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }

        [Route("signout")]
        public IActionResult SignOut()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("index", "login", new { area = "admin" });
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var username = user.Value;
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));
            if (account != null)
            {
                
            }
            return View("Profile",account);
        }

        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(Account account)
        {
            var currentAccount = db.Accounts.SingleOrDefault(a => a.Id==account.Id);
            if (!string.IsNullOrEmpty(account.Password))
            {
                currentAccount.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            currentAccount.Username = account.Username;
            currentAccount.FullName = account.FullName;
            currentAccount.Email = account.Email;
            db.SaveChanges();
            ViewBag.msg = "Đã lưu";
            return View("Profile");
        }

        [Route("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}