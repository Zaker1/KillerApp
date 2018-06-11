using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twooter.Context;
using Twooter.Repos;
using Twooter.ViewModels;
using Twooter.Models;
using System.Data.SqlClient;

namespace Twooter.Controllers
{
    public class TwootController : Controller
    {
        TwootRepo tRepo = new TwootRepo(new TwootMSSQLContext());
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Plaats()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Plaats(string bericht, int profielID)
        {
            Twoot twoot = new Twoot
            {
                Bericht = bericht,
                ProfielID = profielID
            };

            try
            {
                tRepo.TwootPlaatsen(twoot);
            }
            catch (ArgumentOutOfRangeException)
            {
                TempData["BerichtLang"] = "Post is too long!";
            }
            catch (ArgumentNullException)
            {
                TempData["ProfielNull"] = "Are you sure you're logged in?";
            }
            catch (ArgumentException)
            {
                TempData["BerichtNull"] = "Type something and then post it!";
            }
            catch (SqlException)
            {
                TempData["DatabaseNull"] = "Database connection failed, connect to the VPN";
            }
            catch (Exception e)
            {
                if (e.Message == "Query fout")
                {
                    TempData["DatabaseQuery"] = "Database query failed, try again";
                }
            }
            return Redirect("~/Tijdlijn/Index");
        }

        [HttpGet]
        public void Favoriet(int id)
        {
            tRepo.TwootFavorieten(id, Convert.ToInt32(Request.Cookies["ProfielID"]));
        }
        [HttpGet]
        public void UnFavoriet(int id)
        {
            tRepo.TwootUnfavorieten(id, Convert.ToInt32(Request.Cookies["ProfielID"]));
        }
    }
}