using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twooter.Repos;
using Twooter.Context;
using Twooter.Models;
using Twooter.ViewModels;
using System.Data;

namespace Twooter.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ProfielRepo pRepo = new ProfielRepo(new ProfielMSSQLContext());
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Reports(int id)
        {
            ReportViewModel reportViewModel = new ReportViewModel
            {
                Reported = pRepo.ProfielOphalen(id)
            };
            reportViewModel.Reported.Reports = pRepo.GetReportsOfProfiel(id);
            return View(reportViewModel);
        }
        [HttpGet]
        public IActionResult ReportsOverzicht()
        {
            ReportListViewModel viewModel = new ReportListViewModel
            {
                ReportedProfielen = pRepo.GetReportedProfielen()
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Ban(int id)
        {
            Profiel profiel = pRepo.ProfielOphalen(id);
            profiel.ID = id;
            return View(new BanViewModel { Profiel = profiel });
        }
        [HttpPost]
        public IActionResult Ban(BanViewModel vm)
        {
            try
            {
                pRepo.UpdateBan(vm.Profiel.ID, true);
            }
            catch (ArgumentOutOfRangeException)
            {
                TempData["Ongeldig"] = "ID is invalid, try again!";
            }
            return RedirectToAction("ReportsOverzicht");
        }
        [HttpGet]
        public IActionResult BannedOverzicht()
        {
            BannedOverzichtViewModel vm = new BannedOverzichtViewModel()
            {
                BannedProfielen = pRepo.GetBannedProfiel()
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult UnBan(int id)
        {
            Profiel profiel = pRepo.ProfielOphalen(id);
            profiel.ID = id;
            return View(new BanViewModel { Profiel = profiel });
        }
        [HttpPost]
        public IActionResult UnBan(BanViewModel vm)
        {
            try
            {
                pRepo.UpdateBan(vm.Profiel.ID, false);
            }
            catch (ArgumentOutOfRangeException)
            {
                TempData["Ongeldig"] = "ID is invalid, try again!";
            }
            return RedirectToAction("BannedOverzicht");
        }
    }
}