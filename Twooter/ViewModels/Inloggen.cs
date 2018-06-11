using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class Inloggen
    {
        public Account Account { get; set; }
        public Inloggen(Account account)
        {
            this.Account = account;
        }
    }
}
