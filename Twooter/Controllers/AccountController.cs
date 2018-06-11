using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twooter.Repos;
using Twooter.Context;
using Twooter.Models;
using Microsoft.AspNetCore.Http;
using Twooter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Data.SqlClient;

namespace Twooter.Controllers
{
    public class AccountController : Controller
    {
        AccountRepo aRepo = new AccountRepo(new AccountContext());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return PartialView();
        }
        public IActionResult Registreer()
        {
            return PartialView();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/Account/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account
                {
                    Email = loginViewModel.Email,
                    Wachtwoord = loginViewModel.Password
                };
                bool inloggen = false;
                try
                {
                    inloggen = aRepo.Inloggen(account);
                }
                catch (SqlException)
                {
                    TempData["DatabaseNull"] = "Database connection failed, connect to the VPN";
                }

                if (inloggen)
                {
                    //account = aRepo.AccountOphalen(account);
                    List<Claim> claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Role, account.Rol.Naam));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, account.ID.ToString()));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (account.Rol.Naam == "gebruiker")
                    {

                        return Redirect("~/Tijdlijn/Index");
                    }
                    else
                    {
                        return Redirect("~/Admin/Index");
                    }
                }
                else
                {
                    TempData["UserLoginFailed"] = "You're banned! Try again when you're unbanned";
                    return Redirect("~/Account/Index");
                }
            }
            else
            {
                return Redirect("~/Account/Index");
            }

        }
        [HttpPost]
        public IActionResult Registreer(Registreren registreer)
        {
            aRepo.Registreren(registreer);
            return Redirect("~/Account/Index");
        }
    }
}