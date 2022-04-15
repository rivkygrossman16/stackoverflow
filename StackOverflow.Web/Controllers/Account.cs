using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackOverflow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace StackOverflow.Web.Controllers
{
    public class Account : Controller
    {
        private readonly string _connectionString;
        public Account(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Signup()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Signup(User user)
        {
            var repo = new DataRepository(_connectionString);
            repo.AddUser(user);

            return Redirect("/Account/Login");
        }
        public IActionResult Login()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var repo = new DataRepository(_connectionString);
            var user = repo.Login(Email, Password);
            if (user == null)
            {
                TempData["message"] = "Invalid login!";
                return RedirectToAction("Login");
            }

            var claims = new List<Claim>
            {
                new Claim("user", Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
            new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect("/home/Index");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/Home/Index");
        }
    }
}
