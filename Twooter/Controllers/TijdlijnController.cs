using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twooter.Models;
using Twooter.ViewModels;
using Twooter.Context;
using Twooter.Repos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Twooter.Klassen;
using Microsoft.AspNetCore.Http;

namespace Twooter.Controllers
{
    [Authorize]
    public class TijdlijnController : Controller
    {
        HttpContextKlasse httpContextKlasse = new HttpContextKlasse(new HttpContextAccessor()); 
        TijdlijnRepo tRepo = new TijdlijnRepo(new TijdlijnMSSQLContext());
        ProfielRepo pRepo = new ProfielRepo(new ProfielMSSQLContext());
        Profiel profiel;
        Tijdlijn tijdlijn;
        
        [HttpGet]
        public IActionResult Index()
        {
            TempData.Clear();
            profiel = new Profiel
            {
                Account = new Account()
            };
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            profiel.Account.ID = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            tijdlijn = new Tijdlijn(profiel);

            pRepo.ProfielOphalen(profiel);
            tijdlijn.Twoots = tRepo.TwootsOphalen(tijdlijn);
            tijdlijn.Vrienden = tRepo.VriendenOphalen(tijdlijn);
            Response.Cookies.Append("ProfielID", profiel.ID.ToString());

            GetTijdlijn get = new GetTijdlijn(tijdlijn);

            return View(get);
        }

        [HttpGet]
        public IActionResult Bezoek(string id)
        {
            if (id != null)
            {
                profiel = new Profiel
                {
                    Account = new Account(),
                    ID = Convert.ToInt32(id)
                };
                
                profiel = pRepo.ProfielOphalen(profiel.ID);
                tijdlijn = new Tijdlijn(profiel);
                tijdlijn.Twoots = tRepo.TwootsOphalen(tijdlijn);
                tijdlijn.Vrienden = tRepo.VriendenOphalen(tijdlijn);
                bool volgend = CheckVolgend(Convert.ToInt32(id));

                GetTijdlijn get = new GetTijdlijn(tijdlijn, volgend);

                return View(get);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        private bool CheckVolgend(int id)
        {
            Profiel profiel = new Profiel
            {
                ID = Convert.ToInt32(Request.Cookies["ProfielID"])
            };
            Tijdlijn tijdlijn = new Tijdlijn(profiel);
            tijdlijn.Vrienden = tRepo.VriendenOphalen(tijdlijn);

            var vriend = tijdlijn.Vrienden.FirstOrDefault(x => x.ID == id);

            if (vriend != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}