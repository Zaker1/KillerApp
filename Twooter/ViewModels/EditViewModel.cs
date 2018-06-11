using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class EditViewModel
    {
        public Profiel huidigeProfiel { get; set; }
        public string nieuweGebruikersnaam { get; set; }
        public string nieuweBeschrijving { get; set; }
        public IFormFile nieuweFoto { get; set; }
    }
}
