using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Interfaces;
using Twooter.Models;
using Twooter.ViewModels;
using System.Data;

namespace Twooter.Repos
{
    public class AccountRepo
    {
        private readonly IAccountContext _context;
        public AccountRepo(IAccountContext context)
        {
            _context = context;
        }

        public bool Inloggen(Account account)
        {
            try
            {
                account = _context.Select(account);
            }
            catch (RowNotInTableException)
            {
                return false;
            }

            if (account.Banned)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Registreren(Registreren registreren)
        {
            _context.Insert(registreren);
            return true;
        }

        public Account IdOphalen(Account account)
        {
            account.ID = _context.SelectID(account);
            return account;
        }
        public Account AccountOphalen(Account account)
        {
            return _context.Select(account);
        }
    }
}
