using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twooter.Models;
using Twooter.Repos;
using Twooter.Context;
using Twooter.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Twooter.Klassen;

namespace Twooter.Controllers
{
    public class ProfielController : Controller
    {
        ProfielRepo pRepo = new ProfielRepo(new ProfielMSSQLContext());
        HttpContextKlasse httpContext = new HttpContextKlasse(new HttpContextAccessor());
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {
            Profiel profiel = pRepo.ProfielOphalen(Convert.ToInt32(Request.Cookies["ProfielID"]));
            return View(new EditViewModel { huidigeProfiel = profiel });
        }
        [HttpPost]
        public IActionResult Edit(EditViewModel vm)
        {
            //Profiel profiel = null;
            //if (vm.NieuweFoto != null)
            //{
            //    if (vm.NieuweFoto.Length > 0)
            //    {
            //        byte[] vs = null;
            //        using (var fs1 = vm.NieuweFoto.OpenReadStream())
            //        using (var ms1 = new MemoryStream())
            //        {
            //            fs1.CopyTo(ms1);
            //            vs = ms1.ToArray();
            //        }

            //        profiel = new Profiel
            //        {
            //            ID = vm.huidigeProfiel.ID,
            //            Gebruikersnaam = vm.nieuweGebruikersnaam,
            //            Beschrijving = vm.nieuweBeschrijving,
            //            Foto = vs
            //        };
            //    }
            //    pRepo.UpdateProfiel(profiel);
            //}
            //return RedirectToAction("Index", "Tijdlijn");

            Profiel profiel = null;
            if (HttpContext.Request.Form.Files != null)
            {
                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        byte[] vs = null;
                        using (var fs1 = file.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            vs = ms1.ToArray();
                        }

                        profiel = new Profiel
                        {
                            ID = vm.huidigeProfiel.ID,
                            Gebruikersnaam = vm.nieuweGebruikersnaam,
                            Beschrijving = vm.nieuweBeschrijving,
                            Foto = vs
                        };
                    }
                }
            }
            pRepo.UpdateProfiel(profiel);

            return RedirectToAction("Index", "Tijdlijn");
        }

        [HttpGet]
        public void Volg([FromRoute]int id)
        {
            pRepo.ProfielVolgen(id, Convert.ToInt32(Request.Cookies["ProfielID"]));
        }
        [HttpGet]
        public void UnVolg([FromRoute]int id)
        {
            pRepo.ProfielUnVolgen(id, Convert.ToInt32(Request.Cookies["ProfielID"]));
        }
        [HttpGet]
        public IActionResult Zoek()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Zoek(ZoekViewModel viewModel)
        {
            return RedirectToAction("ZoekResultaten", new { id = viewModel.zoekTerm });
        }
        [HttpGet]
        public IActionResult ZoekResultaten([FromRoute]string id)
        {
            ZoekViewModel zoekViewModel = new ZoekViewModel();
            try
            {
                zoekViewModel.Profielen = pRepo.ProfielZoeken(id);
            }
            catch (ArgumentNullException)
            {
                TempData["ZoektermNull"] = "No profiles found, try something else";
            }
            return View(zoekViewModel);
        }
        [HttpGet]
        public IActionResult Rapporteren(int id)
        {
            ReportViewModel reportViewModel = new ReportViewModel
            {
                Reported = pRepo.ProfielOphalen(id)
            };
            return View(reportViewModel);
        }
        [HttpPost]
        public IActionResult Rapporteren(ReportViewModel reportViewModel)
        {
            Report report = new Report
            {
                Reported_id = reportViewModel.Reported.ID,
                Bericht = reportViewModel.Bericht,
                Reporter_id = Convert.ToInt32(Request.Cookies["ProfielID"])
            };
            try
            {
                pRepo.ReportProfiel(report);
            }
            catch (ArgumentNullException)
            {
                TempData["GeenBericht"] = "Enter a message!";
                return View();
            }
            return Redirect("~/Tijdlijn/Index");
        }
    }
}