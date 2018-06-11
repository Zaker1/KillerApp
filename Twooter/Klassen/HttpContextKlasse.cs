using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Klassen
{
    public class HttpContextKlasse
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextKlasse(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
        public bool IsGebruiker()
        {
            return _httpContextAccessor.HttpContext.User.IsInRole("gebruiker");
        }
        public bool IsAdmin()
        {
            return _httpContextAccessor.HttpContext.User.IsInRole("admin");
        }
        public int HaalIdOp()
        {
            var gebruiker = _httpContextAccessor.HttpContext.User;
            var id = gebruiker.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Convert.ToInt32(id);
        }
    }
}
