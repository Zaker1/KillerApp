using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public Rol Rol { get; set; }
        public bool Banned { get; set; }
    }
}
