using Twooter.Models;
using Twooter.ViewModels;

namespace Twooter.Interfaces
{
    public interface IAccountContext
    {
        void Insert(Registreren registreren);
        Account Select(Account account);
        int SelectID(Account account);
    }
}
