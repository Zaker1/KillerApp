using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twooter.Interfaces;
using Twooter.Context;
using Twooter.Repos;
using Twooter.ViewModels;
using Twooter.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;

namespace Twooter.Controllers
{
    [Authorize(Roles = "admin")]
    public class FilterController : Controller
    {
        FilterRepo fRepo = new FilterRepo(new FilterMSSQLContext());
        [HttpGet]
        public IActionResult Index()
        {
            FilterViewModel filters = new FilterViewModel(fRepo.HaalFiltersOp());
            return View(filters);
        }

        [HttpGet]
        public IActionResult Lijst()
        {
            FilterViewModel filters = new FilterViewModel(fRepo.HaalFiltersOp());
            return PartialView(filters);
        }
        [HttpPost]
        public IActionResult Lijst(FilterViewModel filters)
        {
            fRepo.MaakFilterAan(filters.FilterWoordAdd);
            return PartialView(filters);
        }

        [HttpPost]
        public IActionResult Index(FilterViewModel filters)
        {
            fRepo.MaakFilterAan(filters.FilterWoordAdd);
            filters.Filter = fRepo.HaalFiltersOp();
            return View(filters);
        }

        [HttpPost]
        public IActionResult Verwijder(FilterViewModel filters)
        {
            try
            {
                fRepo.VerwijderFilter(filters.FilterWoordDelete);
            }
            catch (ArgumentException)
            {
                TempData["DatabaseFout"] = "This filter doesn't exist, try again!";
            }
            return RedirectToAction("Index");
        }
    }
}